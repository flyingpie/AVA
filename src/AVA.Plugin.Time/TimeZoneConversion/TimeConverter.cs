using ExpNodaTime.TimeConverterRules;
using System;
using System.Collections.Generic;

namespace ExpNodaTime
{
    public class TimeConverter
    {
        private Func<DateTimeOffset> _now;

        private ITimeZoneFactory _timeZoneFactory;

        private List<ITimeConverterRule> _rules = new List<ITimeConverterRule>()
        {
            new _12PmEstTimeConverterRule(),
            new _1630GmtTimeConverterRule()
        };

        public TimeConverter() : this(new TimeZoneFactory("en"), () => DateTimeOffset.Now)
        { }

        public TimeConverter(ITimeZoneFactory timeZoneFactory, Func<DateTimeOffset> now)
        {
            _now = now ?? throw new ArgumentNullException(nameof(now));
            _timeZoneFactory = timeZoneFactory ?? throw new ArgumentNullException(nameof(timeZoneFactory));
        }

        public IEnumerable<TimeConverterResult> Parse(string expression)
        {
            var context = new TimeConverterContext()
            {
                Expression = expression,
                Now = _now(),
                TimeZones = _timeZoneFactory.GetTimeZones()
            };

            foreach (var rule in _rules)
            {
                if (rule.TryParse(context, out var result)) yield return result;
            }
        }
    }
}