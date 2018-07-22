using MLaunch.Plugins.Dummy;
using MUI;
using MUI.DI;
using System.Linq;
using System.Reflection;

namespace MLaunch
{
    public class Program
    {
        private static int Main(string[] args)
        {
            // TODO: Make nicer
            typeof(DummyQueryExecutor).GetType();

            var uiContext = new UIContext();

            var container = new Container()
                .Register<IQueryExecutorManager, QueryExecutorManager>()
                .Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
                .Register(uiContext)
                .Register<UI, UI>()
            ;

            RegisterServices(container);

            return uiContext.Run(container.Resolve<UI>());

            //var index = new Indexer();
            //index.Rebuild();
            //index.SearchRepl();

            //return 0;
        }

        private static void RegisterServices(IContainer container)
        {
            Assembly
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
}