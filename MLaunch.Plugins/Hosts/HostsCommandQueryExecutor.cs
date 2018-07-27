using ImGuiNET;
using MLaunch.Core;
using MLaunch.Core.QueryExecutors.CommandQuery;
using System.Diagnostics;

namespace MLaunch.Plugins.Hosts
{
    public class HostsCommandQueryExecutor : CommandQueryExecutor
    {
        public override string[] CommandPrefixes => new[] { "hosts" };

        public override string Description => "Opens the local hosts file";

        public override string Name => "Hosts";

        public override string ExampleUsage => CommandPrefixes[0];

        public override bool TryExecute(QueryContext query)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "notepad",
                Arguments = @"C:\Windows\System32\drivers\etc\hosts",
                Verb = "RunAs"
            });

            return true;
        }

        public override void Draw()
        {
            ImGui.Text("Open hosts file");
        }
    }
}