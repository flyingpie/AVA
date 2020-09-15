using System;
using System.Runtime.InteropServices;

namespace MUI.Win32
{
	public static class PInvoke
	{
		[DllImport("user32.dll")]
		public static extern int SetForegroundWindow(IntPtr hwnd);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, NCmdShow nCmdShow);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

		public const int GWL_EX_STYLE = -20;

		public const int LWA_ALPHA = 0x2;

		public const int WS_EX_APPWINDOW = 0x00040000;
		public const int WS_EX_LAYERED = 0x80000;
		public const int WS_EX_TOOLWINDOW = 0x00000080;
	}

	public enum NCmdShow
	{
		SW_HIDE = 0,
		SW_SHOW = 5
	}
}