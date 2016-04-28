using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Exceptions
{
    class LoginInfoWrongException : Exception
    {
        public LoginInfoWrongException()
        {
        }

        public LoginInfoWrongException(string message) : base(message)
        {
        }

        public LoginInfoWrongException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
