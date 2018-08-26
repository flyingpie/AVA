using MUI.Graphics;
using Newtonsoft.Json;

namespace AVA.Indexing
{
    public class IndexedItem
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public float Score { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Extension { get; set; }

        public virtual Image GetIcon() => null;

        public virtual bool Execute() => false;
    }
}