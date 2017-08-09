using BVGConnector.Model;
using BVGConnector.Utils;
using System;
using System.Collections.Generic;

namespace BVGUnitTest.Mock.Data
{
    internal static class NotificationPushDataProvider
    {
        public static string GetNotificationPushOkResponse()
        {
            return "{\"statusCode\":\"-1\",\"statusMessage\":\"OK\"}";
        }

        public static string GetNotificationPushWithoutField()
        {
            return "{\"statusCode\":\"1014\",\"statusMessage\":\"Completá este campo.\"}";
        }

        public static string GetNotificationPushWrongTransaction()
        {
            return "{\"statusCode\":\"2070\",\"statusMessage\":\"Lo sentimos, la referencia a la transacción enviada es inválida.\"}";
        }

        public static NotificationPushBVG GetNotificationPushBVG()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, "41702");
            generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");
            generalData.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, "f50208ea-be00-4519-bf85-035e2733d09e");
            generalData.Add(ElementNames.BVG_OPERATION_NAME, "Compra");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "-1");
            operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
            operationData.Add(ElementNames.BVG_ID_GATEWAY, "8");
            operationData.Add(ElementNames.BVG_RESULT_MESSAGE, "Aprobada");
            operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20160704085736");
            operationData.Add(ElementNames.BVG_TICKET_MUNBER, "7866463542424");
            operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "455422446756567");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "1234");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");
            operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

            Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
            tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, "sydguyt3e862t76ierh76487638rhkh7");
            tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4507XXXXXXXX0001");

            NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
            return notificationPushBVG;
        }

        public static NotificationPushBVG GetNotificationPushWithoutFieldBVG()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, "41702");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");
            generalData.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, "f50208ea-be00-4519-bf85-035e2733d09e");
            generalData.Add(ElementNames.BVG_OPERATION_NAME, "Compra");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "-1");
            operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
            operationData.Add(ElementNames.BVG_ID_GATEWAY, "8");
            operationData.Add(ElementNames.BVG_RESULT_MESSAGE, "Aprobada");
            operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20160704085736");
            operationData.Add(ElementNames.BVG_TICKET_MUNBER, "7866463542424");
            operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "455422446756567");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "1234");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");
            operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

            Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
            tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, "sydguyt3e862t76ierh76487638rhkh7");
            tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4507XXXXXXXX0001");

            NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
            return notificationPushBVG;
        }

        public static NotificationPushBVG GetNotificationPushWrongFieldBVG()
        {
            Dictionary<string, Object> generalData = new Dictionary<string, Object>();
            generalData.Add(ElementNames.BVG_MERCHANT, "41702");
            generalData.Add(ElementNames.BVG_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
            generalData.Add(ElementNames.BVG_REMOTE_IP_ADDRESS, "192.168.11.87");
            generalData.Add(ElementNames.BVG_PUBLIC_REQUEST_KEY, "f50208ea-be00-4519-bf85-035e2733d09e");
            generalData.Add(ElementNames.BVG_OPERATION_NAME, "Compra");

            Dictionary<string, Object> operationData = new Dictionary<string, Object>();
            operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "-1");
            operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
            operationData.Add(ElementNames.BVG_ID_GATEWAY, "8");
            operationData.Add(ElementNames.BVG_RESULT_MESSAGE, "Aprobada");
            operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "20160704085736");
            operationData.Add(ElementNames.BVG_TICKET_MUNBER, "7866463542424");
            operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "455422446756567");
            operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
            operationData.Add(ElementNames.BVG_OPERATION_ID, "1234");
            operationData.Add(ElementNames.BVG_CONCEPT, "compra?");
            operationData.Add(ElementNames.BVG_AMOUNT, "10,99");
            operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

            Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
            tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, "sydguyt3e862t76ierh76487638rhkh7");
            tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4507XXXXXXXX0001");

            NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
            return notificationPushBVG;
        }
    }
}
