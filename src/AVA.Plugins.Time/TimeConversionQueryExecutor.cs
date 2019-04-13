using AVA.Core;
using AVA.Core.QueryExecutors;
using ExpNodaTime;
using FontAwesomeCS;
using ImGuiNET;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Time
{
    [Service, Help(Name = "Time", Description = "Lists times around the world", ExampleUsage = "time tokyo", Icon = FAIcon.ClockRegular)]
    public class TimeConversionQueryExecutor : IQueryExecutor
    {
        public int Order => 0;

        private TimeConverter _converter;

        private List<TimeConverterResult> _results;

        public TimeConversionQueryExecutor()
        {
            _converter = new TimeConverter();
        }

        public bool TryHandle(QueryContext query)
        {
            try
            {
                _results = _converter.Parse(query.Text).ToList();

                return _results.Any();
            }
            catch
            {
                return false;
            }
        }

        public bool TryExecute(QueryContext query)
        {
            return true;
        }

        public void Draw()
        {
            foreach (var result in _results)
            {
                var local = result.ResultUtc.ToLocalTime();
                var isDst = result.TimeZone.TimeZoneInfo.IsDaylightSavingTime(local);
                var tzi = result.TimeZone.TimeZoneInfo;

                ImGui.PushFont(Fonts.Regular32);
                ImGui.Text($"{local.ToString("HH:mm")}");
                ImGui.PopFont();

                ImGui.PushFont(Fonts.Regular24);
                ImGui.Text($"{local.ToString("yyyy-dd-MM")}");
                ImGui.PopFont();

                ImGui.PushFont(Fonts.Regular16);
                ImGui.Text($"{(isDst ? tzi.DaylightName : tzi.StandardName)}");
                ImGui.PopFont();
            }
        }
    }
}