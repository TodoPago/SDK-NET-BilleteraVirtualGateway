using BVGConnector;
using BVGConnector.Exceptions;
using BVGConnector.Model;
using BVGUnitTest.Mock;
using BVGUnitTest.Mock.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVGUnitTest
{
    [TestClass]
    public class NotificationPushTest
    {
        [TestMethod]
        public void NotificationPushOKTest()
        {
            BvgConnectorMock connector = GetConnector(NotificationPushDataProvider.GetNotificationPushOkResponse());

            NotificationPushBVG response = connector.NotificationPush(NotificationPushDataProvider.GetNotificationPushBVG());

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusCode()));
            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusMessage()));

            Assert.AreEqual("-1", response.GetStatusCode());
        }

        [TestMethod]
        public void NotificationPushFailWithoutFieldTest()
        {
            BvgConnectorMock connector = GetConnector(NotificationPushDataProvider.GetNotificationPushWithoutField());

            NotificationPushBVG response = connector.NotificationPush(NotificationPushDataProvider.GetNotificationPushBVG());

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusCode()));
            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusMessage()));

            Assert.AreEqual("1014", response.GetStatusCode());
        }

        [TestMethod]
        public void NotificationPushFailTransactionTest()
        {
            BvgConnectorMock connector = GetConnector(NotificationPushDataProvider.GetNotificationPushWrongTransaction());

            NotificationPushBVG response = connector.NotificationPush(NotificationPushDataProvider.GetNotificationPushBVG());

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusCode()));
            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetStatusMessage()));

            Assert.AreEqual("2070", response.GetStatusCode());
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldNotificationPushException), "security is required")]
        public void TransactionFailValidationRequiredTest()
        {
            BvgConnectorMock connector = GetConnector(NotificationPushDataProvider.GetNotificationPushOkResponse());

            NotificationPushBVG response = connector.NotificationPush(NotificationPushDataProvider.GetNotificationPushWithoutFieldBVG());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFieldException), "concept with invalid characters")]
        public void TransactionFailValidationTest()
        {
            BvgConnectorMock connector = GetConnector(NotificationPushDataProvider.GetNotificationPushOkResponse());

            NotificationPushBVG response = connector.NotificationPush(NotificationPushDataProvider.GetNotificationPushWrongFieldBVG());
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
