using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Exceptions
{
    public class InfoWrongException : Exception
    {
        public InfoWrongException()
        {
        }

        public InfoWrongException(string message) : base(message)
        {
        }

        public InfoWrongException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
