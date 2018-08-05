using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors
{
    public interface IQueryExecutorManager
    {
        void Draw();

        bool TryHandle(QueryContext query);

        Task<bool> TryExecuteAsync(QueryContext query);
    }
}