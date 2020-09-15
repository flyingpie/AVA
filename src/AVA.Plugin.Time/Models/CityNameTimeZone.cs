using System;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Time.Models
{
	public class CityNameTimeZone
	{
		public string CityName { get; set; }

		public string CityNameLower { get; set; }

		public int Population { get; set; }

		public string TimeZoneId { get; set; }

		public static IEnumerable<CityNameTimeZone> Load(List<GNCity> gnCities, ICollection<CLDRWindowsZone> cldrWindowsZones, ICollection<TimeZoneInfo> dotNetTimeZones)
		{
			foreach (var gnName in gnCities)
			{
				var cldrWindowsZone = cldrWindowsZones.FirstOrDefault(t => t.Types.Any(tt => tt == gnName.TimeZoneId));
				if (cldrWindowsZone == null) continue;

				var dotNetTimeZone = dotNetTimeZones.FirstOrDefault(t => t.Id == cldrWindowsZone.Other);
				if (dotNetTimeZone == null) continue;

				var cityNames = new List<string>(gnName.AlternateNames);
				cityNames.Insert(0, gnName.Name);

				foreach (var cityName in cityNames)
				{
					yield return new CityNameTimeZone()
					{
						CityName = cityName,
						CityNameLower = cityName.ToLowerInvariant(),
						Population = gnName.Population,
						TimeZoneId = dotNetTimeZone.Id
					};
				}
			}
		}
	}
}