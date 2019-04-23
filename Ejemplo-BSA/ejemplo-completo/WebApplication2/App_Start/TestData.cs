using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.App_Start
{
    public class TestData
    {
        private OperationLogs _transaction;

        public string id_operation { get; set; }
        public Dictionary<string, string> config_data { get; set; }
        public OperationLogs transaction { get => _transaction; set => _transaction = value; }
        public OperationLogs formTP { get; set; }
        public OperationLogs decidir_payment { get; set; }
        public OperationLogs push_notification { get; set; }

        public TestData()
        {
            config_data = new Dictionary<string, string>();
            transaction = new OperationLogs();
            formTP = new OperationLogs();
            decidir_payment = new OperationLogs();
            push_notification = new OperationLogs();
        }
    }
}