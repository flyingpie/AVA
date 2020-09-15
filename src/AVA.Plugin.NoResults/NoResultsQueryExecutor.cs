using AVA.Core;
using AVA.Core.QueryExecutors;
using ImGuiNET;
using MUI.DI;

namespace AVA.Plugin.NoResults
{
	[Service]
	public class NoResultsQueryExecutor : IQueryExecutor
	{
		public int Order => 1000;

		public void Draw()
		{
			ImGui.Text("That didn't work :(");
		}

		public bool TryExecute(QueryContext query) => false;

		public bool TryHandle(QueryContext query) => true;
	}
}