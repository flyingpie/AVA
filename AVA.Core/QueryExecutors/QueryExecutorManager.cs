using MUI;
using MUI.DI;
using MUI.Logging;
using System.Linq;

namespace AVA.Core.QueryExecutors
{
    [Service]
    public class QueryExecutorManager : IQueryExecutorManager
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        [Dependency] public IQueryExecutor[] QueryExecutors { get; set; }

        private ILog _log;

        [RunAfterInject]
        private void Init()
        {
            _log = Log.Get(this);

            QueryExecutors = QueryExecutors.OrderBy(qe => qe.Order).ToArray();
        }

        public IQueryExecutor GetQueryExecutor(string term)
        {
            var qex = QueryExecutors.FirstOrDefault(qe => qe.TryHandle(term));

            _log.Info(qex.ToString());

            return qex;
        }
    }
}