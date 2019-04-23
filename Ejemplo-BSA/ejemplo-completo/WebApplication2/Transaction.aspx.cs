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
using BVGConnector.Exceptions;
/**************************/
namespace WebApplication2
{
    public partial class Transaction : Page
    {
        public string response = "";
        public string responseError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, Object> trxResponse = null;
            BvgConnector connector = null;

            TestData testData = new TestData();
            testData = TestDataController.getTestDataById(Request["id_operation"]);
            if (testData.transaction == null)
            {
                testData.transaction = new OperationLogs();
            }

            var headers = new Dictionary<string, string>();
            Debug.WriteLine(testData.config_data["security"].ToString());
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
            generalData.Add(ElementNames.BVG_MERCHANT, (string) testData.config_data["merchant"]);
            generalData.Add(ElementNames.BVG_SECURITY, (string) testData.config_data["security"]);

            string operationDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            generalData.Add(ElementNames.BVG_OPERATION_DATE_TIME, operationDateTime);
            Globals.operationDateTime = operationDateTime;
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "127.0.0.1");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_OPERATION_TYPE, (string) testData.config_data["operationtype"]); //Ejemplo: "Compra"
            operationData.Add(ElementNames.BVG_OPERATION_ID, (string) testData.config_data["operacion"]);
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, testData.config_data["currency"] ); //Ejemplo: "032"
            operationData.Add(ElementNames.BVG_CONCEPT, (string) testData.config_data["concept"]);
            operationData.Add(ElementNames.BVG_AMOUNT, (string) testData.config_data["amount"]);
            
            List<string> availablePaymentMethods = new List<string>();
            foreach(string pMethod in (testData.config_data["availablepaymentmethods"]).ToString().Split(',')) {
                if(!String.IsNullOrEmpty(pMethod)) {
                    availablePaymentMethods.Add(pMethod);
                }
            }
            operationData.Add(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

            List<string> availableBanks = new List<string>();
            foreach (string aBank in (testData.config_data["availablebanks"]).ToString().Split(','))
            {
                if (!String.IsNullOrEmpty(aBank))
                {
                    availableBanks.Add(aBank);
                }
            }
            operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

            Dictionary<string, Object> buyerPreselection = new Dictionary<string, Object>();
            if(testData.config_data["buyerpreselectionmp"].ToString() != "") {
                buyerPreselection.Add(ElementNames.BVG_PAYMENT_METHODS_ID, testData.config_data["buyerpreselectionmp"]);
            }
            if(testData.config_data["buyerpreselectionbank"].ToString() != "") {
                buyerPreselection.Add(ElementNames.BVG_BANK_ID, testData.config_data["buyerpreselectionbank"]);
            }

            operationData.Add(ElementNames.BVG_BUYER_PRESELECTION, buyerPreselection);

            Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
            technicalData.Add(ElementNames.BVG_SDK, "Net");
            technicalData.Add(ElementNames.BVG_SDK_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_LANGUAGE_VERSION, "3.5");
            technicalData.Add(ElementNames.BVG_PLUGIN_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_NAME, "Comercio");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_VERSION, "3.1");
            technicalData.Add(ElementNames.BVG_CM_VERSION, "2.4");

            TransactionBVG transactionBVG = new TransactionBVG(generalData, operationData, technicalData);


            try
            {

                string jsonRequest = JsonConvert.SerializeObject(transactionBVG, Newtonsoft.Json.Formatting.Indented);
                transactionBVG = connector.Transaction(transactionBVG);
                trxResponse = transactionBVG.toDictionary();

                Debug.WriteLine("TRANSACTION REQUEST");
                //                Debug.WriteLine(JsonConvert.SerializeObject(transactionBVG, Newtonsoft.Json.Formatting.Indented));


                Debug.WriteLine("*****************************");

                Debug.WriteLine("TRANSACTION RESPONSE");
                this.response = JsonConvert.SerializeObject(trxResponse);
                Debug.WriteLine(this.response);
                // guardo en la base la respuesta

                testData.transaction.status = "ACEPTADO";
                testData.transaction.request_json = jsonRequest;
                testData.transaction.response_json = this.response;
                testData.config_data["publicRequestKey"] = (string)trxResponse["publicRequestKey"];
                TestDataController.saveData(testData);

                Globals.publicRequestKey = (string) trxResponse["publicRequestKey"];
                Globals.transactionState = "ACEPTADO";
            }

            catch (EmptyFieldException ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
                testData.transaction.status = "RECHAZADO";
                testData.transaction.response_json = ex.Message;
                TestDataController.saveData(testData);

                Globals.transactionState = "RECHAZADO";
                this.responseError = ex.Message;
            }
            catch (BVGConnector.Exceptions.ResponseException ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Globals.transactionState = "RECHAZADO";
                this.responseError = ex.Message;
                testData.transaction.status = "RECHAZADO";
                testData.transaction.response_json = ex.Message;
                TestDataController.saveData(testData);

            }
            catch (ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
                Globals.transactionState = "RECHAZADO";
                this.responseError = ex.Message;
                testData.transaction.status = "RECHAZADO";
                testData.transaction.response_json = ex.Message;
                TestDataController.saveData(testData);

            }
        }
    }
}