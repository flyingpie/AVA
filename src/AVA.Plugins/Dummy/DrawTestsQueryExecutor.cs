using AVA.Core.QueryExecutors.CommandQuery;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.Glyphs;
using MUI.Graphics;
using System.Numerics;

namespace AVA.Plugins.Dummy
{
    [Service]
    public class DrawTestsQueryExecutor : CommandQueryExecutor
    {
        public override string[] CommandPrefixes => new[] { "drawt" };

        private Image _imgTall;
        private Image _imgWide;
        private Image _imgSquareSmall;
        private Image _imgSquareLarge;

        [RunAfterInject]
        public void Init()
        {
            _imgTall = ResourceManager.Instance.LoadImage("Resources/Images/debug-tall.png");
            _imgWide = ResourceManager.Instance.LoadImage("Resources/Images/debug-wide.png");
            _imgSquareSmall = ResourceManager.Instance.LoadFontAwesomeIcon(FontAwesomeCS.FAIcon.GithubSquareBrands, 20);
            _imgSquareLarge = ResourceManager.Instance.LoadFontAwesomeIcon(FontAwesomeCS.FAIcon.GithubSquareBrands, 200);
        }

        public override void Draw()
        {
            ImGui.Text("Scale modes");

            {
                ImGui.Columns(4, "scale-modes-v", false);

                ImGui.Text("Center");
                _imgSquareSmall.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Center);

                ImGui.NextColumn();

                ImGui.Text("Fill");
                _imgTall.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Fill);

                ImGui.NextColumn();

                ImGui.Text("Fit");
                _imgTall.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Fit);

                ImGui.NextColumn();

                ImGui.Text("Stretch");
                _imgTall.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Stretch);
            }

            ImGui.NextColumn();

            {
                ImGui.Text("Center");
                _imgSquareLarge.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Center);

                ImGui.NextColumn();

                ImGui.Text("Fill");
                _imgWide.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Fill);

                ImGui.NextColumn();

                ImGui.Text("Fit");
                _imgWide.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Fit);

                ImGui.NextColumn();

                ImGui.Text("Stretch");
                _imgWide.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, Vector4.Zero, ScaleMode.Stretch);
            }

            ImGui.NextColumn();

            {
                ImGui.Text("Center");
                ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), ScaleMode.Center);

                ImGui.NextColumn();

                ImGui.Text("Fill");
                ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), ScaleMode.Fill);

                ImGui.NextColumn();

                ImGui.Text("Fit");
                ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), ScaleMode.Fit);

                ImGui.NextColumn();

                ImGui.Text("Stretch");
                ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), ScaleMode.Stretch);
            }
        }
    }
}