using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors
{
    public interface IAsyncQueryExecutor
    {
        string Name { get; }

        string Description { get; }

        string ExampleUsage { get; }

        int Order { get; }

        bool TryHandle(QueryContext query);

        Task<bool> TryExecuteAsync(QueryContext query);

        void Draw();
    }
}