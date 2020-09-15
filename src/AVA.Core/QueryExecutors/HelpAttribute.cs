using FontAwesomeCS;
using System;

namespace AVA.Core.QueryExecutors
{
	public class HelpAttribute : Attribute
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string ExampleUsage { get; set; }

		public FAIcon Icon { get; set; } = FAIcon.None;
	}
}