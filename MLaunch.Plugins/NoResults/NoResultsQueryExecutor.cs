using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI.DI;

namespace MLaunch.Plugins.NoResults
{
    [Service]
    public class NoResultsQueryExecutor : IQueryExecutor
    {
        public int Order => 1000;

        public void Draw()
        {
            ImGui.Text("That didn't work :(");
        }

        public bool TryExecute(string term) => false;

        public bool TryHandle(string term) => true;
    }
}