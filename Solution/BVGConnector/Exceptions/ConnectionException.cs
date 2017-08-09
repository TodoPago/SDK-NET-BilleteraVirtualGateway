using System;

namespace BVGConnector.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException(String message)
            : base(message)
        {
        }

        public ConnectionException(String message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
