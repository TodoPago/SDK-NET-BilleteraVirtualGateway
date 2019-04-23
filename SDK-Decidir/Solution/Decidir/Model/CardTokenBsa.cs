using System;
using Decidir.Model.CyberSource;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class CardTokenBsa
    {
        public string public_token { get; set; }
        public string volatile_encrypted_data { get; set; }
        public string public_request_key { get; set; }
        public string issue_date { get; set; }
        public string flag_security_code { get; set; }
        public string flag_tokenization { get; set; }
        public string flag_selector_key { get; set; }
        public string flag_pei { get; set; }
        public string card_holder_name { get; set; }
        public CardHolderIdentification card_holder_identification { get; set; }
        public FraudDetectionBSA fraud_detection { get; set; }
        
        public static string toJson(CardTokenBsa card_token)
        {
            return JsonConvert.SerializeObject(card_token, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}