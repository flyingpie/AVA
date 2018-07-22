using System.Linq;

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

        public static bool ContainsCaseInsensitive(this string source, string term)
        {
            return source.ToLowerInvariant().Contains(term.ToLowerInvariant());
        }
    }
}