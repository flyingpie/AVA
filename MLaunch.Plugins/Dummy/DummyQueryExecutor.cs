using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using MUI.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.Dummy
{
    public class DummyQueryExecutor : ListQueryExecutor
    {
        [Dependency] private ResourceManager _resourceManager;

        private Image _avatar;

        private ListQueryResult[] _dummyResults;

        [RunAfterInject]
        public /*private*/ void Initialize()
        {
            _avatar = new Image(_resourceManager.LoadTexture(@"Resources\Images\crashlog-doom.png"));

            _dummyResults = new[]
            {
                new ListQueryResult()
                {
                    Name = "Notepad++",
                    Description = @"C:\Program Files\Notepad++\notepad++.exe",
                    Icon = _avatar,
                    OnExecute = t => Console.WriteLine("npp")
                },
                new ListQueryResult()
                {
                    Name = "TailBlazer",
                    Description = @"C:\Syncthing\Apps\TailBlazer\TailBlazer.exe",
                    Icon = _avatar,
                    OnExecute = t => Console.WriteLine("tb")
                },
                new ListQueryResult()
                {
                    Name = "Powershell",
                    Description = @"C:\Program Files\Windows Powershell\powershell.exe",
                    Icon = _avatar,
                    OnExecute = t => Console.WriteLine("ps")
                },
                new ListQueryResult()
                {
                    Name = "KeePass",
                    Description = @"C:\Syncthing\Apps\KeePass\KeePass.exe",
                    Icon = _avatar,
                    OnExecute = t => Console.WriteLine("kp")
                }
            };
        }

        public override int Order => 999;

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return _dummyResults
                .Where(d => d.Name.ToLowerInvariant().Contains(term.ToLowerInvariant()))
                .Cast<IListQueryResult>()
                .ToList();
        }
    }
}