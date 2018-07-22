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
            //var index = new Indexing.Indexer();
            //index.Rebuild();
            //index.SearchRepl();

            // TODO: Make nicer
            typeof(DummyQueryExecutor).GetType();
            typeof(Indexing.Indexer).GetType();

            var uiContext = new UIContext();

            var container = new Container()
                .Register<IQueryExecutorManager, QueryExecutorManager>()
                .Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
                .Register(uiContext)
                .Register<UI, UI>()
            ;

            RegisterServices(container);

            // TODO: Remove (pries cache)
            container.Resolve<Indexing.Indexer>().Query("conemu");

            return uiContext.Run(container.Resolve<UI>());

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