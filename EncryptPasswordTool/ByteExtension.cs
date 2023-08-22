using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPasswordTool
{
    /// <summary>
    ///
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetBase64(this byte[] source)
        {
            if (source == null) return string.Empty;
            return Convert.ToBase64String(source);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] source, string encoding = "UTF8")
        {
            if (source == null) return string.Empty;
            switch (encoding)
            {
                case "UTF8":
                    return Encoding.UTF8.GetString(source);

                case "UTF7":
                    return Encoding.UTF7.GetString(source);

                case "ASCII":
                    return Encoding.ASCII.GetString(source);

                default:
                    return Encoding.UTF8.GetString(source);
            }
        }
    }
}