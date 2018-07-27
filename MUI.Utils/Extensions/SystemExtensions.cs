using System.Linq;
using System.Text;

namespace System
{
    public static class SystemExtensions
    {
        public static string BufferToString(this byte[] buffer)
        {
            return new string(buffer.TakeWhile(c => c > 0).Select(b => (char)b).ToArray());
        }

        public static void ClearBuffer(this byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++) buffer[i] = 0;
        }

        public static void CopyToBuffer(this byte[] buffer, string text)
        {
            buffer.ClearBuffer();

            if (string.IsNullOrWhiteSpace(text)) return;

            var bytes = Encoding.UTF8.GetBytes(text);

            for (int i = 0; i < text.Length && i < buffer.Length; i++) buffer[i] = bytes[i];
        }

        public static bool ContainsCaseInsensitive(this string source, string term)
        {
            return source.ToLowerInvariant().Contains(term.ToLowerInvariant());
        }
    }
}