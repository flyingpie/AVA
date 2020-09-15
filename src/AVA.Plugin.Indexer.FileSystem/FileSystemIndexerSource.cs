using MUI.DI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Plugin.Indexer.FileSystem
{
	[Service]
	public class FileSystemIndexerSource : IIndexerSource
	{
		[Dependency] public FileSystemIndexerSourceSettings Settings { get; set; }

		public IEnumerable<IndexedItem> GetItems() => Settings.Paths
			.SelectMany(f => f.GetFiles())
			.Select(path => new FileSystemIndexedItem()
			{
				DisplayName = Path.GetFileNameWithoutExtension(path),
				IndexerName = Path.GetFileName(path),
				Description = path,

				Path = path,
			})
			.ToList()
		;
	}
}