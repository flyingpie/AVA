using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Core.QueryExecutors.ListQuery
{
    [Service]
    public abstract class ListQueryExecutor : IQueryExecutor
    {
        public abstract string Description { get; }

        public abstract string Name { get; }

        public abstract string ExampleUsage { get; }

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

        public virtual bool TryHandle(string term)
        {
            SelectedItemIndex = 0;

            // Require at least some term
            if (string.IsNullOrWhiteSpace(term)) return false;

            // Require a prefix (when specified in the plugin)
            if (Prefix != null && !term.ToLowerInvariant().StartsWith(Prefix.ToLowerInvariant())) return false;

            // Execute the query
            QueryResults = GetQueryResults(term).ToList();

            // Succeeds when something was returned
            return QueryResults.Any();
        }

        public virtual bool TryExecute(QueryContext query)
        {
            if (IsSelectable && QueryResults.Count > SelectedItemIndex)
            {
                QueryResults[SelectedItemIndex].Execute(query);

                return true;
            }

            return false;
        }

        public void Draw()
        {
            // Previous
            if (Input.IsKeyPressed(Keys.Up) || (Input.IsKeyPressed(Keys.K) && Input.IsKeyDown(Keys.LeftControl)) || (Input.IsKeyPressed(Keys.Tab) && Input.IsKeyDown(Keys.LeftShift))) PreviousItem();

            // Next
            if (Input.IsKeyPressed(Keys.Down) || (Input.IsKeyPressed(Keys.J) && Input.IsKeyDown(Keys.LeftControl)) || (Input.IsKeyPressed(Keys.Tab) && !Input.IsKeyDown(Keys.LeftShift))) NextItem();

            for (int i = 0; i < QueryResults.Count; i++)
            {
                QueryResults[i].Draw(IsSelectable && i == SelectedItemIndex);
            }
        }

        #endregion Query executor
    }
}