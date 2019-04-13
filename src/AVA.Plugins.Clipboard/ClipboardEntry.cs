//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MUI;
//using MUI.DI;
//using MUI.Graphics;
//using MUI.Logging;
//using MUI.Win32.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Security.Cryptography;
//using System.Text;
//using System.Windows.Forms;
//using AVA.Plugins.Clipboard.ClipboardDataTypes;

//namespace AVA.Plugins.Clipboard
//{
//    public class ClipboardEntry
//    {
//        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

//        public ClipboardData Data { get; set; }

//        public bool IsText { get; set; }

//        public string Text { get; set; }

//        public bool IsImage { get; set; }

//        public string FileName { get; set; }

//        public bool IsFileName { get; set; }

//        public Image Icon { get; set; }

//        public static ClipboardEntry Create()
//        {
//            var isText = Clipboard.ContainsText();
//            var isImage = Clipboard.ContainsImage();

//            var cd = new ClipboardEntry();

//            if (TryParseFromFileName(cd, out var fileName))
//            {
//                cd.IsFileName = true;
//                cd.FileName = fileName;
//                cd.Icon = cd.Icon = ResourceManager.Instance.LoadImageFromIcon(fileName);
//            }
//            else if (isText)
//            {
//                cd.IsText = true;
//                cd.Text = Clipboard.GetText();
//            }
//            else if (isImage)
//            {
//                cd.IsImage = true;
//                cd.Icon = LoadImage();
//            }

//            return cd;
//        }

//        private static bool TryParseFromFileName(ClipboardEntry cd, out string fn)
//        {
//            fn = null;
//            var fileName = Clipboard.GetData("FileName");

//            if (fileName is string path && File.Exists(path))
//            {
//                fn = path;
//                return true;
//            }

//            if (fileName is string[] paths && paths.Length > 0 && File.Exists(paths[0]))
//            {
//                fn = paths[0];
//                return true;
//            }

//            return false;
//        }

//        private static Image LoadImage()
//        {
//            var imgStream = new MemoryStream();

//            var img = Clipboard.GetImage();
//            img.Save(imgStream, ImageFormat.Png);

//            using (var thumbStream = new MemoryStream())
//            {
//                var ratio = (float)img.Height / (float)img.Width;
//                var thumb = img.GetThumbnailImage(50, (int)Math.Ceiling(50f * ratio), new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);
//                thumb.Save(thumbStream, ImageFormat.Png);

//                var bytes = thumbStream.ToArray();
//                return ResourceManager.Instance.LoadImage(bytes.HashSHA1(), bytes);
//            }
//        }

//        private string _hash;

//        public string Hash
//        {
//            get
//            {
//                if (_hash == null)
//                {
//                    if (IsText) _hash = Text.HashSHA1();

//                    if (IsImage) _hash = Image.ToArray().HashSHA1();

//                    if (!string.IsNullOrWhiteSpace(FileName)) return FileName.HashSHA1();
//                }

//                return _hash;
//            }
//        }

//        public void Restore()
//        {
//            //if (IsText)
//            //{
//            //    Clipboard.SetText(Text);
//            //    return;
//            //}

//            //if (IsImage)
//            //{
//            //    Clipboard.SetImage(System.Drawing.Image.FromStream(Image));
//            //    return;
//            //}

//            //if (!string.IsNullOrWhiteSpace(FileName))
//            //{
//            //    Clipboard.SetData("FileName", new[] { FileName });
//            //}
//        }

//        public override string ToString()
//        {
//            if (IsText) return string.Join("", Text.Take(10));

//            return "[IMAGE]";
//        }
//    }
//}
