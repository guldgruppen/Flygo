using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Exceptions
{
    public class LoginIsNullOrEmptyException : Exception
    {
        public LoginIsNullOrEmptyException()
        {
        }

        public LoginIsNullOrEmptyException(string message) : base(message)
        {
            
        }

        public LoginIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
