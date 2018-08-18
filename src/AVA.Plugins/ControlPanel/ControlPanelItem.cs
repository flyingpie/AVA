using MUI.Win32;
using System.Diagnostics;
using System.Drawing;

namespace WindowsControlPanelItems
{
    public class ControlPanelItem
    {
        public string LocalizedString { get; private set; }

        public string InfoTip { get; private set; }

        public PSI ProcessStartInfo { get; set; }

        public string Key { get; set; }

        public ControlPanelItem(
            string newLocalizedString,
            string newInfoTip,
            PSI newExecutablePath,
            string registryKey)
        {
            LocalizedString = newLocalizedString;
            InfoTip = newInfoTip;
            ProcessStartInfo = newExecutablePath;
            Key = registryKey;
        }

        public void Execute()
        {
            var proc = Process.Start(ProcessStartInfo.ToProcessStartInfo());

            PInvoke.SetForegroundWindow(proc.MainWindowHandle);
        }

        public Icon GetIcon(int size)
        {
            return ControlPanelItemList.getIcon(Key, size);
        }
    }

    public class PSI
    {
        public string FileName { get; set; }

        public string Arguments { get; set; }

        public ProcessWindowStyle WindowStyle { get; set; }

        public ProcessStartInfo ToProcessStartInfo()
        {
            return new ProcessStartInfo()
            {
                FileName = FileName,
                Arguments = Arguments,
                WindowStyle = WindowStyle
            };
        }
    }

    public static class Ext
    {
        public static PSI ToPSI(this ProcessStartInfo psi)
        {
            return new PSI()
            {
                FileName = psi.FileName,
                Arguments = psi.Arguments,
                WindowStyle = psi.WindowStyle
            };
        }
    }
}