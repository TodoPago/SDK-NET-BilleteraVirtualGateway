using System;

namespace BVGConnector.Exceptions
{
    public class EmptyFieldPassException : EmptyFieldException
    {
        public EmptyFieldPassException(String message)
           : base(message)
        {
        }

        public EmptyFieldPassException(String message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
