using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI.DI;

namespace MLaunch.Plugins.NoResults
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

        public bool TryExecute(string term) => false;

        public bool TryHandle(string term) => true;
    }
}