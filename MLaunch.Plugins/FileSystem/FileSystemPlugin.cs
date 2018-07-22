using MLaunch.Core.QueryExecutors.ListQuery;
using System.Collections.Generic;

namespace MLaunch.Plugins.FileSystem
{
    public class FileSystemPlugin : ListQueryExecutor
    {
        public override int Order => 1000;

        public override bool CanHandle(string term) => true;

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            throw new System.NotImplementedException();
        }
    }
}