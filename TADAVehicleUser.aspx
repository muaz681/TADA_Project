<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADAVehicleUser.aspx.cs" Inherits="UI.HR.TADA.TADAVehicleUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"><%: Scripts.Render("~/Content/Bundle/jqueryJS") %></asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder2" runat="server"><%: Scripts.Render("~/Content/Bundle/updatedJs") %></asp:PlaceHolder>
    <%--<webopt:BundleReference ID="BundleReference1" runat="server" Path="~/Content/Bundle/updatedCss" />--%>
    <webopt:BundleReference ID="BundleReference2" runat="server" Path="~/Content/Bundle/defaultCSS" />
    <webopt:BundleReference ID="BundleReference3" runat="server" Path="~/Content/Bundle/hrCSS" />
    <link href="../../Content/CSS/bootstrap_4_5.css" rel="stylesheet" />
    <script>
        function Confirm() {
            document.getElementById("hdnconfirm").value = "0";

            var txtDteFrom = document.forms["frmpdv"]["txtDate"].value;

            if (txtDteFrom == null || txtDteFrom == "") {
                alert("From date must be filled by valid formate (yyyy-MM-dd).");
                document.getElementById("txtDteFrom").focus();
            }

            else {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden"; confirm_value.name = "confirm_value";
                if (confirm("Do you want to proceed?")) { confirm_value.value = "Yes"; document.getElementById("hdnconfirm").value = "1"; }
                else { confirm_value.value = "No"; document.getElementById("hdnconfirm").value = "0"; }
            }
        }
    </script>
    <style>
        .br_right {
            border-right: 1px solid #ccc;
        }

        .limit {
            font-size: 12px !important;
            color: red !important;
            font-weight: 600 !important;
        }

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
        <asp:ScriptManager ID="ScriptManager0" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <%--<cc1:AlwaysVisibleControlExtender TargetControlID="pnlUpperControl" ID="AlwaysVisibleControlExtender1" runat="server"></cc1:AlwaysVisibleControlExtender>--%>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <h3>TA & DA Vehicle Entry</h3>
                            <div class="btn-group btn-group-sm text-right" role="group" aria-label="Basic mixed styles example">
                                <asp:Button ID="btnSubID" runat="server" class="btn btn-sm btn-success cmmn_fnt_empList" Text="Submit" OnClick="subBtnClick" />
                            </div>
                        </div>
                        <div class="card-body secd_card_body">
                            <div class="row comm_css_row">
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lblFromDate" class="form-label commn_fnt_size" runat="server" Text="Date:  "></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" class="form-control cmmn_fnt_empList" Enabled="true" autocomplete="off"></asp:TextBox>
                                        <script type="text/javascript"> new datepickr('txtDate', { 'dateFormat': 'Y-m-d' });</script>
                                        <lord-icon
                                            src="../../Content/JS/calendar/CalenderIcon.js"
                                            trigger="hover"
                                            colors="outline:#121331,primary:#f24c00,secondary:#ebe6ef,tertiary:#4bb3fd"
                                            style="width: 27px; height: 27px; background: #fff;">
                                        </lord-icon>
                                    </div>

                                </div>
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lblTxtLoc" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Start Time: "></asp:Label>
                                        <asp:TextBox ID="strtTime" runat="server" CssClass="form-control cmmn_fnt_empList" autocomplete="off" placeholder="HH:mm" Text="23:59"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="Label1" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="End Time: "></asp:Label>
                                        <asp:TextBox ID="endTime" runat="server" CssClass="form-control cmmn_fnt_empList" autocomplete="off" placeholder="HH:mm" Text="23:59"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="lblRason" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Movement Area: "></asp:Label>
                                        <asp:TextBox ID="mvmntArea" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="Label8" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Mileage Start: "></asp:Label>
                                        <asp:TextBox ID="milStart" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-4 comm_css_col">
                                    <div class="d-flex align-items-center">
                                        <asp:Label ID="Label9" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Mileage End: "></asp:Label>
                                        <asp:TextBox ID="milEnd" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="row comm_css_row">
                                <div class="col-6 br_right comm_css_col">
                                    <div class="row comm_css_row">

                                        <div class="col-6 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="Label2" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Fuel Type: "></asp:Label>
                                                <asp:DropDownList ID="fuelTypeID" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-6 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="Label3" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Payment: "></asp:Label>
                                                <asp:DropDownList ID="paymentID" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="paymentType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Type"></asp:ListItem>
                                                    <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Credit" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-6 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="stationNam" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Station Name: "></asp:Label>
                                                <asp:DropDownList ID="stationNameID" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-6 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="lblAmnt" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Fuel Price: "></asp:Label>
                                                <asp:TextBox ID="fuelPriceID" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row comm_css_row">
                                        <div class="col-12 text-right comm_css_col">
                                            <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                <asp:Button ID="btnAdd" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Add" OnClick="addBtnClick" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row comm_css_row">
                                        <div class="col-12 comm_css_col">
                                            <div class="card">
                                                <div class="card-body secd_card_body">
                                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="10px" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                                        BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" ShowFooter="true" FooterStyle-Font-Bold="true" FooterStyle-BackColor="#999999" FooterStyle-HorizontalAlign="Right" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />

                                                        <Columns>

                                                            <asp:BoundField DataField="fuelID" Visible="false" HeaderText="Fuel ID" SortExpression="fuelID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10" />
                                                            <asp:BoundField DataField="strFuel" HeaderText="Fuel Type" SortExpression="strFuel" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />

                                                            <asp:BoundField DataField="paymentID" Visible="false" HeaderText="Payment ID" SortExpression="paymentID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10" />
                                                            <asp:BoundField DataField="strPayment" HeaderText="Payment Type" SortExpression="strPayment" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />

                                                            <asp:BoundField DataField="stationID" Visible="false" HeaderText="Station ID" SortExpression="stationID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10" />
                                                            <asp:BoundField DataField="strStation" HeaderText="Credit Station" SortExpression="strStation" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />

                                                            <asp:BoundField DataField="fuelPrc" HeaderText="Fuel Price" SortExpression="fuelPrc" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />

                                                            <asp:CommandField ShowDeleteButton="true" ControlStyle-ForeColor="Red" ControlStyle-Font-Bold="true" ControlStyle-Font-Size="8px" />

                                                        </Columns>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />


                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6 comm_css_col">
                                    <div class="row comm_css_row">
                                        <div class="col-4 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="Label5" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Service Type: "></asp:Label>
                                                <asp:DropDownList ID="serviceTypeID" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="TADAExpense_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-5 comm_css_col">
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="Label6" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Details & Reason: "></asp:Label>
                                                <asp:TextBox ID="txtDtlsReason" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-3 comm_css_col">
                                            <asp:Label ID="transID" class="form-label limit commn_fnt_size" runat="server"></asp:Label>
                                            <div class="d-flex align-items-center">
                                                <asp:Label ID="Label7" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Amount: "></asp:Label>
                                                <asp:HiddenField ID="hiddenTALimitAmount" runat="server" />

                                                <asp:TextBox ID="txtAmnt" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row comm_css_row">
                                        <div class="col-12 text-right comm_css_col">
                                            <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                <asp:Button ID="addBtnTwo" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Add" OnClick="addBtnClickTwo" />
                                                <%--<asp:Button ID="sbmtBtnTwo" runat="server" class="btn btn-sm btn-success" Text="Submit" OnClick="subBtnClickTwo" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row comm_css_row">
                                        <div class="col-12 comm_css_col">
                                            <div class="card">
                                                <div class="card-body secd_card_body">
                                                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="9px" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                                        BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" ShowFooter="true" FooterStyle-Font-Bold="true" FooterStyle-BackColor="#999999" FooterStyle-HorizontalAlign="Right" OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />

                                                        <Columns>


                                                            <asp:BoundField DataField="serviceID" Visible="false" HeaderText="Service ID" SortExpression="serviceID" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />
                                                            <asp:BoundField DataField="strService" HeaderText="Service Type" SortExpression="strService" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />
                                                            <asp:BoundField DataField="strReason" HeaderText="Reason" SortExpression="strReason" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />

                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="8px" />


                                                            <asp:CommandField ShowDeleteButton="true" ControlStyle-ForeColor="Red" ControlStyle-Font-Bold="true" />

                                                        </Columns>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />


                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <%--<script src="../../Content/JS/JQUERY/JavaScript-3-5-1.js"></script>
    <script src="../../Content/JS/bootstrap_4_5.js"></script>--%>
    <script type="text/javascript">

        $(document).ready(function () {
            Init();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Init);

        });
        function Init() {
            $("#strtTime").timepicker();
            $("#endTime").timepicker();
        }



    </script>
    <script src="../../Content/JS/calendar/Lordidcon.js"></script>
</body>
</html>
