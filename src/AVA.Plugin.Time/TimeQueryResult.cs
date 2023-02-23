using AVA.Core.QueryExecutors.ListQuery;
using ImGuiNET;
using MUI;
using System;
using System.Numerics;

namespace AVA.Plugin.Time
{
	public class TimeQueryResult : ListQueryResult
	{
		public DateTimeOffset Time { get; set; }

		public ImFontPtr Font { get; set; }

		public override void Draw(bool isSelected)
		{
			ImGui.PushFont(Fonts.Regular24);

			var cMin = ImGui.GetWindowContentRegionMin();
			var cMax = ImGui.GetWindowContentRegionMax();
			var cWidth = cMax.X - cMin.X;

			ImGui.BeginChild($"query-result-{Id}", new Vector2(cWidth, 50), false, ImGuiWindowFlags.None);
			{
				ImGui.Columns(2, " ", false);

				ImGui.SetColumnWidth(0, 80);

				ImGui.PushFont(Fonts.Regular32);
				ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 1f, 1f, .7f));
				ImGui.Text(Time.ToString("HH:mm"));
				ImGui.PopStyleColor();
				ImGui.PopFont();

				ImGui.SetCursorScreenPos(new Vector2(ImGui.GetCursorScreenPos().X + 15, ImGui.GetCursorScreenPos().Y - 5));
				ImGui.PushFont(Fonts.Regular16);
				ImGui.Text(Time.ToString("MM-dd"));
				ImGui.PopFont();

				ImGui.NextColumn();

				// Name and subtext
				{
					if (Name != null)
						ImGui.Text(Name);

					if (Mode == ListMode.Large && !string.IsNullOrWhiteSpace(Description))
					{
						ImGui.PushFont(Fonts.Regular16);
						ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 1f, 1f, 0.5f));
						ImGui.Text(Description);
						ImGui.PopStyleColor();
						ImGui.PopFont();
					}
				}

				ImGui.Columns(1, "_", false);
			}
			ImGui.EndChild();
		}
	}
}