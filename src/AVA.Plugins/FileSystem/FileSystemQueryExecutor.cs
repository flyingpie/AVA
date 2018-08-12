using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using AVA.Indexing;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.FileSystem
{
    [Help(Name = "File system", Description = "Search the file system for apps, shortcuts and media files", ExampleUsage = "notepad", Icon = FAIcon.SearchSolid)]
    public class FileSystemQueryExecutor : ListQueryExecutor
    {
        [Dependency] public Indexer Indexer { get; set; }

        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term) =>
            Indexer
            .Query(term)
            .Select(r => (IListQueryResult)new ListQueryResult()
            {
                Name = r.Name,
                Description = r.Description,
                Icon = r.GetIcon(ResourceManager),
                OnExecute = t => r.Execute()
            })
            .Take(4);
    }
}