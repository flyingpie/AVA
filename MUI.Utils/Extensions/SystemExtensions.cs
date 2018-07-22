using System.Linq;

namespace MUI.Utils.Extensions
{
    public static class SystemExtensions
    {
        public static void ClearBuffer(this byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++) buffer[i] = 0;
        }

        public static string BufferToString(this byte[] buffer)
        {
            return new string(buffer.TakeWhile(c => c > 0).Select(b => (char)b).ToArray());
        }
    }
}