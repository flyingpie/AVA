using System;
using System.Windows.Forms;
using MUI.Graphics;

namespace AVA.Plugins.Clipboard.ClipboardDataTypes
{
    public class FileDropClipboardDataParser : ClipboardDataParser
    {
        public override bool TryParse(IDataObject dataObject, out ClipboardData data)
        {
            throw new NotImplementedException();
        }
    }

    public class FileDropClipboardData : ClipboardData
    {
        public override string Hash => throw new NotImplementedException();

        public override Image Icon => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override void GetFormatAndData(out string format, out object data)
        {
            throw new NotImplementedException();
        }

        public override void Restore()
        {
            throw new NotImplementedException();
        }
    }
}