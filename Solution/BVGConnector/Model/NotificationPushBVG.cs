using System;
using System.Collections.Generic;
using BVGConnector.Utils;

namespace BVGConnector.Model
{
    public class NotificationPushBVG
    {// Request
        private Dictionary<string, Object> generalData;
        private Dictionary<string, Object> operationData;
        private Dictionary<string, Object> tokenizationData;

        // Response
        private string statusCode;
        private string statusMessage;

        public NotificationPushBVG() { }

        public NotificationPushBVG(Dictionary<string, Object> generalData, Dictionary<string, Object> operationData, Dictionary<string, Object> tokenizationData)
        {
            this.generalData = generalData;
            this.operationData = operationData;
            this.tokenizationData = tokenizationData;
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

        public Dictionary<string, Object> GetTokenizationData()
        {
            return this.tokenizationData;
        }

        public void SetTokenizationData(Dictionary<string, Object> tokenizationData)
        {
            this.tokenizationData = tokenizationData;
        }

        public string GetStatusCode()
        {
            return this.statusCode;
        }

        public void SetStatusCode(string statusCode)
        {
            this.statusCode = statusCode;
        }

        public string GetStatusMessage()
        {
            return this.statusMessage;
        }

        public void SetStatusMessage(string statusMessage)
        {
            this.statusMessage = statusMessage;
        }


        public String toString()
        {
            string result = "NotificationPushBSA [" + ElementNames.BVG_STATUS_CODE + " = " + statusCode + ", " + ElementNames.BVG_STATUS_MESSAGE + " = " + statusMessage;
            return result;
        }

        public Dictionary<string, Object> toDictionary()
        {
            Dictionary<string, Object> dic = new Dictionary<string, Object>();
            dic.Add(ElementNames.BVG_STATUS_CODE, this.statusCode);
            dic.Add(ElementNames.BVG_STATUS_MESSAGE, this.statusMessage);
            return dic;
        }
    }
}
