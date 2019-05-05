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

        public Font Font { get; set; }

        public override void Draw(bool isSelected)
        {
            ImGui.PushFont(Fonts.Regular24);

            ImGui.BeginChild($"query-result-{Id}", new Vector2(ImGui.GetWindowContentRegionWidth(), 50), false, WindowFlags.Default);
            {
                ImGui.Columns(2, " ", false);

                ImGui.SetColumnWidth(0, 80);

                ImGui.PushFont(Fonts.Regular32);
                ImGui.Text(Time.ToString("HH:mm"), new Vector4(1f, 1f, 1f, .7f));
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
                        ImGui.Text(Description, new Vector4(1f, 1f, 1f, 0.5f));
                        ImGui.PopFont();
                    }
                }

                ImGui.Columns(1, "_", false);
            }
            ImGui.EndChild();
        }
    }
}