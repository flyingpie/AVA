using SQLite;

namespace AVA.Plugins.FirefoxBookmarks.Models
{
    [Table("moz_places")]
    public class MozPlace
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("url")]
        public string Url { get; set; }
    }
}