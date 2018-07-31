using AVA.Core.QueryExecutors.ListQuery;
using ImGuiNET;
using MUI;
using System;

namespace AVA.Plugins.Time
{
    public class TimeListQueryResult : ListQueryResult
    {
        public DateTimeOffset DateTime { get; set; }

        public bool IsDst { get; set; }

        public override void Draw(bool isSelected)
        {
            ImGui.BeginChild(" ");
            ImGui.Columns(2, Guid.NewGuid().ToString(), true);

            {
                ImGui.PushFont(Fonts.Regular32);
                ImGui.Text(DateTime.ToString("HH:mm"));
                ImGui.PopFont();

                ImGui.SameLine();

                ImGui.PushFont(Fonts.Regular16);
                ImGui.Text(DateTime.ToString("dd-MM"));
                ImGui.PopFont();
            }

            ImGui.NextColumn();

            {
                ImGui.PushFont(Fonts.Regular24);
                ImGui.Text(Name);
                ImGui.PopFont();
            }

            ImGui.EndChild();

            if (isSelected) ImGui.SetScrollHere();
        }
    }
}