using System.Collections.Generic;

namespace AVA.Plugin.Indexer
{
    public interface IIndexerSource
    {
        IEnumerable<IndexedItem> GetItems();
    }
}