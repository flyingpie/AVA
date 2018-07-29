using System;

namespace MUI.Logging
{
    public class ConsoleLogger : ILog
    {
        private string _category;

        public ConsoleLogger(string category)
        {
            _category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public void Error(string message) => Write(ConsoleColor.Red, "ERROR", message);

        public void Info(string message) => Write(ConsoleColor.White, "INFO", message);

        public void Warning(string message) => Write(ConsoleColor.Yellow, "WARN", message);

        private void Write(ConsoleColor color, string type, string message)
        {
            var fg = Console.ForegroundColor;

            //Console.WriteLine($"{DateTimeOffset.Now.ToString("s")} {_category} [{type}] {message}");

            Console.Write(DateTimeOffset.Now.ToString("s"));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ");
            Console.Write(_category);

            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.Write(type);

            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
            Console.Write(Environment.NewLine);

            Console.ForegroundColor = fg;
        }
    }
}