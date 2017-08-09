using System;
using System.Runtime.Serialization;

namespace BVGConnector.Exceptions
{
    public class EmptyFieldUserException : Exception
    {
        public EmptyFieldUserException()
        {
        }

        public EmptyFieldUserException(string message) : base(message)
        {
        }

        public EmptyFieldUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyFieldUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}