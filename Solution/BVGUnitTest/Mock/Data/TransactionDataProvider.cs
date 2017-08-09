using BVGConnector.Model;
using BVGConnector.Utils;
using System;
using System.Collections.Generic;

namespace BVGUnitTest.Mock.Data
{
    internal static class TransactionDataProvider
    {
        public static string GetTransactionOkResponse()
        {
            return "{\"publicRequestKey\":\"1cb1567a-08f3-4558-ab7e-2b492236acce\",\"merchantId\":\"41702\",\"channel\":\"11\"}";
        }

        public static string GetTransactionFailResponse()
        {
            return "{\"errorCode\":\"1014\",\"errorMessage\":\"Completá este campo.\",\"channel\":\"11\"}";
        }

        public static string GetTransactionFailVendedorResponse()
        {
            return "{\"errorCode\":\"702\",\"errorMessage\":\"Cuenta de vendedor invalida\",\"channel\":\"11\"}";
        }

        public static TransactionBVG GetTransaction()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, "41702");
            generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
            generalData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20170308041300");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_OPERATION_TYPE, "Compra");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "12345");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");

            List<string> availablePaymentMethods = new List<string>();
            availablePaymentMethods.Add("1");
            availablePaymentMethods.Add("42");
            operationData.Add(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

            List<string> availableBanks = new List<string>();
            availableBanks.Add("6");
            availableBanks.Add("24");
            availableBanks.Add("29");
            operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

            Dictionary<string, Object> buyerPreselection = new Dictionary<string, Object>();
            buyerPreselection.Add(ElementNames.BVG_PAYMENT_METHODS_ID, "42");
            buyerPreselection.Add(ElementNames.BVG_BANK_ID, "6");
            operationData.Add(ElementNames.BVG_BUYER_PRESELECTION, buyerPreselection);

            Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
            technicalData.Add(ElementNames.BVG_SDK, "Net");
            technicalData.Add(ElementNames.BVG_SDK_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_LANGUAGE_VERSION, "3.5");
            technicalData.Add(ElementNames.BVG_PLUGIN_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_NAME, "Bla");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_VERSION, "3.1");
            technicalData.Add(ElementNames.BVG_CM_VERSION, "2.4");

            TransactionBVG trasactionBVG = new TransactionBVG(generalData, operationData, technicalData);

            return trasactionBVG;
        }

        public static TransactionBVG GetTransactionWithoutField()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
            generalData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20170308041300");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_OPERATION_TYPE, "Compra");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "12345");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");

            List<string> availablePaymentMethods = new List<string>();
            availablePaymentMethods.Add("1");
            availablePaymentMethods.Add("42");
            operationData.Add(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

            List<string> availableBanks = new List<string>();
            availableBanks.Add("6");
            availableBanks.Add("24");
            availableBanks.Add("29");
            operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

            Dictionary<string, Object> buyerPreselection = new Dictionary<string, Object>();
            buyerPreselection.Add(ElementNames.BVG_PAYMENT_METHODS_ID, "42");
            buyerPreselection.Add(ElementNames.BVG_BANK_ID, "6");
            operationData.Add(ElementNames.BVG_BUYER_PRESELECTION, buyerPreselection);

            Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
            technicalData.Add(ElementNames.BVG_SDK, "Net");
            technicalData.Add(ElementNames.BVG_SDK_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_LANGUAGE_VERSION, "3.5");
            technicalData.Add(ElementNames.BVG_PLUGIN_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_NAME, "Bla");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_VERSION, "3.1");
            technicalData.Add(ElementNames.BVG_CM_VERSION, "2.4");

            TransactionBVG trasactionBVG = new TransactionBVG(generalData, operationData, technicalData);

            return trasactionBVG;
        }

        public static TransactionBVG GetTransactionWrongField()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, "41702");
            generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
            generalData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20170308041300");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_OPERATION_TYPE, "Compra");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "12345");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra?");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");

            List<string> availablePaymentMethods = new List<string>();
            availablePaymentMethods.Add("1");
            availablePaymentMethods.Add("42");
            operationData.Add(ElementNames.BVG_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

            List<string> availableBanks = new List<string>();
            availableBanks.Add("6");
            availableBanks.Add("24");
            availableBanks.Add("29");
            operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

            Dictionary<string, Object> buyerPreselection = new Dictionary<string, Object>();
            buyerPreselection.Add(ElementNames.BVG_PAYMENT_METHODS_ID, "42");
            buyerPreselection.Add(ElementNames.BVG_BANK_ID, "6");
            operationData.Add(ElementNames.BVG_BUYER_PRESELECTION, buyerPreselection);

            Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
            technicalData.Add(ElementNames.BVG_SDK, "Net");
            technicalData.Add(ElementNames.BVG_SDK_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_LANGUAGE_VERSION, "3.5");
            technicalData.Add(ElementNames.BVG_PLUGIN_VERSION, "1.0");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_NAME, "Bla");
            technicalData.Add(ElementNames.BVG_ECOMMERCE_VERSION, "3.1");
            technicalData.Add(ElementNames.BVG_CM_VERSION, "2.4");

            TransactionBVG trasactionBVG = new TransactionBVG(generalData, operationData, technicalData);

            return trasactionBVG;
        }
    }
}
