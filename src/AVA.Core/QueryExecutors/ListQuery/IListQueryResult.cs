using MUI;

namespace AVA.Core.QueryExecutors.ListQuery
{
    public interface IListQueryResult
    {
        void Draw(bool isSelected);

        void Execute(QueryContext query);
    }
}