using AVA.Core.QueryExecutors.ListQuery;
using AVA.Plugins.FirefoxBookmarks.Models;
using MUI;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Plugins.FirefoxBookmarks
{
    public class FirefoxBookmarksQueryExecutor : ListQueryExecutor
    {
        private List<Bookmark> _bookmarks;

        public override int Order => 0;

        public override string Prefix => "ff ";

        public override void Initialize()
        {
            RefreshBookmarksCache();
        }

        private void RefreshBookmarksCache()
        {
            var ffProfile = FindMostRecentFirefoxProfileDb();

            if (ffProfile == null) return;

            var databasePath = Path.Combine(ffProfile.FullName);

            var fileInf = new FileInfo(databasePath);
            var lastWrite = fileInf.LastWriteTimeUtc; // Use for cache

            using (var db = new SQLiteConnection(databasePath))
            {
                _bookmarks = db.GetBookmarks().ToList();
            }
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length);

            return _bookmarks
                .Where(bm => bm.Matches(term))
                .Take(10)
                .Select(bm => new ListQueryResult()
                {
                    Icon = ResourceManager.Instance.LoadImageFromUrl(bm.FaviconUrl),
                    Name = bm.Title,
                    Description = bm.Url,
                    OnExecute = qt => System.Diagnostics.Process.Start(bm.Url).Dispose()
                })
                .ToList();
        }

        private static FileInfo FindMostRecentFirefoxProfileDb()
        {
            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\Profiles");

            return Directory
                .GetFiles(appData, "places.sqlite", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();
        }
    }
}