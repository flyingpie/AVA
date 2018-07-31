namespace AVA.Core.QueryExecutors
{
    public interface IQueryExecutor
    {
        string Name { get; }

        string Description { get; }

        string ExampleUsage { get; }

        int Order { get; }

        bool TryHandle(QueryContext query);

        bool TryExecute(QueryContext query);

        void Draw();
    }
}