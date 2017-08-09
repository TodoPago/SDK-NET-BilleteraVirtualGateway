<a name="inicio"></a>
Todo Pago - módulo SDK-.NET para conexión con gateway de pago
=======

+ [Instalación](#instalacion)
 	+ [Versiones de .NET soportadas](#Versionesdenetsoportadas)
 	+ [Generalidades](#general)
+ [Uso](#uso)
    + [Inicializar la clase correspondiente al conector (TodoPago\Sdk)](#initconector)
	+ [Ambientes](#test)
	+ [Billetera Virtual Para Gateways](#bvg)
		+ [Diagrama de secuencia](#bvg-uml)
		+ [Descubrimiento de medios de pago](#bvg-discover)
		+ [Transacciones](#bvg-transaction)
		+ [NotificationPush](#bvg-notificationPush)
		+ [Obtener Credenciales](#credenciales)

<a name="instalacion"></a>
## Instalación
Se debe descargar la última versión del SDK desde el botón Download ZIP del branch master.
Una vez descargado y descomprimido, se debe agregar la librería TodoPagoConnector.dll que se encuentra dentro de la carpeta dist, a las librerías del proyecto y en el código se debe agregar siguiente using.

```C#
using TodoPagoConnector;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
```

<a name="Versionesdenetsoportadas"></a>
#### 1. Versiones de .Net soportadas
La versi&oacute;n implementada de la SDK, esta testeada para versiones desde .net 3.5

<a name="general"></a>
#### 2. Generalidades
Esta versión soporta únicamente pago en moneda nacional argentina (CURRENCYCODE = 32).

[<sub>Volver a inicio</sub>](#inicio)

<a name="uso"></a>
## Uso

<a name="initconector"></a>
#### Inicializar la clase correspondiente al conector (TodoPago\Sdk).

Si se cuenta con los http header suministrados por Todo Pago

- Crear un Dictionary<String, String> con los http header suministrados por Todo Pago
```C#
var headers = new Dictionary<String, String>();
headers.Add("Authorization", "PRISMA 912EC803B2CE49E4A541068D495AB570");
```

- Crear un String con los Endpoint suministrados por TodoPago
```C#
int endPointDev = BvgConnector.developerEndpoint; // EndPoint de Developer
int endPointPrd = BvgConnector.productionEndpoint; // EndPoint de Production
```

- Crear una instancia de la clase TodoPago\Sdk
```C#
BvgConnector connector = new BvgConnector(endPointDev, headers);
```

[<sub>Volver a inicio</sub>](#inicio)

<a name="test"></a>
#### Ambientes

El SDK-NET permite trabajar con los ambiente de Developers y de Producción de Todo Pago.<br>
El ambiente se debe instanciar como se indica a continuacion.

```C#
//identificador de entorno obligatorio, la otra opcion es BvgConnector.productionEndpoint
int mode = BvgConnector.developerEndpoint;

//authorization key del ambiente requerido
var headers = new Dictionary<String, String>();
headers.Add("Authorization", "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");

BvgConnector conector = new BvgConnector(mode, headers)
```

Puede consultar los datos de prueba en la [web de TodoPago](https://developers.todopago.com.ar/site/datos-de-prueba).

[<sub>Volver a inicio</sub>](#inicio)

<a name="bvg"></a>
### Billetera Virtual para Gateways

La Billetera Virtual para Gateways es la versión de Todo Pago para los comercios que permite utilizar los servicios de la billetera TodoPago dentro de los e-commerce, respetando y manteniendo sus respectivas promociones con bancos y marcas y números de comercio (métodos de adquirencia). Manteniendo su Gateway de pago actual, y utilizando BVG para la selección del medio de pago y la tokenizacion de la información para mayor seguridad en las transacciones.

<a name="bvg-uml"></a>
#### Diagrama de secuencia

![Diagrama de Secuencia BSA](http://www.plantuml.com/plantuml/png/ZL9BJiCm4Dtd5BDi5roW2oJw0I7ngMWlC3ZJOd0zaUq4XJknuWYz67Q-JY65bUNHlFVcpHiKZWqib2JjACdGE2baXjh1DPj3hj187fGNV20ZJehppTNWVuEEth5C4XHE5lxJAJGlN5nsJ323bP9xWWptQ42mhlXwQAlO0JpOTtZSXfMNT0YFcQzhif1MD0oJfRI22pBJdYYm1jnG-ubinjhZjcXUoQ654kQe1TiafG4srczzpE0-9-iC0f-CSDPgQ3v-wQvtLAVskTB5yHE156ISofG33dEVdFp0ccYoDQXje64z7N4P1iN_cRgZmkU8yH48Gm4JLIA3VJM0UIzrRob2H6s_xl1PAaME38voRqYH28l6DgzJqjxpaegSLE6JvJVIthZNu7BW83BVtAp7hVqTLcVezrr3Eo_jORVD8wTaoERAOHMKgXEErjwI_CpvLk_yS1ZX6pXCrhbzUM0dTsKJRoJznsMUdwOZYMirnpS0)

Para acceder al servicio, los vendedores podrán adherirse en el sitio exclusivo de Botón o a través de su ejecutivo comercial. En estos procesos se generará el usuario y clave para este servicio.

<a name="bvg-discover"></a>
##### Discover
La SDK cuenta con un método para consultar cuales son los medios de pago disponibles.
El método retorna el objeto PaymentMethodsBVG con los medios de pago asignados en el atributo paymentMethodsBSAList.
Se utiliza de la siguiente manera:

```C#
BvgConnector connector = new BvgConnector(endpoint, headers);

PaymentMethodsBVG paymentMethodsBVG = new PaymentMethodsBVG();

try{
    paymentMethodsBVG = connector.DiscoverPaymentMethodBVG();
	List<Dictionary<string, Object>> paymentMethodsBSAList = paymentMethodsBVG.GetPaymentMethodsBVGList();

} catch (ResponseException ex) {
    Console.WriteLine(ex.Message);
} catch (ConnectionException ex) {
    Console.WriteLine(ex.Message);
}
```

Por cada medio de pago veremos lo siguiente:

Campo       | Descripción           | Tipo de dato | Ejemplo
------------|-----------------------|--------------|--------
id          | Id del medio de pago  | numérico     | 42
nombre      | Marca de la tarjeta   | string       | "VISA"
tipo        | Tipo de medio de pago | string       | "Crédito"
idBanco     | Id del banco          | numérico     | 10
nombreBanco | Nombre del banco      | string       | "Banco Ciudad"

<ins><strong>Ejemplo de Respuesta</strong></ins>

```C#
List<Dictionary<string, Object>> ()
     Dictionary<string, Object>()
		{  idMedioPago = 1,
           nombre = AMEX,
		   tipoMedioPago = Crédito,
           idBanco = 1,
           nombreBanco = Provincia
	    }
```

[<sub>Volver a inicio</sub>](#inicio)

<a name="bvg-transaction"></a>
#### Transaction
La SDK cuenta con un método que permite registrar una transacción. El método retorna el objeto TransactionBVG con el resultado de la transacción. Se utiliza de la siguiente manera:


```C#
BvgConnector connector = new BvgConnector(endpoint, headers);
TransactionBVG trasactionBVG = new TransactionBVG();

try{

    trasactionBVG = connector.transaction(trasactionBVG);
	Dictionary<string, Object> dic = trasactionBVG.toDictionary();

}catch (EmptyFieldException ex){
    Console.WriteLine(ex.Message);
} catch (ResponseException ex) {
    Console.WriteLine(ex.Message);
} catch (ConnectionException ex) {
    Console.WriteLine(ex.Message);
}
```

El parámetro trasactionBVG, debe ser un objeto TransactionBVG con la siguiente estructura:

```C#
    Dictionary<string, Object> generalData = new Dictionary<string, Object>();
    generalData.Add(ElementNames.BSA_MERCHANT, "41702");
    generalData.Add(ElementNames.BSA_SECURITY, "TODOPAGO 8A891C0676A25FBF052D1C2FFBC82DEE");
    generalData.Add(ElementNames.BSA_OPERATION_DATE_TIME, "20170308041300");
    generalData.Add(ElementNames.BSA_REMOTE_IP_ADDRESS, "127.0.0.1");

    Dictionary<string, Object> operationData = new Dictionary<string, Object>();
    operationData.Add(ElementNames.BSA_OPERATION_TYPE, "Compra");
    operationData.Add(ElementNames.BSA_OPERATION_ID, "12345");
    operationData.Add(ElementNames.BSA_CURRENCY_CODE, "032");
    operationData.Add(ElementNames.BSA_CONCEPT, "compra");
    operationData.Add(ElementNames.BSA_AMOUNT, "10,99");

    List<string> availablePaymentMethods = new List<string>();
    availablePaymentMethods.Add("1");
    availablePaymentMethods.Add("42");
    operationData.Add(ElementNames.BSA_AVAILABLE_PAYMENT_METHODS, availablePaymentMethods);

	List<string> availableBanks = new List<string>();
	availableBanks.Add("6");
	availableBanks.Add("24");
	availableBanks.Add("29");
	operationData.Add(ElementNames.BVG_AVAILABLE_BANK, availableBanks);

    Dictionary<string, Object> technicalData = new Dictionary<string, Object>();
    technicalData.Add(ElementNames.BSA_SDK, "Net");
    technicalData.Add(ElementNames.BSA_SDK_VERSION, "1.0");
    technicalData.Add(ElementNames.BSA_LANGUAGE_VERSION, "3.5");
    technicalData.Add(ElementNames.BSA_PLUGIN_VERSION, "1.0");
    technicalData.Add(ElementNames.BSA_ECOMMERCE_NAME, "Bla");
    technicalData.Add(ElementNames.BSA_ECOMMERCE_VERSION, "3.1");
    technicalData.Add(ElementNames.BSA_CM_VERSION, "2.4");

    TransactionBVG trasactionBVG = new TransactionBVG(generalData, operationData, technicalData);
```

<ins><strong>Ejemplo de Respuesta</strong></ins>

```C#
Dictionary<string, Object>()
		{  transactionid = "f9878b59-5ce6-408b-ace6-02ccc2d16ecb", //string(36)
		   publicRequestKey = "b6f492ea-b829-43c0-a8f6-5af95ae93001", //string(36)
		   requestKey = "9ca41afb-48d0-4268-a9c5-5904d9f207a4", //string(36)
		   url_HibridFormResuorces = "www.google.com.ar/Formulario", //string(28)
   		   channel = "11" //string(2)
	    }
```

#### Datos de referencia

<table>
<tr><th>Nombre del campo</th><th>Required/Optional</th><th>Data Type</th><th>Comentarios</th></tr>
<tr><td>security</td><td>Required</td><td>String</td><td>Authorization que deberá contener el valor del api key de la cuenta del vendedor (Merchant)</td></tr>
<tr><td>operationDatetime</td><td>Required</td><td>String</td><td>Fecha Hora de la invocacion en Formato yyyyMMddHHmmssSSS</td></tr>
<tr><td>remoteIpAddress</td><td>Required</td><td>String</td><td>IP desde la cual se envía el requerimiento</td></tr>
<tr><td>merchant</td><td>Required</td><td>String</td><td>ID de cuenta del vendedor</td></tr>
<tr><td>operationType</td><td>Optional</td><td>String</td><td>Valor fijo definido para esta operatoria de integración</td></tr>
<tr><td>operationID</td><td>Required</td><td>String</td><td>ID de la operación en el eCommerce</td></tr>
<tr><td>currencyCode</td><td>Required</td><td>String</td><td>Valor fijo 32</td></tr>
<tr><td>concept</td><td>Optional</td><td>String</td><td>Especifica el concepto de la operación</td></tr>
<tr><td>amount</td><td>Required</td><td>String</td><td>Formato 999999999,99</td></tr>
<tr><td>availablePaymentMethods</td><td>Optional</td><td>Array</td><td>Array de Strings obtenidos desde el servicio de descubrimiento de medios de pago. Lista de ids de Medios de Pago habilitados para la transacción. Si no se envía están habilitados todos los Medios de Pago del usuario.</td></tr>
<tr><td>availableBanks</td><td>Optional</td><td>Array</td><td>Array de Strings obtenidos desde el servicio de descubrimiento de medios de pago. Lista de ids de Bancos habilitados para la transacción. Si no se envía están habilitados todos los bancos del usuario.</td></tr>
<tr><td>buyerPreselection</td><td>Optional</td><td>BuyerPreselection</td><td>Preselección de pago del usuario</td></tr>
<tr><td>sdk</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>sdkversion</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>lenguageversion</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>pluginversion</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>ecommercename</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>ecommerceversion</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
<tr><td>cmsversion</td><td>Optional</td><td>String</td><td>Parámetro de versión de API</td></tr>
</table>
<br>
<strong>BuyerPreselection</strong>
<br>
<table>
<tr><th>Nombre del campo</th><th>Data Type</th><th>Comentarios</th></tr>
<tr><td>paymentMethodId</td><td>String</td><td>Id del medio de pago seleccionado</td></tr>
<tr><td>bankId</td><td>String</td><td>Id del banco seleccionado</td></tr>
</table>

[<sub>Volver a inicio</sub>](#inicio)

<a name="bvg-notificationPush"></a>
#### Notification Push
La SDK cuenta con un método que permite registrar la finalización de una transacción. El método retorna el objeto NotificationPushBVG con el resultado de la notificación. Se utiliza de la siguiente manera:

```C#
	BvgConnector connector = new BvgConnector(endpoint, headers);
	NotificationPushBVG notificationPushBVG = new NotificationPushBVG();

    try{

         notificationPushBVG = connector.notificationPush(notificationPushBVG);
         Dictionary<string, Object> dic = notificationPushBVG.toDictionary();

    }catch (EmptyFieldException ex){
        Console.WriteLine(ex.Message);
    }catch (ResponseException ex){
        Console.WriteLine(ex.Message);
    }catch (ConnectionException ex) {
        Console.WriteLine(ex.Message);
    }
```

El parámetro notificationPushBVG, debe ser un objeto NotificationPushBVG con la siguiente estructura:

```C#
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
	operationData.Add(ElementNames.BVG_OPERATION_DATE_TIME, "201607040857364");
	operationData.Add(ElementNames.BVG_TICKET_MUNBER, "7866463542424");
	operationData.Add(ElementNames.BVG_CODIGO_AUTORIZATION, "455422446756567");
	operationData.Add(ElementNames.BVG_CURRENCY_CODE, "032");
	operationData.Add(ElementNames.BVG_OPERATION_ID, "1234");
	operationData.Add(ElementNames.BVG_CONCEPT, "compra");
	operationData.Add(ElementNames.BVG_AMOUNT, "10,99");
	operationData.Add(ElementNames.BVG_FACILITIES_PAYMENT, "03");

	Dictionary<string, Object> tokenizationData = new Dictionary<string, Object>();
	tokenizationData.Add(ElementNames.BVG_PUBLIC_TOKENIZATION_FIELD, "sydguyt3e862t76ierh76487638rhkh7");
	tokenizationData.Add(ElementNames.BVG_CREDENTIAL_MASK, "4510XXXXX00001");

    NotificationPushBVG notificationPushBVG = new NotificationPushBVG(generalData, operationData, tokenizationData);
```

<ins><strong>Ejemplo de Respuesta</strong></ins>

```C#
	Dictionary<string, Object>() = notificationPushBVG.toDictionary();
		{  statusCode = -1, //string(2)
           statusMessage = OK //string(2)
	    }
```

#### Datos de referencia

<table>
<tr><th>Nombre del campo</th><th>Required/Optional</th><th>Data Type</th><th>Comentarios</th></tr>
<tr><td>Security</td><td>Required</td><td>String</td><td>Authorization que deberá contener el valor del api key de la cuenta del vendedor (Merchant). Este dato viaja en el Header HTTP</td></tr>
<tr><td>Merchant</td><td>Required</td><td>String</td><td>ID de cuenta del comercio</td></tr>
<tr><td>RemoteIpAddress</td><td>Optional</td><td>String</td><td>IP desde la cual se envía el requerimiento</td></tr>
<tr><td>PublicRequestKey</td><td>Required</td><td>String</td><td>publicRequestKey de la transacción creada. Ejemplo: 710268a7-7688-c8bf-68c9-430107e6b9da</td></tr>
<tr><td>OperationName</td><td>Required</td><td>String</td><td>Valor que describe la operación a realizar, debe ser fijo entre los siguientes valores: “Compra”, “Devolucion” o “Anulacion”</td></tr>
<tr><td>ResultCodeMedioPago</td><td>Optional</td><td>String</td><td>Código de respuesta de la operación propocionado por el medio de pago</td></tr>
<tr><td>ResultCodeGateway</td><td>Optional</td><td>String</td><td>Código de respuesta de la operación propocionado por el gateway</td></tr>
<tr><td>idGateway</td><td>Optional</td><td>String</td><td>Id del Gateway que procesó el pago. Si envían el resultCodeGateway, es obligatorio que envíen este campo</td></tr>
<tr><td>ResultMessage</td><td>Optional</td><td>String</td><td>Detalle de respuesta de la operación.</td></tr>
<tr><td>OperationDatetime</td><td>Required</td><td>String</td><td>Fecha Hora de la operación en el comercio en Formato yyyyMMddHHmmssMMM</td></tr>
<tr><td>TicketNumber</td><td>Optional</td><td>String</td><td>Numero de ticket generado</td></tr>
<tr><td>CodigoAutorizacion</td><td>Optional</td><td>String</td><td>Codigo de autorización de la operación</td></tr>
<tr><td>CurrencyCode</td><td>Required</td><td>String</td><td>Valor fijo 32</td></tr>
<tr><td>OperationID</td><td>Required</td><td>String</td><td>ID de la operación en el eCommerce</td></tr>
<tr><td>Amount</td><td>Required</td><td>String</td><td>Formato 999999999,99</td></tr>
<tr><td>FacilitiesPayment</td><td>Required</td><td>String</td><td>Formato 99</td></tr>
<tr><td>Concept</td><td>Optional</td><td>String</td><td>Especifica el concepto de la operación dentro del ecommerce</td></tr>
<tr><td>PublicTokenizationField</td><td>Required</td><td>String</td><td></td></tr>
<tr><td>CredentialMask</td><td>Optional</td><td>String</td><td></td></tr>
</table>

[<sub>Volver a inicio</sub>](#inicio)

<a name="credenciales"></a>
#### Obtener credenciales
El SDK permite obtener las credenciales "Authentification", "MerchandId" y "Security" de la cuenta de Todo Pago, ingresando el usuario y contraseña.
Esta funcionalidad es util para obtener los parámetros de configuracion dentro de la implementacion.

- Crear una instancia de la clase User:

```C#
User user = new User("test@Test.com.ar","pass1234");// user y pass de TodoPago

    try {
          user = tpc.getCredentials(user);
          tpc.setAuthorize(user.getApiKey());// set de la APIKey a TodoPagoConector

         }catch (EmptyFieldException ex){ //se debe realizar catch por campos en blanco
			Console.WriteLine(ex.Message);

         }catch (ResponseException ex) { //se debe realizar catch User y pass invalidos
            Console.WriteLine(ex.Message);
         }
          Console.WriteLine(user.toString());
     }
```
[<sub>Volver a inicio</sub>](#inicio)
