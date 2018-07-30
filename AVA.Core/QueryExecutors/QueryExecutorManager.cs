using MUI.DI;
using MUI.Logging;
using System.Linq;

namespace AVA.Core.QueryExecutors
{
    [Service]
    public class QueryExecutorManager : IQueryExecutorManager
    {
        [Dependency] public IQueryExecutor[] QueryExecutors { get; set; }

        private ILog _log;
        private IQueryExecutor _activeQueryExecutor;

        [RunAfterInject]
        private void Init()
        {
            _log = Log.Get(this);

            QueryExecutors = QueryExecutors.OrderBy(qe => qe.Order).ToArray();
        }

        public bool TryHandle(QueryContext query)
        {
            _activeQueryExecutor = QueryExecutors.FirstOrDefault(qe => qe.TryHandle(query));

            _log.Info($"Term '{query}', selected query executor: {_activeQueryExecutor}");

            return _activeQueryExecutor != null;
        }

        public bool TryExecute(QueryContext query)
        {
            return _activeQueryExecutor?.TryExecute(query) ?? false;
        }

        public void Draw()
        {
            _activeQueryExecutor?.Draw();
        }
    }
}