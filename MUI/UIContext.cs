using ImGuiNET;
using Veldrid;

namespace MUI
{
    public class UIContext
    {
        public Font Font32 { get; set; }

        public Font Font24 { get; set; }

        public Font Font16 { get; set; }

        public Font FontAwesome { get; set; }

        public InputSnapshot Input { get; set; }

        public UIContext(ResourceManager resourceManager)
        {
            // TODO: Move
            Font16 = resourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 16);
            Font24 = resourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 24);
            Font32 = resourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 32);

            FontAwesome = resourceManager.LoadFont(@"Resources\Fonts\FontAwesome\FontAwesome.ttf", 32);
        }
    }
}