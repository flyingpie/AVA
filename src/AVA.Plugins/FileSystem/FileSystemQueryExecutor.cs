using AVA.Core.QueryExecutors.ListQuery;
using AVA.Indexing;
using MUI;
using MUI.DI;
using MUI.Logging;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AVA.Plugins.FileSystem
{
    public class FileSystemQueryExecutor : ListQueryExecutor
    {
        [Dependency] public Indexer Indexer { get; set; }

        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Name => "File system";

        public override string Description => "Search the file system for apps, shortcuts and media files";

        public override string ExampleUsage => "notepad";

        private ILog _log = Log.Get<FileSystemQueryExecutor>();

        public override IEnumerable<IListQueryResult> GetQueryResults(string term) => Indexer
            .Query(term)
            .Select(r => (IListQueryResult)new ListQueryResult()
            {
                Name = Path.GetFileNameWithoutExtension(r.Document.Get("path")),
                Description = r.Document.Get("path"),
                Icon = ResourceManager.LoadImageFromIcon(r.Document.Get("path")),
                OnExecute = t => Open(r)
            })
            .Take(4);

        private void Open(QS qs)
        {
            try
            {
                var path = qs.Document.Get("path");

                var startInfo = new ProcessStartInfo()
                {
                    FileName = path
                };

                if (Input.IsKeyDown(Keys.LeftControl))
                {
                    startInfo.Verb = "runas";
                }

                Process.Start(startInfo).Dispose();
            }
            catch (Exception ex)
            {
                _log.Info($"Wups: {ex.Message}");
            }
        }
    }
}