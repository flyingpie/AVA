using ImGuiNET;
using MLaunch.Core;
using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using MUI.Extensions;
using System.Diagnostics;

namespace MLaunch.Plugins.Shell
{
    [Service]
    public class ShellQueryExecutor : IQueryExecutor
    {
        [Dependency] public UIContext UIContext { get; set; }

        public string Name => "Shell";

        public string Description => "Execute shell commands";

        public string ExampleUsage => ">ipconfig";

        public int Order => 0;

        public bool TryHandle(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return false;

            return term.StartsWith(">");
        }

        public bool TryExecute(QueryContext query)
        {
            var term = query.Query;
            if (term.Length <= 1) return false;

            var cmd = term.Substring(1);
            var args = "";

            var space = cmd.IndexOf(' ');
            if (space > 0)
            {
                args = cmd.Substring(space).Trim();
                cmd = cmd.Substring(0, space).Trim();
            }

            var startInfo = new ProcessStartInfo()
            {
                FileName = cmd,
                Arguments = args
            };

            if (UIContext.Input.IsKeyDown(Veldrid.Key.Enter, Veldrid.ModifierKeys.Control))
            {
                startInfo.Verb = "runas";
            }

            Process.Start(startInfo);

            return true;
        }

        public void Draw()
        {
            ImGui.Text("Shell command");
        }
    }
}