using System;

namespace MUI.Logging
{
    public static class Log
    {
        public static Func<string, ILog> Factory { get; set; } = category => new FileLogger(category); //new ConsoleLogger(category);

        public static ILog Get(string category) => Factory(category);

        public static ILog Get(Type type) => Get(type.FullName);

        public static ILog Get<T>(T obj) => Get(typeof(T));

        public static ILog Get<T>() => Get(typeof(T));
    }
}