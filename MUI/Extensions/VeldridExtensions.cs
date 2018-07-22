using System.Linq;
using Veldrid;

namespace MUI.Extensions
{
    public static class VeldridExtensions
    {
        public static bool IsKeyDown(this InputSnapshot snapshot, Key key, ModifierKeys? modifier = null)
        {
            return snapshot.KeyEvents
                .Where(ke => ke.Key == key)
                .Where(ke => !modifier.HasValue || ke.Modifiers == modifier)
                .Where(ke => ke.Down)
                .Any();
        }
    }
}