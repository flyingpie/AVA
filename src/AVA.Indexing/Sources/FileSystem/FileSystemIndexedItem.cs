using MUI;
using MUI.Graphics;
using MUI.Logging;
using MUI.Win32.Extensions;
using System;
using System.Diagnostics;

namespace AVA.Indexing.Sources.FileSystem
{
    public class FileSystemIndexedItem : IndexedItem
    {
        public string Path { get; set; }

        public override bool Execute()
        {
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = Path
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

        public override Image GetIcon(ResourceManager resourceManager) => resourceManager.LoadImageFromIcon(Path);
    }
}