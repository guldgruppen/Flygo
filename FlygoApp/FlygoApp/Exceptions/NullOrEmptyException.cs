using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Exceptions
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {
        }

        public NullOrEmptyException(string message) : base(message)
        {
            
        }

        public NullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
