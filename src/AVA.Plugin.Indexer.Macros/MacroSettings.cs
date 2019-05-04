using AVA.Core.Settings;
using System.Collections.Generic;

namespace AVA.Plugin.Indexer.Macros
{
    [Section("Macros")]
    public class MacroSettings : Settings
    {
        public List<MacroIndexedItem> Macros { get; set; }
    }
}