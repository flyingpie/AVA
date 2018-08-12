using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Time
{
    [Help(Name = "Time", Description = "Lists times around the world", ExampleUsage = "time", Icon = FAIcon.ClockRegular)]
    public class TimeQueryExecutor : ListQueryExecutor
    {
        public override int Order => 0;

        public override string Prefix => "time";

        private IEnumerable<TimeZoneInfo> _allTimeZones;

        private IEnumerable<TimeZoneInfo> _defaultTimeZones;

        [RunAfterInject]
        private void Init()
        {
            _allTimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();

            _defaultTimeZones = new[]
            {
                _allTimeZones.FirstOrDefault(t => t.Id == "UTC"),
                _allTimeZones.FirstOrDefault(t => t.Id == "Pacific Standard Time"),
                _allTimeZones.FirstOrDefault(t => t.Id == "Eastern Standard Time"),
                _allTimeZones.FirstOrDefault(t => t.Id == "Russian Standard Time"),
                _allTimeZones.FirstOrDefault(t => t.Id == "Tokyo Standard Time"),
                _allTimeZones.FirstOrDefault(t => t.Id == "AUS Eastern Standard Time")
            }.OrderBy(t => t.BaseUtcOffset).ToList();
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            var terml = term.Substring(Prefix.Length).Trim();

            var now = DateTimeOffset.UtcNow;

            var source = _defaultTimeZones;

            if (!string.IsNullOrWhiteSpace(terml))
            {
                source = _allTimeZones.Where(tz =>
                        tz.Id.ContainsCaseInsensitive(terml)
                     || tz.DaylightName.ContainsCaseInsensitive(terml)
                     || tz.DisplayName.ContainsCaseInsensitive(terml)
                     || tz.StandardName.ContainsCaseInsensitive(terml));
            }

            var results = source
                .Take(6)
                .Select(t =>
                {
                    var dateTime = now.Add(t.BaseUtcOffset);
                    var isDst = t.IsDaylightSavingTime(dateTime);
                    var name = isDst ? t.DaylightName : t.StandardName;

                    if (isDst) name += " (DST)";

                    return (IListQueryResult)new TimeListQueryResult()
                    {
                        Name = name,
                        DateTime = dateTime,
                        IsDst = isDst
                    };
                })
                .ToList();

            if (!results.Any()) return new[] { new ListQueryResult() { Name = $"No timezone found matching term '{terml}'", Description = "" } };

            return results;
        }
    }
}