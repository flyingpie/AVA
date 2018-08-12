using ImGuiNET;
using AVA.Core;
using AVA.Core.QueryExecutors.CommandQuery;
using System.Diagnostics;
using AVA.Core.QueryExecutors;
using FontAwesomeCS;

namespace AVA.Plugins.Hosts
{
    [Help(Name = "Hosts", Description = "Opens the local hosts file", ExampleUsage = "hosts", Icon = FAIcon.ServerSolid)]
    public class HostsCommandQueryExecutor : CommandQueryExecutor
    {
        public override string[] CommandPrefixes => new[] { "hosts" };
        
        public override bool TryExecute(QueryContext query)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "notepad",
                Arguments = @"C:\Windows\System32\drivers\etc\hosts",
                Verb = "RunAs"
            }).Dispose();

            return true;
        }

        public override void Draw()
        {
            ImGui.Text("Open hosts file");
        }
    }
}