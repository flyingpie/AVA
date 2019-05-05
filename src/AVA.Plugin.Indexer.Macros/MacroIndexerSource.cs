using AVA.Plugin.Indexer;
using MUI.DI;
using MUI.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Indexer.Macros
{
    [Service]
    public class MacroIndexerSource : IIndexerSource
    {
        [Dependency] public MacroSettings Settings { get; set; }

        public IEnumerable<IndexedItem> GetItems()
        {
            var log = Log.Get(this);

            if (Settings == null || Settings.Macros == null || !Settings.Macros.Any())
            {
                log.Error($"No macros found");
            }

            return Settings?.Macros ?? Enumerable.Empty<IndexedItem>();
        }
    }
}