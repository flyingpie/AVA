﻿using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AVA.Plugins.Help
{
    [Service]
    public class HelpQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override int Order => 999;

        private List<HelpAttribute> _helpAttrs;

        [RunAfterInject]
        private void Init()
        {
            _helpAttrs = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(ass => ass.GetTypes())
                .Select(t => t.GetCustomAttribute<HelpAttribute>())
                .Where(attr => attr != null)
                .OrderBy(attr => attr.Name)
                .ToList()
            ;

            QueryResults = GetQueryResults(null).ToList();
        }

        public override bool TryHandle(QueryContext query) => query.IsEmpty || query.Text.ContainsCaseInsensitive("help");

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            return _helpAttrs
                .OrderBy(qe => qe.Name)
                .Select(qe => (IListQueryResult)new ListQueryResult()
                {
                    Name = $"{qe.Name} (eg. '{qe.ExampleUsage}')",
                    Description = qe.Description,
                    OnExecute = t =>
                    {
                        t.Text = qe.ExampleUsage;
                        t.HideUI = false;
                        t.ResetText = false;
                    }
                });
        }
    }
}