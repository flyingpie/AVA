using MLaunch.Core.QueryExecutors.ListQuery;
using MLaunch.Indexing;
using MUI.DI;
using System.Collections.Generic;

namespace MLaunch.Plugins.Settings
{
    public class SettingsListQueryExecutor : ListQueryExecutor
    {
        [Dependency] public Indexer Indexer { get; set; }

        public override string Prefix => "settings";

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return new[]
            {
                new ListQueryResult()
                {
                    Name = "Rebuild index",
                    Description = "",
                    OnExecute = t => Indexer.Rebuild()
                }
            };
        }
    }
}