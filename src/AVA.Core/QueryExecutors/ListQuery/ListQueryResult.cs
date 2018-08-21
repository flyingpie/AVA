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
        public static readonly int IconSize = 50;

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public ImageBox Icon { get; set; }

        public ListMode Mode { get; set; } = ListMode.Large;

        public Action<QueryContext> OnExecute { get; set; }

        public Func<QueryContext, Task> OnExecuteAsync { get; set; }

        public virtual void Draw(bool isSelected)
        {
            ImGui.PushFont(Fonts.Regular24);

            var IconSize = Mode == ListMode.Large ? 50 : 25;

            // Selection
            if (isSelected) ImGui.PushStyleColor(ColorTarget.ChildBg, new Vector4(1, 1, 1, .1f));

            ImGui.BeginChild($"query-result-{Id}", new Vector2(ImGui.GetWindowContentRegionWidth(), IconSize), false, WindowFlags.Default);
            {
                ImGui.Columns(2, " ", false);

                ImGui.SetColumnWidth(0, IconSize + 10);

                // Icon
                if (Icon != null)
                {
                    Icon.Size = new Vector2(IconSize, IconSize);
                    Icon.Draw();
                }

                ImGui.NextColumn();

                // Name and subtext
                {
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

            if (isSelected) ImGui.PopStyleColor();

            ImGui.PopFont();

            if (isSelected) ImGui.SetScrollHere();
        }

        public Task ExecuteAsync(QueryContext query)
        {
            if (OnExecute != null)
            {
                OnExecute.Invoke(query);
                return Task.CompletedTask;
            }

            if (OnExecuteAsync != null)
            {
                return OnExecuteAsync.Invoke(query);
            }

            return Task.CompletedTask;
        }
    }
}