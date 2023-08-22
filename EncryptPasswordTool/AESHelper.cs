using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPasswordTool
{
    public static class AESHelper
    {
        private static RijndaelManaged CreateAES(string pepper, string salt)
        {
            var aes = new RijndaelManaged() { KeySize = 256, BlockSize = 128, Mode = CipherMode.CBC };
            using (var sha256 = new SHA256Managed())
            {
                aes.Key = sha256.ComputeHash(Encoding.ASCII.GetBytes(pepper + salt));
                aes.GenerateIV();
            }

            return aes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="pepper"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string pepper, string salt)
        {
            if (plainText.IsNullOrWhiteSpace()) throw new EncryptException("Plain text is null or empty");
            if (salt.IsNullOrWhiteSpace()) throw new EncryptException("Salt is null or empty");

            var plainBytes = plainText.GetBytes();
            byte[] cipherBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var aes = CreateAES(pepper, salt))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    var bytes = memoryStream.ToArray();

                    cipherBytes = new byte[bytes.Length + aes.IV.Length];
                    Buffer.BlockCopy(aes.IV, 0, cipherBytes, 0, aes.IV.Length);
                    Buffer.BlockCopy(bytes, 0, cipherBytes, aes.IV.Length, bytes.Length);
                }
            }

            return cipherBytes.GetBase64();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="pepper"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string pepper, string salt)
        {
            if (cipherText.IsNullOrWhiteSpace()) throw new EncryptException("Cipher text is null or empty");
            if (salt.IsNullOrWhiteSpace()) throw new EncryptException("Salt is null or empty");

            var cipherBytes = cipherText.GetBytesFromBase64();
            byte[] plainBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var aes = CreateAES(pepper, salt))
                {
                    var iv = new byte[aes.IV.Length];
                    var actualCiphierBytes = new byte[cipherBytes.Length - aes.IV.Length];
                    Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
                    Buffer.BlockCopy(cipherBytes, iv.Length, actualCiphierBytes, 0, actualCiphierBytes.Length);

                    aes.IV = iv;
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(actualCiphierBytes, 0, actualCiphierBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        plainBytes = memoryStream.ToArray();
                    }
                }
            }

            return plainBytes.GetString();
        }
    }
}