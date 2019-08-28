using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpNodaTime
{
    public interface ITimeZoneFactory
    {
        ICollection<TimeZone> GetTimeZones();
    }

    public class TimeZoneFactory : ITimeZoneFactory
    {
        private string _languageCode;
        private ICollection<TimeZone> _timeZones;

        public TimeZoneFactory(string languageCode)
        {
            _languageCode = languageCode ?? throw new ArgumentNullException(nameof(languageCode));
        }

        public ICollection<TimeZone> GetTimeZones()
        {
            if (_timeZones == null)
            {
                _timeZones = TimeZoneInfo
                    .GetSystemTimeZones()
                    .OrderBy(tz => tz.Id)
                    .Select(tzi => TimeZone.FromTimeZoneInfo(tzi, _languageCode))
                    .ToList();
            }

            return _timeZones;
        }
    }
}