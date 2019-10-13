using MUI;
using MUI.Graphics;
using MUI.Logging;
using MUI.UWP;
using System;

namespace AVA.Plugin.Indexer.UWP
{
    public class UWPAppIndexedItem : IndexedItem
    {
        public string FullName { get; set; }

        public string LogoPath { get; set; }

        public override int Boost
        {
            get => 10;
            set { }
        }

        public override bool Execute()
        {
            try
            {
                NativeApiHelper.LaunchApp(FullName);

                return true;
            }
            catch (Exception ex)
            {
                Log.Get(this).Info($"Wups: {ex.Message}");
            }

            return false;
        }

        public override Image GetIcon() => ResourceManager.Instance.TryLoadImage(LogoPath, out var image) ? image : null;
    }
}