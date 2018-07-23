using MLaunch.Core.QueryExecutors.ListQuery;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.Time
{
    public class TimeQueryExecutor : ListQueryExecutor
    {
        public override int Order => 0;

        public override string Prefix => "time";

        private TimeZoneInfo[] _timeZones;

        [RunAfterInject]
        private void Init()
        {
            var tz = TimeZoneInfo.GetSystemTimeZones().ToList();

            _timeZones = new[]
            {
                tz.FirstOrDefault(t => t.Id == "UTC"),
                tz.FirstOrDefault(t => t.Id == "Pacific Standard Time"),
                tz.FirstOrDefault(t => t.Id == "Eastern Standard Time"),
                tz.FirstOrDefault(t => t.Id == "Russian Standard Time"),
                tz.FirstOrDefault(t => t.Id == "Tokyo Standard Time"),
                tz.FirstOrDefault(t => t.Id == "AUS Eastern Standard Time")
            }.OrderBy(t => t.BaseUtcOffset).ToArray();
        }

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            var now = DateTimeOffset.UtcNow;

            return _timeZones
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