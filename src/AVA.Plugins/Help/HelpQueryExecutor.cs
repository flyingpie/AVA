using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Help
{
    [Service]
    public class HelpQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        [Dependency] public IQueryExecutor[] QueryExecutors { get; set; }

        public override string Name => "Help";

        public override string Description => "Displays this listing";

        public override string ExampleUsage => "help";

        public override int Order => 999;

        [RunAfterInject]
        private void Init()
        {
            QueryResults = GetQueryResults(null).ToList();
        }

        public override bool TryHandle(QueryContext query) => query.IsEmpty || query.Text.ContainsCaseInsensitive("help");

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            return QueryExecutors
                .OrderBy(qe => qe.Name)
                .Select(qe => (IListQueryResult)new ListQueryResult()
                {
                    Name = $"{qe.Name} (eg. '{qe.ExampleUsage}')",
                    Description = qe.Description,
                    OnExecute = t =>
                    {
                        t.Text = qe.ExampleUsage;
                        t.HideUI = false;
                    }
                });
        }
    }
}