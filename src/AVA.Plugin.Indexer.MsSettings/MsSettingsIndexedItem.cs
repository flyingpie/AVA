using AVA.Indexing;
using MUI;
using MUI.Graphics;
using System;
using System.Diagnostics;

namespace AVA.Plugin.Indexer.MsSettings
{
    public class MsSettingsIndexedItem : IndexedItem
    {
        public MsSettingsIndexedItem(string name, string commandUri)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CommandUri = commandUri ?? throw new ArgumentNullException(nameof(commandUri));

            Description = $"Settings - {Name} - {CommandUri}";
            Extension = ".exe";
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

        public override Image GetIcon() => ResourceManager.Instance.DefaultImage;
    }
}