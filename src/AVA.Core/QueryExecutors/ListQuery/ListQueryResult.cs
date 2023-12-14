using ImGuiNET;
using MUI;
using MUI.ImGuiControls;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors.ListQuery
{
	public enum ListMode
	{
		Large,
		Small
	}

	public class ListQueryResult : IListQueryResult
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Name { get; set; }

		public string Description { get; set; }

		public ImageBox Icon { get; set; }

		public ListMode Mode { get; set; } = ListMode.Large;

		public Action<QueryContext> OnExecute { get; set; }

		//public Func<QueryContext, Task> OnExecuteAsync { get; set; }

		public virtual void Draw(bool isSelected)
		{
			ImGui.PushFont(Fonts.Regular24);

			var iconSize = Mode == ListMode.Large ? 50 : 25;

			// Selection
			if (isSelected) ImGui.PushStyleColor(ImGuiCol.ChildBg, new Vector4(1, 1, 1, .1f));

			// ImGui.GetWindowContentRegionWidth()
			var cMin = ImGui.GetWindowContentRegionMin();
			var cMax = ImGui.GetWindowContentRegionMax();
			var cWidth = cMax.X - cMin.X;

			ImGui.BeginChild($"query-result-{Id}", new Vector2(cWidth, iconSize), ImGuiChildFlags.None, ImGuiWindowFlags.None);
			{
				ImGui.Columns(2, " ", false);

				ImGui.SetColumnWidth(0, iconSize + 10);

				// Icon
				if (Icon != null)
				{
					Icon.Size = new Vector2(iconSize, iconSize);
					Icon.Draw();
				}

				ImGui.NextColumn();

				// Name and subtext
				{
					if (Name != null)
						ImGui.Text(Name);

					if (Mode == ListMode.Large && !string.IsNullOrWhiteSpace(Description))
					{
						ImGui.PushFont(Fonts.Regular16);
						// TODO: new Vector4(1f, 1f, 1f, 0.5f)
						ImGui.Text(Description);
						ImGui.PopFont();
					}
				}

				ImGui.Columns(1, "_", false);
			}
			ImGui.EndChild();

			if (isSelected) ImGui.PopStyleColor();

			ImGui.PopFont();

			if (isSelected) ImGui.SetScrollHereY();
		}

		public void ExecuteAsync(QueryContext query)
		{
			if (OnExecute != null)
			{
				OnExecute.Invoke(query);
				//return Task.CompletedTask;
				return;
			}

			//if (OnExecuteAsync != null)
			//{
			//	OnExecuteAsync.Invoke(query);
			//}

			//return Task.CompletedTask;
		}
	}
}