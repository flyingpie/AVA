using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors.ListQuery
{
	public interface IListQueryResult
	{
		void Draw(bool isSelected);

		Task ExecuteAsync(QueryContext query);
	}
}