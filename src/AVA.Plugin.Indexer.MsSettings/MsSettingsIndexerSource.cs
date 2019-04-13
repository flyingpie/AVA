using AVA.Indexing;
using MUI.DI;
using System.Collections.Generic;

namespace AVA.Plugin.Indexer.MsSettings
{
    [Service]
    public class MsSettingsIndexerSource : IIndexerSource
    {
        private static List<MsSettingsIndexedItem> MsSettings = new List<MsSettingsIndexedItem>()
        {
            new MsSettingsIndexedItem("Battery Saver", "ms-settings:batterysaver"),
            new MsSettingsIndexedItem("Battery Saver", "ms-settings:batterysaver-settings"),
            new MsSettingsIndexedItem("Battery Saver Settings", "ms-settings:batterysaver-usagedetails"),
            new MsSettingsIndexedItem("Bluetooth", "ms-settings:bluetooth"),
            new MsSettingsIndexedItem("Colors", "ms-settings:colors"),
            new MsSettingsIndexedItem("Data Usage", "ms-settings:datausage"),
            new MsSettingsIndexedItem("Date and Time", "ms-settings:dateandtime"),
            new MsSettingsIndexedItem("Closed Captioning", "ms-settings:easeofaccess-closedcaptioning"),
            new MsSettingsIndexedItem("High Contrast", "ms-settings:easeofaccess-highcontrast"),
            new MsSettingsIndexedItem("Magnifier", "ms-settings:easeofaccess-magnifier"),
            new MsSettingsIndexedItem("Narrator", "ms-settings:easeofaccess-narrator"),
            new MsSettingsIndexedItem("Keyboard", "ms-settings:easeofaccess-keyboard"),
            new MsSettingsIndexedItem("Mouse", "ms-settings:easeofaccess-mouse"),
            new MsSettingsIndexedItem("Other options (Ease of Access)", "ms-settings:easeofaccess-otheroptions"),
            new MsSettingsIndexedItem("Lockscreen", "ms-settings:lockscreen"),
            new MsSettingsIndexedItem("Offline maps", "ms-settings:maps"),
            new MsSettingsIndexedItem("Airplane mode", "ms-settings:network-airplanemode"),
            new MsSettingsIndexedItem("Proxy", "ms-settings:network-proxy"),
            new MsSettingsIndexedItem("VPN", "ms-settings:network-vpn"),
            new MsSettingsIndexedItem("Notifications & actions", "ms-settings:notifications"),
            new MsSettingsIndexedItem("Account info", "ms-settings:privacy-accountinfo"),
            new MsSettingsIndexedItem("Calendar", "ms-settings:privacy-calendar"),
            new MsSettingsIndexedItem("Contacts", "ms-settings:privacy-contacts"),
            new MsSettingsIndexedItem("Other Devices", "ms-settings:privacy-customdevices"),
            new MsSettingsIndexedItem("Feedback", "ms-settings:privacy-feedback"),
            new MsSettingsIndexedItem("Location", "ms-settings:privacy-location"),
            new MsSettingsIndexedItem("Messaging", "ms-settings:privacy-messaging"),
            new MsSettingsIndexedItem("Motion", "ms-settings:privacy-motion"),
            new MsSettingsIndexedItem("Radios", "ms-settings:privacy-radios"),
            new MsSettingsIndexedItem("Speech, inking, & typing", "ms-settings:privacy-speechtyping"),
            new MsSettingsIndexedItem("Camera", "ms-settings:privacy-webcam"),
            new MsSettingsIndexedItem("Region & language", "ms-settings:regionlanguage"),
            new MsSettingsIndexedItem("Speech", "ms-settings:speech"),
            new MsSettingsIndexedItem("Windows Update", "ms-settings:windowsupdate"),
            new MsSettingsIndexedItem("Work access", "ms-settings:workplace"),
            new MsSettingsIndexedItem("Connected devices", "ms-settings:connecteddevices"),
            new MsSettingsIndexedItem("For developers", "ms-settings:developers"),
            new MsSettingsIndexedItem("Display", "ms-settings:display"),
            new MsSettingsIndexedItem("Mouse & touchpad", "ms-settings:mousetouchpad"),
            new MsSettingsIndexedItem("Cellular", "ms-settings:network-cellular"),
            new MsSettingsIndexedItem("Dial-up", "ms-settings:network-dialup"),
            new MsSettingsIndexedItem("DirectAccess", "ms-settings:network-directaccess"),
            new MsSettingsIndexedItem("Ethernet", "ms-settings:network-ethernet"),
            new MsSettingsIndexedItem("Mobile hotspot", "ms-settings:network-mobilehotspot"),
            new MsSettingsIndexedItem("Wi-Fi", "ms-settings:network-wifi"),
            new MsSettingsIndexedItem("Manage Wi-Fi Settings", "ms-settings:network-wifisettings"),
            new MsSettingsIndexedItem("Optional features", "ms-settings:optionalfeatures"),
            new MsSettingsIndexedItem("Family & other users", "ms-settings:otherusers"),
            new MsSettingsIndexedItem("Personalization", "ms-settings:personalization"),
            new MsSettingsIndexedItem("Backgrounds", "ms-settings:personalization-background"),
            new MsSettingsIndexedItem("Colors", "ms-settings:personalization-colors"),
            new MsSettingsIndexedItem("Start", "ms-settings:personalization-start"),
            new MsSettingsIndexedItem("Power & Sleep", "ms-settings:powersleep"),
            new MsSettingsIndexedItem("Proximity", "ms-settings:proximity"),
            new MsSettingsIndexedItem("Display", "ms-settings:screenrotation"),
            new MsSettingsIndexedItem("Sign-in options", "ms-settings:signinoptions"),
            new MsSettingsIndexedItem("Storage Sense", "ms-settings:storagesense"),
            new MsSettingsIndexedItem("Themes", "ms-settings:themes"),
            new MsSettingsIndexedItem("Typing", "ms-settings:typing"),
            new MsSettingsIndexedItem("Tablet move", "ms-settings://tabletmode/"),
            new MsSettingsIndexedItem("Privacy", "ms-settings:privacy"),
            new MsSettingsIndexedItem("Microphone", "ms-settings:privacy-microphone")
        };

        public IEnumerable<IndexedItem> GetItems() => MsSettings;
    }
}