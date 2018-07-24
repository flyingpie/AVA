using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using MUI.Win32;
using MUI.Win32.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WindowsControlPanelItems;

namespace MLaunch.Plugins.ControlPanel
{
    public class ControlPanelQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Name => "Control panel";

        public override string Description => "Search through and opens control panel items";

        public override string ExampleUsage => "cp <term?>";

        public override int Order => 0;

        public override string Prefix => "cp ";

        private List<ControlPanelItem> _items;

        [RunAfterInject]
        public void Init()
        {
            _items = List.Create(32);
        }

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length).ToLowerInvariant();

            return _items
                .Where(i => i.LocalizedString.ToLowerInvariant().Contains(term))
                .OrderBy(i => i.LocalizedString)
                .Select(i => (IListQueryResult)new ListQueryResult()
                {
                    Name = i.LocalizedString,
                    Description = i.ExecutablePath.FileName + " " + i.ExecutablePath.Arguments,
                    Icon = ResourceManager.LoadImageFromIcon(i.ExecutablePath.FileName + " " + i.ExecutablePath.Arguments, i.Icon),
                    OnExecute = t =>
                    {
                        var proc = Process.Start(i.ExecutablePath);

                        PInvoke.SetForegroundWindow((int)proc.MainWindowHandle);
                    }
                })
                .ToList();
        }
    }
}