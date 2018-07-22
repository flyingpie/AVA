using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using MUI.Graphics;
using MUI.Win32.Extensions;
using System;
using System.Diagnostics;
using System.Numerics;

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

            var path = "";
            path = @"D:\Syncthing\Apps\ConEmu\ConEmu.exe";
            path = @"C:\Program Files\Firefox Developer Edition\firefox.exe";
            path = @"C:\Users\Marco van den Oever\Desktop\Code.exe - Shortcut.lnk";

            _image = _resourceManager.LoadImageFromIcon(path);

            sw.Stop();
            Console.WriteLine($"Loaded image in {sw.Elapsed}");
        }

        public void Draw()
        {
            ImGui.Text("Hello from help!");
            ImGui.Image(_image.GetTexture(), new Vector2(50, 50), Vector2.Zero, Vector2.One, Vector4.One, Vector4.One);
        }
    }
}