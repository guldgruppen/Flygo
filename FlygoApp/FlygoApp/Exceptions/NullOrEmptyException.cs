using System;

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
