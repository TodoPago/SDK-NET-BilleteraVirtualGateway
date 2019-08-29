using System;
using System.Collections.Generic;
using BVGConnector;
using BVGConnector.Model;
using BVGConnector.Exceptions;
using BVGConnector.Utils;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace BVGTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("init BVGConnectorSample");

            BVGConnectorSample bvg = new BVGConnectorSample();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Transaction");
            bvg.TransactionBVG();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("DiscoverPaymentMethod");
            bvg.DiscoverPaymentMethodBVG();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("NotificationPush");
            bvg.NotificationPushBVG();

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("GetCredentials");
            bvg.GetCredentials();

            Console.Read();
        }

        private class BVGConnectorSample
        {
            //Connector
            private BvgConnector connector;

            //Authentification and Endpoint
            private string authorization = "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE";

            //Constructor
            public BVGConnectorSample()
            {
                var headers = new Dictionary<String, String>();
                headers.Add("Authorization", authorization);

                //Override SSL security - must be removed on PRD
                //System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                connector = new BvgConnector(BvgConnector.developerEndpoint, headers);
            }

            //--------------------- BVG --------------------------
            public void TransactionBVG()
            {
                TransactionBVG trasactionBVG = new TransactionBVG();

                try
                {
                    trasactionBVG = connector.Transaction(GetTransaction());
                    printDictionary(trasactionBVG.toDictionary(), "");
                }
                catch (EmptyFieldException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ConnectionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            private TransactionBVG GetTransaction()
            {
                Dictionary<string, Object> generalData = new Dictionary<string, Object>();
                generalData.Add(ElementNames.BVG_MERCHANT, "41702");
                generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
                generalData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20170308041300");
                generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");

                Dictionary<string, Object> operationData = new Dictionary<string, Object>();
                operationData.Add(ElementNames.BVG_OPERATION_TYPE, "Compra");
                operationData.Add(ElementNames.BVG_OPERATION_ID, "12345");
                operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
                operationData.Add(ElementNames.BVG_CONCEPT, "Compra");
                operationData.Add(ElementNames.BVG_AMOUNT, "10,00");

                List<string> availablePaymentMethods = new List<string>();
                availablePaymentMethods.Add("1");
                availablePaymentMethods.Add("42");
                operationData.Add(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

                List<string> availableBanks = new List<string>();
                availableBanks.Add("6");
                availableBanks.Add("24");
                availableBanks.Add("29");
                operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

                Dictionary<string, Object> buyerPreselection = new Dictionary<string, Object>();
                buyerPreselection.Add(ElementNames.BVG_PAYMENT_METHODS_ID, "42");
                buyerPreselection.Add(ElementNames.BVG_BANK_ID, "6");
                operationData.Add(ElementNames.BVG_BUYER_PRESELECTION, buyerPreselection);

                Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
                technicalData.Add(ElementNames.BVG_SDK, "Net");
                technicalData.Add(ElementNames.BVG_SDK_VERSION, "1.0");
                technicalData.Add(ElementNames.BVG_LANGUAGE_VERSION, "3.5");
                technicalData.Add(ElementNames.BVG_PLUGIN_VERSION, "1.0");
                technicalData.Add(ElementNames.BVG_ECOMMERCE_NAME, "Bla");
                technicalData.Add(ElementNames.BVG_ECOMMERCE_VERSION, "3.1");
                technicalData.Add(ElementNames.BVG_CM_VERSION, "2.4");

                TransactionBVG trasactionBVG = new TransactionBVG(generalData, operationData, technicalData);

                return trasactionBVG;
            }

            public void DiscoverPaymentMethodBVG()
            {
                PaymentMethodsBVG paymentMethodsBVG = new PaymentMethodsBVG();

                try
                {
                    paymentMethodsBVG = connector.DiscoverPaymentMethodBVG();

                    foreach (Dictionary<string, Object> elementDic in paymentMethodsBVG.GetPaymentMethodsBVGList())
                    {
                        printDictionary(elementDic, "");
                    }
                }
                catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ConnectionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public void NotificationPushBVG()
            {
                NotificationPushBVG notificationPushBVG = new NotificationPushBVG();

                try
                {
                    notificationPushBVG = connector.NotificationPush(GetNotificationPushBVG());
                    printDictionary(notificationPushBVG.toDictionary(), "");
                }
                catch (EmptyFieldException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ConnectionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            private NotificationPushBVG GetNotificationPushBVG()
            {
                Dictionary<string, Object> generalData = new Dictionary<string, Object>();
                generalData.Add(ElementNames.BVG_MERCHANT, "41702");
                generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
                generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");
                generalData.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, "f50208ea-be00-4519-bf85-035e2733d09e");
                generalData.Add(ElementNames.BVG_OPERATION_NAME, "Compra");

                Dictionary<string, Object> operationData = new Dictionary<string, Object>();
                operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "-1");
                operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
                operationData.Add(ElementNames.BVG_ID_GATEWAY, "8");
                operationData.Add(ElementNames.BVG_RESULT_MESSAGE, "Aprobada");
                operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20160704085736");
                operationData.Add(ElementNames.BVG_TICKET_MUNBER, "7866463542424");
                operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "455422446756567");
                operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
                operationData.Add(ElementNames.BVG_OPERATION_ID, "1234");
                operationData.Add(ElementNames.BVG_CONCEPT, "compra");
                operationData.Add(ElementNames.BVG_AMOUNT, "10,99");
                operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

                Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
                tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, "sydguyt3e862t76ierh76487638rhkh7");
                tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4507XXXXXXXX0001");

                NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
                return notificationPushBVG;
            }

            public void GetCredentials()
            {
                User user = new User();

                try
                {
                    user = connector.GetCredentials(GetUser());
                    connector.SetAuthorize(user.GetApiKey());
                }
                catch (EmptyFieldException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(user.toString());
            }

            private User GetUser()
            {
                String mail = "test@Test.com.ar";
                String pass = "pass1234";
                User user = new User(mail, pass);
                return user;
            }

            //Utils

            /// <summary>
            /// Permite emular la validación del Certificado SSL devolviendo true siempre
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="certificate"></param>
            /// <param name="chain"></param>
            /// <param name="sslPolicyErrors"></param>
            /// <returns>bool true</returns>
            private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            private void printDictionary(Dictionary<string, object> p, string tab)
            {
                foreach (string k in p.Keys)
                {
                    if (p[k] != null && p[k].GetType().ToString().Contains("System.Collections.Generic.Dictionary"))//.ToString().Contains("string"))
                    {
                        Console.WriteLine(tab + "- " + k);
                        Dictionary<string, object> n = (Dictionary<string, object>)p[k];
                        printDictionary(n, tab + "  ");
                    }
                    else
                    {
                        Console.WriteLine(tab + "- " + k + ": " + p[k]);
                    }
                }
            }
        }
    }
}
