using MUI;

namespace AVA.Core.Settings
{
    public class AVASettings : Settings
    {
        public Keys ToggleUIKey { get; set; } = Keys.Space;

        public KeyModifiers ToggleUIKeyModifiers { get; set; } = KeyModifiers.Alt;
    }
}