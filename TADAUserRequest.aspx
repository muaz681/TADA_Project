<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADAUserRequest.aspx.cs" Inherits="UI.HR.TADA.TADAUserRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"><%: Scripts.Render("~/Content/Bundle/jqueryJS") %></asp:PlaceHolder>
    <webopt:BundleReference ID="BundleReference2" runat="server" Path="~/Content/Bundle/defaultCSS" />
    <webopt:BundleReference ID="BundleReference3" runat="server" Path="~/Content/Bundle/hrCSS" />
    <link href="../../Content/CSS/bootstrap5.css" rel="stylesheet" />

    <script>
        function Confirm() {
            document.getElementById("hdnconfirm").value = "0";

            var txtDteFrom = document.forms["frmpdv"]["txtFromDate"].value;
            var txtDteTo = document.forms["frmpdv"]["txtToDate"].value;

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

    </script>
    <script type="text/javascript">  
        function Confirm() {
            document.getElementById("hdnconfirm").value = "0";

            var txtDteFrom = document.forms["frmpdv"]["fromDate"].value;
            var txtDteTo = document.forms["frmpdv"]["endDate"].value;;

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
    </script>
    <style type="text/css">
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
            margin-bottom: 0px !important;
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

        .nav-pills .nav-link {
            padding: 7px;
            font-size: 11px;
        }

            .nav-pills .nav-link.active, .nav-pills .show > .nav-link {
                background-color: #198754 !important;
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
        <asp:ScriptManager ID="ScriptManager0" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body first_card_body">
                            <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="publicTab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Private</button>
                                </li>
                                <% 
                                    TADA_BLL.HRTADA.EmployeeTADA_BLL bll = new TADA_BLL.HRTADA.EmployeeTADA_BLL();
                                    System.Data.DataTable dt = new System.Data.DataTable();
                                    int actionBy = int.Parse(HttpContext.Current.Session[UI.ClassFiles.SessionParams.USER_ID].ToString());
                                    dt = bll.GetTADAUserReq(15, actionBy, DateTime.Now, DateTime.Now, "", "", 0, "", 0, "");
                                    if (dt.Rows.Count > 0)
                                    {
                                %>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="privateTab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Public</button>
                                </li>
                                <%} %>
                            </ul>
                            <div class="tab-content" id="pills-tabContent">
                                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="publicTab" tabindex="0">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    <h3>TA & DA Advance Request</h3>
                                                </div>
                                                <div class="card-body secd_card_body">
                                                    <div class="row comm_css_row">
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblEnroll" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Enroll: "></asp:Label>
                                                                <asp:Label class="form-control cmmn_fnt_empList" ID="enrollID" Font-Size="Medium" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblJrnyStart" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Start Date: "></asp:Label>

                                                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control cmmn_fnt_empList" autocomplete="off"></asp:TextBox>
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
                                                                <asp:Label ID="Label1" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="End Date: "></asp:Label>

                                                                <asp:TextBox ID="txtToDate" runat="server" class="form-control cmmn_fnt_empList" autocomplete="off"></asp:TextBox>
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
                                                                <asp:Label ID="lblTxtLoc" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Start Location: "></asp:Label>
                                                                <asp:TextBox ID="txtStartLc" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">

                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lbltxtLast" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Destination: "></asp:Label>
                                                                <asp:TextBox ID="txtJrnDest" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblAmnt" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Estimate Amount: "></asp:Label>

                                                                <asp:TextBox ID="txtAmnt" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblRason" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Reason: "></asp:Label>

                                                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label2" class="form-label commn_fnt_size" runat="server" Text="Action: "></asp:Label>
                                                                <asp:Button Style="width: 80%;" ID="btnSbmt" runat="server" class="btn btn-success btn-sm cmmn_fnt_empList" Text="Submit" OnClick="btnSbmt_Click"></asp:Button>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    TA & DA Status
                                                </div>
                                                <div class="card-body secd_card_body">
                                                    <asp:GridView ID="dgvlist" runat="server" AutoGenerateColumns="False" Font-Size="11px" ShowFooter="True" CellPadding="3" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" CssClass="auto-style5" Width="100%">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="estAmount" Font-Size="9px" class="form-control" runat="server" Text='<%# Bind("estimatAmount") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="strtDate" Font-Size="9px" class="form-control" runat="server" Text='<%# Bind("startDate") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="End Date" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="enDate" class="form-control" Font-Size="9px" runat="server" Text='<%# Bind("endDate") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Location" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="strtLocation" class="form-control" Font-Size="9px" runat="server" Text='<%# Bind("JourneyStartLocation") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Destination" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="destination" runat="server" Font-Size="9px" class="form-control" Text='<%# Bind("JourneyDestination") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="reason" runat="server" Font-Size="9px" class="form-control" Text='<%# Bind("reason") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supervisor Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="suppAppStatus" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("supapproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HR Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="hrAppStatus" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("hrApproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Account Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="assApproval" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("accApproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Superisor Approval Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="supAppAmount" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("supAprvalAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HR Approval Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="hrAppAmount" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("hrApprovalAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Action" SortExpression="srId">
                                                                <ItemTemplate>
                                                                    <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                                        <asp:Button ID="btnUpId" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Update" OnClick="updBtnClick" CommandArgument='<%# Eval("intID") %>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
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
                                                <div class="card-body secd_card_body">
                                                    <asp:GridView ID="dgvlist2" runat="server" AutoGenerateColumns="False" Font-Size="12px" ShowFooter="True" CellPadding="3" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Left" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" CssClass="auto-style5" Width="100%">
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="User Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="estAmount" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("estimatAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="strtDate" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("startDate", "{0:dd MMMM yyyy} ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="End Date" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="enDate" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("endDate", "{0:dd MMMM yyyy} ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Location" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="strtLocation" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("JourneyStartLocation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Destination" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="destination" runat="server" class="form-label" Font-Size="11px" Text='<%# Bind("JourneyDestination") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="reason" runat="server" class="form-label" Font-Size="11px" Text='<%# Bind("reason") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supervisor Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="suppAppStatus" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("supapproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HR Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="hrAppStatus" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("hrApproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Account Approval" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="assApprovalst" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("accApproval") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Superisor Approval Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="supAppAmount" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("supAprvalAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HR Approval Amount" SortExpression="srId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="hrAppAmount" Font-Size="11px" class="form-label" runat="server" Text='<%# Bind("hrApprovalAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>


                                                        </Columns>

                                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
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
                                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="privateTab" tabindex="0">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    <h3>TA & DA Advance Request</h3>
                                                </div>
                                                <div class="card-body secd_card_body">
                                                    <div class="row comm_css_row">
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label3" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Enroll: "></asp:Label>
                                                                <asp:TextBox class="form-control cmmn_fnt_empList" ID="enrlID" Font-Size="Medium" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="ACE" runat="server" TargetControlID="enrlID"
                                                                    ServiceMethod="GetSearchAssignedTo" MinimumPrefixLength="1" CompletionSetCount="1"
                                                                    CompletionInterval="1" FirstRowSelected="true" EnableCaching="false" CompletionListCssClass="autocomplete_completionListElementBig"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label5" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Start Date: "></asp:Label>

                                                                <asp:TextBox ID="fromDate" runat="server" class="form-control cmmn_fnt_empList" autocomplete="off"></asp:TextBox>
                                                                <script type="text/javascript"> new datepickr('fromDate', { 'dateFormat': 'Y-m-d' });</script>
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
                                                                <asp:Label ID="Label6" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="End Date: "></asp:Label>

                                                                <asp:TextBox ID="endDate" runat="server" class="form-control cmmn_fnt_empList" autocomplete="off"></asp:TextBox>
                                                                <script type="text/javascript"> new datepickr('endDate', { 'dateFormat': 'Y-m-d' });</script>
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
                                                                <asp:Label ID="Label7" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Start Location: "></asp:Label>
                                                                <asp:TextBox ID="strLoc" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">

                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label8" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Destination: "></asp:Label>
                                                                <asp:TextBox ID="endLoc" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label9" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Estimate Amount: "></asp:Label>

                                                                <asp:TextBox ID="totalAmount" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label10" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Reason: "></asp:Label>

                                                                <asp:TextBox ID="strReasn" TextMode="MultiLine" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label11" class="form-label commn_fnt_size" runat="server" Text="Action "></asp:Label>
                                                                <asp:Button Style="width: 80%;" ID="btSubAdmID" runat="server" class="btn btn-success btn-sm cmmn_fnt_empList" Text="Submit" OnClick="btnSbmt_Adm_CLick"></asp:Button>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </form>
    <script src="../../Content/JS/bootstrap5.js"></script>
    <script src="../../Content/JS/calendar/Lordidcon.js"></script>
</body>
</html>
