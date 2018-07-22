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

        private IList<IListQueryResult> _queryResults;
        private int _selectedItemIndex;

        public ListQueryExecutor()
        {
            _queryResults = new List<IListQueryResult>(0);
        }

        #region List

        public virtual int Order => 0;

        public abstract IList<IListQueryResult> GetQueryResults(string term);

        public void NextItem()
        {
            _selectedItemIndex++;
            if (_selectedItemIndex >= _queryResults.Count) _selectedItemIndex = 0;
        }

        public void PreviousItem()
        {
            _selectedItemIndex--;
            if (_selectedItemIndex < 0) _selectedItemIndex = _queryResults.Count - 1;
        }

        #endregion List

        #region Query executor

        public virtual bool TryHandle(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return false;

            _queryResults = GetQueryResults(term);

            return _queryResults.Any();
        }

        public virtual bool TryExecute(string term)
        {
            if (_queryResults.Count > _selectedItemIndex)
            {
                _queryResults[_selectedItemIndex].Execute(term);

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

            for (int i = 0; i < _queryResults.Count; i++)
            {
                _queryResults[i].Draw(_context, i == _selectedItemIndex);
            }
        }

        #endregion Query executor
    }
}