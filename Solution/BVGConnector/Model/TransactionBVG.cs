using System;
using System.Collections.Generic;
using BVGConnector.Utils;

namespace BVGConnector.Model
{
    public class TransactionBVG
    {
        // Request
        private Dictionary<string, Object> generalData;
        private Dictionary<string, Object> operationData;
        private Dictionary<string, Object> technicalData;
        private const string BVG_CHANNEL = "channel";

        // Response
        private string channel;
        private string urlHibridFormResuorces;
        private string publicRequestKey;
        private string requestKey;
        private string transactionID;
        private string merchantID;

        public TransactionBVG() { }

        public TransactionBVG(Dictionary<string, Object> generalData, Dictionary<string, Object> operationData, Dictionary<string, Object> technicalData)
        {
            this.generalData = generalData;
            this.operationData = operationData;
            this.technicalData = technicalData;
        }

        public Dictionary<string, Object> GetGeneralData()
        {
            return this.generalData;
        }

        public void SetGeneralData(Dictionary<string, Object> generalData)
        {
            this.generalData = generalData;
        }

        public Dictionary<string, Object> GetOperationData()
        {
            return this.operationData;
        }

        public void SetOperationData(Dictionary<string, Object> operationData)
        {
            this.operationData = operationData;
        }

        public Dictionary<string, Object> GetTecnicalData()
        {
            return this.technicalData;
        }

        public void SetTecnicalData(Dictionary<string, Object> tecnicalData)
        {
            this.technicalData = tecnicalData;
        }

        public string GetChannel()
        {
            return this.channel;
        }

        public void SetChannel(string channel)
        {
            this.channel = channel;
        }

        public string GetUrlHibridFormResuorces()
        {
            return this.urlHibridFormResuorces;
        }

        public void SetUrlHibridFormResuorces(string urlHibridFormResuorces)
        {
            this.urlHibridFormResuorces = urlHibridFormResuorces;
        }

        public string GetPublicRequestKey()
        {
            return this.publicRequestKey;
        }

        public void SetPublicRequestKeys(string publicRequestKey)
        {
            this.publicRequestKey = publicRequestKey;
        }

        public string GetRequestKey()
        {
            return this.requestKey;
        }

        public void SetRequestKey(string requestKey)
        {
            this.requestKey = requestKey;
        }

        public string GetTransactionID()
        {
            return this.transactionID;
        }

        public void SetTransactionID(string transactionID)
        {
            this.transactionID = transactionID;
        }

        public string GetMerchantID()
        {
            return this.merchantID;
        }

        public void SetMerchantID(string merchantID)
        {
            this.merchantID = merchantID;
        }

        public String toString()
        {
            string result = "TransactionBSA [" + BVG_CHANNEL + " = " + channel;
            //result = result + ", " + ElementNames.BVG_URL_HIBRID_FROM_RESOURCES + " = " + urlHibridFormResuorces;
            result = result + ", " + ElementNames.BVG_PUBLIC_REQUEST_KEY + " = " + publicRequestKey;
            //result = result + ", " + ElementNames.BVG_REQUEST_KEY + " = " + requestKey;
            //result = result + ", " + ElementNames.BVG_TRANSACTION_ID_RESPONSE + " = " + transactionID;
            result = result + ", " + ElementNames.BVG_MERCHANTID + " = " + this.merchantID;
            result = result + " ]";

            return result;
        }

        public Dictionary<string, Object> toDictionary()
        {
            Dictionary<string, Object> dic = new Dictionary<string, Object>();
            dic.Add(BVG_CHANNEL, this.channel);
            //dic.Add(ElementNames.BVG_URL_HIBRID_FROM_RESOURCES, this.urlHibridFormResuorces);
            dic.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, this.publicRequestKey);
            //dic.Add(ElementNames.BVG_REQUEST_KEY, this.requestKey);
            //dic.Add(ElementNames.BVG_TRANSACTION_ID_RESPONSE, this.transactionID);
            dic.Add(ElementNames.BVG_MERCHANTID, this.merchantID);
            return dic;
        }
    }
}
