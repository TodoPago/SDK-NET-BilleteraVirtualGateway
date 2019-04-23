using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Diagnostics;
using Newtonsoft.Json;
using WebApplication2.App_Start;
/*** TP  *****************/
using BVGConnector;
using BVGConnector.Utils;
using BVGConnector.Model;
/**************************/

namespace WebApplication2
{
    public partial class PushNotification : Page
    {
        public string response = "";
        public string responseError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            BvgConnector connector = null;

            TestData testData = new TestData();
            testData = TestDataController.getTestDataById(Request["id_operation"]);
            if (testData.push_notification == null)
            {
                testData.push_notification = new OperationLogs();
            }
            
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", testData.config_data["security"].ToString());

            if (String.Equals("test", testData.config_data["environment"]))
            {
                connector = new BvgConnector(BvgConnector.developerEndpoint, headers); // EndPoint de Developer
            }
            if (String.Equals("prod", testData.config_data["environment"]))
            {
                connector = new BvgConnector(BvgConnector.productionEndpoint, headers); // EndPoint de Produccion
            }

            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, (string)testData.config_data["merchant"]);
            generalData.Add(ElementNames.BVG_SECURITY, (string)testData.config_data["security"]);
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "127.0.0.1");
            generalData.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, (!String.IsNullOrEmpty(Globals.publicRequestKey)) ? Globals.publicRequestKey : "5516e585-c0b6-447c-bbb0-8ee1fa75d8ca");
            generalData.Add(ElementNames.BVG_OPERATION_NAME, "Compra");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "-1");
            operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
            operationData.Add(ElementNames.BVG_ID_GATEWAY, "1");
            operationData.Add(ElementNames.BVG_RESULT_MESSAGE, Globals.status);
            operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, Globals.operationDateTime);
            operationData.Add(ElementNames.BVG_TICKET_MUNBER, Globals.ticket);
            operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, (string)testData.config_data["currency"]);
            operationData.Add(ElementNames.BVG_OPERATION_ID, (string)testData.config_data["operacion"]);
            operationData.Add(ElementNames.BVG_CONCEPT, (string)testData.config_data["concept"]);
            operationData.Add(ElementNames.BVG_AMOUNT, (string)testData.config_data["amount"]);
            operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

            Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
            tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, Globals.token_decidir);
            tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4507XXXXXXXX7787");

            Debug.WriteLine("NOTIFICATION PUSH RESPONSE");
            Debug.WriteLine("general data = " + JsonConvert.SerializeObject(generalData));
            Debug.WriteLine("operation data = " + JsonConvert.SerializeObject(operationData));
            Debug.WriteLine("tokenization data = " + JsonConvert.SerializeObject(tokenizationData));
            Debug.WriteLine("*****************************");
            Dictionary<string, Object> request_push = new Dictionary<string, Object>
                {
                    { "generalData",   generalData },
                    { "operationData", operationData },
                    { "tokenizationData", tokenizationData }
                };
            try
            {
                NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
                notificationPushBVG = connector.NotificationPush(notificationPushBVG);
                var notificationPushResponse = notificationPushBVG.toDictionary();

                Debug.WriteLine("NOTIFICATION PUSH RESPONSE");
                this.response = JsonConvert.SerializeObject(notificationPushResponse);
                Debug.WriteLine(this.response);
                Debug.WriteLine("*****************************");
                Globals.pushNotifState = "ACEPTADO";

                testData.push_notification.status = Globals.pushNotifState;               
                testData.push_notification.request_json = JsonConvert.SerializeObject(request_push, Formatting.Indented);
                testData.push_notification.response_json = this.response;
                TestDataController.saveData(testData);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Globals.pushNotifState = "RECHAZADO";
                this.responseError = ex.Message;

                testData.push_notification.status = Globals.pushNotifState;
                testData.push_notification.request_json = JsonConvert.SerializeObject(request_push, Formatting.Indented);
                testData.push_notification.response_json = ex.Message;
                TestDataController.saveData(testData);
            }

        }
    }
}