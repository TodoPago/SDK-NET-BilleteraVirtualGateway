using System;
using System.Collections.Generic;
using BVGConnector.Connectors;
using BVGConnector.Model;
using BVGConnector.Operations;
using BVGConnector.Exceptions;

namespace BVGConnector
{
    public class BvgConnector
    {
        public const string versionTodoPago = "1.2.0";

        #region Constants
        public const int developerEndpoint = 0;
        public const int productionEndpoint = 1;

        private const string endPointDev = "https://apis.integration.todopago.com.ar/";
        private const string endPointPrd = "https://apis.todopago.com.ar/";

        private const string tenant = "t/1.1";

        private const string BVGTenant = "ms/";
        private const string BVGDesaTenant = "ms/";

        private const string restAppend = "/api/";

        private const string BVG_CHANNEL = "channel";
        #endregion

        private Dictionary<string, string> Headers;

        private string bvgEndpoint;
        private string restEndpoint;
        
        protected TodoPago todoPagoClient;
        protected BVG bvgClient;

        public BvgConnector(int endpoint, Dictionary<string, string> headers)
        {
            string ep = String.Empty;
            string t = String.Empty;

            switch (endpoint)
            {
                case developerEndpoint:
                    ep = endPointDev;
                    t = BVGTenant;
                    break;
                case productionEndpoint:
                    ep = endPointPrd;
                    t = BVGTenant;
                    break;
            }

            this.bvgEndpoint = ep + t;
            this.restEndpoint = ep + tenant + restAppend;

            if (headers != null)
            {
                this.Headers = headers;
            }

            CreateClients();
        }

        public void SetAuthorize(String authorization)
        {
            var headers = new Dictionary<String, String>();

            if (authorization != null && !authorization.Equals(""))
            {
                headers.Add("Authorization", authorization);
                this.Headers = headers;
                CreateClients();
            }
            else
            {
                throw new ResponseException("ApiKey is null");
            }
        }

        public TransactionBVG Transaction(TransactionBVG transaction)
        {
            TransactionBVG result = transaction;
            TransactionValidate tv = new TransactionValidate();
            transaction.GetGeneralData().Add(BVG_CHANNEL, "BVTP");
            if (tv.ValidateTransaction(transaction))
            {
                result = bvgClient.Transaction(transaction);
            }

            return result;
        }

        public PaymentMethodsBVG DiscoverPaymentMethodBVG()
        {
            PaymentMethodsBVG result = bvgClient.DiscoverPaymentMethodBVG();
            return result;
        }

        public NotificationPushBVG NotificationPush(NotificationPushBVG notificationPush)
        {
            NotificationPushBVG result = notificationPush;
            NotificationPushValidate nv = new NotificationPushValidate();

            if (nv.ValidateNotificationPush(notificationPush))
            {
                result = bvgClient.NotificationPush(notificationPush);
            }

            return result;
        }

        public User GetCredentials(User user)
        {
            User result = user;
            CredentialsValidate cv = new CredentialsValidate();

            if (cv.ValidateCredentials(user))
                result = todoPagoClient.GetCredentials(user);

            return result;
        }

        private void CreateClients()
        {
            this.bvgClient = new BVG(this.bvgEndpoint, this.Headers);
            this.todoPagoClient = new TodoPago(this.restEndpoint, this.Headers);
        }
    }
}
