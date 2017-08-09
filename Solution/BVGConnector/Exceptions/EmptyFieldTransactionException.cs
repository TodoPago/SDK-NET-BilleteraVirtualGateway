using System;

namespace BVGConnector.Exceptions
{
    public class EmptyFieldTransactionException : EmptyFieldException
    {
        public EmptyFieldTransactionException(String message)
            : base(message)
        {
        }

        public EmptyFieldTransactionException(String message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
