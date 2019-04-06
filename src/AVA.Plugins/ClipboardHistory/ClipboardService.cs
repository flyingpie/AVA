using MUI;
using MUI.DI;
using MUI.Graphics;
using MUI.Logging;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AVA.Plugins.ClipboardHistory
{
    [Service]
    public class ClipboardService
    {
        public List<ClipboardEntry> History { get; set; }

        private ILog _log = Log.Get<ClipboardService>();

        public ClipboardService()
        {
            History = new List<ClipboardEntry>();

            ClipboardNotification.ClipboardUpdate += (s, a) =>
            {
                try
                {
                    _log.Info("UPDATE");
                    History.Insert(0, ClipboardEntry.Create());
                    _log.Info("/UPDATE");

                    RemoveDupes();

                    while (History.Count > 10) History.RemoveAt(History.Count - 1);
                }
                catch (Exception ex)
                {
                    _log.Error($"Error while processing clipboard entry: '{ex.Message}'.", ex);
                }
            };
        }

        public void Clear() => History.Clear();

        public void Restore(ClipboardEntry data)
        {
            _log.Info("RESTORE");
            data.Restore();
            _log.Info("/RESTORE");
        }

        public void RemoveDupes()
        {
            History
                .GroupBy(h => h.Hash)
                .Where(g => g.Count() > 1)
                .ToList()
                .ForEach(h =>
                {
                    _log.Info($"Removing dupe '{h.ToString()}'");

                    History.Remove(h.Last());
                });
        }
    }

    public class ClipboardEntry
    {
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        public List<ClipboardData> Data { get; set; }

        public bool IsText { get; set; }

        public string Text { get; set; }

        public bool IsImage { get; set; }

        public string FileName { get; set; }

        public bool IsFileName { get; set; }

        public Image Icon { get; set; }

        public static ClipboardEntry Create()
        {
            var isText = Clipboard.ContainsText();
            var isImage = Clipboard.ContainsImage();

            var cd = new ClipboardEntry();

            if (TryParseFromFileName(cd, out var fileName))
            {
                cd.IsFileName = true;
                cd.FileName = fileName;
                cd.Icon = cd.Icon = ResourceManager.Instance.LoadImageFromIcon(fileName);
            }
            else if (isText)
            {
                cd.IsText = true;
                cd.Text = Clipboard.GetText();
            }
            else if (isImage)
            {
                cd.IsImage = true;
                cd.Icon = LoadImage();
            }

            return cd;
        }

        private static bool TryParseFromFileName(ClipboardEntry cd, out string fn)
        {
            fn = null;
            var fileName = Clipboard.GetData("FileName");

            if (fileName is string path && File.Exists(path))
            {
                fn = path;
                return true;
            }

            if (fileName is string[] paths && paths.Length > 0 && File.Exists(paths[0]))
            {
                fn = paths[0];
                return true;
            }

            return false;
        }

        private static Image LoadImage()
        {
            var imgStream = new MemoryStream();

            var img = Clipboard.GetImage();
            img.Save(imgStream, ImageFormat.Png);

            using (var thumbStream = new MemoryStream())
            {
                var ratio = (float)img.Height / (float)img.Width;
                var thumb = img.GetThumbnailImage(50, (int)Math.Ceiling(50f * ratio), new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);
                thumb.Save(thumbStream, ImageFormat.Png);

                var bytes = thumbStream.ToArray();
                return ResourceManager.Instance.LoadImage(bytes.HashSHA1(), bytes);
            }
        }

        private string _hash;

        public string Hash
        {
            get
            {
                if (_hash == null)
                {
                    if (IsText) _hash = Text.HashSHA1();

                    if (IsImage) _hash = Image.ToArray().HashSHA1();

                    if (!string.IsNullOrWhiteSpace(FileName)) return FileName.HashSHA1();
                }

                return _hash;
            }
        }

        public void Restore()
        {
            //if (IsText)
            //{
            //    Clipboard.SetText(Text);
            //    return;
            //}

            //if (IsImage)
            //{
            //    Clipboard.SetImage(System.Drawing.Image.FromStream(Image));
            //    return;
            //}

            //if (!string.IsNullOrWhiteSpace(FileName))
            //{
            //    Clipboard.SetData("FileName", new[] { FileName });
            //}
        }

        public override string ToString()
        {
            if (IsText) return string.Join("", Text.Take(10));

            return "[IMAGE]";
        }
    }

    public abstract class ClipboardData
    {
        public abstract string Hash { get; }

        public abstract void Restore();

        public abstract bool TryParse(IDataObject dataObject, out ClipboardData data);
    }

    public class ImageClipboardData : ClipboardData
    {
        public override string Hash => throw new NotImplementedException();

        public override void Restore()
        {
            throw new NotImplementedException();
        }
        
        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            throw new NotImplementedException();
        }
    }

    public class TestClipboardData : ClipboardData
    {
        public override string Hash => throw new NotImplementedException();

        public override void Restore()
        {
            throw new NotImplementedException();
        }

        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            throw new NotImplementedException();
        }
    }

    public class FileDropClipboardData : ClipboardData
    {
        public override string Hash => throw new NotImplementedException();

        public override void Restore()
        {
            throw new NotImplementedException();
        }

        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            throw new NotImplementedException();
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
            ClipboardUpdate?.Invoke(null, e);
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

    public static class Extensions
    {
        public static string HashSHA1(this string data)
        {
            return HashSHA1(Encoding.UTF8.GetBytes(data));
        }

        public static string HashSHA1(this byte[] data)
        {
            using (var algo = SHA1.Create())
            {
                var hash = algo.ComputeHash(data);

                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}