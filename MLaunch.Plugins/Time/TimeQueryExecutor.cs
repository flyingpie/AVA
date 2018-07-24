using MLaunch.Core.QueryExecutors.ListQuery;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.Time
{
    public class TimeQueryExecutor : ListQueryExecutor
    {
        public override string Name => "Time";

        public override string Description => "Lists times around the world";

        public override string ExampleUsage => Prefix;

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

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length).Trim();

            var now = DateTimeOffset.UtcNow;

            var source = _defaultTimeZones;

            if (!string.IsNullOrWhiteSpace(term))
            {
                source = _allTimeZones.Where(tz =>
                        tz.Id.ContainsCaseInsensitive(term)
                     || tz.DaylightName.ContainsCaseInsensitive(term)
                     || tz.DisplayName.ContainsCaseInsensitive(term)
                     || tz.StandardName.ContainsCaseInsensitive(term));
            }

            return source
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
        }
    }
}