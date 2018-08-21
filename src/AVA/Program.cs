using AVA.Plugins.Dummy;
using AVA.Plugins.FirefoxBookmarks.Models;
using MUI;
using MUI.DI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AVA
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            SetupNativeDependencies();

            // TODO: Make nicer
            typeof(DummyQueryExecutor).GetType();
            typeof(Indexing.Indexer).GetType();
            typeof(MozPlace).GetType();

            var uiContext = new UIContext(600, 300);

            var container = new Container()
                .Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
                .Register(uiContext)
                .Register<UI, UI>()
            ;

            RegisterServices(container);

            // TODO: Remove (pries cache)
            container.Resolve<Indexing.Indexer>().Query("conemu");

            uiContext.PushUI(container.Resolve<UI>());
            uiContext.Run();
        }

        private static void SetupNativeDependencies()
        {
            var output = Path.GetDirectoryName(new Uri(typeof(Program).Assembly.CodeBase).LocalPath);

            foreach (var dependency in Directory.GetFiles(GetDependenciesDirectory()))
            {
                var fileName = Path.GetFileName(dependency);
                var target = Path.Combine(output, fileName);
                if (!File.Exists(target))
                {
                    File.Copy(dependency, target);
                }
            }
        }

        private static string GetDependenciesDirectory()
        {
            var root = "Deps/";
            var is64 = Environment.Is64BitOperatingSystem;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return is64 ? root + "x64" : root + "x86";
            }

            throw new NotSupportedException($"Apparently, platform is not supported: '{Environment.OSVersion}'");
        }

        private static void RegisterServices(IContainer container) => Assembly
            .GetEntryAssembly()
            .GetReferencedAssemblies()
            .Select(ass => Assembly.Load(ass))
            .SelectMany(ass => ass.DefinedTypes)
            .Where(t => !t.IsAbstract)
            .Where(t => t.GetCustomAttribute<ServiceAttribute>(true) != null)
            .ToList()
            .ForEach(s => container.Register(s.AsType(), s.GetCustomAttribute<ServiceAttribute>(true).Lifetime));
    }
}