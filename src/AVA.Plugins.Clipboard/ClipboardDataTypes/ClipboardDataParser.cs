using System.Windows.Forms;

namespace AVA.Plugins.Clipboard.ClipboardDataTypes
{
    public abstract class ClipboardDataParser
    {
        public abstract bool TryParse(IDataObject dataObject, out ClipboardData data);
    }
}