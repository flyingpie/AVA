using AVA.Core.QueryExecutors.ListQuery;
using AVA.Indexing;
using MUI.DI;
using System.Collections.Generic;

namespace AVA.Plugins.Settings
{
    public class SettingsListQueryExecutor : ListQueryExecutor
    {
        public override string Name => "Settings";

        public override string Description => "Shows the settings set";

        public override string ExampleUsage => Prefix;

        [Dependency] public Indexer Indexer { get; set; }

        public override string Prefix => "ava";

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
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