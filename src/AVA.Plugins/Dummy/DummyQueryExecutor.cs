using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using MUI.Graphics;
using MUI.Logging;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Dummy
{
    public class DummyQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Name => "Dummy";

        public override string Description => "Test plugin";

        public override string ExampleUsage => "dummy ";

        public override string Prefix => "dummy ";

        private Image _dummyImage;

        private ListQueryResult[] _dummyResults;
        private ILog _log = Log.Get<DummyQueryExecutor>();

        [RunAfterInject]
        private void Initialize()
        {
            _dummyImage = ResourceManager.LoadImage(@"Resources\Images\crashlog-doom.png");

            _dummyResults = new[]
            {
                new ListQueryResult()
                {
                    Name = "Notepad++",
                    Description = @"D:\Syncthing\Apps\Notepad++\notepad++.exe",
                    Icon = _dummyImage,
                    OnExecute = t => _log.Info("npp")
                },
                new ListQueryResult()
                {
                    Name = "TailBlazer",
                    Description = @"D:\Syncthing\Apps\Dev\TailBlazer\TailBlazer.exe",
                    Icon = _dummyImage,
                    OnExecute = t => _log.Info("tb")
                },
                new ListQueryResult()
                {
                    Name = "Powershell",
                    Description = @"C:\Users\Marco van den Oever\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Windows PowerShell\Windows PowerShell.lnk",
                    Icon = _dummyImage,
                    OnExecute = t => _log.Info("ps")
                },
                new ListQueryResult()
                {
                    Name = "KeePass",
                    Description = @"D:\Syncthing\Apps\KeePass\KeePass.exe",
                    Icon = _dummyImage,
                    OnExecute = t => _log.Info("kp")
                }
            };
        }

        public override int Order => 999;

        public override IEnumerable<IListQueryResult> GetQueryResults(string term) => _dummyResults
            .Where(d => d.Name.ToLowerInvariant().Contains(term.ToLowerInvariant()))
            .Select(d => (IListQueryResult)new ListQueryResult()
            {
                Name = d.Name,
                Description = d.Description,
                Icon = ResourceManager.LoadImageFromIcon(d.Description),
                OnExecute = d.OnExecute
            });
    }
}