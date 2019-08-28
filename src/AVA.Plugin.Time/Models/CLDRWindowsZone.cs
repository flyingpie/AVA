using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Plugin.Time.Models
{
    public class CLDRTimeZone
    {
        public CLDRWindowsZone MapZone { get; set; }
    }

    public class CLDRWindowsZone
    {
        [JsonProperty("_other")]
        public string Other { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        public string[] Types => Type.Split(' ');

        [JsonProperty("_territory")]
        public string Territory { get; set; }

        public static List<CLDRWindowsZone> Load()
        {
            var json = Resources.Resources.windowsZones;
            var tz = JsonConvert.DeserializeObject<JObject>(json);

            var tzs = tz["supplemental"]["windowsZones"]["mapTimezones"];

            return JsonConvert.DeserializeObject<List<CLDRTimeZone>>(JsonConvert.SerializeObject(tzs)).Select(t => t.MapZone).ToList();
        }
    }
}