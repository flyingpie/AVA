using AVA.Core.Settings;
using System.Collections.Generic;

namespace AVA.Plugin.Macros
{
	[Section("Macros")]
	public class MacroSettings : Settings
	{
		public List<Macro> Macros { get; set; }
	}
}