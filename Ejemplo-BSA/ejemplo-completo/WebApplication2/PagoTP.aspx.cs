﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI;
using WebApplication2.App_Start;

namespace WebApplication2
{
    public partial class PagoTP : Page
    {
        public string publicRequestKey;
        public string merchant_todopago;

        protected void Page_Load(object sender, EventArgs e)
        {
            TestData testData = new TestData();
            testData = TestDataController.getTestDataById(Request["id_operation"]);

            publicRequestKey = testData.config_data["publicRequestKey"];
            merchant_todopago = testData.config_data["merchant"];
            if (testData.formTP == null)
            {
                testData.formTP = new OperationLogs();
            }

            Globals.token_todopago = Request["token_todopago"];
            Globals.token_date = Request["token_date"];
            Globals.payment_method_id = Request["payment_method_id"];
            if (String.IsNullOrEmpty(Globals.token_todopago))
            {
                Globals.pagoTPState = "RECHAZADO";
                testData.formTP.status = Globals.pagoTPState;
                testData.formTP.request_json = "{}";
                testData.formTP.response_json = "{}";
                TestDataController.saveData(testData);
            } else
            {
                Globals.pagoTPState = "ACEPTADO";
                testData.formTP.status = Globals.pagoTPState;
                testData.formTP.request_json = "{}";
                Dictionary<string, Object> response_form = new Dictionary<string, Object>
                {
                    { "token_todopago",  Globals.token_todopago },
                    { "token_date", Globals.token_date },
                    { "payment_method_id", Globals.payment_method_id }
                };
                testData.formTP.response_json = JsonConvert.SerializeObject(response_form, Formatting.Indented);
                TestDataController.saveData(testData);
                
                Response.Redirect("Default.aspx");
            }
        }

    }
}