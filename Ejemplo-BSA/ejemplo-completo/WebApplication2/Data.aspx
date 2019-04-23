<%@ Page Title="Data" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="WebApplication2.Data" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="container">
	<div id="content">
		<div class="w-content">
		  <div class="w-section"></div>
			<div id="m-status" style="margin-bottom: 300px">
                       
						<table id="tablelist" class="full">
							<tbody>
								<tr style="background-color: #bee2be;">
								  <td><b>Ambiente</b></td><td>
                                      <select name="environment">
                                          <option value="test" <% if (String.Equals("test", configData["environment"])) {%>selected="selected" <%} %>>Pruebas</option>
                                          <option value="prod" <% if (String.Equals("prod", configData["environment"])) {%>selected="selected" <%} %>>Produccion</option>
                                      </select>
								  </td>
								</tr>
								<tr style="background: gray;">
									<td><b>Datos Decidir</b></td>
									<td></td>
								</tr>
								<tr>
							  	   <td><b>Public Key</b></td><td>
                                         <input type="text" name="key_public" value="<%=configData["key_public"] %>" required>
							  	   <div class="description">ejemplo: 41cbc74acc604a109157bb8394561d27</div>
							  	</td>
							  	</tr>
								<tr>
							  	   <td><b>Private Key</b></td><td>
                                         <input type="text" name="key_private" value="<%=configData["key_private"] %>" required>
							  	   <div class="description">ejemplo: 1fb6dc55c0a1489db411a8ee8f9c9707</div>
							  	</td>
							  	</tr>
								<tr>
								  <td><b>Operacion </b></td><td>
                                      <input type="text" name="operacion" value="<%=configData["operacion"] %>"  disabled ></td>
								</tr>
								<tr>
								  <td><b>Currency</b></td><td><input type="text" name="currencydec" value="<%=configData["currencydec"] %>" required></td>
								</tr>
								<tr>
								  <td><b>User Id</b></td><td><input type="text" name="user_id" value="<%=configData["user_id"] %>" required></td>
								</tr>
								<tr>
								  <td><b>User mail</b></td><td><input type="email" name="user_mail" value="<%=configData["user_mail"] %>"  required ></td>
								</tr>
								<tr>
								  <td><b>Medio Pago</b></td><td><select name="decpaymentmethod">
                                      <option value="1">VISA</option>
                                      <option value="6" >AMEX</option>
                                      <option value="15" >MASTERCARD</option></select>
								  </td>
								</tr>
								<tr>
								  <td>
								  	<b>Bin</b></td><td><input type="text" name="bin" value="<%=configData["bin"] %>" required>
								  	<div class="clear">
							  	   		<div class="description">Primeros seis numeros de la tarjeta</div>
							  	   	</div>	
								  </td>
								</tr>
								<tr>
								  <td><b>Cuotas</b></td><td><select name="cuotas">
                                      <option value="1">1</option>
                                      <option value="6" >6</option>
                                      <option value="12" >12</option></select></td>
							    </tr>
								<tr>
								  <td><b>Nombre de establecimiento</b></td><td>
                                      <input type="text" name="establishment" value="<%=configData["establishment"] %>" required></td>
								</tr>
								<tr style="background: gray;">
								  <td><b>Datos Todopago</b></td>
								  <td></td>
								</tr>
								<tr>
							  	   <td><b>Merchant</b></td><td><input type="text" name="merchant" value="<%=configData["merchant"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>Security</b></td><td><input type="text" name="security" value="<%=configData["security"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>channel</b></td><td><input type="text" name="channel" value="<%=configData["channel"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>Tipo de operacion</b></td><td><input type="text" name="operationtype" value="<%=configData["operationtype"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>Codigo de moneda</b></td><td><input type="text" name="currency" value="<%=configData["currency"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>Concepto</b></td><td><input type="text" name="concept" value="<%=configData["concept"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td><b>Monto</b></td><td><input type="text" name="amount" value="<%=configData["amount"] %>" required></td>
							  	</tr>
							  	<tr>
							  	   <td>
							  	         <b>Preselección de pago del usuario</b>
							  	   </td>
							  	   <td>
							  	   	<div class="left">
							  	   		<input type="text" name="buyerpreselectionmp" id="preselectionmp" size="10" value="<%=configData["buyerpreselectionmp"] %>">
							  	   	</div>
							  	   	<div class="left">
							  	   		<input type="text" name="buyerpreselectionbank" id="preselectionbank" size="10" value="<%=configData["buyerpreselectionbank"] %>">
							  	   	</div>
							  	   	<div class="clear">
							  	   		<div class="description">ejemplo: 1-17 (id medio de pago y id de banco)</div>
							  	   	</div>
							  	   </td>
							  	</tr>
							  	<tr>
							  	   <td><b>Medios de pago disponibles</b></td><td><input type="text" name="availablepaymentmethods" value="<%=configData["availablepaymentmethods"] %>">
							  	   	<div class="description">ejemplo: 1,2,3</div>
							  	   </td>
							  	</tr>
							  	<tr>
							  	   <td><b>Bancos disponibles</b></td><td><input type="text" name="availablebanks" value="<%= configData["availablebanks"] %>">
							  	   	<div class="description">ejemplo: 1,2,17 (Si no se envían datos se habilitan todos los bancos del usuario)</div>
							  	   </td>
							  	</tr>
							</tbody>
                         
							<tfoot>
							  <tr>
								<td colspan="2"><a href="Default" class="btn error site">Ir al Listado</a>&nbsp;&nbsp;&nbsp;
                                    <input id="btn-save" type="submit" value="Guardar datos" class="submit btn site"  onclick='this.form.action="Data?action=save&id_operation=<%=configData["operacion"] %>";' >&nbsp;&nbsp;&nbsp;
                                   
								</td>
							  </tr>
							</tfoot>
						</table>
			</div>
		</div>
		<div class="clearfix"></div>
	</div>
	<div id="footer">
	</div>

</div>
</asp:Content>
