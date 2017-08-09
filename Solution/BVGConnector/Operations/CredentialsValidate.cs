using BVGConnector.Exceptions;
using BVGConnector.Model;
using System;

namespace BVGConnector.Operations
{
    internal class CredentialsValidate
    {
        public Boolean ValidateCredentials(User user)
        {
            Boolean result = true;

            if (user != null)
            {
                if (user.GetUser() == null || user.GetUser().Equals(""))
                    throw new EmptyFieldUserException("User/Mail is empty");

                if (user.GetPassword() == null || user.GetPassword().Equals(""))
                    throw new EmptyFieldPassException("Password is empty");
            }
            else
                throw new EmptyFieldPassException("User is null");

            return result;
        }
    }
}
