using System.Windows.Forms;
using Veldrid.Sdl2;

namespace MUI.Win32.Extensions
{
    public static class Sdl2WindowExtensions
    {
        public static void CenterToActiveMonitor(this Sdl2Window window)
        {
            var screen = Screen.FromPoint(new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y));

            var centerX = screen.WorkingArea.Width / 2;
            var centerY = screen.WorkingArea.Height / 2;

            var x = centerX - (window.Width / 2);
            var y = centerY - (window.Height / 2);

            window.X = screen.WorkingArea.Location.X + x;
            window.Y = screen.WorkingArea.Location.Y + y;
        }
    }
}