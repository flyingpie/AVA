using AVA.Core.QueryExecutors.ListQuery;
using AVA.Plugins.Time.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugins.Time
{
    public class TimeQueryExecutor : ListQueryExecutor
    {
        public static string DefaultTerm = "amsterdam";
        public static int MinPopulation = 500_000;

        public override string Prefix => "time";

        private ICollection<TimeZoneInfo> _dotNetTimeZones;
        private ICollection<CityNameTimeZone> _cities;

        public override void Initialize()
        {
            _dotNetTimeZones = TimeZoneInfo.GetSystemTimeZones();

            var gnCities = GNCity.LoadGNCities("AVA/Data/GeoNames/cities15000.txt".FromAppRoot(), MinPopulation).ToList();
            var cldrWindowsZones = CLDRWindowsZone.Load("AVA/Data/CLDR/windowsZones.json".FromAppRoot()).ToList();

            _cities = CityNameTimeZone.Load(gnCities, cldrWindowsZones, _dotNetTimeZones).ToList();
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Substring(Prefix.Length).Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(term)) term = DefaultTerm;

            return _cities
                .Where(c => c.CityNameLower.Contains(term))
                .OrderBy(c => !c.CityNameLower.StartsWith(term))
                .ThenByDescending(c => c.Population)
                .Take(25)
                .GroupBy(c => c.CityNameLower).Select(c => c.First())
                .Select(c =>
                {
                    var netTimeZone = _dotNetTimeZones.FirstOrDefault(t => t.Id == c.TimeZoneId);
                    var utcNow = DateTimeOffset.UtcNow;
                    var utcOfffset = netTimeZone.GetUtcOffset(utcNow);
                    var time = utcNow.Add(utcOfffset);
                    var isDst = netTimeZone.IsDaylightSavingTime(time);

                    var desc = isDst ? $"{netTimeZone.DaylightName} (DST)" : netTimeZone.StandardName;

                    return new TimeQueryResult()
                    {
                        Time = time,
                        Name = $"{c.CityName}",
                        Description = $"UTC+{netTimeZone.BaseUtcOffset.ToString("hh':'mm")} - {desc}"
                    };
                })
                .ToList()
            ;
        }
    }
}