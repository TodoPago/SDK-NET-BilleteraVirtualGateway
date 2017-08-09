using System;
using System.Collections.Generic;

namespace BVGConnector.Model
{
    public class User
    {
        private String user;
        private String password;
        private String merchant;
        private String apiKey;

        public User() { }

        public User(String user, String password)
        {
            this.user = user;
            this.password = password;
        }

        public String GetMerchant()
        {
            return merchant;
        }

        public String GetApiKey()
        {
            return apiKey;
        }

        public void SetPassword(String password)
        {
            this.password = password;
        }

        public String GetPassword()
        {
            return password;
        }

        public void SetMerchant(String merchant)
        {
            this.merchant = merchant;
        }

        public void SetApiKey(String apiKey)
        {
            this.apiKey = apiKey;
        }

        public String GetUser()
        {
            return user;
        }

        public void SetUser(String user)
        {
            this.user = user;
        }

        public String toString()
        {
            return "User [ merchant=" + merchant + ", apiKey=" + apiKey + " ]";
        }

        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("USUARIO", this.user);
            dic.Add("CLAVE", this.password);

            return dic;
        }
    }
}
