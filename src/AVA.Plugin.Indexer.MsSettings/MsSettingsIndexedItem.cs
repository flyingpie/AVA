using AVA.Plugin.Indexer;
using AVA.Plugin.Indexer.MsSettings.Resources;
using MUI;
using MUI.Graphics;
using MUI.Win32.Extensions;
using System;
using System.Diagnostics;

namespace AVA.Plugin.Indexer.MsSettings
{
    public class MsSettingsIndexedItem : IndexedItem
    {
        private static Image _icon;

        public static Image GetSettingsIcon()
        {
            if (_icon == null)
            {
                _icon = ResourceManager.Instance.LoadImageFromBitmap(Guid.NewGuid().ToString(), _Resources.SettingsIcon);
            }

            return _icon;
        }

        public MsSettingsIndexedItem()
        { }

        public MsSettingsIndexedItem(string name, string commandUri)
        {
            IndexerName = name ?? throw new ArgumentNullException(nameof(name));
            CommandUri = commandUri ?? throw new ArgumentNullException(nameof(commandUri));

            Description = $"Settings - {IndexerName} - {CommandUri}";
        }

        public override int Boost
        {
            get => 10;
            set { }
        }

        public string CommandUri { get; set; }

        public override bool Execute()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = CommandUri
            }).Dispose();

            return true;
        }

        public override Image GetIcon() => GetSettingsIcon();
    }
}