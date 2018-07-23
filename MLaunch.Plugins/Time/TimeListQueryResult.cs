using ImGuiNET;
using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using System;

namespace MLaunch.Plugins.Time
{
    public class TimeListQueryResult : ListQueryResult
    {
        public DateTimeOffset DateTime { get; set; }

        public bool IsDst { get; set; }

        public override void Draw(UIContext context, bool isSelected)
        {
            ImGui.BeginChild(" ");
            ImGui.Columns(2, Guid.NewGuid().ToString(), true);

            {
                ImGui.PushFont(context.Font32);
                ImGui.Text(DateTime.ToString("HH:mm"));
                ImGui.PopFont();

                ImGui.SameLine();

                ImGui.PushFont(context.Font16);
                ImGui.Text(DateTime.ToString("dd-MM"));
                ImGui.PopFont();
            }

            ImGui.NextColumn();

            {
                ImGui.PushFont(context.Font24);
                ImGui.Text(Name);
                ImGui.PopFont();
            }

            ImGui.EndChild();

            if (isSelected) ImGui.SetScrollHere();
        }
    }
}