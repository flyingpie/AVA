using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVA.Plugin.Indexer
{
	public interface IIndexer
	{
		IIndexerSource[] IndexerSources { get; set; }

		void Init();

		List<IndexedItem> Query(string term);

		Task RebuildAsync(IndexerProgress progress);
	}
}