using SQLite;

namespace AVA.Plugins.FirefoxBookmarks.Models
{
    [Table("moz_bookmarks")]
    public class MozBookmark
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("fk")]
        public int? Fk { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}