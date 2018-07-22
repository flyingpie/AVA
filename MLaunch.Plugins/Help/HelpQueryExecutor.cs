using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;

namespace MLaunch.Plugins.Help
{
    public class HelpQueryExecutor : IQueryExecutor
    {
        public int Order => 0;

        public void Draw(UIContext uiContext)
        {
            ImGui.Text("Hello from help!");
        }

        public bool TryExecute(string term) => false;

        public bool TryHandle(string term) => string.IsNullOrWhiteSpace(term);
    }
}