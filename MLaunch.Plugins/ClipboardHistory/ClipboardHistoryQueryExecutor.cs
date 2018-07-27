using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.ClipboardHistory
{
    public class ClipboardHistoryQueryExecutor : ListQueryExecutor
    {
        [Dependency] public ClipboardService ClipboardService { get; set; }

        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string Name => "Clipboard history";

        public override string Description => "View and reactivate past clips";

        public override string ExampleUsage => "cb <term?>";

        public override string Prefix => "cb ";

        public ClipboardHistoryQueryExecutor()
        {
            //ClipboardNotification.ClipboardUpdate += ClipboardNotification_ClipboardUpdate;
        }

        //private void ClipboardNotification_ClipboardUpdate(object sender, EventArgs e)
        //{
        //    Console.WriteLine($"Sup: ");
        //}

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return ClipboardService.History
                .OrderByDescending(h => h.Timestamp)
                .Select(h =>
                {
                    var res = new ListQueryResult()
                    {
                        Name = h.Timestamp.ToString("s"),
                        Description = "",
                        OnExecute = t => h.Restore()
                    };

                    if (h.ImageThumbnail != null)
                    {
                        res.Icon = ResourceManager.LoadImage($"cb_{h.Timestamp.ToString("s")}", h.ImageThumbnail);
                    }

                    return (IListQueryResult)res;
                })
                .ToList();

            //return new List<IListQueryResult>();
        }
    }
}