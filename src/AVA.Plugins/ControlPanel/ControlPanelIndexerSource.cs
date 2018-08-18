using AVA.Indexing;
using MUI;
using MUI.DI;
using MUI.Graphics;
using MUI.Win32.Extensions;
using System.Collections.Generic;
using System.Linq;
using WindowsControlPanelItems;

namespace AVA.Plugins.ControlPanel
{
    [Service]
    public class ControlPanelIndexerSource : IIndexerSource
    {
        public IEnumerable<IndexedItem> GetItems() =>
            ControlPanelItemList
            .Create()
            .Select(cpi => new ControlPanelIndexedItem()
            {
                Item = cpi,

                Name = cpi.LocalizedString,
                Description = cpi.ProcessStartInfo.FileName + " " + cpi.ProcessStartInfo.Arguments,
                Extension = ".exe",
            })
            .ToList();
    }

    public class ControlPanelIndexedItem : IndexedItem
    {
        public ControlPanelItem Item { get; set; }

        public override bool Execute()
        {
            Item.Execute();

            return true;
        }

        public override Image GetIcon() => ResourceManager.Instance.LoadImageFromIcon(Item.ProcessStartInfo.FileName + " " + Item.ProcessStartInfo.Arguments, Item.GetIcon(64));
    }
}