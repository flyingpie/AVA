using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using AVA.Plugin.Time.Models;
using FontAwesomeCS;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Time
{
	[Help(Name = "Time", Description = "Lists times around the world", ExampleUsage = "time tokyo", Icon = FAIcon.ClockRegular)]
	public class TimeQueryExecutor : ListQueryExecutor
	{
		[Dependency] public TimeSettings Settings { get; set; }

		public override string Prefix => "time";

		private ICollection<TimeZoneInfo> _dotNetTimeZones;
		private ICollection<CityNameTimeZone> _cities;

		public override void Initialize()
		{
			_dotNetTimeZones = TimeZoneInfo.GetSystemTimeZones();

			var gnCities = GNCity.LoadGNCities(Settings.MinPopulation).ToList();
			var cldrWindowsZones = CLDRWindowsZone.Load().ToList();

			_cities = CityNameTimeZone.Load(gnCities, cldrWindowsZones, _dotNetTimeZones).ToList();
		}

		public override IEnumerable<IListQueryResult> GetQueryResults(string term)
		{
			term = term.Substring(Prefix.Length).Trim().ToLowerInvariant();

			var filter = string.IsNullOrWhiteSpace(term) ? _cities.Default(Settings.DefaultCities) : _cities.Search(term);

			return filter
				.Select(c =>
				{
					var netTimeZone = _dotNetTimeZones.FirstOrDefault(t => t.Id == c.TimeZoneId);
					var utcNow = DateTimeOffset.UtcNow;
					var utcOfffset = netTimeZone.GetUtcOffset(utcNow);
					var time = utcNow.Add(utcOfffset);
					var isDst = netTimeZone.IsDaylightSavingTime(time);

					var mod = netTimeZone.BaseUtcOffset >= TimeSpan.Zero ? "+" : "-";
					var offsetStr = $"UTC{mod}{utcOfffset.ToString("hh':'mm")}";
					var desc = isDst ? $"{netTimeZone.DaylightName} (DST)" : netTimeZone.StandardName;

					return new TimeQueryResult()
					{
						Time = time,
						Name = c.CityName,
						Description = $"{offsetStr} {desc}"
					};
				})
				.ToList()
			;
		}
	}

	public static class Extensions
	{
		public static IEnumerable<CityNameTimeZone> Default(this IEnumerable<CityNameTimeZone> source, string[] defaultCitites) => source
			.Where(c => defaultCitites.Any(dc => c.CityNameLower.Equals(dc)))
			.GroupBy(c => c.CityNameLower).Select(c => c.First())
		;

		public static IEnumerable<CityNameTimeZone> Search(this IEnumerable<CityNameTimeZone> source, string term) => source
			.Where(c => c.CityNameLower.Contains(term))
			.OrderByDescending(c => c.CityNameLower == term)
			.ThenByDescending(c => c.CityNameLower.StartsWith(term))
			.ThenByDescending(c => c.Population)
			.Take(25)
			.GroupBy(c => c.CityNameLower).Select(c => c.First())
			.Take(4)
		;
	}
}