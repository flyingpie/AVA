using ImGuiNET;
using MLaunch.Core;
using MLaunch.Core.QueryExecutors.CommandQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace MLaunch.Plugins.WebQuery
{
    public class WebQueryExecutor : CommandQueryExecutor
    {
        [Dependency] public UIContext UI { get; set; }

        public override string Description => "Search sites and open urls";

        public override string Name => "Web queries";

        public override string ExampleUsage => "ddg <Duck Duck Go Search Term>";

        public override string[] CommandPrefixes => _commands.Select(c => c.Prefix).ToArray();

        private List<Command> _commands = new List<Command>()
        {
            new Command()
            {
                Prefix = "ddg ",
                Execute = term => Process.Start($"https://duckduckgo.com/?q={term}").Dispose(),
                Description = "Duck Duck Go"
            },
            new Command()
            {
                Prefix = "gh ",
                Execute = term => Process.Start($"https://github.com/search?q={term}").Dispose(),
                Description = "GitHub"
            },
            new Command()
            {
                Prefix = "nuget ",
                Execute = term => Process.Start($"https://www.nuget.org/packages?q={term}").Dispose(),
                Description = "NuGet"
            },
            new Command()
            {
                Prefix = "wiki ",
                Execute = term => Process.Start($"https://en.wikipedia.org/w/index.php?search={term}").Dispose(),
                Description = "Wikipedia"
            }
        };

        private Command _command;
        private string _term;

        public override bool TryHandle(string term)
        {
            var terml = term.ToLowerInvariant();

            _command = _commands.FirstOrDefault(c => terml.StartsWith(c.Prefix));

            if (_command != null)
            {
                _term = term.Substring(_command.Prefix.Length);
            }

            return _command != null;
        }

        public override bool TryExecute(QueryContext query)
        {
            var term = query.Query;
            var terml = term.ToLowerInvariant();

            foreach (var command in _commands)
            {
                if (terml.StartsWith(command.Prefix))
                {
                    _term = term.Substring(command.Prefix.Length);
                    _term = WebUtility.UrlEncode(_term);

                    command.Execute(_term);

                    return true;
                }
            }

            return false;
        }

        public override void Draw()
        {
            // TODO: Icon
            ImGui.Text(_command?.Description ?? "");
            ImGui.PushFont(UI.Font32);
            ImGui.Text(_term);
            ImGui.PopFont();
        }

        private class Command
        {
            public string Prefix { get; set; }

            public string Description { get; set; }

            public Action<string> Execute { get; set; }
        }
    }
}