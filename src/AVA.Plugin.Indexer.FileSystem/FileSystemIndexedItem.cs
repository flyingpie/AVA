using AVA.Plugin.Indexer;
using MUI;
using MUI.Graphics;
using MUI.Logging;
using MUI.Win32.Extensions;
using System;
using System.Diagnostics;

namespace AVA.Plugin.Indexer.FileSystem
{
    public class FileSystemIndexedItem : IndexedItem
    {
        public string Path { get; set; }

        public override int Boost
        {
            get
            {
                var ext = System.IO.Path.GetExtension(Path);

                if (ext?.Equals(".exe", StringComparison.OrdinalIgnoreCase) ?? false) return 10;
                if (ext?.Equals(".lnk", StringComparison.OrdinalIgnoreCase) ?? false) return 10;

                return 0;
            }
            set { }
        }

        public override bool Execute()
        {
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = Path,
                    WorkingDirectory = System.IO.Path.GetDirectoryName(Path)
                };

                if (Input.IsKeyDown(Keys.LeftControl))
                {
                    startInfo.Verb = "runas";
                }

                Process.Start(startInfo).Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Log.Get(this).Info($"Wups: {ex.Message}");
            }

            return false;
        }

        public override Image GetIcon() => ResourceManager.Instance.LoadImageFromIcon(Path);
    }
}