using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.CommandQuery;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;

namespace AVA.Plugins.WebQuery
{
    [Help(Name = "Web queries", Description = "Search sites and open urls", ExampleUsage = "ddg <Duck Duck Go Search Term>")]
    public class WebQueryExecutor : CommandQueryExecutor
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public static int IconWidth = 140;

        public override string[] CommandPrefixes => _commands.Select(c => c.Prefix).ToArray();

        private List<Command> _commands;

        private Command _command;
        private string _term;

        [RunAfterInject]
        public void Init()
        {
            _commands = new List<Command>()
            {
                new Command()
                {
                    Prefix = "ddg ",
                    Icon = () => ResourceManager.LoadImage("WebQuery/Resources/Images/DuckDuckGo.png"),
                    Execute = term => Process.Start($"https://duckduckgo.com/?q={term}").Dispose(),
                    Description = "Duck Duck Go"
                },
                new Command()
                {
                    Prefix = "gh ",
                    Icon = () => ResourceManager.LoadImage("WebQuery/Resources/Images/GitHub.png"),
                    Execute = term => Process.Start($"https://github.com/search?q={term}").Dispose(),
                    Description = "GitHub"
                },
                new Command()
                {
                    Prefix = "nuget ",
                    Icon = () => ResourceManager.LoadImage("WebQuery/Resources/Images/NuGet.png"),
                    Execute = term => Process.Start($"https://www.nuget.org/packages?q={term}").Dispose(),
                    Description = "NuGet"
                },
                new Command()
                {
                    Prefix = "wiki ",
                    Icon = () => ResourceManager.LoadImage("WebQuery/Resources/Images/Wikipedia.png"),
                    Execute = term => Process.Start($"https://en.wikipedia.org/w/index.php?search={term}").Dispose(),
                    Description = "Wikipedia"
                }
            };
        }

        public override bool TryHandle(QueryContext query)
        {
            var terml = query.Text.ToLowerInvariant();

            _command = _commands.FirstOrDefault(c => terml.StartsWith(c.Prefix));

            if (_command != null)
            {
                _term = query.Text.Substring(_command.Prefix.Length);
            }

            return _command != null;
        }

        public override bool TryExecute(QueryContext query)
        {
            var term = query.Text;
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
            ImGui.Text(_command?.Description ?? "");
            ImGui.PushFont(Fonts.Regular32);
            ImGui.Text(_term);
            ImGui.PopFont();

            if (_command?.GetIcon() != null)
            {
                ImGui.SetCursorScreenPos(new Vector2(ImGui.GetContentRegionAvailableWidth() / 2f - IconWidth / 2, ImGui.GetCursorScreenPos().Y));

                _command.GetIcon().Draw(new Vector2(IconWidth, IconWidth * _command.GetIcon().Ratio), new Vector4(.5f), Vector4.Zero);
            }
        }

        private class Command
        {
            private Image _icon;

            public string Prefix { get; set; }

            public Func<Image> Icon { get; set; }

            public string Description { get; set; }

            public Action<string> Execute { get; set; }

            public Image GetIcon()
            {
                if (_icon == null)
                {
                    _icon = Icon?.Invoke();
                }

                return _icon;
            }
        }
    }
}