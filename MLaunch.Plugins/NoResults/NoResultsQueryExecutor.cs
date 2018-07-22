using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;

namespace MLaunch.Plugins.NoResults
{
    public class NoResultsQueryExecutor : IQueryExecutor
    {
        public int Order => 1000;

        public void Draw(UIContext uiContext)
        {
            ImGui.Text("That didn't work :(");
        }

        public bool TryExecute(string term) => false;

        public bool TryHandle(string term) => true;
    }
}