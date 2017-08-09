using BVGConnector.Exceptions;
using BVGConnector.Model;
using BVGConnector.Utils;
using System;
using System.Collections.Generic;

namespace BVGConnector.Operations
{
    internal class NotificationPushValidate : BVGValidate
    {
        public Boolean ValidateNotificationPush(NotificationPushBVG notificationPush)
        {
            Boolean valid = true;
            Dictionary<string, Object> generalData = notificationPush.GetGeneralData();
            Dictionary<string, Object> operationData = notificationPush.GetOperationData();
            Dictionary<string, Object> tokenizationData = notificationPush.GetTokenizationData();

            valid = ValidateGeneralData(generalData) && ValidateOperationData(operationData);
            valid = valid && ValidateTokenizationData(tokenizationData);
            valid = valid && ValidateFormatNotificationPush(generalData, operationData, tokenizationData);

            return valid;
        }

        private Boolean ValidateGeneralData(Dictionary<string, Object> generalData)
        {
            Boolean generalDataOK = true;
            String errorMessage = "";

            List<String> keyGeneralDataList = new List<String> { ElementNames.BVG_MERCHANT, ElementNames.BVG_SECURITY,
                                                                 ElementNames.BVG_OPERATION_NAME, ElementNames.BVG_PUBLIC_REQUEST_KEY };

            for (int i = 0; i < keyGeneralDataList.Count; i++)
            {
                string key = keyGeneralDataList[i];

                if (!generalData.ContainsKey(key))
                {
                    i = keyGeneralDataList.Count + 1;
                    errorMessage = key + " is required";
                    generalDataOK = false;
                }
                else
                {
                    if ((string)generalData[key] == null || ((string)generalData[key]).Equals(""))
                    {
                        i = keyGeneralDataList.Count + 1;
                        errorMessage = key + " is  empty/null";
                        generalDataOK = false;
                    }
                }
            }

            if (generalDataOK != true)
            {
                throw new EmptyFieldNotificationPushException(errorMessage);
            }

            return generalDataOK;
        }

        private Boolean ValidateOperationData(Dictionary<string, Object> operationData)
        {
            Boolean operationDataOK = true;
            String errorMessage = "";

            List<String> keyOperationDataList = new List<String> { ElementNames.BVG_OPERATION_ID, ElementNames.BVG_OPERATION_DATE_TIME,
                                                                   ElementNames.BVG_CURRENCY_CODE,ElementNames.BVG_AMOUNT,
                                                                   ElementNames.BVG_FACILITIES_PAYMENT};

            for (int i = 0; i < keyOperationDataList.Count; i++)
            {
                string key = keyOperationDataList[i];

                if (!operationData.ContainsKey(key))
                {
                    i = keyOperationDataList.Count + 1;
                    errorMessage = key + " is required";
                    operationDataOK = false;
                }
                else
                {
                    if ((string)operationData[key] == null || ((string)operationData[key]).Equals(""))
                    {
                        i = keyOperationDataList.Count + 1;
                        errorMessage = key + " is  empty/null";
                        operationDataOK = false;
                    }
                }
            }

            if (operationDataOK != true)
            {
                throw new EmptyFieldNotificationPushException(errorMessage);
            }

            return operationDataOK;
        }

        private Boolean ValidateTokenizationData(Dictionary<string, Object> tokenizationData)
        {
            Boolean tokenizationDataOK = true;
            String errorMessage = "";

            List<String> keyGeneralDataList = new List<String> { ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD };

            for (int i = 0; i < keyGeneralDataList.Count; i++)
            {
                string key = keyGeneralDataList[i];

                if (!tokenizationData.ContainsKey(key))
                {
                    i = keyGeneralDataList.Count + 1;
                    errorMessage = key + " is required";
                    tokenizationDataOK = false;
                }
                else
                {
                    if ((string)tokenizationData[key] == null || ((string)tokenizationData[key]).Equals(""))
                    {
                        i = keyGeneralDataList.Count + 1;
                        errorMessage = key + " is  empty/null";
                        tokenizationDataOK = false;
                    }
                }
            }

            if (tokenizationDataOK != true)
            {
                throw new EmptyFieldNotificationPushException(errorMessage);
            }

            return tokenizationDataOK;
        }

