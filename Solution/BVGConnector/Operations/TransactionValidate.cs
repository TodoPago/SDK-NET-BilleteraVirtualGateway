using BVGConnector.Exceptions;
using BVGConnector.Model;
using BVGConnector.Utils;
using System;
using System.Collections.Generic;

namespace BVGConnector.Operations
{
    internal class TransactionValidate : BVGValidate
    {
        private const string BVG_CHANNEL = "channel";

        public Boolean ValidateTransaction(TransactionBVG transaction)
        {
            Boolean valid = true;
            Dictionary<string, Object> operationData = transaction.GetOperationData();
            Dictionary<string, Object> generalData = transaction.GetGeneralData();
            Dictionary<string, Object> tecnicalData = transaction.GetTecnicalData();

            valid = ValidateGeneralData(generalData) && ValidateOperationData(operationData);
            valid = valid && ValidateFormatTransaction(generalData, operationData);

            valid = valid && ValidateInvalidCharacters(generalData, operationData, tecnicalData);

            return valid;
        }

        private Boolean ValidateGeneralData(Dictionary<string, Object> generalData)
        {
            Boolean generalDataOK = true;
            String errorMessage = "";

            List<String> keyGeneralDataList = new List<String> { ElementNames.BVG_MERCHANT, ElementNames.BVG_SECURITY, ElementNames.BVG_OPERATION_DATE_TIME,
                                                                 ElementNames.BVG_REMOTE_IP_ADDRESS, BVG_CHANNEL };

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
                throw new EmptyFieldTransactionException(errorMessage);
            }

            return generalDataOK;
        }

        private Boolean ValidateOperationData(Dictionary<string, Object> operationData)
        {
            Boolean operationDataOK = true;
            String errorMessage = "";

            List<String> keyOperationDataList = new List<String> { ElementNames.BVG_OPERATION_ID, ElementNames.BVG_CURRENCY_CODE, ElementNames.BVG_AMOUNT };

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

                    //if (key.Equals(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS)){
                    //    if ((List<string>)operationData[key] == null || ((List<string>)operationData[key]).Count <= 0) {
                    //        i = keyOperationDataList.Count + 1;
                    //        errorMessage = key + " is  empty/null";
                    //        operationDataOK = false;
                    //    }
                    //}else{
                    if ((string)operationData[key] == null || ((string)operationData[key]).Equals(""))
                    {
                        i = keyOperationDataList.Count + 1;
                        errorMessage = key + " is  empty/null";
                        operationDataOK = false;
                    }
                    //}

                }

            }

            if (operationDataOK != true)
            {
                throw new EmptyFieldTransactionException(errorMessage);
            }

            return operationDataOK;
        }

        private Boolean ValidateFormatTransaction(Dictionary<string, object> generalData, Dictionary<string, object> operationData)
        {
            Boolean formatOK = true;

            formatOK = ValidateInteger(generalData, ElementNames.BVG_MERCHANT);

            formatOK = formatOK && ValidateOperationDateTime(generalData, ElementNames.BVG_OPERATION_DATE_TIME, "yyyyMMddHHmmss");
            formatOK = formatOK && ValidateIPv4(generalData, ElementNames.BVG_REMOTE_IP_ADDRESS);

            formatOK = formatOK && ValidateBuyerPreselection(operationData, ElementNames.BVG_BUYER_PRESELECTION);

            formatOK = formatOK && ValidateArrayInteger(operationData, ElementNames.BVG_AVAILABLE_PAYMENT_METHODS);
            formatOK = formatOK && ValidateArrayInteger(operationData, ElementNames.BVG_AVAILABLE_BANK);
            formatOK = formatOK && ValidateCurreny(operationData, ElementNames.BVG_AMOUNT, ',');

            formatOK = formatOK && ValidateCurrenyCode(operationData, ElementNames.BVG_CURRENCY_CODE, "032");

            return formatOK;
        }

        private bool ValidateBuyerPreselection(Dictionary<string, object> array, string key)
        {
            Dictionary<string, object> objectToValidate;

            if (array.ContainsKey(key))
            {
                objectToValidate = (Dictionary<string, object>)array[key];

                if (objectToValidate.Count > 0)
                {
                    ValidateInteger(objectToValidate, ElementNames.BVG_PAYMENT_METHODS_ID);
                    ValidateInteger(objectToValidate, ElementNames.BVG_BANK_ID);
                }
            }

            return true;
        }

        private bool ValidateInvalidCharacters(Dictionary<string, object> generalData, Dictionary<string, object> operationData, Dictionary<string, object> tecnicalData)
        {
            List<String> keyGeneralDataList = new List<String> { ElementNames.BVG_SECURITY, ElementNames.BVG_OPERATION_DATE_TIME, BVG_CHANNEL };
            List<String> keyOperationDataList = new List<String> { ElementNames.BVG_OPERATION_TYPE, ElementNames.BVG_CONCEPT };
            List<String> keyTecnicalDataList = new List<String> { ElementNames.BVG_SDK, ElementNames.BVG_SDK_VERSION, ElementNames.BVG_LANGUAGE_VERSION,
                    ElementNames.BVG_PLUGIN_VERSION, ElementNames.BVG_ECOMMERCE_NAME, ElementNames.BVG_ECOMMERCE_VERSION, ElementNames.BVG_CM_VERSION};

            foreach (string s in keyGeneralDataList)
                ValidateStringCharacters(generalData, s);

            foreach (string s in keyOperationDataList)
                ValidateStringCharacters(operationData, s);

            foreach (string s in keyTecnicalDataList)
                ValidateStringCharacters(tecnicalData, s);

            return true;
        }
    }
}
