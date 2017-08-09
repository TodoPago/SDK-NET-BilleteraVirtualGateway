using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVGConnector.Model;
using System.Collections.Generic;
using BVGUnitTest.Mock;
using BVGUnitTest.Mock.Data;
using BVGConnector;
using BVGConnector.Exceptions;

namespace BVGUnitTest
{
    [TestClass]
    public class CredentialsTest
    {
        [TestMethod]
        public void GetCredentialsOKTest()
        {
            User user = new User("ejemplo@mail.com", "mypassword");

            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsOkResponse());

            user = connector.GetCredentials(user);

            Assert.AreEqual(true, !String.IsNullOrEmpty(user.GetApiKey()));
            Assert.AreEqual(true, !String.IsNullOrEmpty(user.GetMerchant()));
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldPassException), "Password is empty")]
        public void GetCredentialsEmptyPasswordTest()
        {
            User user = new User();
            user.SetUser("ejemplo@mail.com");

            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsOkResponse());

            user = connector.GetCredentials(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldUserException), "User/Mail is empty")]
        public void GetCredentialsEmptyUserTest()
        {
            User user = new User();
            user.SetPassword("mypassword");

            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsOkResponse());

            user = connector.GetCredentials(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldPassException), "User is null")]
        public void GetCredentialsUserNullTest()
        {
            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsOkResponse());

            connector.GetCredentials(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ResponseException), "1050 - Este usuario no se encuentra registrado. Revisá la información indicada o registrate.")]
        public void GetCredentialsFailureUserTest()
        {
            User user = new User("ejemplo@mail.com", "mypassword");

            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsWrongUserResponse());

            user = connector.GetCredentials(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ResponseException), "1055 - La contraseña ingresada es incorrecta. Revisala.")]
        public void GetCredentialsFailurePasswordTest()
        {
            User user = new User("ejemplo@mail.com", "mypassword");

            BvgConnectorMock connector = GetConnector(CredentialsDataProvider.GetCredentialsWrongPasswordResponse());

            user = connector.GetCredentials(user);
        }

        [TestMethod]
        public void UserInstance()
        {
            User user = new User();

            user.SetUser("ejemplo@mail.com");
            user.SetPassword("mypassword");

            Assert.AreEqual("ejemplo@mail.com", user.GetUser());
            Assert.AreEqual("mypassword", user.GetPassword());
        }

        [TestMethod]
        public void UserInstanceTwo()
        {
            User user = new User("ejemplo@mail.com", "mypassword");

            Assert.AreEqual("ejemplo@mail.com", user.GetUser());
            Assert.AreEqual("mypassword", user.GetPassword());
        }

        private BvgConnectorMock GetConnector(string response)
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            BvgTodoPagoClientMock bvgConnector = new BvgTodoPagoClientMock("https://developers.todopago.com.ar/t/1.1/api/", headers);
            bvgConnector.SetRequestResponse(response);
            BvgConnectorMock connector = new BvgConnectorMock(BvgConnector.developerEndpoint, headers, bvgConnector);

            return connector;
        }
    }
}
