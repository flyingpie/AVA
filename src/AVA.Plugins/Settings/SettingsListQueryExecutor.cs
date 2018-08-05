using AVA.Core.QueryExecutors.ListQuery;
using AVA.Indexing;
using MUI;
using MUI.DI;
using System.Collections.Generic;

namespace AVA.Plugins.Settings
{
    public class SettingsListQueryExecutor : ListQueryExecutor
    {
        [Dependency] public Indexer Indexer { get; set; }

        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Name => "Settings";

        public override string Description => "Shows the settings set";

        public override string ExampleUsage => Prefix;

        public override string Prefix => "ava";

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            return new[]
            {
                CreateRebuildIndexSetting()
            };
        }

        private ListQueryResult CreateRebuildIndexSetting()
        {
            var res = new ListQueryResult()
            {
                Name = "Rebuild index",
                Description = "",
                Icon = ResourceManager.DefaultImage
            };

            res.OnExecuteAsync = async query =>
            {
                res.Description = "Rebuilding index...";
                res.Icon = ResourceManager.LoadingImage;

                await Indexer.RebuildAsync();

                res.Icon = ResourceManager.DefaultImage;
                res.Description = "";

                query.HideUI = false;
                query.ResetText = false;
            };

            return res;
        }
    }
}