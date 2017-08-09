using BVGConnector;
using BVGConnector.Exceptions;
using BVGConnector.Model;
using BVGUnitTest.Mock;
using BVGUnitTest.Mock.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BVGUnitTest
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionOKTest()
        {
            BvgConnectorMock connector = GetConnector(TransactionDataProvider.GetTransactionOkResponse());

            TransactionBVG response = connector.Transaction(TransactionDataProvider.GetTransaction());

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetPublicRequestKey()));
            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetMerchantID()));
            Assert.AreEqual(false, String.IsNullOrEmpty(response.GetChannel()));
        }

        [TestMethod]
        [ExpectedException(typeof(ResponseException), "1014 - Completá este campo.")]
        public void TransactionWithoutFieldTest()
        {
            BvgConnectorMock connector = GetConnector(TransactionDataProvider.GetTransactionFailResponse());

            TransactionBVG response = connector.Transaction(TransactionDataProvider.GetTransaction());
        }

        [TestMethod]
        [ExpectedException(typeof(ResponseException), "702 - Cuenta de vendedor invalida")]
        public void TransactionFailVendedorTest()
        {
            BvgConnectorMock connector = GetConnector(TransactionDataProvider.GetTransactionFailVendedorResponse());

            TransactionBVG response = connector.Transaction(TransactionDataProvider.GetTransaction());
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldTransactionException), "merchant is required")]
        public void TransactionFailValidationRequiredTest()
        {
            BvgConnectorMock connector = GetConnector(TransactionDataProvider.GetTransactionFailVendedorResponse());

            TransactionBVG response = connector.Transaction(TransactionDataProvider.GetTransactionWithoutField());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFieldException), "concept with invalid characters")]
        public void TransactionFailValidationTest()
        {
            BvgConnectorMock connector = GetConnector(TransactionDataProvider.GetTransactionOkResponse());

            TransactionBVG response = connector.Transaction(TransactionDataProvider.GetTransactionWrongField());
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
