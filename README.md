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
		+ [Formulario Billetera](#formbilletera)
		+ [NotificationPush](#bvg-notificationPush)
		+ [Obtener Credenciales](#credenciales)
	+ [Integracion BSA con Decidir](README-INT-BSA-DECIDIR.md)
	
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
La versi&oacute;n implementada de la SDK, esta testeada para versiones desde .net 4.5

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

El SDK-NET permite trabajar con los ambiente de Developer(Integration) y de Producción de Todo Pago.<br>
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


<a name="formbilletera"></a>
#### Formulario Billetera

Para abrir el formulario se debe agregar un archivo javascript provisto por TodoPago e instanciar la API Javascript tal cual se muestra en el ejemplo correspondiente.

##### Endpoints:
+ Ambientes desarrollo: https://forms.integration.todopago.com.ar/resources/TPBSAForm.min.js
+ Ambiente Produccion: https://forms.todopago.com.ar/resources/TPBSAForm.min.js

```html

<html>
    <head>
        <title>Formulario de pago TP</title>
        <meta charset="UTF-8">
        <script src="https://forms.integration.todopago.com.ar/resources/TPBSAForm.min.js"></script>
        <link rel="stylesheet" type="text/css" href="css/styles.css">
        <script type="text/javascript">
        </script>
    </head>
	<body>
	    <script>
		var success = function(data) {
		    console.log(data);
		};
		var error = function(data) {
		    console.log(data);
		};
		var validation = function(data) {
		    console.log(data);
		}
		window.TPFORMAPI.hybridForm.initBSA({
		    publicKey: "requestpublickey",
		    merchantAccountId: "merchant",
		    callbackCustomSuccessFunction: "success",
		    callbackCustomErrorFunction: "error",
		    callbackValidationErrorFunction: "validation"
		});
	    </script>
	</body>
</html>
```
El formulario requiere obligatoriamente ingresar en los campos **merchantAccountId** y **publicKey** dentro del "window.TPFORMAPI.hybridForm.initBSA", el atributo [publicRequestKey](#publicRequestKey) que devuelve request del servicio [Transaction](#transaction).

Al cargar el formulario se mostrara una ventana de Login para ingresar el usuario de billetera.

![login](https://raw.githubusercontent.com/guillermoluced/docbsadec/master/img/login-formulario-tp.png)

Luego de loguearse el formulario mostrara la lista de medios de pago habilitados.

![formulario](https://raw.githubusercontent.com/guillermoluced/docbsadec/master/img/formulario-bsa_medios_pago.png)

####  Respuesta
Si la compra fue aprobada el formulario devolverá un JSON con la siguiente estructura.
<a name="formularioresponse"></a>
```html
{
"ResultCode":1,
"ResultMessage":"El medio de pago se selecciono correctamente",
"IdCuenta":"41703",
"Token":"e7ebadfc7223838015e3b160c04b623fca7a4d0",
"BankId":"17",
"CardNumberBin": 450799,
"FourLastDigitsOfCardNumber":"7783",
"PaymentMethodID":"42",
"TokenDate": "20180427",
"DatosAdicionales": {
	"tipoDocumento": "DNI",
	"numeroDocumento": "45998745",
	"generoCuentaCompradora": "M",
	"nombre": "Comprador",
	"apellido": "BSA",
  	"permiteObtenerMP": false
  }
} 
```
Los atributos **Token** y **TokenDate** serán requeridos por el siguiente servicio [solicitud de token de pago de Decidir](#tokendecidir).

El atributo **PaymentMethodID** representa el id del medio de pago en TP. Se debera convertir utilizando la [tabla](#mapeomediosdepago) correspondiente para ser enviado a Decidir en el servicio [Decidir Payment](#pagodecidir)

El atributo **ResultCode** se utiiza en el servicio [Decidir Payment](#pagodecidir). Se envia en el campo **ResultCodeMedioPago**

<a name="tokendecidir"></a>
###  Solicitud de Token de Pago para BSA en Decidir

Para implementar los servicios de Decidir en NET se deberá descargar la ultima versión del SDK [SDK NET Decidir](https://github.com/decidir/sdk-.net-v2). Ademas es necesario tener disponibles las claves publicas y privadas provistas por Decidir.
Luego de importar el SDK en el proyecto e instanciar el SDK, se debe llamar a este servicio para obtener el token de pago de Decidir.

Campo       | Descripción           | Tipo de dato | Ejemplo
------------|-----------------------|--------------|--------
public_token| Campo String que se obtiene en la respuesta del formulario de pago de Todopago ("Token":"e7ebadfc7223838015e3b160c04b623fca7a4d0")| String     | "e7ebadfc7223838015e3b160c04b623fca7a4d0"
issue_date| Campo String que se obtiene en la respuesta del formulario de pago de Todopago ("TokenDate": "20180427")| String | "20180427"
merchant_id| Id del comercio que si informa en window.TPFORMAPI.hybridForm.initBSA al implementar el formulario | String - longitud 10 completada con 0 a la izquierda  | "0000012345" 
card_holder_name| Nombre del titular de la tarjeta | String | "Pepe"
card_holder_identification.type| tipo de identificacion | String | "dni"
card_holder_identification.number| Numero de identificacion | String | "23968498"
fraud_detection.device_unique_identifier | Numero unico de identificacion | String | "12345"

#### Ejemplo de implementacion
```C#
string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";

//Ambiente.AMBIENTE_SANDBOX
//Ambiente.AMBIENTE_PRODUCCION
//Para el ambiente de desarrollo
DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

Tokens tokensData = new Tokens();

tokensData.public_token = "e7ebadfc7223838015e3b160c04b623fca7a4d0";
tokensData.issue_date = "20180427";
tokensData.merchant_id = "0000012345";
tokensData.card_holder_name = "Horacio";
tokensData.card_holder_identification.type = "single";
tokensData.card_holder_identification.number = "23968498";
tokensData.fraud_detection.device_unique_identifier = "12345";

try
{
    PaymentResponse resultPaymentResponse = decidir.Tokens(tokensData);
}
catch (ResponseException)
{

}
```
Este servicio requiere los siguientes atributos de la respuesta del Formulario de pago Todopago:
+ [Token](#formularioresponse) para el campo "tokensData.public_token"
+ [TokenDate](#formularioresponse) para el campo "tokensData.issue_date"
<a name="tokenresponse"></a>
#### Respuesta:
```C#
{
	{
	   "id": "708fe42a-c8f9-4468-8029-6d06dc3fca9a",
	   "status": "active",
	   "card_number_length": 16,
	   "date_created": "2019-01-11T12:12Z",
	   "bin": "450799",
	   "last_four_digits": "4905",
	   "security_code_length": 0,
	   "expiration_month": 8,
	   "expiration_year": 19,
	   "date_due": "2019-01-11T14:42Z",
	   "cardholder": {
	       "identification": {
	           "type": "dni",
	           "number": "33222444"
	       },
	       "name": "Comprador"
	   }
	}
}
```
El servicio [Decidir Payment](#pagodecidir) requiere el token devuelto en el Request en el campo **id** :"708fe42a-c8f9-4468-8029-6d06dc3fca9a".

<a name="pagodecidir"></a>
### Ejecución del Pago para BSA en Decidir

Luego de generar el Token de pago con el servicio anterior se deberá ejecutar la solicitud de pago de la siguiente manera. Ingresando en "token" el **token** de pago previamente generado en el servicio anterior.


|Campo | Descripcion  | Oblig | Restricciones  |Ejemplo   |
| ------------ | ------------ | ------------ | ------------ | ------------ |
|id  | id usuario que esta haciendo uso del sitio, pertenece al campo customer (ver ejemplo)  |Condicional |Sin validacion   | user_id: "marcos",  |
|site_transaction_id   | nro de operacion  |SI   | Alfanumerico de hasta 39 caracteres  | "prueba 1"  |
| site_id  |Site relacionado a otro site, este mismo no requiere del uso de la apikey ya que para el pago se utiliza la apikey del site al que se encuentra asociado.   | NO  | Se debe encontrar configurado en la tabla site_merchant como merchant_id del site_id  | 28464385  |
| token  | token generado en el servicio token de Decidir, se puede obtener desde el campo id de la respuesta. Ejemplo: "id" : "708fe42a-c8f9-4468-8029-6d06dc3fca9a"  |SI   |Alfanumerico de hasta 36 caracteres. No se podra ingresar un token utilizado para un  pago generado anteriormente.   | ""  |
| payment_method_id  | id del medio de pago en decidir  |SI  |El id debe coincidir con el medio de pago de tarjeta ingresada. Referirse a la siguiente [tabla](#mapeomediosdepago) para obtener este id según el id de medio de pago de todo pago.    | payment_method_id: 1,  |
|amount  |importe del pago   |  SI| Importe Maximo = 9223372036854775807 ($92233720368547758.07) |amount=20000  |
|currency   |moneda   | SI|Valor permitido: ARS   | ARS  |
|installments   |cuotas del pago   | SI|"Valor minimo = 1 Valor maximo = 99"     |  installments: 1 |
|payment_type   |forma de pago   | SI| Valor permitido: single / distributed
|"single"   |
|establishment_name   |nombre de comercio |Condicional   | Alfanumerico de hasta 25 caracteres |  "Nombre establecimiento"  |

<a name="mapeomediosdepago"></a>
#### Mapeo entre medios de pago de todo pago y decidir:
Utilizar el payment_method_id_decidir de la siguiente tabla según el payment_method_id_todopago para enviar como parámetro al [servicio de pago en Decidir](#pagodecidir).

| Medio de pago | payment_method_id_todopago | payment_method_id_decidir |
| ------------ | ------------ | ------------ |
|AMEX|1|65|
|DINERS|2|8|
|ARGENCARD|4|30|
|CABAL|6|27|
|MAESTRO|13|99|
|MASTERCARD|14|15|
|NARANJA|30|24|
|NATIVA|31|42|
|VISA|42|1|
|VISA DEBITO|43|31|
|CABAL DEBITO|129|67|
|RAPIPAGO|500|26|
|PAGOFACIL|501|25|
|CREDIMAS|600|38|
|VISA RECARGABLE|900|1|
|VISA EXTERIOR|903|31|
|MASTERCARD PREPAGA|906|15|
|MASTER DEBIT|907|66|
|CABAL PRECARGADA|908|27|
|MONEDERO|909|47|
|ARGENCARD|910|30|


#### Ejemplo:

```C#
string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";

//Para el ambiente de desarrollo
DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);
public static Dictionary<string, string> tpToDecidirMapper = initDecidirMapper();

//Mapeo id medio de pago de TP a Decidir
String todoPago_PaymentMethodId = (!String.IsNullOrEmpty(Globals.payment_method_id)) ? Globals.payment_method_id : "1"; //Visa por defecto
String decidir_PaymentMethodId = tpToDecidirMapper[todoPago_PaymentMethodId];

Payment payment = new Payment();



payment.site_transaction_id = "[ID DE LA TRANSACCIÓN]"; //string unico que identifica la transaccion
payment.payment_method_id = Convert.ToInt32(decidir_PaymentMethodId);
payment.token = "[TOKEN DE PAGO]"; //token de pago provisto por el servicio tokens
payment.bin = "450799";
payment.amount = 2000;
payment.currency = "ARS";
payment.installments = 1;
payment.description = "";
payment.payment_type = "single";
payment.establishment_name = "single";

try
{
    PaymentResponse resultPaymentResponse = decidir.Payment(payment);
}
catch (ResponseException)
{
}

private static Dictionary<string, string> initDecidirMapper()
{
    Dictionary<string, string> initData = new Dictionary<string, string>();
    initData.Add("1", "65"); //AMEX
    initData.Add("42", "1"); //Visa Credito
    initData.Add("43", "31"); //Visa Debito
    return initData;
}

```
Este servicio requiere el siguiente atributo de la respuesta del servicio [Token](#tokendecidir) de Decidir:
+ [id](#tokenresponse) para el campo "payment.token"
+ [paymentMethodID](#tokenresponse) convertido mediante la tabla, para el campo "payment.payment_method_id"

<a name="pagodecidirresponse"></a>
#### Respuesta:
```C#
{
    "id": 1391404,
    "site_transaction_id": "110119_02",
    "payment_method_id": 1,
    "card_brand": "Visa",
    "amount": 2000,
    "currency": "ars",
    "status": "approved",
    "status_details": {
        "ticket": "5746",
        "card_authorization_code": "151936",
        "address_validation_code": "VTE0011",
        "error": null
    },
    "date": "2019-01-11T12:19Z",
    "customer": {
        "id": "user",
        "email": "user@mail.com"
    },
    "bin": "450799",
    "installments": 1,
    "first_installment_expiration_date": null,
    "payment_type": "single",
    "sub_payments": [],
    "site_id": "00030118",
    "fraud_detection": {
        "status": null
    },
    "aggregate_data": null,
    "establishment_name": "prueba desa soft",
    "spv": null,
    "confirmed": null,
    "pan": null,
    "customer_token": "f2931755d7e472d2c553eef9026717a9cb3bb91185c6e44f6c02f8ac46b9659e",
    "card_data": "/tokens/1391404"
}
```
Los datos necesarios para el siguiente servicio [Notification Push](#pushnotification) son **status**, **ticket**, **authorization**.

<a name="pushnotification"></a>

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
	operationData.Add(ElementNames.BVG_RESULT_CODE_MEDIOPAGO, "1");
    //Opcionales. Decidir no esta enviando un codigo de resultado, soll un mensaje en el campo status
	//operationData.Add(ElementNames.BVG_RESULT_CODE_GATEWAY, "-1");
	//operationData.Add(ElementNames.BVG_ID_GATEWAY, "8");
	operationData.Add(ElementNames.BVG_RESULT_MESSAGE, "approved"); //Campo status
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
<tr><td>ResultCodeMedioPago</td><td>Optional</td><td>String</td><td>Código de respuesta de la operación propocionado por el medio de pago. </td></tr>
<tr><td>ResultCodeGateway</td><td>Optional</td><td>String</td><td>Código de respuesta de la operación propocionado por el gateway</td></tr>
<tr><td>idGateway</td><td>Optional</td><td>String</td><td>Id del Gateway que procesó el pago. Si envían el resultCodeGateway, es obligatorio que envíen este campo</td></tr>
<tr><td>ResultMessage</td><td>Optional</td><td>String</td><td>Detalle de respuesta de la operación.</td></tr>
<tr><td>OperationDatetime</td><td>Required</td><td>String</td><td>Fecha Hora de la operación en el comercio en Formato yyyyMMddHHmmssMMM</td></tr>
<tr><td>TicketNumber</td><td>Optional</td><td>String</td><td>Numero de ticket generado en la respuesta del pago a Decidir. Campo "status_details.ticket"</td></tr>
<tr><td>CodigoAutorizacion</td><td>Optional</td><td>String</td><td>Codigo de autorización de la operación generado en la respuesta del pago a Decidir. Campo "status_details.card_authorization_code"</td></tr>
<tr><td>CurrencyCode</td><td>Required</td><td>String</td><td>Valor fijo 32</td></tr>
<tr><td>OperationID</td><td>Required</td><td>String</td><td>ID de la operación en el eCommerce</td></tr>
<tr><td>Amount</td><td>Required</td><td>String</td><td>Formato 999999999,99</td></tr>
<tr><td>FacilitiesPayment</td><td>Required</td><td>String</td><td>Formato 99</td></tr>
<tr><td>Concept</td><td>Optional</td><td>String</td><td>Especifica el concepto de la operación dentro del ecommerce</td></tr>
<tr><td>PublicTokenizationField</td><td>Required</td><td>String</td><td>Token de pago de Decidir</td></tr>
<tr><td>CredentialMask</td><td>Optional</td><td>String</td><td>Mask de la tarjeta</td></tr>
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
