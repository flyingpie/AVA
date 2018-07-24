using MUI;
using MUI.DI;
using MUI.Extensions;
using System.Collections.Generic;
using System.Linq;
using Veldrid;

namespace MLaunch.Core.QueryExecutors.ListQuery
{
    [Service]
    public abstract class ListQueryExecutor : IQueryExecutor
    {
        [Dependency] private UIContext _context;

        [Dependency] private ResourceManager _resourceManager;

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

        public abstract IList<IListQueryResult> GetQueryResults(string term);

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
            QueryResults = GetQueryResults(term);

            // Succeeds when something was returned
            return QueryResults.Any();
        }

        public virtual bool TryExecute(string term)
        {
            if (IsSelectable && QueryResults.Count > SelectedItemIndex)
            {
                QueryResults[SelectedItemIndex].Execute(term);

                return true;
            }

            return false;
        }

        public void Draw()
        {
            // Previous
            if (_context.Input.IsKeyDown(Key.Up) || _context.Input.IsKeyDown(Key.K, ModifierKeys.Control) || _context.Input.IsKeyDown(Key.Tab, ModifierKeys.Shift)) PreviousItem();

            // Next
            if (_context.Input.IsKeyDown(Key.Down) || _context.Input.IsKeyDown(Key.J, ModifierKeys.Control) || _context.Input.IsKeyDown(Key.Tab, ModifierKeys.None)) NextItem();

            for (int i = 0; i < QueryResults.Count; i++)
            {
                QueryResults[i].Draw(_context, IsSelectable && i == SelectedItemIndex);
            }
        }

        #endregion Query executor
    }
}