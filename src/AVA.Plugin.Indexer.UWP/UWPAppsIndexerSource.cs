using AVA.Plugin.Indexer;
using MUI.DI;
using MUI.Win32.UWP;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Indexer.UWP
{
    [Service]
    public class UWPAppsIndexerSource : IIndexerSource
    {
        public IEnumerable<IndexedItem> GetItems() => NativeApiManifestHelpers
            .GetAllPackages()
            .Select(p => new UWPAppIndexedItem()
            {
                IndexerName = p.DisplayName,
                Description = p.Description,

                FullName = p.FullName,
                LogoPath = p.FullLogoPath
            })
            .ToList();
    }
}