using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.CommandQuery;
using FontAwesomeCS;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.ImGuiControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;

namespace AVA.Plugins.WebQuery
{
    [Help(Name = "Web queries", Description = "Search sites and open urls", ExampleUsage = "ddg <Duck Duck Go Search Term>", Icon = FAIcon.RedditBrands)]
    public class WebQueryExecutor : CommandQueryExecutor
    {
        public static int IconSize = 140;

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
                    Prefix = "ddg",
                    Icon = "WebQuery/Resources/Images/DuckDuckGo.png",
                    Pattern = "https://duckduckgo.com/?q={term}",
                    Description = "Duck Duck Go"
                },
                new Command()
                {
                    Prefix = "gh",
                    Icon = "WebQuery/Resources/Images/GitHub.png",
                    Pattern = "https://github.com/search?q={term}",
                    Description = "GitHub"
                },
                new Command()
                {
                    Prefix = "imdb",
                    Icon = FAIcon.ImdbBrands,
                    Pattern = "https://www.imdb.com/find?ref_=nv_sr_fn&q={term}&s=all",
                    Description = "IMDb"
                },
                new Command()
                {
                    Prefix = "nuget",
                    Icon = "WebQuery/Resources/Images/NuGet.png",
                    Pattern = "https://www.nuget.org/packages?q={term}",
                    Description = "NuGet"
                },
                new Command()
                {
                    Prefix = "reddit",
                    Icon = FAIcon.RedditBrands,
                    Pattern = "https://www.reddit.com/search?q={term}",
                    Description = "Reddit"
                },
                new Command()
                {
                    Prefix = "twitter",
                    Icon = FAIcon.TwitterBrands,
                    Pattern = "https://twitter.com/search?f=users&vertical=news&q={term}&src=typd",
                    Description = "Twitter"
                },
                new Command()
                {
                    Prefix = "wiki",
                    Icon = "WebQuery/Resources/Images/Wikipedia.png",
                    Pattern = "https://en.wikipedia.org/w/index.php?search={term}",
                    Description = "Wikipedia"
                }
            };
        }

        public override bool TryHandle(QueryContext query)
        {
            _command = _commands.FirstOrDefault(c => query.HasPrefix($"{c.Prefix} "));
            _term = query.Arg;

            return _command != null;
        }

        public override bool TryExecute(QueryContext query) =>
            _commands
            .FirstOrDefault(c => query.HasPrefix(c.Prefix))
            ?.Execute(query.Arg)
            ?? false;

        public override void Draw()
        {
            ImGui.Text(_command?.Description ?? "");
            ImGui.PushFont(Fonts.Regular32);
            ImGui.Text(_term);
            ImGui.PopFont();

            if (_command.Icon != null)
            {
                _command.Icon.Size = new Vector2(ImGui.GetContentRegionAvailableWidth(), IconSize);
                _command.Icon.Tint = new Vector4(Vector3.Zero, .25f);

                _command.Icon.Draw();
            }
        }

        private class Command
        {
            public ImageBox Icon { get; set; }

            public string Prefix { get; set; }

            public string Description { get; set; }

            public string Pattern { get; set; }

            public Func<string, bool> Execute { get; set; }

            public Command()
            {
                Execute = term =>
                {
                    Process.Start(Pattern.Replace("{term}", WebUtility.UrlEncode(term))).Dispose();

                    return true;
                };
            }
        }
    }
}