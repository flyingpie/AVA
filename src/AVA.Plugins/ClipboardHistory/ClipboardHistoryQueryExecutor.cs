﻿using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.ClipboardHistory
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
                .Select(h =>
                {
                    var res = new ListQueryResult()
                    {
                        Name = h.FileName ?? h.Timestamp.ToString("s"),
                        Description = "",
                        OnExecute = t => ClipboardService.Restore(h)
                    };

                    if (!string.IsNullOrEmpty(h.Text))
                    {
                        res.Description = string.Join("", h.Text.Replace(Environment.NewLine, "").Take(40));
                    }

                    //if (h.ImageThumbnail != null)
                    if(h.Icon != null)
                    {
                        //res.Icon = ResourceManager.Instance.LoadImage($"cb_{h.Timestamp.ToString("s")}", h.ImageThumbnail);
                        res.Icon = h.Icon;
                    }

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