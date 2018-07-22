using MLaunch.Core.QueryExecutors;
using MLaunch.Plugins.Dummy;
using MLaunch.Plugins.Help;
using MLaunch.Plugins.NoResults;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch
{
    public class QueryExecutorManager
    {
        [Dependency] public /*private*/ ResourceManager _resourceManager;

        private IList<IQueryExecutor> _queryExecutors;

        [RunAfterInject]
        public /*private*/ void Initialize()
        {
            _queryExecutors = new List<IQueryExecutor>();

            _queryExecutors.Add(new HelpQueryExecutor());
            _queryExecutors.Add(new NoResultsQueryExecutor());
            _queryExecutors.Add(CreateDummyQueryExecutor());

            _queryExecutors = _queryExecutors.OrderBy(qe => qe.Order).ToList();
        }

        public IQueryExecutor GetQueryExecutor(string term)
        {
            return _queryExecutors.FirstOrDefault(qe => qe.TryHandle(term));
        }

        private IQueryExecutor CreateDummyQueryExecutor()
        {
            var queryExecutor = new DummyQueryExecutor();
            queryExecutor._resourceManager = _resourceManager;
            queryExecutor.Initialize();

            return queryExecutor;
        }
    }
}