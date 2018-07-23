using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;

namespace MLaunch.Plugins.Help
{
    [Service]
    public class HelpQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override int Order => 999;

        [RunAfterInject]
        private void Init()
        {
            QueryResults = GetQueryResults(null);
        }

        public override bool TryHandle(string term) => string.IsNullOrWhiteSpace(term) || term.ContainsCaseInsensitive("help");

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return new[]
            {
                new ListQueryResult()
                {
                    Name = "<file>",
                    Description = "Looks for files on the machine and opens or executes them when selected"
                }
            };
        }
    }
}