using MUI;

namespace MLaunch.Core.QueryExecutors
{
    public interface IQueryExecutor
    {
        int Order { get; }

        bool TryHandle(string term);

        bool TryExecute(string term);

        void Draw(UIContext uiContext); // TODO: Move context parameter to DI (or remove entirely)
    }
}