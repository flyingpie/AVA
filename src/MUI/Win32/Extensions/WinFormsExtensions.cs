using System.Windows.Forms;

namespace MUI.Win32.Extensions;

public static class WinFormsExtensions
{
	public static void InvokeIfRequired(this Control control, MethodInvoker action)
	{
		if (control.InvokeRequired)
		{
			control.Invoke(action);
		}
		else
		{
			action();
		}
	}
}