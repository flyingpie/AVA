using System;
using System.Collections.Generic;

namespace ExpNodaTime
{
	public class TimeConverterContext
	{
		public string Expression { get; set; }

		public DateTimeOffset Now { get; set; }

		public ICollection<TimeZone> TimeZones { get; set; }
	}
}