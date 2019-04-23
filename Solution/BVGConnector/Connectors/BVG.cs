using BVGConnector.Model;
using BVGConnector.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BVGConnector.Utils;

namespace BVGConnector.Connectors
{
    public class BVG : RestConnector
    {
        private const string BVTP_TRANSACTION = "transactions/api/BSA/transaction/";
        private const string BVTP_PAYMENTMETHOD_DISCOVER = "discover/api/BSA/paymentMethod/discover";
        private const string BVTP_NOTIFICATION_PUSH = "transactions/api/BSA/transaction/notificacionPush";
        private const string CREDENTIALS = "Credentials";

        public BVG(string endpoint, Dictionary<string, string> headders)
              : base(endpoint, headders)
        {
        }

        public TransactionBVG Transaction(TransactionBVG transaction)
        {
            string URL = endpoint + BVTP_TRANSACTION;

            string json = OperationsParserBVG.GenerateTransactionJson(transaction);

            string result = ExecuteRequest(json, URL, METHOD_POST, true);

            return OperationsParserBVG.ParseJsonToTransaction(result);
        }

        public PaymentMethodsBVG DiscoverPaymentMethodBVG()
        {
            string URL = endpoint + BVTP_PAYMENTMETHOD_DISCOVER;

            string result = ExecuteRequest(null, URL, METHOD_GET, false);

            return OperationsParserBVG.ParseJsonToPaymentMethod(result);
        }

        public NotificationPushBVG NotificationPush(NotificationPushBVG notificationPush)
        {
            string publicRequestKey = (string)(notificationPush.GetGeneralData()[ElementNames.BVG_PUBLIC_REQUEST_KEY]);
            string URL = endpoint + BVTP_NOTIFICATION_PUSH;

            string json = OperationsParserBVG.GenerateNotificationPushJson(notificationPush);

            string result = ExecuteRequest(json, URL, METHOD_POST, true);

            return OperationsParserBVG.ParseJsonToNotificationPushBVG(result);
        }

        protected virtual string ExecuteRequest(string param, string url, string method, bool withApiKey)
        {
            string result = String.Empty;

            var httpWebRequest = GenerateHttpWebRequest(url, CONTENT_TYPE_APP_JSON, method, withApiKey);
            Debug.WriteLine(url);
            Debug.WriteLine(param);
            try
            {
                if (method == METHOD_POST || method == METHOD_PUT)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(param);
                    }
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }
    }
}
