using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.CrossCutting.Cryptography
{
    public class CryptographyManager
    {
        private static readonly byte[] IvSha = { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

        public static string Encrypt(string value, string secretKey)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var mySha256 = SHA256.Create();
                var key = mySha256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                var encrypt = Aes.Create();
                encrypt.Mode = CipherMode.CBC;
                encrypt.Key = key;
                encrypt.IV = IvSha;

                var memoryStream = new MemoryStream();
                var aesEncrypt = encrypt.CreateEncryptor();
                var cryptoStream = new CryptoStream(memoryStream, aesEncrypt, CryptoStreamMode.Write);
                var plainBytes = Encoding.ASCII.GetBytes(value);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                var cipherBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                var cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
                return EncryptUrlString(cipherText);
            }

            return string.Empty;
        }

        public static string Decrypt(string value, string secretKey)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var mySha256 = SHA256.Create();
                var key = mySha256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                var encrypt = Aes.Create();
                encrypt.Mode = CipherMode.CBC;
                encrypt.Key = key;
                encrypt.IV = IvSha;

                var memoryStream = new MemoryStream();
                var aesDecrypt = encrypt.CreateDecryptor();
                var cryptoStream = new CryptoStream(memoryStream, aesDecrypt, CryptoStreamMode.Write);

                string plainText;

                try
                {
                    value = DecryptUrlString(value);
                    var cipherBytes = Convert.FromBase64String(value);
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    var plainBytes = memoryStream.ToArray();
                    plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
                }
                finally
                {
                    memoryStream.Close();
                    cryptoStream.Close();
                }

                return plainText;
            }

            return string.Empty;
        }

        public static string EncryptUrlString(string text)
        {
            return text.Replace('+', '-').Replace('/', '_');
        }

        public static string DecryptUrlString(string text)
        {
            return text.Replace('-', '+').Replace('_', '/');
        }
    }
}