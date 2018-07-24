using ImGuiNET;
using MLaunch.Core.QueryExecutors.CommandQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MLaunch.Plugins.WebQuery
{
    public class WebQueryExecutor : CommandQueryExecutor
    {
        public override string Description => "Search sites and open urls";

        public override string Name => "Web queries";

        public override string ExampleUsage => "ddg <Duck Duck Go Search Term>";

        public override string[] CommandPrefixes => _commands.Select(c => c.Prefix).ToArray();

        private List<Command> _commands = new List<Command>()
        {
            new Command()
            {
                Prefix = "ddg ",
                Execute = term => Process.Start($"https://duckduckgo.com/?q={term}"),
                Description = "Duck Duck Go"
            },
            new Command()
            {
                Prefix = "gh ",
                Execute = term => Process.Start($"https://github.com/search?q={term}"),
                Description = "GitHub"
            },
            new Command()
            {
                Prefix = "wiki ",
                Execute = term => Process.Start($"https://en.wikipedia.org/w/index.php?search={term}"),
                Description = "Wikipedia"
            }
        };

        private Command _command;

        public override bool TryHandle(string term)
        {
            var terml = term.ToLowerInvariant();

            _command = _commands.FirstOrDefault(c => terml.StartsWith(c.Prefix));

            return _command != null;
        }

        public override bool TryExecute(string term)
        {
            var terml = term.ToLowerInvariant();

            foreach (var command in _commands)
            {
                if (terml.StartsWith(command.Prefix))
                {
                    term = term.Substring(command.Prefix.Length);

                    command.Execute(term);

                    return true;
                }
            }

            return false;
        }

        public override void Draw()
        {
            ImGui.Text(_command?.Description ?? "");
        }

        private class Command
        {
            public string Prefix { get; set; }

            public string Description { get; set; }

            public Action<string> Execute { get; set; }
        }
    }
}