namespace AVA.Core.QueryExecutors
{
    public interface IQueryExecutorManager
    {
        void Draw();

        bool TryHandle(QueryContext query);

        bool TryExecute(QueryContext query);
    }
}