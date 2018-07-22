using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using MUI.Graphics;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;

namespace MLaunch.Plugins.Help
{
    [Service]
    public class HelpQueryExecutor : IQueryExecutor
    {
        [Dependency]
        private ResourceManager _resourceManager;

        public int Order => 999;

        public bool TryHandle(string term) => string.IsNullOrEmpty(term) || term.ToLowerInvariant() == "help";

        public bool TryExecute(string term) => false;

        private Image _image;

        [RunAfterInject]
        private void Init()
        {
            var sw = new Stopwatch();
            sw.Start();

            Image img = null;

            using (var icoExe = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Syncthing\Apps\ConEmu\ConEmu.exe"))
            using (var str = new MemoryStream())
            {
                icoExe.ToBitmap().Save(str, ImageFormat.Bmp);

                img = new Image(_resourceManager.LoadTexture(str.ToArray()));
            }

            sw.Stop();
            Console.WriteLine($"Loaded image in {sw.Elapsed}");

            var xx = 2;
        }

        public void Draw()
        {
            ImGui.Text("Hello from help!");
        }
    }
}