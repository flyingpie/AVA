using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors.ListQuery
{
	[Service]
	public abstract class ListQueryExecutor : IAsyncQueryExecutor
	{
		public IList<IListQueryResult> QueryResults { get; set; }

		public int SelectedItemIndex { get; set; }

		public ListQueryExecutor()
		{
			QueryResults = new List<IListQueryResult>(0);
		}

		#region List

		public virtual int Order => 500;

		public virtual string Prefix => null;

		public virtual bool IsSelectable => true;

		public abstract IEnumerable<IListQueryResult> GetQueryResults(string term);

		public void NextItem()
		{
			SelectedItemIndex++;
			if (SelectedItemIndex >= QueryResults.Count) SelectedItemIndex = 0;
		}

		public void PreviousItem()
		{
			SelectedItemIndex--;
			if (SelectedItemIndex < 0) SelectedItemIndex = QueryResults.Count - 1;
		}

		#endregion List

		#region Query executor

		[RunAfterInject]
		public virtual void Initialize()
		{
		}

		public virtual bool TryHandle(QueryContext query)
		{
			SelectedItemIndex = 0;

			// Require at least some term
			if (string.IsNullOrWhiteSpace(query.Text)) return false;

			// Require a prefix (when specified in the plugin)
			if (Prefix != null && !query.Text.ToLowerInvariant().StartsWith(Prefix.ToLowerInvariant())) return false;

			// Execute the query
			QueryResults = GetQueryResults(query.Text).ToList();

			// Succeeds when something was returned
			return QueryResults.Any();
		}

		public virtual bool TryExecuteAsync(QueryContext query)
		{
			if (IsSelectable && QueryResults.Count > SelectedItemIndex)
			{
				QueryResults[SelectedItemIndex].ExecuteAsync(query);

				return true;
			}

			return false;
		}

		public virtual void Draw()
		{
			// Previous
			if (Input.IsKeyPressed(Keys.Up) || (Input.IsKeyPressed(Keys.K) && Input.IsKeyDown(Keys.LeftControl))) PreviousItem();

			// Next
			if (Input.IsKeyPressed(Keys.Down) || (Input.IsKeyPressed(Keys.J) && Input.IsKeyDown(Keys.LeftControl))) NextItem();

			for (int i = 0; i < QueryResults.Count; i++)
			{
				QueryResults[i].Draw(IsSelectable && i == SelectedItemIndex);
			}
		}

		#endregion Query executor
	}
}