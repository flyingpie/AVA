using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using System.Linq;

namespace MLaunch
{
    public interface IQueryExecutorManager
    {
        IQueryExecutor GetQueryExecutor(string term);
    }

    public class QueryExecutorManager : IQueryExecutorManager
    {
        [Dependency] public /*private*/ ResourceManager _resourceManager;

        [Dependency] private IQueryExecutor[] _queryExecutors;

        //private IList<IQueryExecutor> _queryExecutors;

        [RunAfterInject]
        public /*private*/ void Initialize()
        {
            //_queryExecutors = new List<IQueryExecutor>();

            //_queryExecutors.Add(new HelpQueryExecutor());
            //_queryExecutors.Add(new NoResultsQueryExecutor());
            //_queryExecutors.Add(CreateDummyQueryExecutor());

            _queryExecutors = _queryExecutors.OrderBy(qe => qe.Order).ToArray();
        }

        public IQueryExecutor GetQueryExecutor(string term)
        {
            return _queryExecutors.FirstOrDefault(qe => qe.TryHandle(term));
        }

        //private IQueryExecutor CreateDummyQueryExecutor()
        //{
        //    //var queryExecutor = new DummyQueryExecutor();
        //    //queryExecutor._resourceManager = _resourceManager;
        //    //queryExecutor.Initialize();

        //    //return queryExecutor;
        //}
    }
}