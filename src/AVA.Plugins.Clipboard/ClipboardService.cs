using AVA.Plugins.Clipboard.ClipboardDataTypes;
using AVA.Plugins.Clipboard.Win32;
using MUI.DI;
using MUI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Clipboard
{
    [Service]
    public class ClipboardService
    {
        public List<ClipboardData> History { get; }

        private List<ClipboardDataParser> _parsers = new List<ClipboardDataParser>()
        {
            //new FileDropClipboardDataParser(),
            //new ImageClipboardDataParser(),
            new TextClipboardDataParser()
        };

        private ILog _log = Log.Get<ClipboardService>();

        public ClipboardService()
        {
            History = new List<ClipboardData>();

            ClipboardNotification.ClipboardUpdate += (s, a) =>
            {
                try
                {
                    var dataObject = System.Windows.Forms.Clipboard.GetDataObject();

                    foreach (var parser in _parsers)
                    {
                        if (parser.TryParse(dataObject, out var data))
                        {
                            History.Insert(0, data);
                            break;
                        }
                    }

                    RemoveDupes();

                    while (History.Count > 10) History.RemoveAt(History.Count - 1);
                }
                catch (Exception ex)
                {
                    _log.Error($"Error while processing clipboard entry: '{ex.Message}'.", ex);
                }
            };
        }

        public void Clear() => History.Clear();

        public void Restore(ClipboardData data)
        {
            //data.Restore();

            data.GetFormatAndData(out var format, out var d);

            System.Windows.Forms.Clipboard.SetData(format, d);
        }

        public void RemoveDupes()
        {
            History
                .GroupBy(h => h.Hash)
                .Where(g => g.Count() > 1)
                .ToList()
                .ForEach(h =>
                {
                    _log.Info($"Removing dupe '{h.ToString()}'");

                    History.Remove(h.Last());
                });
        }
    }
}