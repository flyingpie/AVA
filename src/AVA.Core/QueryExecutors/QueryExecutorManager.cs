using MUI.DI;
using MUI.Logging;
using System;
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
            _activeQueryExecutor = QueryExecutors.FirstOrDefault(qe =>
            {
                try
                {
                    return qe.TryHandle(query);
                }
                catch (Exception ex)
                {
                    _log.Error($"Error while calling 'TryHandle' on query executor '{qe}': '{ex.Message}'");
                }

                return false;
            });

            _log.Info($"Term '{query}', selected query executor: {_activeQueryExecutor}");

            return _activeQueryExecutor != null;
        }

        public bool TryExecute(QueryContext query)
        {
            try
            {
                return _activeQueryExecutor?.TryExecute(query) ?? false;
            }
            catch (Exception ex)
            {
                _log.Error($"Error while calling 'TryExecute' on query executor '{_activeQueryExecutor}': '{ex.Message}'");
            }

            return false;
        }

        public void Draw()
        {
            _activeQueryExecutor?.Draw();
        }
    }
}