using ImGuiNET;
using AVA.Core;
using AVA.Core.QueryExecutors;
using MUI.DI;

namespace AVA.Plugins.NoResults
{
    [Service]
    public class NoResultsQueryExecutor : IQueryExecutor
    {
        public string Name => "No results";

        public string Description => "Shows up when no results were found";

        public string ExampleUsage => "some weird query";


        public int Order => 1000;

        public void Draw()
        {
            ImGui.Text("That didn't work :(");
        }

        public bool TryExecute(QueryContext query) => false;

        public bool TryHandle(QueryContext query) => true;
    }
}