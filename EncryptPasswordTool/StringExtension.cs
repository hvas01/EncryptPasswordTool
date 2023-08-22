using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPasswordTool
{
    /// <summary>
    /// This class includes extension functions for string.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string source, string encoding = "UTF8")
        {
            if (source.IsNullOrWhiteSpace()) return null;
            switch (encoding)
            {
                case "UTF8":
                    return Encoding.UTF8.GetBytes(source);

                case "UTF7":
                    return Encoding.UTF7.GetBytes(source);

                case "ASCII":
                    return Encoding.ASCII.GetBytes(source);

                default:
                    return Encoding.UTF8.GetBytes(source);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromBase64(this string source)
        {
            if (source.IsNullOrWhiteSpace()) return null;
            return Convert.FromBase64String(source);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            var result = string.IsNullOrWhiteSpace(source);
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool EqualsWithIgnoreCase(this string source, string dest)
        {
            if (source == null) return false;
            var result = source.Equals(dest, StringComparison.CurrentCultureIgnoreCase);
            return result;
        }
    }
}