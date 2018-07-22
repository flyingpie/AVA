using System.Runtime.InteropServices;

namespace MUI.Win32
{
    public static class PInvoke
    {
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);
    }
}