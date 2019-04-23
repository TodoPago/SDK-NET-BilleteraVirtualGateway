using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.App_Start;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        public List<TestData> test_list = new List<TestData>();
        public string transactionState = Globals.transactionState;
        public string pagoTPState = Globals.pagoTPState;
        public string pagoDecidirState = Globals.pagoDecidirState;
        public string pushNotifState = Globals.pushNotifState;

        public StreamWriter file { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            test_list = TestDataController.GetAllData();

            foreach (TestData test in test_list)
            {
                if (test.transaction != null) {
                    Debug.WriteLine(test.transaction.status);
                    if (string.IsNullOrEmpty(test.transaction.request_json)) {
                        test.transaction.request_json = (string)JsonConvert.SerializeObject(test.transaction.request);
                    }

                    if (string.IsNullOrEmpty(test.transaction.response_json))
                    {
                        test.transaction.response_json = (string)JsonConvert.SerializeObject(test.transaction.response);
                    }
                }
                else
                {
                    test.transaction = new OperationLogs();
                }


                if (test.formTP != null)
                {
                    Debug.WriteLine(test.formTP.status);
                    test.formTP.request_json = (string)JsonConvert.SerializeObject(test.formTP.request);
                    test.formTP.response_json = (string)JsonConvert.SerializeObject(test.formTP.response);
                }
                else
                {
                    test.formTP = new OperationLogs();
                }

                if (test.decidir_payment != null)
                {
                    Debug.WriteLine(test.decidir_payment.status);
                    test.decidir_payment.request_json = (string)JsonConvert.SerializeObject(test.decidir_payment.request);
                    test.decidir_payment.response_json = (string)JsonConvert.SerializeObject(test.decidir_payment.response);
                }
                else
                {
                    test.decidir_payment = new OperationLogs();
                }

                if (test.push_notification != null)
                {
                    Debug.WriteLine(test.push_notification.status);
                    test.push_notification.request_json = (string)JsonConvert.SerializeObject(test.push_notification.request);
                    test.push_notification.response_json = (string)JsonConvert.SerializeObject(test.push_notification.response);
                }
                else
                {
                    test.push_notification = new OperationLogs();
                }

            }

        }

    }
}