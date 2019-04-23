using System;
using System.Collections.Generic;

namespace WebApplication2.App_Start
{
    public class OperationLogs
    {
        public string status { get; set; }
        public Dictionary<string, Object> request { get; set; }
        public Dictionary<string, Object> response { get; set; }
        public string request_json { get; set; }
        public string response_json { get; set; }

        public OperationLogs()
        {
            status = "PENDIENTE";
            request = new Dictionary<string, Object>();
            response = new Dictionary<string, Object>();
            request_json = "";
            response_json = "";
        }
    }
}