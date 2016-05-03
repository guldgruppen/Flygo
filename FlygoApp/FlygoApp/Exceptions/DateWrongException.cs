using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Exceptions
{
    public class DateWrongException : Exception
    {
        public DateWrongException()
        {
        }

        public DateWrongException(string message) : base(message)
        {
        }

        public DateWrongException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
