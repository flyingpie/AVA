using AVA.Core;
using AVA.Core.QueryExecutors;
using ImGuiNET;
using MUI.DI;
using System;

namespace AVA.Plugin.Tests
{
	[Service]
	public class BuggyQueryExecutor : IQueryExecutor
	{
		public int Order => 0;

		public bool TryHandle(QueryContext query)
		{
			if (query.Text.ToLowerInvariant().Equals("bug handle")) throw new InvalidOperationException($"Exception from {nameof(BuggyQueryExecutor)}.{nameof(TryHandle)}");

			if (query.HasPrefix("bug ")) return true;

			return false;
		}

		public bool TryExecute(QueryContext query)
		{
			if (query.Text.ToLowerInvariant().Equals("bug execute")) throw new InvalidOperationException($"Exception from {nameof(BuggyQueryExecutor)}.{nameof(TryExecute)}");

			return false;
		}

		public void Draw()
		{
			ImGui.Text("Crash on execute: 'bug execute'");
			ImGui.Text("Crash on handle: 'bug handle'");
		}
	}
}