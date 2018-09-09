using System;
using System.Linq;

namespace AVA.Plugins.FirefoxBookmarks.Models
{
    public class Bookmark
    {
        public string FaviconUrl { get; set; }

        public string Title { get; set; }

        public string TitleLower { get; set; }

        public string Url { get; set; }

        public string UrlLower { get; set; }

        public bool Matches(string term)
        {
            var terms = term.ToLowerInvariant().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            return terms.All(t => (TitleLower?.Contains(t) ?? false) || (UrlLower?.Contains(t) ?? false));
        }

        public override string ToString() => $"{Title} ({Url})";
    }
}