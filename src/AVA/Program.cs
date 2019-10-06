using AVA.Core.Settings;
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
            MUI.Utils.DependencyMapper.Map();

            //typeof(Indexing.Indexer).GetType();

            var uiContext = new UIContext(600, 300);
            uiContext.RunOneFrame();

            var container = new Container()
                .Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
                .Register(uiContext)
                .Register<UI, UI>()
            ;

            container.LoadPlugins("".FromAppRoot());

            SettingsRoot.Instance.Initialize(container);

            RegisterServices(container);

            // TODO: Remove (pries cache)
            //container.Resolve<Indexing.Indexer>().Query("conemu");

            uiContext.PushUI(container.Resolve<UI>());
            using (uiContext) uiContext.Run();

            //SettingsRoot.Instance.Save();
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

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return Path.Combine(root, "win64");
            }

            throw new NotSupportedException($"Apparently, platform is not supported: '{Environment.OSVersion}'");
        }

        private static void RegisterServices(IContainer container) =>
            AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(ass => ass.DefinedTypes)
            .Where(t => !t.IsAbstract)
            .Where(t => t.GetCustomAttribute<ServiceAttribute>(true) != null)
            .OrderBy(t => t.FullName)
            .ToList()
            .ForEach(s => container.Register(s.AsType(), s.GetCustomAttribute<ServiceAttribute>(true).Lifetime));
    }
}