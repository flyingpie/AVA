//using AVA.Core.QueryExecutors;
//using AVA.Core.QueryExecutors.ListQuery;
//using FontAwesomeCS;
//using MUI;
//using MUI.DI;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace AVA.Plugins.ClipboardHistory
//{
//    [Help(Name = "Clipboard history", Description = "View and reactivate past clips", ExampleUsage = "cc <term?>", Icon = FAIcon.CopyRegular)]
//    public class ClipboardHistoryQueryExecutor : ListQueryExecutor
//    {
//        [Dependency] public ClipboardService ClipboardService { get; set; }
        
//        public override string Prefix => "cc ";

//        public override IEnumerable<IListQueryResult> GetQueryResults(string term) =>
//            ClipboardService
//            .History
//            .Select(h =>
//            {
//                var res = new ListQueryResult()
//                {
//                    Name = h.Timestamp.ToString("s"),
//                    Description = "",
//                    OnExecute = t => ClipboardService.Restore(h)
//                };

//                if (!string.IsNullOrEmpty(h.Text))
//                {
//                    res.Description = string.Join("", h.Text.Replace(Environment.NewLine, "").Take(40));
//                }

//                if (h.ImageThumbnail != null)
//                {
//                    res.Icon = ResourceManager.Instance.LoadImage($"cb_{h.Timestamp.ToString("s")}", h.ImageThumbnail);
//                }

//                return (IListQueryResult)res;
//            });
//    }
//}