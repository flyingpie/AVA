using System;
using System.Collections.Concurrent;

namespace MUI.Logging
{
	public static class Log
	{
		private static ConcurrentDictionary<string, ILog> _loggerCache = new ConcurrentDictionary<string, ILog>();

		public static Func<string, ILog> Factory { get; set; } = category => _loggerCache.GetOrAdd(category, c => new FileLogger(category));

		public static ILog Get(string category) => Factory(category);

		public static ILog Get(Type type) => Get(type.FullName);

		public static ILog Get<T>(T obj) => Get(typeof(T));

		public static ILog Get<T>() => Get(typeof(T));
	}
}