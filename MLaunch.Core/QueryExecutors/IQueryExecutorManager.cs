namespace MLaunch.Core.QueryExecutors
{
    public interface IQueryExecutorManager
    {
        IQueryExecutor GetQueryExecutor(string term);
    }
}