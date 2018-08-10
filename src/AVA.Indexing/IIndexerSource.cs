using System.Collections.Generic;

namespace AVA.Indexing
{
    public interface IIndexerSource
    {
        IEnumerable<IndexedItem> GetItems();
    }
}