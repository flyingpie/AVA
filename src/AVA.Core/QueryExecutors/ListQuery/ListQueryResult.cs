using ImGuiNET;
using MUI;
using MUI.Graphics;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors.ListQuery
{
    public class ListQueryResult : IListQueryResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public Image Icon { get; set; }

        public Action<QueryContext> OnExecute { get; set; }

        public Func<QueryContext, Task> OnExecuteAsync { get; set; }

        private static readonly int Height = 50;

        public virtual void Draw(bool isSelected)
        {
            ImGui.PushFont(Fonts.Regular24);

            // Selection
            if (isSelected) ImGui.PushStyleColor(ColorTarget.ChildBg, new Vector4(1, 1, 1, .1f));

            ImGui.BeginChild($"query-result-{Id}", new Vector2(ImGui.GetWindowContentRegionWidth(), Height), false, WindowFlags.Default);
            {
                ImGui.Columns(2, " ", false);

                ImGui.SetColumnWidth(0, Height + 10);

                // Icon
                {
                    Icon?.Draw(new Vector2(Height - 2, Height - 2), Vector4.One, new Vector4(1, 1, 1, .6f));
                }

                ImGui.NextColumn();

                // Name and subtext
                {
                    ImGui.Text(Name);

                    ImGui.PushFont(Fonts.Regular16);
                    ImGui.Text(Description, new Vector4(1f, 1f, 1f, 0.5f));
                    ImGui.PopFont();
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
                OnExecute?.Invoke(query);
            }

            if (OnExecuteAsync != null)
            {
                return OnExecuteAsync?.Invoke(query) ?? Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}