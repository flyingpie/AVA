using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors
{
    public interface IAsyncQueryExecutor
    {
        int Order { get; }

        bool TryHandle(QueryContext query);

        Task<bool> TryExecuteAsync(QueryContext query);

        void Draw();
    }
}