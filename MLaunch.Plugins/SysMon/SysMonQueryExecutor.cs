using ImGuiNET;
using MLaunch.Core.QueryExecutors.CommandQuery;
using MUI.DI;

namespace MLaunch.Plugins.SysMon
{
    [Service]
    public class SysMonQueryExecutor : CommandQueryExecutor
    {
        public override string[] CommandPrefixes => new[] { "sys" };

        public override void Draw()
        {
            ImGui.Text("Sys");
        }
    }
}