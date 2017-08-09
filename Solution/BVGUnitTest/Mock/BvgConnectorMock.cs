using BVGConnector;
using BVGConnector.Connectors;
using System.Collections.Generic;

namespace BVGUnitTest.Mock
{
    public class BvgConnectorMock : BvgConnector
    {
        public BvgConnectorMock(int endpoint, Dictionary<string, string> headers, BVG bvgClient) : base(endpoint, headers)
        {
            this.bvgClient = bvgClient;
        }

        public BvgConnectorMock(int endpoint, Dictionary<string, string> headers, TodoPago todoPagoClient) : base(endpoint, headers)
        {
            this.todoPagoClient = todoPagoClient;
        }
    }
}
