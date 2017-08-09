using System;

namespace BVGConnector.Exceptions
{
    public class ResponseException : Exception
    {
        public ResponseException(String message)

            : base(message)
        {
        }

        public ResponseException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
