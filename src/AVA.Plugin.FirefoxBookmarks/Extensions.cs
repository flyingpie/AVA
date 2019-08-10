using AVA.Plugins.FirefoxBookmarks.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.FirefoxBookmarks
{
    public static class Extensions
    {
        public static IEnumerable<Bookmark> GetBookmarks(this SQLiteConnection db)
        {
            var mozBookmarks = db
                .Table<MozBookmark>()
                .Where(b => b.Fk != null)
                .ToList()
            ;

            var mozPlaces = db
                .Table<MozPlace>()
                .ToDictionary(p => p.Id, p => p)
            ;

            return mozBookmarks
                .Select(b =>
                {
                    var url = mozPlaces.TryGetValue(b.Fk.Value, out var place) ? place.Url : null;
                    string fav = null;

                    if (url != null)
                    {
                        var uri = new Uri(url);
                        fav = $"{uri.Scheme}://{uri.Host}/favicon.ico";
                    }

                    return new Bookmark()
                    {
                        FaviconUrl = fav,

                        Title = b.Title,
                        TitleLower = b.Title?.ToLowerInvariant(),

                        Url = url,
                        UrlLower = url?.ToLowerInvariant()
                    };
                })
                .Where(b => !string.IsNullOrWhiteSpace(b.Url))
                .ToList()
            ;
        }
    }
}