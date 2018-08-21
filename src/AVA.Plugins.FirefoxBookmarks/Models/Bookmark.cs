namespace AVA.Plugins.FirefoxBookmarks.Models
{
    public class Bookmark
    {
        public string FaviconUrl { get; set; }

        public string Title { get; set; }

        public string TitleLower { get; set; }

        public string Url { get; set; }

        public string UrlLower { get; set; }

        public bool Matches(string term) => (TitleLower?.Contains(term) ?? false) || (UrlLower?.Contains(term) ?? false);

        public override string ToString() => $"{Title} ({Url})";
    }
}