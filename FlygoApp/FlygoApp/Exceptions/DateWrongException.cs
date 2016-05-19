using System;

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
