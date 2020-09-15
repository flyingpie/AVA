using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpNodaTime.TimeConverterRules
{
	public class _12PmEstTimeConverterRule : BaseTimeConverterRule
	{
		public override string Name => "12pm est";

		public override Regex Regex => new Regex("^(?<hours>[0-9]{1,2})(?<ampm>[A-z]{2}) ?(?<timezone>[A-z]{1,4})$");

		protected override bool TryParseUtc(TimeConverterContext context, Match match, out TimeConverterResult result)
		{
			result = null;

			var hours = match.Groups["hours"].Value;
			var ampm = match.Groups["ampm"].Value;
			var timezone = match.Groups["timezone"].Value;

			var tzz = context.TimeZones.FirstOrDefault(tz => tz.IsMatch(timezone));

			if (tzz != null && DateTime.TryParseExact($"{hours}{ampm}", $"htt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
			{
				var offset = tzz.TimeZoneInfo.GetUtcOffset(time);

				result = new TimeConverterResult()
				{
					Expression = context.Expression,
					ResultUtc = new DateTimeOffset(time.Subtract(offset), TimeSpan.Zero),
					Rule = this,
					TimeZone = tzz
				};

				return true;
			}

			return false;
		}
	}
}