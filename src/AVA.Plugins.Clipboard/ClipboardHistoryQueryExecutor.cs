using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Clipboard
{
    [Help(Name = "Clipboard history", Description = "View and reactivate past clips", ExampleUsage = "cc <term?>", Icon = FAIcon.CopyRegular)]
    public class ClipboardHistoryQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ClipboardService ClipboardService { get; set; }

        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Prefix => "cc ";

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            var items = ClipboardService
                .History
                .Where(h => h.Name.ToLowerInvariant().Contains(term.ToLowerInvariant()))
                .Select(h =>
                {
                    var res = new ListQueryResult()
                    {
                        Name = h.Name,
                        Description = h.Timestamp.ToString("MM-dd HH:mm"),
                        Icon = h.Icon,
                        OnExecute = t => ClipboardService.Restore(h)
                    };

                    return (IListQueryResult)res;
                })
                .ToList();

            items.Add(new ListQueryResult()
            {
                Icon = ResourceManager.DefaultImage,
                Name = "Clear",
                OnExecute = qc => ClipboardService.Clear()
            });

            return items;
        }
    }
}