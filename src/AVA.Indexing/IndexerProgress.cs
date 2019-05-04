using System;

namespace AVA.Indexing
{
    public class IndexerProgress
    {
        public string CurrentIndexerName { get; set; }

        public int TotalIndexedItems { get; set; }

        public int ProcessedIndexedItems { get; set; }

        public int ProcessedIndexedItemsPercentage
            => HasStarted ? (int)Math.Round((float)ProcessedIndexedItems / (float)TotalIndexedItems * 100, 0) : 0;

        public bool HasStarted => !string.IsNullOrWhiteSpace(CurrentIndexerName) && TotalIndexedItems > 0;
    }
}