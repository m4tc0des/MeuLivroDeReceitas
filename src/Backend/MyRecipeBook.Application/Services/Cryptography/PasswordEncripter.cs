using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        public string Encrypt(string password)
        {
            var adicionalKey = "ABC";
            var newPassword = $"{password}{adicionalKey}";

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }

        private static string StringBytes(byte[] bytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (var b in bytes)
            {
                var x = b.ToString("x2");
                stringBuilder.Append(x);
            }
            return stringBuilder.ToString();
        }
    }
}
