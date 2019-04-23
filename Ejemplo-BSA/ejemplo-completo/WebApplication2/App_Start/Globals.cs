using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.App_Start
{
    public static class Globals
    {
        public static string publicRequestKey = "";
        public static string token_todopago = "";
        public static string volatile_encrypted_data = "";
        public static string token_decidir = "";
        public static string file_path = HttpContext.Current.Server.MapPath("~/logs");
        public static string file_name = "json_test.json"; 
        public static string file_dir = Path.Combine(file_path , file_name);
        public static string token_date = "";
        public static string operationDateTime = "";
        public static string transactionState = "PENDIENTE";
        public static string pagoTPState = "PENDIENTE";
        public static string pagoDecidirState = "PENDIENTE";
        public static string pushNotifState = "PENDIENTE";
        public static string ticket = "";
        public static string status = "";

        public static Dictionary<string, string> configData = initConfigData();

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

    internal interface IObserver
    {
    }
}