using System.Security.Cryptography;
using System.Text;

namespace AVA.Plugins.Clipboard
{
    public static class Hashing
    {
        public static string HashSHA1(this string data) => HashSHA1(Encoding.UTF8.GetBytes(data));

        public static string HashSHA1(this byte[] data)
        {
            using (var algo = SHA1.Create())
            {
                var hash = algo.ComputeHash(data);

                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}