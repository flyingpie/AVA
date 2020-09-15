using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpNodaTime.TimeConverterRules
{
	public class _1630GmtTimeConverterRule : BaseTimeConverterRule
	{
		public override string Name => "1630 gmt";

		public override Regex Regex => new Regex("^(?<hoursMinutes>[0-9]{3,4}) ?(?<timezone>[A-z]{1,4})$");

		protected override bool TryParseUtc(TimeConverterContext context, Match match, out TimeConverterResult result)
		{
			result = null;

			var hoursMinutes = match.Groups["hoursMinutes"].Value.PadLeft(4, '0');
			var timezone = match.Groups["timezone"].Value;

			var tz = context.TimeZones.FirstOrDefault(t => t.IsMatch(timezone));

			if (tz != null && DateTime.TryParseExact($"{hoursMinutes}", $"HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
			{
				var offset = tz.TimeZoneInfo.GetUtcOffset(time);

				result = new TimeConverterResult()
				{
					Expression = context.Expression,
					ResultUtc = new DateTimeOffset(time.Subtract(offset), TimeSpan.Zero),
					Rule = this,
					TimeZone = tz
				};

				return true;
			}

			return false;
		}
	}
}