using MUI;
using MUI.Graphics;

namespace AVA.Indexing
{
    public class IndexedItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Extension { get; set; }

        public virtual Image GetIcon(ResourceManager resourceManager) => null;

        public virtual bool Execute() => false;
    }
}