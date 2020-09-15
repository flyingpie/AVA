using System;

namespace AVA.Plugin.LocalBookmarks
{
	public class Bookmark
	{
		public bool IsActive { get; set; }

		public string Category { get; set; }

		public string CategoryLower { get; set; }

		public string Name { get; set; }

		public string NameLower { get; set; }

		private string _url;

		public string Url
		{
			get => _url;
			set
			{
				_url = value;

				try
				{
					var uri = new Uri(value);
					var fav = $"{uri.Scheme}://{uri.Host}/favicon.ico";

					FaviconUrl = fav;
				}
				catch { }
			}
		}

		public string FaviconUrl { get; set; }
	}
}