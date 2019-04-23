<%@ Page Title="PagoDecidir" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagoDecidir.aspx.cs" Inherits="WebApplication2.PagoDecidir" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="container" style="margin:10px;">
		<div class="w-section">
		    <div >
                <% if (!String.IsNullOrEmpty(responseTokenizer))
                    { %>
			    <div class="alert alert-success"><p>Tokenizer response Correcto</p></div>
			    <pre>
				    <%=responseTokenizer%>
			    </pre>
                <%} %>

                <% if (!String.IsNullOrEmpty(responsePayment))
                    { %>
                <div class="alert alert-success"><p>Payment response Correcto</p></div>
			    <pre>
				    <%=responsePayment%>
			    </pre>
                <%} %>

			    <% if (!String.IsNullOrEmpty(responseError))
                    { %>
			    <div class="alert alert-danger"><p>Tokenizer o payment error</p></div>
			    <pre>
				    <%=responseError%>
			    </pre>
                <%} %>
			    <br><br><a href="Default">Volver al listado</a>

		    </div>
	    </div>
    </div>
</asp:Content>
