using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.Logging;
using MUI.Win32.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Tests
{
    public class DummyQueryExecutor : ListQueryExecutor
    {
        public override string Prefix => "dummy ";

        private ListQueryResult[] _dummyResults;
        private ILog _log = Log.Get<DummyQueryExecutor>();

        public DummyQueryExecutor()
        {
            _dummyResults = new[]
            {
                new ListQueryResult()
                {
                    Name = "Notepad++",
                    Description = @"D:\Syncthing\Apps\Notepad++\notepad++.exe",
                    Icon = ResourceManager.Instance.DefaultImage,
                    OnExecute = t => _log.Info("npp")
                },
                new ListQueryResult()
                {
                    Name = "TailBlazer",
                    Description = @"D:\Syncthing\Apps\Dev\TailBlazer\TailBlazer.exe",
                    Icon = ResourceManager.Instance.DefaultImage,
                    OnExecute = t => _log.Info("tb")
                },
                new ListQueryResult()
                {
                    Name = "Powershell",
                    Description = @"C:\Users\Marco van den Oever\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Windows PowerShell\Windows PowerShell.lnk",
                    Icon = ResourceManager.Instance.DefaultImage,
                    OnExecute = t => _log.Info("ps")
                },
                new ListQueryResult()
                {
                    Name = "KeePass",
                    Description = @"D:\Syncthing\Apps\KeePass\KeePass.exe",
                    Icon = ResourceManager.Instance.DefaultImage,
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
                Icon = ResourceManager.Instance.LoadImageFromIcon(d.Description),
                OnExecute = d.OnExecute
            });
    }
}