        private bool ValidateFormatNotificationPush(Dictionary<string, object> generalData, Dictionary<string, object> operationData, Dictionary<string, object> tokenizationData)
        {
            bool formatOK = true;

            formatOK = ValidateInteger(generalData, ElementNames.BVG_MERCHANT);

            formatOK = formatOK && ValidateIPv4(generalData, ElementNames.BVG_REMOTE_IP_ADDRESS);

            formatOK = formatOK && ValidateOperationDateTime(operationData, ElementNames.BVG_OPERATION_DATE_TIME, "yyyyMMddHHmmss");

//            formatOK = formatOK && ValidateInteger(operationData, ElementNames.BVG_RESULT_CODE_MEDIOPAGO);
//            formatOK = formatOK && ValidateInteger(operationData, ElementNames.BVG_RESULT_CODE_GATEWAY);
//            formatOK = formatOK && ValidateInteger(operationData, ElementNames.BVG_ID_GATEWAY);

            formatOK = formatOK && ValidateCurreny(operationData, ElementNames.BVG_AMOUNT, ',');

            formatOK = formatOK && ValidateCurrenyCode(operationData, ElementNames.BVG_CURRENCY_CODE, "032");

            formatOK = formatOK && ValidateInteger(operationData, ElementNames.BVG_FACILITIES_PAYMENT, 1, 12, 2);

            formatOK = formatOK && ValidateCredentialMask(tokenizationData, ElementNames.BVG_CREDENTIAL_MASK);

            formatOK = formatOK && ValidateInvalidCharacters(generalData, operationData, tokenizationData);

            return formatOK;
        }
        
        private bool ValidateCredentialMask(Dictionary<string, object> array, string key)
        {
            string objectToValidate;
            int outValue;

            if (array.ContainsKey(key))
            {
                objectToValidate = (string)array[key];

                string stringToValidate = objectToValidate.Substring(0, 4);
                if (!Int32.TryParse(stringToValidate, out outValue))
                    throw new InvalidFieldException(key + " is not valid");

                stringToValidate = objectToValidate.Substring(objectToValidate.Length - 4, 4);
                if (!Int32.TryParse(stringToValidate, out outValue))
                    throw new InvalidFieldException(key + " is not valid");
            }

            return true;
        }

        protected bool ValidateInteger(Dictionary<string, object> array, string key, int since, int until, int? length = null)
        {
            string objectToValidate;
            int outValue;

            if (array.ContainsKey(key))
            {
                objectToValidate = (string)array[key];

                if (objectToValidate.Length != length)
                    throw new InvalidFieldException(key + " is not valid");

                if (!Int32.TryParse(objectToValidate, out outValue))
                    throw new InvalidFieldException(key + " is not valid");

                if (outValue < since || outValue > until)
                    throw new InvalidFieldException(key + " is not valid");
            }

            return true;
        }

        private bool ValidateInvalidCharacters(Dictionary<string, object> generalData, Dictionary<string, object> operationData, Dictionary<string, object> tokenizationData)
        {
            List<String> keyGeneralDataList = new List<String> { ElementNames.BVG_SECURITY, ElementNames.BVG_OPERATION_NAME, ElementNames.BVG_PUBLIC_REQUEST_KEY };
            List<String> keyOperationDataList = new List<String> { ElementNames.BVG_RESULT_MESSAGE, ElementNames.BVG_CONCEPT };
            List<String> keyTokenizationDataList = new List<String> { ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, ElementNames.BVG_CREDENTIAL_MASK };

            foreach (string s in keyGeneralDataList)
                ValidateStringCharacters(generalData, s);

            foreach (string s in keyOperationDataList)
                ValidateStringCharacters(operationData, s);

            foreach (string s in keyTokenizationDataList)
                ValidateStringCharacters(tokenizationData, s);

            return true;
        }
    }
}
