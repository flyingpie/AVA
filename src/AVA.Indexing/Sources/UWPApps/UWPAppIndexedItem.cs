using MUI;
using MUI.Graphics;
using MUI.Logging;
using MUI.Win32.UWP;
using System;

namespace AVA.Indexing.Sources.UWPApps
{
    public class UWPAppIndexedItem : IndexedItem
    {
        public string FullName { get; set; }

        public string LogoPath { get; set; }

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

        public override Image GetIcon() => ResourceManager.Instance.LoadImage(LogoPath);
    }
}