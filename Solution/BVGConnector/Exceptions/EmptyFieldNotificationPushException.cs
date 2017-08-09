using System;

namespace BVGConnector.Exceptions
{
    public class EmptyFieldNotificationPushException : EmptyFieldException
    {
        public EmptyFieldNotificationPushException(String message)
            : base(message)
        {
        }

        public EmptyFieldNotificationPushException(String message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
