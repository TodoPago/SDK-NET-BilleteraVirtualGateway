using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Diagnostics;
using Decidir;
using Decidir.Model;
using Decidir.Constants;
using Newtonsoft.Json;
using WebApplication2.App_Start;

namespace WebApplication2
{
    public partial class PagoDecidir : Page
    {
        public string responseTokenizer = "";
        public string responsePayment = "";
        public string responseError = "";
        public static Dictionary<string, string> tpToDecidirMapper = initDecidirMapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetTokenResponse resultGetTokenResponse = null;

            TestData testData = new TestData();
            testData = TestDataController.getTestDataById(Request["id_operation"]);
            if (testData.decidir_payment == null)
            {
                testData.decidir_payment = new OperationLogs();
            }
            
            CardHolderIdentification card_holder_identification = new CardHolderIdentification();
            card_holder_identification.type = "DNI";
            card_holder_identification.number = "33222444";

            FraudDetectionBSA fraud_detection = new FraudDetectionBSA();
            fraud_detection.device_unique_identifier = "12345";


            //OBS: Al no poder acceder al formulario de TP si no vienen cargados los datos necesarios se completan con otros valores de prueba 
            CardTokenBsa card_token_bsa = new CardTokenBsa();
            card_token_bsa.public_token = (!String.IsNullOrEmpty(Globals.token_todopago) )? Globals.token_todopago : "96291105";
            card_token_bsa.issue_date = (!String.IsNullOrEmpty(Globals.token_date)) ? Globals.token_date:"20190108";
            card_token_bsa.card_holder_name = "Comprador";
            card_token_bsa.card_holder_identification = card_holder_identification;
            card_token_bsa.fraud_detection = fraud_detection;
            card_token_bsa.merchant_id = testData.config_data["merchant"];

            //Para el ambiente de desarrollo
            int environment = Ambiente.AMBIENTE_SANDBOX;
            if (testData.config_data["environment"] == "prod" ) {
                environment = Ambiente.AMBIENTE_PRODUCCION;
            }
            DecidirConnector decidir = new DecidirConnector(environment, (string) testData.config_data["key_private"], (string)testData.config_data["key_public"]);

            Debug.WriteLine("DECIDIR GET TOKEN REQUEST");
            string requestTokenizer = JsonConvert.SerializeObject(card_token_bsa);
            Debug.WriteLine(requestTokenizer);
            Debug.WriteLine("*****************************");
            try
            {
                resultGetTokenResponse = decidir.GetToken(card_token_bsa);
                string outputResutGetToken = JsonConvert.SerializeObject(resultGetTokenResponse);
                Debug.WriteLine("DECIDIR GET TOKEN RESPONSE");
                Debug.WriteLine(outputResutGetToken);
                Debug.WriteLine("*****************************");
                this.responseTokenizer = outputResutGetToken;
                Globals.token_decidir = resultGetTokenResponse.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Globals.pagoDecidirState = "RECHAZADO";
                this.responseError = ex.Message;
                testData.decidir_payment.status = Globals.pagoDecidirState;
                testData.decidir_payment.request_json = requestTokenizer;
                testData.decidir_payment.response_json = ex.Message;
                TestDataController.saveData(testData);

                return;
            }


            /********** PAYMENT DECIDIR **********/
            // DecidirConnector decidirConnector = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, (string)Globals.configData["key_private"], (string)Globals.configData["key_public"]);

            //Mapeo id medio de pago de TP a Decidir
            String todoPago_PaymentMethodId = (!String.IsNullOrEmpty(Globals.payment_method_id)) ? Globals.payment_method_id : "1"; //Visa por defecto
            String decidir_PaymentMethodId = tpToDecidirMapper[todoPago_PaymentMethodId];

            Payment payment = new Payment();
            PaymentResponse resultPaymentResponse = new PaymentResponse();

            payment.site_transaction_id = (string) testData.config_data["operacion"];
            payment.token = resultGetTokenResponse.id;
            payment.payment_type = "bsa";
            payment.payment_method_id = Convert.ToInt32(decidir_PaymentMethodId);
            payment.installments = Convert.ToInt32((string)testData.config_data["cuotas"]);
            payment.currency = (string) testData.config_data["currencydec"];
            payment.amount = Convert.ToDouble(((string)testData.config_data["amount"]).Replace(',','.'));
            payment.payment_type = "single";
            payment.sub_payments = new List<object>();


            Debug.WriteLine("PAYMENT REQUEST");
            string requestPayment = JsonConvert.SerializeObject(payment);
            Debug.WriteLine(requestPayment);
            Debug.WriteLine("*****************************");
            try
            {
                Debug.WriteLine("PAYMENT RESPONSE");
                resultPaymentResponse = decidir.Payment(payment);
                this.responsePayment = JsonConvert.SerializeObject(resultPaymentResponse);
                Debug.WriteLine(this.responsePayment);
                Debug.WriteLine("*****************************");
                Globals.status = resultPaymentResponse.status;
                Globals.ticket = resultPaymentResponse.status_details.ticket;
                Globals.pagoDecidirState = "ACEPTADO";

                testData.decidir_payment.status = Globals.pagoDecidirState;
                testData.decidir_payment.request_json = requestPayment;
                testData.decidir_payment.response_json = this.responsePayment;
                TestDataController.saveData(testData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Globals.pagoDecidirState = "RECHAZADO";
                this.responseError = ex.Message;
                testData.decidir_payment.status = Globals.pagoDecidirState;
                testData.decidir_payment.request_json = requestPayment;
                testData.decidir_payment.response_json = ex.Message;
                TestDataController.saveData(testData);
            }
           

        }

        private static Dictionary<string, string> initDecidirMapper()
        {
            Dictionary<string, string> initData = new Dictionary<string, string>();
            initData.Add("1", "65"); //AMEX
            initData.Add("42", "1"); //Visa Credito
            initData.Add("43", "31"); //Visa Debito
            return initData;
        }
    }
}