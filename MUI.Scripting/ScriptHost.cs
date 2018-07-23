using Dotnet.Script.Core;
using Dotnet.Script.DependencyModel.Context;
using Dotnet.Script.DependencyModel.Logging;
using Dotnet.Script.DependencyModel.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MUI.Scripting
{
    public class ScriptHost
    {
        public string Path { get; private set; }

        private ScriptCompiler _compiler;
        private ScriptRunner _runner;
        private ScriptCompilationContext<int> _context;
        private ScriptState _state;
        private Script _script;

        private Action _run;

        public ScriptInterop Interop { get; set; } = new ScriptInterop();

        public ScriptHost(string path)
        {
            Path = path;

            _compiler = GetScriptCompiler(false);
            _runner = new ScriptRunner(_compiler, _compiler.Logger, ScriptConsole.Default);
        }

        public Task Compile()
        {
            var fullPath = System.IO.Path.GetFullPath(Path);

            var sourceText = SourceText.From(File.ReadAllText(fullPath));
            var context = new ScriptContext(sourceText, System.IO.Path.GetDirectoryName(fullPath), new string[0], null, OptimizationLevel.Release, ScriptMode.Eval);

            _context = _compiler.CreateCompilationContext<int, ScriptInterop>(context);
            _script = _context.Script;

            return Task.CompletedTask;
        }

        public async Task Run()
        {
            _state = await _script.RunAsync(Interop, ex =>
            {
                Console.WriteLine("EX: " + ex.Message);

                return true;
            });
        }

        public async Task Reload()
        {
            var fullPath = System.IO.Path.GetFullPath(Path);

            Console.WriteLine($"Recompiling script at '{fullPath}'...");

            _script = _script.ContinueWith(File.ReadAllText(fullPath), _context.ScriptOptions);

            Console.WriteLine("Reloaded");
        }

        private static ScriptCompiler GetScriptCompiler(bool debugMode)
        {
            var logger = new ScriptLogger(ScriptConsole.Default.Error, debugMode);
            var runtimeDependencyResolver = new RuntimeDependencyResolver(type => ((level, message) =>
            {
                if (level == LogLevel.Debug)
                {
                    logger.Verbose(message);
                }
                if (level == LogLevel.Info)
                {
                    logger.Log(message);
                }
            }));

            var compiler = new ScriptCompiler(logger, runtimeDependencyResolver);

            return compiler;
        }
    }

    public class ScriptInterop
    {
        public Dictionary<Type, Func<object>> Services { get; set; } = new Dictionary<Type, Func<object>>();

        public void Add<T>(Func<T> factory)
        {
            Services.Add(typeof(T), () => factory());
        }

        public T Get<T>()
        {
            return (T)Services[typeof(T)]();
        }
    }
}