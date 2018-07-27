using ImGuiNET;
using MUI;
using MUI.Graphics;
using System;
using System.Numerics;

namespace MLaunch.Core.QueryExecutors.ListQuery
{
    public class ListQueryResult : IListQueryResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public Image Icon { get; set; }

        public Action<QueryContext> OnExecute { get; set; }

        private static readonly int Height = 50;

        private static readonly int BorderWidth = 1;

        public virtual void Draw(UIContext context, bool isSelected)
        {
            ImGui.PushFont(context.Font24);

            // Selection
            if (isSelected) ImGui.PushStyleColor(ColorTarget.ChildBg, new Vector4(1, 1, 1, .1f));

            ImGui.BeginChild($"query-result-{Id}", new Vector2(ImGui.GetWindowContentRegionWidth(), Height), false, WindowFlags.Default);
            {
                ImGui.Columns(2, " ", false);

                ImGui.SetColumnWidth(0, Height + 10);

                // Icon
                {
                    var texture = Icon?.GetTexture();
                    if (texture != null)
                    {
                        var size = Height - 2;
                        ImGui.Image(texture.Value, new Vector2(size, size), Vector2.Zero, Vector2.One, Vector4.One, new Vector4(1, 1, 1, .6f));
                    }
                }

                ImGui.NextColumn();

                // Name and subtext
                {
                    ImGui.Text(Name);

                    ImGui.PushFont(context.Font16);
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

        public void Execute(QueryContext query)
        {
            OnExecute?.Invoke(query);
        }
    }
}