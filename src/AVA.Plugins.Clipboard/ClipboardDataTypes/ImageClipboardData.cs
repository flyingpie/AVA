using System;
using System.Windows.Forms;
using MUI.Graphics;

namespace AVA.Plugins.Clipboard.ClipboardDataTypes
{
    public class ImageClipboardDataParser : ClipboardDataParser
    {
        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageClipboardData : ClipboardData
    {
        public override string Hash => throw new NotImplementedException();

        public override Image Icon => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override void Restore()
        {
            throw new NotImplementedException();
        }
    }
}