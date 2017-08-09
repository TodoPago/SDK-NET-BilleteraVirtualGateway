using System;
using System.Collections.Generic;

namespace BVGConnector.Model
{
    public class PaymentMethodsBVG
    {
        private List<Dictionary<string, Object>> paymentMethodsBVGList;

        public List<Dictionary<string, Object>> GetPaymentMethodsBVGList()
        {
            return this.paymentMethodsBVGList;
        }

        public void SetPaymentMethodsBVGList(List<Dictionary<string, Object>> paymentMethodsBVGList)
        {
            this.paymentMethodsBVGList = paymentMethodsBVGList;
        }
    }
}
