using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MUI.Logging
{
    public class FileLogger : ILog
    {
        private static ConcurrentQueue<string> _queue;

        private static CancellationTokenSource _cts;
        private static Task _worker;

        static FileLogger()
        {
            _queue = new ConcurrentQueue<string>();

            _cts = new CancellationTokenSource();
            _worker = Task.Run(async () =>
            {
                Directory.CreateDirectory("logs");

                while (!_cts.Token.IsCancellationRequested)
                {
                    string line = null;

                    bool gotLine = false;

                    do
                    {
                        if ((gotLine = _queue.TryDequeue(out line)))
                        {
                            File.AppendAllLines(@"logs/log.txt", new[] { line });
                        }
                    }
                    while (gotLine);

                    await Task.Delay(1000);
                }
            });
        }

        private string _category;

        public FileLogger(string category)
        {
            _category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public void Error(string message) => Write("ERROR", message);

        public void Info(string message) => Write("INFO", message);

        public void Warning(string message) => Write("WARN", message);

        private void Write(string type, string message)
        {
            _queue.Enqueue($"{DateTimeOffset.Now.ToString("s")} {_category} [{type}] {message}");
        }
    }
}