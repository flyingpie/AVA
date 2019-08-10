using System;
using TimeZoneNames;

namespace ExpNodaTime
{
    public class TimeZone
    {
        public string Id => TimeZoneInfo.Id;

        public TimeZoneInfo TimeZoneInfo { get; set; }

        public TimeZoneValues TimeZoneValues { get; set; }

        public bool IsMatch(string expression) =>
            (TimeZoneValues.Daylight?.Equals(expression, StringComparison.OrdinalIgnoreCase) ?? false)
            ||
            (TimeZoneValues.Generic?.Equals(expression, StringComparison.OrdinalIgnoreCase) ?? false)
            ||
            (TimeZoneValues.Standard?.Equals(expression, StringComparison.OrdinalIgnoreCase) ?? false)
        ;

        public static TimeZone FromTimeZoneInfo(TimeZoneInfo tzi, string languageCode)
        {
            return new TimeZone()
            {
                TimeZoneInfo = tzi,
                TimeZoneValues = TZNames.GetAbbreviationsForTimeZone(tzi.Id, languageCode)
            };
        }
    }
}