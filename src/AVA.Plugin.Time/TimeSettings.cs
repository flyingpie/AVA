using AVA.Core.Settings;

namespace AVA.Plugin.Time
{
	[Section("Time")]
	public class TimeSettings : Settings
	{
		public string[] DefaultCities { get; set; } = new[] { "tokyo", "sydney", "moscow", "new york" };

		public int MinPopulation { get; set; } = 500_000;
	}
}