using BVGConnector.Exceptions;
using BVGConnector.Model;
using BVGConnector.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BVGConnector.Operations
{
    internal static class OperationsParserBVG
    {
        private const string BVG_CHANNEL = "channel";

        public static string GenerateTransactionJson(TransactionBVG transaction)
        {
            Dictionary<string, object> aux = new Dictionary<string, object>();
            aux.Add(ElementNames.BVG_GENERAL_DATA, transaction.GetGeneralData());
            aux.Add(ElementNames.BVG_OPERATION_DATA, transaction.GetOperationData());
            aux.Add(ElementNames.BVG_TECHNICAL_DATA, transaction.GetTecnicalData());

            string transactionJson = JsonConvert.SerializeObject(aux, Newtonsoft.Json.Formatting.Indented);
            return transactionJson;
        }

        public static Dictionary<string, object> ParseJsonToDictionary(string json)
        {
            Dictionary<string, object> aux = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return aux;
        }

        public static List<Dictionary<string, Object>> ParseJsonToList(string json)
        {
            List<Dictionary<string, Object>> aux = JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);
            return aux;
        }

        public static TransactionBVG ParseJsonToTransaction(string json)
        {
            TransactionBVG transaction = new TransactionBVG();
            if (json != null && json.Equals(""))
            {
                string message = "Response vacio o nulo";
                throw new ResponseException(message);
            }

            Dictionary<string, object> result = ParseJsonToDictionary(json);
            if (result.ContainsKey(ElementNames.BVG_ERROR_CODE))
            {
                string message = (string)result[ElementNames.BVG_ERROR_CODE] + " - " + (string)result[ElementNames.BVG_ERROR_MESSAGE];
                throw new ResponseException(message);
            }

            if (result.ContainsKey("error"))
            {
                var status = result["status"];
                string message = status + " - " + (string)result["message"];
                throw new ResponseException(message);
            }

            transaction.SetChannel((string)result[BVG_CHANNEL]);
            //transaction.setUrlHibridFormResuorces((string)result[ElementNames.BVG_URL_HIBRID_FROM_RESOURCES]);
            transaction.SetPublicRequestKeys((string)result[ElementNames.BVG_PUBLIC_REQUEST_KEY]);
            //transaction.setRequestKey((string)result[ElementNames.BVG_REQUEST_KEY]);
            //transaction.setTransactionID((string)result[ElementNames.BVG_TRANSACTION_ID_RESPONSE]);
            transaction.SetMerchantID((string)result[ElementNames.BVG_MERCHANTID]);

            return transaction;
        }

        public static PaymentMethodsBVG ParseJsonToPaymentMethod(string json)
        {
            PaymentMethodsBVG paymentMethodsBVG = new PaymentMethodsBVG();
            List<Dictionary<string, Object>> paymentMethodsBVGList = new List<Dictionary<string, Object>>();
            string message = "Response vacio o nulo";

            if (json != null && json.Equals(""))
            {
                throw new ResponseException(message);
            }

            try
            {
                List<Dictionary<string, Object>> jsonlist = ParseJsonToList(json);

                foreach (Dictionary<string, Object> jsonElement in jsonlist)
                {
                    Dictionary<string, Object> paymentMethodsBVGDic = new Dictionary<string, Object>();
                    paymentMethodsBVGDic.Add(ElementNames.BVG_ID_MEDIO_PAGO, (string)jsonElement[ElementNames.BVG_ID_MEDIO_PAGO]);
                    paymentMethodsBVGDic.Add(ElementNames.BVG_NOMBRE, (string)jsonElement[ElementNames.BVG_NOMBRE]);
                    paymentMethodsBVGDic.Add(ElementNames.BVG_TIPO_MEDI_PAGO, (string)jsonElement[ElementNames.BVG_TIPO_MEDI_PAGO]);
                    paymentMethodsBVGDic.Add(ElementNames.BVG_ID_BANCO, (string)jsonElement[ElementNames.BVG_ID_BANCO]);
                    paymentMethodsBVGDic.Add(ElementNames.BVG_NOMBRE_BANCO, (string)jsonElement[ElementNames.BVG_NOMBRE_BANCO]);
                    paymentMethodsBVGList.Add(paymentMethodsBVGDic);
                }

            }
            catch (JsonSerializationException)
            {
                Dictionary<string, Object> paymentMethodsBVGDic = new Dictionary<string, Object>();
                paymentMethodsBVGList.Add(paymentMethodsBVGDic);
            }
            catch (Exception ex)
            {
                throw new ResponseException(ex.Message);
            }

            paymentMethodsBVG.SetPaymentMethodsBVGList(paymentMethodsBVGList);

            return paymentMethodsBVG;
        }

        public static string GenerateNotificationPushJson(NotificationPushBVG notificationPush)
        {
            Dictionary<string, object> aux = new Dictionary<string, object>();
            aux.Add(ElementNames.BVG_GENERAL_DATA, notificationPush.GetGeneralData());
            aux.Add(ElementNames.BVG_OPERATION_DATA, notificationPush.GetOperationData());
            aux.Add(ElementNames.BVG_TOKENIZATION_DATA, notificationPush.GetTokenizationData());

            string notificationPushJson = JsonConvert.SerializeObject(aux, Newtonsoft.Json.Formatting.Indented);

            return notificationPushJson;
        }

        public static NotificationPushBVG ParseJsonToNotificationPushBVG(string json)
        {
            NotificationPushBVG notificationPush = new NotificationPushBVG();
            if (json != null && json.Equals(""))
            {
                string message = "Response vacio o nulo";
                throw new ResponseException(message);
            }

            Dictionary<string, object> result = ParseJsonToDictionary(json);
            if (result.ContainsKey(ElementNames.BVG_ERROR_CODE))
            {
                string message = (string)result[ElementNames.BVG_ERROR_CODE] + " - " + (string)result[ElementNames.BVG_ERROR_MESSAGE];
                throw new ResponseException(message);
            }

            if (result.ContainsKey("error"))
            {
                var status = result["status"];
                string message = status + " - " + (string)result["message"];
                throw new ResponseException(message);
            }

            notificationPush.SetStatusCode((string)result[ElementNames.BVG_STATUS_CODE]);
            notificationPush.SetStatusMessage((string)result[ElementNames.BVG_STATUS_MESSAGE]);

            return notificationPush;
        }

        public static User ParseJsonToUser(string json)
        {
            Dictionary<string, object> aux = ParseJsonToDictionary(json);

            User user = new User();
            JObject jCredentials = (JObject)aux["Credentials"];
            JObject jresultado = (JObject)jCredentials["resultado"];

            if ((int)jresultado["codigoResultado"] != 0)
            {
                throw new ResponseException((string)jresultado["mensajeKey"] + " - " + (string)jresultado["mensajeResultado"]);
            }
            else
            {
                user.SetApiKey((string)jCredentials["APIKey"]);
                user.SetMerchant((string)jCredentials["merchantId"]);
            }

            return user;
        }
    }
}
