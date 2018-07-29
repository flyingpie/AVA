using AVA.Plugins.Dummy;
using MUI;
using MUI.DI;
using System;
using System.Linq;
using System.Reflection;

namespace AVA
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            // TODO: Make nicer
            typeof(DummyQueryExecutor).GetType();
            typeof(Indexing.Indexer).GetType();

            var uiContext = new UIContext(600, 300);

            var container = new Container()
                .Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
                .Register(uiContext)
                .Register<UI, UI>()
            ;

            RegisterServices(container);

            // TODO: Remove (pries cache)
            container.Resolve<Indexing.Indexer>().Query("conemu");

            uiContext.Run(container.Resolve<UI>());
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