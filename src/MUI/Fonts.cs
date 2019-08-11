using ImGuiNET;

namespace MUI
{
    public static class Fonts
    {
        public static ImFontPtr Regular32 { get; set; }

        public static ImFontPtr Regular24 { get; set; }

        public static ImFontPtr Regular16 { get; set; }

        public static void Load(ResourceManager resourceManager)
        {
            var openSansRegular = @"Resources\Fonts\OpenSans\OpenSans-Regular.ttf";

            Regular32 = resourceManager.LoadFont(openSansRegular, 32);
            Regular24 = resourceManager.LoadFont(openSansRegular, 24);
            Regular16 = resourceManager.LoadFont(openSansRegular, 16);
        }
    }
}