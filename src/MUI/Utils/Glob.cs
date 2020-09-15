using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MUI.Utils
{
	public static class Glob
	{
		public static ICollection<string> Execute(string dir, string[] includes, string[] excludes)
		{
			var dirInfo = new DirectoryInfo(dir);
			var dirInfoWrapper = new DirectoryInfoWrapper(dirInfo);

			var matcher = new Microsoft.Extensions.FileSystemGlobbing.Matcher();

			foreach (var incl in includes ?? new string[0]) matcher.AddInclude(incl);
			foreach (var excl in excludes ?? new string[0]) matcher.AddExclude(excl);

			var result = matcher.Execute(dirInfoWrapper);

			return result.Files
				.Select(f => Path.Combine(dir, f.Path))
				.ToList()
			;
		}
	}
}