using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using AVA.Plugin.Indexer;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using MUI.ImGuiControls;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.IndexerQuery
{
	[Help(Name = "File system", Description = "Search the file system for apps, shortcuts and media files", ExampleUsage = "notepad", Icon = FAIcon.SearchSolid)]
	public class FileSystemQueryExecutor : ListQueryExecutor
	{
		[Dependency] public IIndexer Indexer { get; set; }

		public override IEnumerable<IListQueryResult> GetQueryResults(string term) =>
			Indexer
			.Query(term)
			.Select(r => (IListQueryResult)new ListQueryResult()
			{
				Name = r.DisplayName,
				//Name = $"{r.DisplayName} ({r.Score})",
				Description = r.Description,
				Icon = new ImageBox(r.GetIcon())
				{
					Padding = new Margin(5)
				},
				OnExecute = t => r.Execute()
			})
			.Take(4);
	}
}