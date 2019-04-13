using MUI.Graphics;
using System;

namespace AVA.Plugins.Clipboard.ClipboardDataTypes
{
    public abstract class ClipboardData
    {
        public abstract string Hash { get; }

        public abstract Image Icon { get; }

        public abstract string Name { get; }

        public virtual DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;

        public abstract void GetFormatAndData(out string format, out object data);

        public abstract void Restore();
    }
}