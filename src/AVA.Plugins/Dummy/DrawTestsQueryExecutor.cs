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
        [Dependency] public ResourceManager ResourceManager { get; set; }

        public override string[] CommandPrefixes => new[] { "drawt" };

        private Image _image1;
        private Image _image2;
        private Image _image3;

        [RunAfterInject]
        public void Init()
        {
            _image1 = ResourceManager.LoadImage("Resources/Images/test-image.png");
            _image2 = ResourceManager.LoadImage("Resources/Images/test-image-2.png");
            _image3 = ResourceManager.LoadFontAwesomeIcon(FontAwesomeCS.FAIcon.GitkrakenBrands, 20);
        }

        public override void Draw()
        {
            ImGui.Text("Scale modes");

            {
                ImGui.Columns(4, "scale-modes-v", true);

                ImGui.Text("Center");
                _image3.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, ScaleMode.Center);

                ImGui.NextColumn();

                ImGui.Text("Fill");
                _image1.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, ScaleMode.Fill);

                ImGui.NextColumn();

                ImGui.Text("Fit");
                _image1.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, ScaleMode.Fit);

                ImGui.NextColumn();

                ImGui.Text("Stretch");
                _image1.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, ScaleMode.Stretch);
            }

            ImGui.NextColumn();

            {
                ImGui.Text("Center");
                _image3.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 120), Vector4.One, Vector4.One, ScaleMode.Center);

                ImGui.NextColumn();

                ImGui.Text("Fill");
                _image2.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 200), Vector4.One, Vector4.One, ScaleMode.Fill);

                ImGui.NextColumn();

                ImGui.Text("Fit");
                _image2.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 200), Vector4.One, Vector4.One, ScaleMode.Fit);

                ImGui.NextColumn();

                ImGui.Text("Stretch");
                _image2.Draw(new Vector2(ImGui.GetContentRegionAvailableWidth(), 200), Vector4.One, Vector4.One, ScaleMode.Stretch);
            }
        }
    }
}