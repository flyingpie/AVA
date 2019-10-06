using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Plugin.LocalBookmarks
{
    [Service, Help(Name = "Local bookmarks", Description = "Queries local bookmarks file", ExampleUsage = "bm podcast")]
    public class LocalBookmarksQueryExecutor : ListQueryExecutor
    {
        private List<Bookmark> Bookmarks { get; set; }

        [Dependency] public LocalBookmarksSettings Settings { get; set; }

        public override int Order => 0;

        public override string Prefix => "dd ";

        public override void Initialize()
        {
            base.Initialize();

            try
            {
                Bookmarks = File.ReadAllLines(Environment.ExpandEnvironmentVariables(Settings.PathToBookmarks))
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Split(','))
                    .Where(l => l.Length == 4)
                    .Select(l => new Bookmark()
                    {
                        IsActive = bool.TryParse(l[0], out var isActive) ? isActive : true,
                        Category = l[1],
                        CategoryLower = l[1].ToLowerInvariant(),
                        Name = l[2],
                        NameLower = l[2].ToLowerInvariant(),
                        Url = l[3]
                    })
                    .ToList()
                ;
            }
            catch
            {
                // TODO
            }
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length);
            term = term.ToLowerInvariant();
            var terms = term.Split(' ');

            return Bookmarks
                .Where(b => terms.All(t => b.CategoryLower.Contains(t) || b.NameLower.Contains(t)))
                .Select(b => new ListQueryResult()
                {
                    Icon = ResourceManager.Instance.LoadImageFromUrl(b.FaviconUrl),
                    Name = b.Name,
                    Description = b.Url
                })
                .ToList()
            ;
        }

        public class Bookmark
        {
            public bool IsActive { get; set; }

            public string Category { get; set; }

            public string CategoryLower { get; set; }

            public string Name { get; set; }

            public string NameLower { get; set; }

            private string _url;

            public string Url
            {
                get => _url;
                set
                {
                    _url = value;

                    try
                    {
                        var uri = new System.Uri(value);
                        var fav = $"{uri.Scheme}://{uri.Host}/favicon.ico";

                        FaviconUrl = fav;
                    }
                    catch { }
                }
            }

            public string FaviconUrl { get; set; }
        }
    }
}