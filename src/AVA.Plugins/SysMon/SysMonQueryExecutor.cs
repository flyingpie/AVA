using ImGuiNET;
using AVA.Core.QueryExecutors.CommandQuery;
using MUI.DI;

namespace AVA.Plugins.SysMon
{
    [Service]
    public class SysMonQueryExecutor : CommandQueryExecutor
    {
        public override string Name => "Sys mon";

        public override string Description => "Shows information about the system's resources";

        public override string ExampleUsage => "sys";

        public override string[] CommandPrefixes => new[] { "sys" };

        public override void Draw()
        {
            ImGui.Text("Sys");
        }
    }
}