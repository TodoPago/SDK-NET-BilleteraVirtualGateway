<%@ Page Title="PagoTP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagoTP.aspx.cs" Inherits="WebApplication2.PagoTP" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://forms.integration.todopago.com.ar/resources/TPBSAForm.min.js"></script>    
    <div class="contentContainer">
    </div>
    <script>    
        var success = function(data) {
			console.log(data);
			console.log(data.PaymentMethodID);
            document.forms[0].token_todopago.value = data.Token;
			document.forms[0].token_date.value = data.TokenDate;
			document.forms[0].payment_method_id.value = data.PaymentMethodID;
            document.forms[0].response_tp.value = JSON.stringify(data);
			document.forms[0].enviar.click();
        };
        
        var error = function(data) {
            console.log(data);
        };
    
        var validation = function(data) {
            console.log(data);
        }

        window.TPFORMAPI.hybridForm.initBSA({
            publicKey: "<%= publicRequestKey%>",
            merchantAccountId: <%= merchant_todopago%>,
            callbackCustomSuccessFunction: "success",
            callbackCustomErrorFunction: "error",
            callbackValidationErrorFunction: "validation"
        });
        
    </script>
    <input type="hidden" name="token_todopago" id="token_todopago"/>
    <input type="hidden" name="token_date" id="token_date" />
	<input type="hidden" name="payment_method_id" id="payment_method_id" />
    <input type="hidden" name="response_tp" id="response_tp" />
    <input type="submit" style="display:none" id="enviar" name="enviar" />
</asp:Content>
