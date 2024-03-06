<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADADetailsAuditApprov.aspx.cs" Inherits="UI.HR.TADA.TADADetailsAuditApprov" %>

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
            window.open('TADADetailsAudit.aspx?&ID=' + id, '', 'height=375, width=930, scrollbars=yes, left=250, top=200, resizable=no, title=Preview');
        }
    </script>
    <style>
        body {
            font-family: Verdana !important;
        }

        #ParentDIV {
            width: 50%;
            height: 100%;
            font-size: 12px;
            font-family: Calibri;
        }

        .calendar:nth-child(4) {
            top: 209px !important;
            left: 515px !important;
            z-index: 100 !important;
        }

        .calendar:nth-child(5) {
            top: 209px !important;
            left: 872px !important;
            z-index: 100 !important;
        }

        .comm_css_col {
            border-right: 1px solid #fff;
            padding-top: 3px !important;
            padding-bottom: 1px !important;
        }

        .comm_css_row {
            color: #000000;
            background-color: #DDD;
            border-bottom: 1px solid #fff;
            border-radius: 10px;
        }

        .commn_fnt_size {
            font-size: 11px !important;
            font-weight: 600;
            font-family: Trebuchet MS, Lucida Sans Unicode, Arial, sans-serif;
            width: 30%;
        }

        .cmmn_fnt_empList {
            font-size: 10px !important;
            overflow-y: auto;
            border-radius: 0px !important;
        }

        .comm_css_row {
            color: #000000;
            background-color: #DDD;
            border-bottom: 1px solid #fff;
            border-radius: 10px;
        }

        .comm_css_row:last-child {
            border-bottom: 1px solid transparent;
        }

        .secd_card_body {
            padding: 1px 15px 5px 16px;
        }

        h3 {
            font-size: 13px !important;
            margin-bottom: 0px !important;
        }

        h4 {
            font-size: 13px !important;
            margin-bottom: 0px !important;
        }

        .nav-tabs .nav-link {
            padding: 7px;
            font-size: 11px;
        }

        .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
            background-color: #198754 !important;
            color: #fff !important;
        }

        .nav {
            margin-bottom: 0px !important;
        }

        .first_card_body {
            padding: 5px !important;
            background-color: #DDD !important;
        }

        .card-header {
            padding: 5px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3>TA & DA Audit Approve</h3>
                        </div>
                        <div class="card-body secd_card_body">
                            <div class="row comm_css_row">

                                <div class="col-3 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lblFromDate" class="form-label commn_fnt_size" runat="server" Text="From Date:  "></asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control cmmn_fnt_empList" Enabled="true" autocomplete="off"></asp:TextBox>
                                        <script type="text/javascript"> new datepickr('txtFromDate', { 'dateFormat': 'Y-m-d' });</script>
                                        <lord-icon
                                            src="../../Content/JS/calendar/CalenderIcon.js"
                                            trigger="hover"
                                            colors="outline:#121331,primary:#f24c00,secondary:#ebe6ef,tertiary:#4bb3fd"
                                            style="width: 27px; height: 27px; background: #fff;">
                                        </lord-icon>
                                    </div>

                                </div>

                                <div class="col-3 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="Label1" class="form-label commn_fnt_size" runat="server" Text="To Date:  "></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" class="form-control cmmn_fnt_empList" Enabled="true" autocomplete="off"></asp:TextBox>
                                        <script type="text/javascript"> new datepickr('txtToDate', { 'dateFormat': 'Y-m-d' });</script>
                                        <lord-icon
                                            src="../../Content/JS/calendar/CalenderIcon.js"
                                            trigger="hover"
                                            colors="outline:#121331,primary:#f24c00,secondary:#ebe6ef,tertiary:#4bb3fd"
                                            style="width: 27px; height: 27px; background: #fff;">
                                        </lord-icon>
                                    </div>

                                </div>
                                <div class="col-3 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lblUnit" runat="server" CssClass="form-label commn_fnt_size" Font-Names="Times New Roman" Font-Size="Medium" Text="Unit :"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%"></asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-3 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lbltxtLast" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Action: "></asp:Label>
                                        <asp:Button ID="btnShow" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Show" Width="100%" OnClick="showBtnClick" />
                                    </div>

                                </div>
                            </div>
                            <div class="row comm_css_row">
                                <div class="col-12 text-right comm_css_col">
                                    <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                        <asp:Button ID="btnSubID" runat="server" class="btn btn-sm btn-success cmmn_fnt_empList" Text="Submit" OnClick="subBtnClick" />
                                        <asp:Button ID="btnRejct" runat="server" class="btn btn-sm btn-danger cmmn_fnt_empList" Text="Reject" OnClick="rejectBtnClick" />
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
                        <div class="card-body secd_card_body">
                            <asp:GridView ID="dgvlist" runat="server" AutoGenerateColumns="False" Font-Size="12px" ShowFooter="True" CellPadding="3" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Left" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" CssClass="auto-style5" Width="100%">
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

                                    <asp:TemplateField HeaderText="Action" SortExpression="intId">
                                        <ItemTemplate>
                                            <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                <asp:Button ID="btnEdit" runat="server" class="btn btn-sm btn-primary" Text="Details" OnClick="editBtnClick" CommandArgument='<%# Eval("TadaID") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
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
    <script src="../../Content/JS/calendar/Lordidcon.js"></script>
</body>
</html>
