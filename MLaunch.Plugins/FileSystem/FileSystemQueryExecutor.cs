using MLaunch.Core.QueryExecutors.ListQuery;
using System.Collections.Generic;

namespace MLaunch.Plugins.FileSystem
{
    public class FileSystemQueryExecutor : ListQueryExecutor
    {
        public override bool TryHandle(string term) => false;

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            throw new System.NotImplementedException();
        }
    }
}