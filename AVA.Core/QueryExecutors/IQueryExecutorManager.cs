namespace AVA.Core.QueryExecutors
{
    public interface IQueryExecutorManager
    {
        IQueryExecutor GetQueryExecutor(string term);
    }
}