using ExpNodaTime.TimeConverterRules;
using System;

namespace ExpNodaTime
{
	public class TimeConverterResult
	{
		public string Expression { get; set; }

		public ITimeConverterRule Rule { get; set; }

		public DateTimeOffset ResultUtc { get; set; }

		public TimeZone TimeZone { get; set; }

		public bool Success { get; set; }
	}
}