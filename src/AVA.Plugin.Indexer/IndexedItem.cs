using MUI.Graphics;
using Newtonsoft.Json;

namespace AVA.Plugin.Indexer
{
	public class IndexedItem
	{
		[JsonIgnore]
		public int Id { get; set; }

		[JsonIgnore]
		public float Score { get; set; }

		public virtual int Boost { get; set; }

		public virtual string DisplayName
		{
			get => IndexerName;
			set { }
		}

		public virtual string IndexerName { get; set; }

		public virtual string Description { get; set; }

		public virtual Image GetIcon() => null;

		public virtual bool Execute() => false;
	}
}