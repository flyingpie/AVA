using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using MUI.Glyphs;
using MUI.ImGuiControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace AVA.Plugin.Help
{
    [Service]
    public class HelpQueryExecutor : ListQueryExecutor
    {
        public override int Order => 999;

        private List<HelpAttribute> _helpAttrs;

        public HelpQueryExecutor()
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

        public override IEnumerable<IListQueryResult> GetQueryResults(string term) =>
            _helpAttrs
            .OrderBy(qe => qe.Name)
            .Select(qe => (IListQueryResult)new ListQueryResult()
            {
                Icon = new ImageBox(qe.Icon != FAIcon.None ? ResourceManager.Instance.LoadFontAwesomeIcon(qe.Icon, 50 / 3) : null)
                {
                    ScaleMode = MUI.Graphics.ScaleMode.Center,
                    Tint = new Vector4(1, 1, 1, .4f),
                },
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