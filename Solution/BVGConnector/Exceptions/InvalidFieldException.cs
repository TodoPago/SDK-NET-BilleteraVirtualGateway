using System;

namespace BVGConnector.Exceptions
{
    public class InvalidFieldException : Exception
    {
        public InvalidFieldException(String message)
            : base(message)
        {
        }

        public InvalidFieldException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
