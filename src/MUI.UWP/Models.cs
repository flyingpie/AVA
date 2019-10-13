using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUI.UWP
{
    public class ManifestInfo
    {
        public List<ManifestApplication> _apps = new List<ManifestApplication>();

        public NativeApiHelper.IAppxManifestProperties Properties { get; set; }

        public ManifestInfo()
        {
        }

        public string FullName { get; set; }

        public string ApplicationUserModelId { get; set; }


        public PackageArchitecture ProcessorArchitecture { get; set; }

        public List<ManifestApplication> Apps
        {
            get
            {
                return _apps;
            }
        }

        public string GetPropertyStringValue(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return NativeApiHelper.GetStringValue(Properties, name);
        }

        public bool GetPropertyBoolValue(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return NativeApiHelper.GetBoolValue(Properties, name);
        }

        public string LoadResourceString(string resource)
        {
            return NativeApiHelper.LoadResourceString(FullName, resource);
        }
    }

    public class ManifestApplication
    {
        private NativeApiHelper.IAppxManifestApplication _app;

        internal ManifestApplication(NativeApiHelper.IAppxManifestApplication app)
        {
            _app = app;
        }

        public string GetStringValue(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return NativeApiHelper.GetStringValue(_app, name);
        }

        // we code well-known but there are others (like Square71x71Logo, Square44x44Logo, whatever ...)
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh446703.aspx
        public string Description { get; internal set; }

        public string DisplayName { get; internal set; }

        public string EntryPoint { get; internal set; }

        public string Executable { get; internal set; }

        public string Id { get; internal set; }

        public string Logo { get; internal set; }

        public string SmallLogo { get; internal set; }

        public string StartPage { get; internal set; }

        public string Square150x150Logo { get; internal set; }

        public string Square30x30Logo { get; internal set; }

        public string BackgroundColor { get; internal set; }

        public string ForegroundText { get; internal set; }

        public string WideLogo { get; internal set; }

        public string Wide310x310Logo { get; internal set; }

        public string ShortName { get; internal set; }

        public string Square310x310Logo { get; internal set; }

        public string Square70x70Logo { get; internal set; }

        public string MinWidth { get; internal set; }

        public string AppListEntry { get; set; }

    }

    public enum PackageArchitecture
    {
        x86 = 0,
        Arm = 5,
        x64 = 9,
        Neutral = 11,
        Arm64 = 12
    }

    public class PackageInfoEx
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FullLogoPath { get; set; }
        public ManifestApplication AppInfo { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
