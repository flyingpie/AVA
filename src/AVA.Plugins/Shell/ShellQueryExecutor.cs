using AVA.Core;
using AVA.Core.QueryExecutors;
using ImGuiNET;
using MUI;
using MUI.DI;
using System.Diagnostics;

namespace AVA.Plugins.Shell
{
    [Service, Help(Name = "Shell", Description = "Execute shell commands", ExampleUsage = ">ipconfig")]
    public class ShellQueryExecutor : IQueryExecutor
    {
        public int Order => 0;

        public bool TryHandle(QueryContext query) => query.Text?.StartsWith(">") ?? false;

        public bool TryExecute(QueryContext query)
        {
            var term = query.Text;
            if (term.Length <= 1) return false;

            var cmd = term.Substring(1);

            var startInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{cmd}\" & pause"
            };

            if (Input.IsKeyDown(Keys.LeftControl))
            {
                startInfo.Verb = "runas";
            }

            Process.Start(startInfo).Dispose();

            return true;
        }

        public void Draw()
        {
            ImGui.Text("Shell command");
        }
    }
}