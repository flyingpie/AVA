using AVA.Core.Settings;

namespace AVA.Plugin.Indexer.FileSystem
{
    [Section("Indexer.FileSystem")]
    public class FileSystemIndexerSourceSettings : Settings
    {
        public string[] Paths { get; set; } = new[]
        {
            @"%ProgramData%\Microsoft\Windows\Start Menu\Programs",
            @"%APPDATA%\Microsoft\Windows\Start Menu",
            @"%NEXTCLOUD%"
        };
    }
}