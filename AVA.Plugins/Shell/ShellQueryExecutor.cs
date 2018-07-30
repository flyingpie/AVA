using AVA.Core;
using AVA.Core.QueryExecutors;
using ImGuiNET;
using MUI;
using MUI.DI;
using System.Diagnostics;

namespace AVA.Plugins.Shell
{
    [Service]
    public class ShellQueryExecutor : IQueryExecutor
    {
        [Dependency] public UIContext UIContext { get; set; }

        public string Name => "Shell";

        public string Description => "Execute shell commands";

        public string ExampleUsage => ">ipconfig";

        public int Order => 0;

        public bool TryHandle(QueryContext query)
        {
            if (query.IsEmpty) return false;

            return query.Text.StartsWith(">");
        }

        public bool TryExecute(QueryContext query)
        {
            var term = query.Text;
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

            if (Input.IsKeyDown(Keys.LeftControl))
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