using MUI;
using MUI.Graphics;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AVA.Plugins.Clipboard.ClipboardDataTypes
{
    public class TextClipboardDataParser : ClipboardDataParser
    {
        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            data = null;

            try
            {
                var textData = dataObject.GetData(DataFormats.Text, true) as string;

                if (textData != null)
                {
                    data = new TextClipboardData(textData)
                    {
                    };
                }
            }
            catch
            { }

            return false;
        }
    }

    public class TextClipboardData : ClipboardData
    {
        private string _text;

        public override string Hash { get; }

        public override Image Icon { get; }

        public override string Name { get; }

        public TextClipboardData(string text)
        {
            Hash = text.HashSHA1();
            Icon = ResourceManager.Instance.DefaultImage;
            Name = string.Concat(text.Take(100));

            _text = text;
        }

        public override void Restore()
        {
            throw new NotImplementedException();
        }

        public override void GetFormatAndData(out string format, out object data)
        {
            throw new NotImplementedException();
        }
    }
}