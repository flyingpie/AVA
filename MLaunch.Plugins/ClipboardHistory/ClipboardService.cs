using MUI.DI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MLaunch.Plugins.ClipboardHistory
{
    [Service]
    public class ClipboardService
    {
        public Queue<ClipboardData> History { get; set; }

        public ClipboardService()
        {
            History = new Queue<ClipboardData>();

            ClipboardNotification.ClipboardUpdate += (s, a) =>
            {
                History.Enqueue(ClipboardData.Create());
            };
            
        }
    }

    public class ClipboardData
    {
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        public IDataObject DataObject { get; set; }

        public bool IsText { get; set; }

        public string Text { get; set; }

        public bool IsImage { get; set; }

        public byte[] Image { get; set; }

        public Stream ImageStr { get; set; }

        public byte[] ImageThumbnail { get; set; }

        public static ClipboardData Create()
        {
            var dataObj = Clipboard.GetDataObject();

            var isText = Clipboard.ContainsText();
            var isImage = Clipboard.ContainsImage();

            var cd = new ClipboardData()
            {
                DataObject = dataObj
            };

            if (isText)
            {
                cd.IsText = true;
                cd.Text = Clipboard.GetText();
            }

            if (isImage)
            {
                cd.IsImage = true;

                var imgStream = new MemoryStream();

                using (var thumbStream = new MemoryStream())
                {
                    var img = Clipboard.GetImage();
                    img.Save(imgStream, ImageFormat.Png);
                    //cd.Image = imgStream.ToArray();
                    cd.ImageStr = imgStream;

                    var thumb = img.GetThumbnailImage(50, 50, new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);
                    thumb.Save(thumbStream, ImageFormat.Png);
                    cd.ImageThumbnail = thumbStream.ToArray();
                }
            }

            return cd;
        }

        public void Restore()
        {
            //Clipboard.SetDataObject(DataObject);

            if (IsText)
            {
                Clipboard.SetText(Text);
                return;
            }

            if (IsImage)
            {
                Clipboard.SetImage(System.Drawing.Image.FromStream(ImageStr));
                return;
            }
        }
    }

    /// <summary>
    /// Provides notifications when the contents of the clipboard is updated.
    /// </summary>
    public sealed class ClipboardNotification
    {
        /// <summary>
        /// Occurs when the contents of the clipboard is updated.
        /// </summary>
        public static event EventHandler ClipboardUpdate;

        private static NotificationForm _form = new NotificationForm();

        /// <summary>
        /// Raises the <see cref="ClipboardUpdate"/> event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        private static void OnClipboardUpdate(EventArgs e)
        {
            var handler = ClipboardUpdate;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Hidden form to recieve the WM_CLIPBOARDUPDATE message.
        /// </summary>
        private class NotificationForm : Form
        {
            public NotificationForm()
            {
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                {
                    OnClipboardUpdate(null);
                }
                base.WndProc(ref m);
            }
        }
    }

    internal static class NativeMethods
    {
        // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
        public const int WM_CLIPBOARDUPDATE = 0x031D;

        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        // See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
        // See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}