using MUI;

namespace MLaunch.Core.QueryExecutors.ListQuery
{
    public interface IListQueryResult
    {
        void Draw(UIContext context, bool isSelected);

        void Execute(string term);
    }
}