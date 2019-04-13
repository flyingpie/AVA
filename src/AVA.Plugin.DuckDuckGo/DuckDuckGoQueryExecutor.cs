using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using DuckDuckGo.Net;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.DuckDuckGo
{
    [Help(Name = "DuckDuckGo", Description = "Instant Answers Queries", ExampleUsage = "dg", Icon = FAIcon.DumbbellSolid)]
    public class DuckDuckGoQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Prefix => "dg";

        private Search _search;

        public override void Initialize()
        {
            _search = new Search();
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length).Trim().ToLowerInvariant();

            var result = _search.Query(term, "AVA");

            if (string.IsNullOrEmpty(result.AbstractSource)) return Enumerable.Empty<IListQueryResult>();

            return new IListQueryResult[] {
                new ListQueryResult()
                {
                    Name = result.AbstractSource,
                    Description = result.Abstract,
                    Icon = ResourceManager.TryLoadImageFromUrl(result.Image, out var image) ? image : ResourceManager.DefaultImage,
                    OnExecute = q => System.Diagnostics.Process.Start(result.AbstractUrl)
                }
            };
        }
    }
}