using System;

namespace BVGConnector.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException(String message)
            : base(message)
        {
        }

        public EmptyFieldException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}