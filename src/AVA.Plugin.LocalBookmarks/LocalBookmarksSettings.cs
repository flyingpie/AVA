using AVA.Core.Settings;

namespace AVA.Plugin.LocalBookmarks
{
	[Section("LocalBookmarks")]
	public class LocalBookmarksSettings : Settings
	{
		public string PathToBookmarks { get; set; }
	}
}