using MUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AVA.Core.Settings
{
    [Section("AVA")]
    public class AVASettings : Settings
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Keys ToggleUIKey { get; set; } = Keys.Space;

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyModifiers ToggleUIKeyModifiers { get; set; } = KeyModifiers.Alt;
    }
}