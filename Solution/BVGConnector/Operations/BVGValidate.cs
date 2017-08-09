using BVGConnector.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;

namespace BVGConnector.Operations
{
    internal class BVGValidate
    {
        protected string[] prohibitedChars = { "?", "=", "&", "/", "\"", "'", "\\", ":", "#", ";" };

        protected bool ValidateInteger(Dictionary<string, object> array, string key)
        {
            string objectToValidate;
            int outValue;

            if (array.ContainsKey(key))
            {
                objectToValidate = (string)array[key];
                if (!Int32.TryParse(objectToValidate, out outValue))
                    throw new InvalidFieldException(key + " is not valid");
            }

            return true;
        }

        protected bool ValidateArrayInteger(Dictionary<string, object> array, string key)
        {
            object objectToValidate;
            int outValue;

            if (array.ContainsKey(key))
            {
                objectToValidate = array[key];
                if (objectToValidate != null && ((List<string>)objectToValidate).Count > 0)
                    foreach (string value in ((List<string>)objectToValidate))
                        if (!Int32.TryParse(value, out outValue))
                            throw new InvalidFieldException(key + " is not valid");
            }

            return true;
        }

        protected bool ValidateCurreny(Dictionary<string, object> array, string key, char split)
        {
            string objectToValidate;
            int outValue;

            if (array.ContainsKey(key))
            {
                objectToValidate = (string)array[key];

                string[] aux = objectToValidate.Split(split);

                if (aux.Length != 2)
                    throw new InvalidFieldException(key + " is not valid");

                if (!Int32.TryParse(aux[0], out outValue))
                    throw new InvalidFieldException(key + " is not valid");

                if (!Int32.TryParse(aux[1], out outValue))
                    throw new InvalidFieldException(key + " is not valid");


            }

            return true;
        }

        protected bool ValidateCurrenyCode(Dictionary<string, object> array, string key, string value)
        {
            if (!((string)(array[key])).Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidFieldException(key + " is not valid");
            }

            return true;
        }

        protected bool ValidateIPv4(Dictionary<string, object> array, string key)
        {
            IPAddress address;

            if (array.ContainsKey(key))
            {
                if (IPAddress.TryParse((string)array[key], out address))
                {
                    if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        throw new InvalidFieldException(key + " is not valid");
                    }
                }
            }

            return true;
        }

        protected bool ValidateOperationDateTime(Dictionary<string, object> array, string key, string format)
        {
            if (array.ContainsKey(key))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact((string)array[key], format, null);
                }
                catch (Exception e)
                {
                    throw new InvalidFieldException(key + " is not valid");
                }
            }

            return true;
        }

        protected bool ValidateStringCharacters(Dictionary<string, object> array, string key)
        {
            if (array.ContainsKey(key))
            {
                string stringToValidate = (string)array[key];
                foreach (string c in this.prohibitedChars)
                {
                    if (stringToValidate.Contains(c))
                    {
                        throw new InvalidFieldException(key + " with invalid characters");
                    }
                }
            }

            return true;
        }
    }
}
