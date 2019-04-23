<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
			<div id="content">
			<div class="w-content">

			<div class="w-section"></div>
			<div class="block">
			<table id="tablelist" class="full tablesorter">
			<thead>
				<tr>
					<th class="header">Operacion</th>
					<th class="header">Datos</th>
					<th class="header">Transaction BSA</th>
					<th class="header">Estado de transaccion</th>
					<th class="header">Pagar Todopago</th>
					<th class="header">Estado del Pago</th>
					<th class="header">Confirmar pago Decidir</th>
					<th class="header">Estado del Pago</th>
					<th class="header">Pushnotification BSA</th>
					<th class="header">Estado pushnotification</th>
                    <th></th>
				</tr>
			</thead>
			<tbody>
            <% foreach (var test_data in test_list) {  %>
                 
		    	        <tr>
		      		        <td><%= test_data.id_operation %> </td>
		      		        <td>
		      			        <a href="Data?action=edit&id_operation=<%= test_data.id_operation %>" >VER CARGADOS</a>
		      		        </td>
		      		        <td><a href="Transaction?action=transaction&id_operation=<%= test_data.id_operation %>" class="btn site btn-sm ">Transaction</a></td>
		      		        <td>
		      			        <!-- muestra el estado -->	
		      			         <a href="#" id="myBtn-transaction-<%= test_data.id_operation %>" ><%=test_data.transaction.status %></a>
		      		        </td>

		      		        <td><a href="PagoTP?action=formTP&id_operation=<%= test_data.id_operation %>" class="btn site btn-sm">Pagar TP</a></td>
		      		        <td>
		      			        <!-- muestra el estado -->
		      			        <a href="#" id="myBtn-formTP-<%= test_data.id_operation %>" ><%=test_data.formTP.status %></a>
		      		        </td>

		      		        <td><a href="PagoDecidir?action=decidir_payment&id_operation=<%= test_data.id_operation %>" class="btn site btn-sm">Pago Decidir</a></td>
		      		        <td>
		      			        <!-- muestra el estado -->
		      			        <a href="#" id="myBtn-decidir_payment-<%= test_data.id_operation %>" ><%=test_data.decidir_payment.status %></a>		
		      		        </td>

		      		        <td><a href="PushNotification?action=push_notification&id_operation=<%= test_data.id_operation %>" class="btn site btn-sm">Pushnotification</a></td>
					        <td>
						        <!-- muestra el estado -->
						        <a href="#"  id="myBtn-push_notification-<%= test_data.id_operation %>" ><%=test_data.push_notification.status %></a>	
					        </td>
                            <td><a href="Data?action=delete&id_operation=<%= test_data.id_operation %>" class="btn site btn-sm">Eliminar</a></td>
		      	        </tr>
            <% } %>
			</tbody>
			<tfoot>
			  <tr>
			    <td colspan="10"><a href="Data?action=new" class="btn info">Nuevo</a></td>
			  </tr>
			</tfoot>
			</table>

            <!-- Make Modals -->
            <% foreach (var test_data in test_list)
               {
                     %>
                        <!-- Modal Transaction-->
                        <div class="modal fade" id="myModal-transaction-<%= test_data.id_operation %>" role="dialog">
                            <div class="modal-dialog" style="left: -10%;">
                                <!-- Modal content-->
                                <div class="modal-content" style=" width:130%;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Modal Header</h4>
                                </div>
                                    <% //=test.transaction.request %>
                                <div class="modal-body">
                                    <table id="table-status-transaction-<%= test_data.id_operation %>" class="full tablesorter">
				                        <tr>
					                        <td >Estado</td>
                                            <td class="modal-cell"><%=((test_data.transaction != null) ? test_data.transaction.status : "") %></td>
                                        </tr>
					                    <tr>
					                        <th >Request</th>
                                            <td text-align: left;" class="modal-cell"><%=((test_data.transaction != null) ? test_data.transaction.request_json: "") %></td>
                                        </tr>
                                        <tr>
   					                        <th >Response</th>
                                            <td  class="modal-cell"><%=((test_data.transaction != null) ? test_data.transaction.response_json : "") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal formTP-->
                        <div class="modal fade" id="myModal-formTP-<%= test_data.id_operation %>" role="dialog">
                            <div class="modal-dialog" style="left: -10%;">
                                <!-- Modal content-->
                                <div class="modal-content" style=" width:130%;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Modal Header</h4>
                                </div>
                                    <% //=test.transaction.request %>
                                <div class="modal-body">
                                    <table id="table-status-formTP-<%= test_data.id_operation %>" class="full tablesorter">
				                        <tr>
					                        <td >Estado</td>
                                            <td  class="modal-cell"><%=((test_data.formTP != null) ? test_data.formTP.status : "") %></td>
                                        </tr>
					                    <tr>
					                        <th >Request</th>
                                            <td class="modal-cell"><%=((test_data.formTP != null) ? test_data.formTP.request_json : "") %></td>
                                        </tr>
                                        <tr>
   					                        <th >Response</th>
                                            <td class="modal-cell"><%=((test_data.formTP != null) ? test_data.formTP.response_json: "") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal decidir_payment-->
                        <div class="modal fade" id="myModal-decidir_payment-<%= test_data.id_operation %>" role="dialog">
                            <div class="modal-dialog" style="left: -10%;">
                                <!-- Modal content-->
                                <div class="modal-content" style=" width:130%;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Modal Header</h4>
                                </div>
                                    <% //=test.transaction.request %>
                                <div class="modal-body">
                                    <table id="table-status-decidir_payment-<%= test_data.id_operation %>" class="full tablesorter">
				                        <tr>
					                        <td >Estado</td>
                                            <td class="modal-cell"><%=((test_data.decidir_payment != null) ? test_data.decidir_payment.status : "") %></td>
                                        </tr>
					                    <tr>
					                        <th >Request</th>
                                            <td class="modal-cell"><%=((test_data.decidir_payment != null) ? test_data.decidir_payment.request_json : "") %></td>
                                        </tr>
                                        <tr>
   					                        <th >Response</th>
                                            <td class="modal-cell"><%=((test_data.decidir_payment != null) ? test_data.decidir_payment.response_json : "") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal push_notification-->
                        <div class="modal fade" id="myModal-push_notification-<%= test_data.id_operation %>" role="dialog">
                            <div class="modal-dialog" style="left: -10%;">
                                <!-- Modal content-->
                                <div class="modal-content" style=" width:130%;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Modal Header</h4>
                                </div>
                                    <% //=test.transaction.request %>
                                <div class="modal-body">
                                    <table id="table-status-push_notification-<%= test_data.id_operation %>" class="full tablesorter">
				                        <tr>
					                        <td >Estado</td>
                                            <td class="modal-cell"><%=((test_data.push_notification != null) ? test_data.push_notification.status : "") %></td>
                                        </tr>
					                    <tr>
					                        <th >Request</th>
                                            <td class="modal-cell"><%=((test_data.push_notification != null) ? (string )  test_data.push_notification.request_json : "") %></td>
                                        </tr>
                                        <tr>
   					                        <th >Response</th>
                                            <td class="modal-cell"><%=((test_data.push_notification != null) ? (string) test_data.push_notification.response_json : "") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                                </div>
                            </div>
                        </div>

            <%     
               }
                    %>
           
            <script> 
                $(document).ready(function () {

                    $('body #tablelist').on('click', 'a', function () {
                        var id_op = $(this).attr('id'); //myBtn-transaction-sdk_net_1626547311
                        var arr_op = id_op.split("-");
                        
                        var operation = arr_op[1];
                        var id = arr_op[2];
                        console.log(id_op); 
                        $(".modal-title").text(operation + " - " + id);
                        console.log("#myModal-" +operation + "-" + id);       
                        $("#myModal-" +operation + "-" + id ).modal();
                        

                    });

                });
            </script>
            <style>
                    .modal-cell {
                       word-wrap: break-word; 
                       max-width:200px; 
                       width:200%;
                       text-align: left;
                    }
            </style>
            <!------------------>

			<div>Nota: Realizar las compras de Decidir sin Cybersource</div>
		</div>
	</div>
		<div class="clearfix"></div>
	</div>
</div>

</asp:Content>
