using MLaunch.Core.QueryExecutors.ListQuery;
using MLaunch.Indexing;
using MUI;
using MUI.DI;
using MUI.Extensions;
using MUI.Graphics;
using MUI.Win32.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MLaunch.Plugins.FileSystem
{
    public class FileSystemQueryExecutor : ListQueryExecutor
    {
        [Dependency] private ResourceManager _resourceManager;
        [Dependency] private Indexer _indexer;

        [Dependency] private UIContext _ui;

        private Image _defaultImage;

        [RunAfterInject]
        private void Init()
        {
            _defaultImage = _resourceManager.LoadImage(@"Resources\Images\crashlog-doom.png");
        }

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return _indexer
                .Query(term)
                .Select(r => (IListQueryResult)new ListQueryResult()
                {
                    Name = r.Document.Get("filename"),
                    Description = r.Document.Get("path"),
                    Icon = _resourceManager.LoadImageFromIcon(r.Document.Get("path")),
                    OnExecute = t => Open(r)
                })
                .Take(4)
                .ToList();
        }

        private void Open(QS qs)
        {
            var path = qs.Document.Get("path");

            var startInfo = new ProcessStartInfo()
            {
                FileName = path
            };

            if (_ui.Input.IsKeyDown(Veldrid.Key.Enter, Veldrid.ModifierKeys.Control))
            {
                startInfo.Verb = "runas";
            }

            Process.Start(startInfo);
        }
    }
}