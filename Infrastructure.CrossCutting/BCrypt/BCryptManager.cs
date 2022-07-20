using BCryptNet = BCrypt.Net.BCrypt;

namespace Infrastructure.CrossCutting.BCrypt
{
    public static class BCryptManager
    {
        public static string Encrypt(string textToEncrypt)
        {
            if (string.IsNullOrEmpty(textToEncrypt)) return string.Empty;
            return BCryptNet.HashPassword(textToEncrypt);
        }

        public static bool Verify(string passwordText, string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordText)) return false;
            if (string.IsNullOrEmpty(passwordHash)) return false;
            return BCryptNet.Verify(passwordText, passwordHash);
        } 
    }
}