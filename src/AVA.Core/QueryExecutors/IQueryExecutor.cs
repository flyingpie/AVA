namespace AVA.Core.QueryExecutors
{
    public interface IQueryExecutor
    {
        int Order { get; }

        bool TryHandle(QueryContext query);

        bool TryExecute(QueryContext query);

        void Draw();
    }
}