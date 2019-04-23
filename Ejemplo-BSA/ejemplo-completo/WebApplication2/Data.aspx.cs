using BVGConnector.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.App_Start;

namespace WebApplication2
{
    public partial class Data : Page
    {
        public List<TestData> tests = new List<TestData>();
        public Dictionary<string, string> configData = initConfigData();
        public string id_operation = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("**** PAGE LOAD ****");
            TestData testData = new TestData(); 
        
            switch (Request["action"])
            {
                case "new":
                    Debug.WriteLine("*** asigno nuevo ID");
                    id_operation = "sdk_net_" + new Random().Next();
                    testData.id_operation = id_operation;
                    testData.config_data = initConfigData();

                    break;
                case "edit":
                    Debug.WriteLine("*** estoy en EDIT " + Request["id_operation"]);
                    id_operation = Request["id_operation"];
                    testData = TestDataController.getTestDataById(id_operation);
                    if (testData.config_data != null)
                    {
                        this.configData = testData.config_data;
                        fill_config_data_form(testData.config_data);
                    }
                    
                    break;
                case "save":
                    Debug.WriteLine("*** estoy en SAVE : " + Request["id_operation"]); 
                    testData = TestDataController.getTestDataById(Request["id_operation"]);
                    testData.id_operation = Request["id_operation"];
                    id_operation = testData.id_operation;
                    fill_config_data_form();
                    testData.config_data = this.configData; 

                    TestDataController.saveData(testData);

                    Response.Redirect("Default.aspx");
                    break;
                case "delete":
                    TestDataController.delete(Request["id_operation"]);
                    Response.Redirect("Default.aspx");
                    break;
                default:
                    id_operation = "sdk_net_" + new Random().Next();
                    testData.id_operation = id_operation;
                    testData.config_data = initConfigData();
                    break;
            }

          
            if (!String.IsNullOrEmpty(Request["security"]))
            {
                Response.Redirect("Default.aspx");
            }


        }


        private void fill_config_data_form(Dictionary<string, string>  config_data = null )
        {
            this.configData["environment"] = this.get_field("environment");
            this.configData["key_public"] = this.get_field("key_public");
            this.configData["key_private"] = this.get_field("key_private");
            this.configData["operacion"] = id_operation;
            this.configData["currencydec"] = this.get_field("currencydec");
            this.configData["user_id"] = this.get_field("user_id");
            this.configData["user_mail"] = this.get_field("user_mail");
            this.configData["decpaymentmethod"] = this.get_field("decpaymentmethod");
            this.configData["bin"] = this.get_field("bin");
            this.configData["establishment"] = this.get_field("establishment");
            this.configData["merchant"] = this.get_field("merchant");
            this.configData["security"] = this.get_field("security");
            this.configData["channel"] = this.get_field("channel");
            this.configData["operationtype"] = this.get_field("operationtype");
            this.configData["currency"] = this.get_field("currency");
            this.configData["concept"] = this.get_field("concept");
            this.configData["amount"] = this.get_field("amount");
            this.configData["buyerpreselectionmp"] = this.get_field("buyerpreselectionmp");
            this.configData["buyerpreselectionbank"] = this.get_field("buyerpreselectionbank");
            this.configData["availablepaymentmethods"] = this.get_field("availablepaymentmethods");
            this.configData["availablebanks"] = this.get_field("availablebanks");

            Globals.configData = this.configData;
        }

        private string get_field(string field)
        {
            return (!String.IsNullOrEmpty(Request[field]) ? Request[field] : this.configData[field].ToString());
        }

        private static Dictionary<string, string> initConfigData()
        {
            Dictionary<string, string> initData = new Dictionary<string, string>();
            initData.Add("environment", "test");
            initData.Add("key_public", "");
            initData.Add("key_private", "");
            initData.Add("operacion", "sdk_net_" + new Random().Next());
            initData.Add("currencydec", "ARS");
            initData.Add("user_id", "user");
            initData.Add("user_mail", "user@email.com");
            initData.Add("decpaymentmethod", "");
            initData.Add("bin", "");
            initData.Add("cuotas", "6");
            initData.Add("establishment", "Ejemplo");
            initData.Add("merchant", "1234");
            initData.Add("security", "");
            initData.Add("channel", "BVTP");
            initData.Add("operationtype", "Compra");
            initData.Add("currency", "32");
            initData.Add("concept", "compra");
            initData.Add("amount", "10,99");
            initData.Add("buyerpreselectionmp", "");
            initData.Add("buyerpreselectionbank", "");
            initData.Add("availablepaymentmethods", "");
            initData.Add("availablebanks", "");

            return initData;
        }
    }
}