using BVGConnector.Connectors;
using System;
using System.Collections.Generic;

namespace BVGUnitTest.Mock
{
    public class BvgTodoPagoClientMock : TodoPago
    {
        private string requestResponse;

        public BvgTodoPagoClientMock(string endpoint, Dictionary<string, string> headders) : base(endpoint, headders)
        {
            this.requestResponse = String.Empty;
        }

        public void SetRequestResponse(string response)
        {
            this.requestResponse = response;
        }

        protected override string ExecuteRequest(string param, string url, string method, bool withApiKey)
        {
            return requestResponse;
        }
    }
}
