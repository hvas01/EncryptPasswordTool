using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPasswordTool
{
    public static class SaltHelper
    {
        /// <summary>
        /// Generate a random salt
        /// </summary>
        /// <param name="maxLength">Max length of salt array</param>
        /// <returns></returns>
        public static string GenerateSalt(int maxLength)
        {
            var salt = new byte[maxLength];
            using (var rncCsp = new RNGCryptoServiceProvider())
            {
                rncCsp.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}