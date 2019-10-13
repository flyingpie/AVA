using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using MUI.Glyphs;
using MUI.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AVA.Plugin.LocalBookmarks
{
    [Service, Help(Name = "Local bookmarks", Description = "Queries local bookmarks file", ExampleUsage = "dd podcast", Icon = FAIcon.BookmarkRegular)]
    public class LocalBookmarksQueryExecutor : ListQueryExecutor
    {
        private static string EditTerm = "edit";

        private List<Bookmark> Bookmarks { get; set; }

        [Dependency] public ResourceManager Resources { get; set; }

        [Dependency] public LocalBookmarksSettings Settings { get; set; }

        public override int Order => 0;

        public override string Prefix => "dd ";

        public override void Initialize()
        {
            base.Initialize();

            InitializeBookmarks();
        }

        private FileSystemWatcher _fsWatcher;

        private bool _refresh;

        private void InitializeBookmarks()
        {
            try
            {
                var pathToBookmarks = GetPathToBookmarks();

                Log.Get<LocalBookmarksQueryExecutor>().Info($"Initializing bookmarks from file '{pathToBookmarks}'...");

                if (_fsWatcher == null)
                {
                    _fsWatcher = new FileSystemWatcher();

                    _fsWatcher.Path = Path.GetDirectoryName(pathToBookmarks);
                    _fsWatcher.Filter = Path.GetFileName(pathToBookmarks);

                    _fsWatcher.Changed += (s, a) => _refresh = true;

                    _fsWatcher.EnableRaisingEvents = true;
                }

                Bookmarks = File.ReadAllLines(pathToBookmarks)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Split(','))
                    .Where(l => l.Length == 4)
                    .Select(l => new Bookmark()
                    {
                        IsActive = int.TryParse(l[0], out var isActive) && isActive > 0 ? true : false,
                        Category = l[1],
                        CategoryLower = l[1].ToLowerInvariant(),
                        Name = l[2],
                        NameLower = l[2].ToLowerInvariant(),
                        Url = l[3]
                    })
                    .Where(b => b.IsActive)
                    .ToList()
                ;

                _refresh = false;
            }
            catch
            {
                // TODO
            }
        }

        private string GetPathToBookmarks() => Environment.ExpandEnvironmentVariables(Settings.PathToBookmarks);

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length);
            term = term.ToLowerInvariant();

            if (term.Equals(EditTerm, StringComparison.OrdinalIgnoreCase))
            {
                var pathToBookmarks = GetPathToBookmarks();

                return new[]
                {
                    new ListQueryResult()
                    {
                        Icon = Resources.LoadFontAwesomeIcon(FAIcon.EditRegular, 50 / 3),
                        Name = "Edit bookmarks",
                        Description = pathToBookmarks,
                        OnExecute = qt => Process.Start(pathToBookmarks).Dispose()
                    }
                };
            }

            var terms = term.Split(' ');

            if (_refresh) InitializeBookmarks();

            return Bookmarks
                .Where(b => terms.All(t => b.CategoryLower.Contains(t) || b.NameLower.Contains(t)))
                .Select(b => new ListQueryResult()
                {
                    Icon = ResourceManager.Instance.LoadImageFromUrl(b.FaviconUrl),
                    Name = b.Name,
                    Description = b.Url,
                    OnExecute = qt => Process.Start(b.Url).Dispose()
                })
                .ToList()
            ;
        }
    }
}