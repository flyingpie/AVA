using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using AVA.Indexing;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System.Collections.Generic;

namespace AVA.Plugins.Settings
{
    [Help(Name = "Settings", Description = "Shows the settings set", ExampleUsage = "ava", Icon = FAIcon.VenusSolid)]
    public class SettingsListQueryExecutor : ListQueryExecutor
    {
        [Dependency] public Indexer Indexer { get; set; }

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
                Icon = ResourceManager.Instance.DefaultImage
            };

            res.OnExecuteAsync = async query =>
            {
                res.Description = "Rebuilding index...";
                res.Icon = ResourceManager.Instance.LoadingImage;

                await Indexer.RebuildAsync();

                res.Icon = ResourceManager.Instance.DefaultImage;
                res.Description = "";

                query.HideUI = false;
                query.ResetText = false;
            };

            return res;
        }
    }
}