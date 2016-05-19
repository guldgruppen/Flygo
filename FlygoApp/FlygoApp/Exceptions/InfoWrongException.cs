using System;

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
