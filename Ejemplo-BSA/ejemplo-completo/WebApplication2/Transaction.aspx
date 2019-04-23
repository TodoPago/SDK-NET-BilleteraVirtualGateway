<%@ Page Title="Transaction" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="WebApplication2.Transaction" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container" style="margin:10px;">
		<div class="w-section">
		    <div >
                <% if (!String.IsNullOrEmpty(response))
                    {%>
			    <div class="alert alert-success"><p>Transaction response Correcto</p></div>
			    <pre>
				    <%=response%>
			    </pre>
                <%}
    else
    { %>
			    <div class="alert alert-danger"><p>Transaction error</p></div>
			    <pre>
				    <%=responseError%>
			    </pre>
			    
                <%} %>
                <br><br><a href="Default">Volver al listado</a>
		    </div>
	    </div>
    </div>
</asp:Content>
