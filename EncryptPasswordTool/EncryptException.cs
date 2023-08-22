using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPasswordTool
{
    public class EncryptException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public EncryptException(string message) : base(message)
        {
        }
    }
}