﻿using AVA.Core;
using AVA.Core.QueryExecutors;
using FontAwesomeCS;
using ImGuiNET;
using MUI.DI;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AVA.Plugin.Uri
{
	// TODO: Rename Service to something like Export or Injectable or Dependable
	[Service, Help(Name = "Uri", Description = "Opens uri-like queries", ExampleUsage = "reddit.com", Icon = FAIcon.GlobeAmericasSolid)]
	public class UriQueryExecutor : IQueryExecutor
	{
		public int Order => 0;

		private string _url;

		public void Draw()
		{
			ImGui.Text($"Open url '{_url}'");
		}

		public bool TryExecute(QueryContext query)
		{
			Process.Start(new ProcessStartInfo()
			{
				FileName = _url,
				UseShellExecute = true,
			});

			return true;
		}

		public bool TryHandle(QueryContext query)
		{
			var isMatch = reg.IsMatch(query.Text);

			if (isMatch)
			{
				_url = FormatUrl(query.Text);
			}

			return isMatch;
		}

		private string FormatUrl(string url)
		{
			if (!url.ToLower().StartsWith("http")) url = "http://" + url;

			return url;
		}

		private const string urlPattern = "^" +
			// protocol identifier
			"(?:(?:https?|ftp)://|)" +
			// user:pass authentication
			"(?:\\S+(?::\\S*)?@)?" +
			"(?:" +
			// IP address exclusion
			// private & local networks
			"(?!(?:10|127)(?:\\.\\d{1,3}){3})" +
			"(?!(?:169\\.254|192\\.168)(?:\\.\\d{1,3}){2})" +
			"(?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})" +
			// IP address dotted notation octets
			// excludes loopback network 0.0.0.0
			// excludes reserved space >= 224.0.0.0
			// excludes network & broacast addresses
			// (first & last IP address of each class)
			"(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])" +
			"(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}" +
			"(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))" +
			"|" +
			// host name
			"(?:(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)" +
			// domain name
			"(?:\\.(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)*" +
			// TLD identifier
			"(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))" +
			")" +
			// port number
			"(?::\\d{2,5})?" +
			// resource path
			"(?:/\\S*)?" +
			"$";

		private Regex reg = new Regex(urlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
	}
}