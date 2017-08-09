using BVGConnector;
using BVGConnector.Model;
using BVGUnitTest.Mock;
using BVGUnitTest.Mock.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BVGUnitTest
{
    [TestClass]
    public class DiscoverTest
    {
        [TestMethod]
        public void DiscoverPaymentMethodsOKTest()
        {
            BvgConnectorMock connector = GetConnector(DiscoverDataProvider.GetDiscoverOkResponse());

            PaymentMethodsBVG response = connector.DiscoverPaymentMethodBVG();

            Assert.AreNotEqual(null, response);

            Assert.AreNotEqual(null, response.GetPaymentMethodsBVGList());

            Assert.AreEqual(true, response.GetPaymentMethodsBVGList().Count > 0);

            Dictionary<string, Object> paymentCollection = response.GetPaymentMethodsBVGList()[0];

            Assert.AreEqual(true, paymentCollection.Count > 0);

            Assert.AreEqual(true, paymentCollection.ContainsKey("idMedioPago"));
            Assert.AreEqual(true, paymentCollection.ContainsKey("nombre"));
            Assert.AreEqual(true, paymentCollection.ContainsKey("tipoMedioPago"));
            Assert.AreEqual(true, paymentCollection.ContainsKey("idBanco"));
            Assert.AreEqual(true, paymentCollection.ContainsKey("nombreBanco"));
        }

        [TestMethod]
        public void DiscoverPaymentMethodsFailTest()
        {
            BvgConnectorMock connector = GetConnector(DiscoverDataProvider.GetDiscoverFailResponse());

            PaymentMethodsBVG response = connector.DiscoverPaymentMethodBVG();

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(0, response.GetPaymentMethodsBVGList().Count);
        }

        private BvgConnectorMock GetConnector(string response)
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            BvgClientMock bvgConnector = new BvgClientMock("https://developers.todopago.com.ar/t/1.1/api/", headers);
            bvgConnector.SetRequestResponse(response);
            BvgConnectorMock connector = new BvgConnectorMock(BvgConnector.developerEndpoint, headers, bvgConnector);

            return connector;
        }
    }
}
