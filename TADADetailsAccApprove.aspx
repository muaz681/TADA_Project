<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADADetailsAccApprove.aspx.cs" Inherits="UI.HR.TADA.TADADetailsAccApprove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"><%: Scripts.Render("~/Content/Bundle/jqueryJS") %></asp:PlaceHolder>
    <webopt:BundleReference ID="BundleReference2" runat="server" Path="~/Content/Bundle/defaultCSS" />
    <webopt:BundleReference ID="BundleReference3" runat="server" Path="~/Content/Bundle/hrCSS" />
    <link href="../../Content/CSS/bootstrap_4_5.css" rel="stylesheet" />
    <script>
        function Confirm() {
            document.getElementById("hdnconfirm").value = "0";

            var txtDteFrom = document.forms["frmpdv"]["txtFromDate"].value;
            var txtDteTo = document.forms["frmpdv"]["txtToDate"].value;;

            if (txtDteFrom == null || txtDteFrom == "") {
                alert("From date must be filled by valid formate (yyyy-MM-dd).");
                document.getElementById("txtDteFrom").focus();
            }



            else if (txtDteTo == null || txtDteTo == "") {
                alert("To date must be filled by valid formate (yyyy-MM-dd).");
                document.getElementById("txtDteTo").focus();
            }

            else {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden"; confirm_value.name = "confirm_value";
                if (confirm("Do you want to proceed?")) { confirm_value.value = "Yes"; document.getElementById("hdnconfirm").value = "1"; }
                else { confirm_value.value = "No"; document.getElementById("hdnconfirm").value = "0"; }
            }
        }
        function CheckAll(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=dgvlist.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function Viewdetails(id, type) {
            window.open('TADADetailsAdmin.aspx?&ID=' + id, '', 'height=375, width=930, scrollbars=yes, left=250, top=200, resizable=no, title=Preview');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3>TADA Account Approve</h3>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                
                                <div class="col-3">
                                    <asp:Label ID="lblFromDate" class="form-label" runat="server" Text="From Date:  "></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control" Enabled="true" autocomplete="off"></asp:TextBox>
                                    <script type="text/javascript"> new datepickr('txtFromDate', { 'dateFormat': 'Y-m-d' });</script>
                                </div>
                                
                                <div class="col-3">
                                    <asp:Label ID="Label1" class="form-label" runat="server" Text="To Date:  "></asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control" Enabled="true" autocomplete="off"></asp:TextBox>
                                    <script type="text/javascript"> new datepickr('txtToDate', { 'dateFormat': 'Y-m-d' });</script>
                                </div>
                                <div class="col-3">
                                    <asp:Label ID="lblUnit" runat="server" CssClass="col-md-12 col-sm-12 col-xs-12" Font-Names="Times New Roman" Font-Size="Medium" Text="Unit :"></asp:Label>
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control col-md-12 col-sm-12 col-xs-12" Font-Names="Times New Roman" Width="100%"></asp:DropDownList>
                                </div>
                                <div class="col-3">
                                    <asp:Label ID="lbltxtLast" class="form-label" runat="server" Font-Size="12" Text="Action: "></asp:Label>
                                    <asp:Button ID="btnShow" runat="server" class="btn btn-sm btn-info d-block" Text="Show" Width="100%" OnClick="showBtnClick" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-right py-4">
                                    <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                        <asp:Button ID="btnSubID" runat="server" class="btn btn-sm btn-success" Text="Submit" OnClick="subBtnClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:GridView ID="dgvlist" runat="server" AutoGenerateColumns="False" Font-Size="12px" ShowFooter="True" CellPadding="3" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Left" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" CssClass="auto-style5" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkbxALL" runat="server" onclick="CheckAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkbx" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Auto ID" SortExpression="srId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="intAutoID" Font-Size="11px" runat="server" Text='<%# Bind("TadaID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Enroll" SortExpression="srId">
                                        <ItemTemplate>
                                            <asp:Label ID="intEnrollID" Font-Size="11px" runat="server" Text='<%# Bind("RequesterEnroll") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name" SortExpression="srId">
                                        <ItemTemplate>
                                            <asp:Label ID="name" Font-Size="11px" runat="server" Text='<%# Bind("strEmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation" SortExpression="srId">
                                        <ItemTemplate>
                                            <asp:Label ID="designationID" Font-Size="11px" runat="server" Text='<%# Bind("strDesignation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="estAmount" class="form-control" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditApprovedAmount","{0:0}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>


                                </Columns>

                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../../Content/JS/JQUERY/JavaScript-3-5-1.js"></script>
    <script src="../../Content/JS/bootstrap_4_5.js"></script>
</body>
</html>
