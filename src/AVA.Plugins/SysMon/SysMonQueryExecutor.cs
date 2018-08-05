using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.CommandQuery;
using ImGuiNET;
using MUI.DI;

namespace AVA.Plugins.SysMon
{
    [Service, Help(Name = "Sys mon", Description = "Shows information about the system's resources", ExampleUsage = "sys")]
    public class SysMonQueryExecutor : CommandQueryExecutor
    {
        public override string[] CommandPrefixes => new[] { "sys" };

        public override void Draw()
        {
            ImGui.Text("Sys");
        }
    }
}