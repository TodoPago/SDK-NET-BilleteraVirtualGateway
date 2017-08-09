using BVGConnector.Model;
using BVGConnector.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BVGConnector.Connectors
{
    public class TodoPago : RestConnector
    {
        private const string REQUESTTYPE = "RequestType";
        private const string AUTHORIZE = "Authorize";
        private const string CREDENTIALS = "Credentials";

        public TodoPago(string endpoint, Dictionary<string, string> headders)
             : base(endpoint, headders)
        {
        }

        public User GetCredentials(User user)
        {
            string url = endpoint + CREDENTIALS;
            url = url.Replace("t/1.1/", "");
            User userResponse = new User();

            string json = JsonConvert.SerializeObject(user.toDictionary(), Newtonsoft.Json.Formatting.Indented);
            string result = ExecuteRequest(json, url, METHOD_POST, false);

            userResponse = OperationsParserBVG.ParseJsonToUser(result);

            return userResponse;
        }

        protected virtual string ExecuteRequest(string param, string url, string method, bool withApiKey)
        {
            string result = String.Empty;

            var httpWebRequest = GenerateHttpWebRequest(url, CONTENT_TYPE_APP_JSON, method, withApiKey);

            try
            {
                if (method == METHOD_POST)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(param);
                    }
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }
    }
}
