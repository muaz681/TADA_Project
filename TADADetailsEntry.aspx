<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADADetailsEntry.aspx.cs" Inherits="UI.HR.TADA.TADADetailsEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"><%: Scripts.Render("~/Content/Bundle/jqueryJS") %></asp:PlaceHolder>
    <webopt:BundleReference ID="BundleReference2" runat="server" Path="~/Content/Bundle/defaultCSS" />
    <webopt:BundleReference ID="BundleReference3" runat="server" Path="~/Content/Bundle/hrCSS" />
    <link href="../../Content/CSS/bootstrap_4_5.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                localStorage.setItem('activeTab', $(e.target).attr('href'));
            });
            var activeTab = localStorage.getItem('activeTab');
            if (activeTab) {
                $('#myTab a[href="' + activeTab + '"]').tab('show');
            }
        });
    </script>

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
    </script>
    <style>
        .limit {
            font-size: 12px !important;
            color: red !important;
            font-weight: 600 !important;
        }

        #AdjAmountID {
            color: red !important;
        }

        .mode {
            float: right;
        }

        .change {
            cursor: pointer;
            border: 1px solid #555;
            border-radius: 40%;
            width: 20px;
            text-align: center;
            padding: 5px;
            margin-left: 8px;
        }

        .dark {
            background-color: #222;
            color: #e6e6e6;
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
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">

                        <div class="card-body first_card_body">
                            <ul class="nav nav-tabs" id="myTab">
                                <li class="nav-item">
                                    <a href="#sectionA" class="nav-link active" data-toggle="tab">Regular</a>
                                </li>
                                <% 
                                    IT_BLL.DBInfo.AssetReturnBLL bll = new IT_BLL.DBInfo.AssetReturnBLL();
                                    System.Data.DataTable dt = new System.Data.DataTable();
                                    int actionBy = int.Parse(HttpContext.Current.Session[UI.ClassFiles.SessionParams.USER_ID].ToString());
                                    dt = bll.GetTadaAdvData(actionBy);
                                    if (dt.Rows.Count > 0)
                                    {
                                %>
                                <li class="nav-item">
                                    <a href="#sectionB" class="nav-link" data-toggle="tab">Adjustment</a>
                                </li>
                                <% } %>
                            </ul>
                            <div class="tab-content">
                                <div id="sectionA" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    <h3>TA & DA Details Entry</h3>
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
                                                                <asp:Label ID="lblTxtLoc" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="From Address: "></asp:Label>
                                                                <asp:TextBox ID="txtStartLc" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lbltxtLast" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="To Address: "></asp:Label>
                                                                <asp:TextBox ID="txtJrnDest" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="row comm_css_row">
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label2" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Type: "></asp:Label>
                                                                <asp:DropDownList ID="typeID" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="TADAExpense_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label3" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Category: "></asp:Label>
                                                                <asp:TextBox ID="cateName" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblAmnt" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Amount: "></asp:Label>
                                                                <asp:HiddenField ID="hiddenTALimitAmount" runat="server" />
                                                                <asp:Label ID="transID" class="form-label limit" runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtAmnt" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblRason" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Reason: "></asp:Label>
                                                                <asp:TextBox ID="txtReason" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">
                                                        <div class="col-12 text-right comm_css_col">
                                                            <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                                <asp:Button ID="btnAdd" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Add" OnClick="addBtnClick" />
                                                                <asp:Button ID="btnSubID" runat="server" class="btn btn-sm btn-success cmmn_fnt_empList" Text="Submit" OnClick="subBtnClick" />
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
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="10px" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                                        BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" ShowFooter="true" FooterStyle-Font-Bold="true" FooterStyle-BackColor="#999999" FooterStyle-HorizontalAlign="Right" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />

                                                        <Columns>

                                                            <asp:BoundField DataField="FromDate" HeaderText="From Date." SortExpression="FromDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="ToDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="fromAddress" HeaderText="From Addr." SortExpression="fromAddress" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="toAddress" HeaderText="To Address" SortExpression="toAddress" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="typeID" Visible="false" HeaderText="Type ID" SortExpression="typeID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="typeName" HeaderText="Type Name" SortExpression="typeName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="catName" HeaderText="Category" SortExpression="catName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />

                                                            <asp:BoundField DataField="strReason" HeaderText="Reason" SortExpression="strReason" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />



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
                                <div id="sectionB" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    <h3>TA & DA Adjustment Entry</h3>
                                                </div>
                                                <div class="card-body secd_card_body">
                                                    <div class="row comm_css_row">
                                                        <div class="col-6 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblTada" runat="server" CssClass="form-label commn_fnt_size" Text="Advance TA & DA List :"></asp:Label>
                                                                <asp:DropDownList ID="TadaList" runat="server" CssClass="form-control cmmn_fnt_empList" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="TADAList_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-4 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="lblAdj" class="form-label commn_fnt_size" runat="server" Text="Adjustment Amount: "></asp:Label>
                                                                <asp:HiddenField ID="AdjHiidenAmount" runat="server" />
                                                                <asp:Label ID="AdjAmountID" runat="server" class="form-control cmmn_fnt_empList"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label4" class="form-label commn_fnt_size" runat="server" Text="From Date: "></asp:Label>
                                                                <asp:Label ID="fromDateID" runat="server" class="form-control cmmn_fnt_empList"></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label5" class="form-label commn_fnt_size" runat="server" Text="To Date: "></asp:Label>
                                                                <asp:Label ID="ToDateID" runat="server" class="form-control cmmn_fnt_empList"></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label7" class="form-label commn_fnt_size" runat="server" Text="From Address: "></asp:Label>
                                                                <asp:Label ID="FromAddressID" runat="server" class="form-control cmmn_fnt_empList"></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label6" class="form-label commn_fnt_size" runat="server" Text="To Address: "></asp:Label>
                                                                <asp:Label ID="ToAddressID" runat="server" class="form-control cmmn_fnt_empList"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label8" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Type: "></asp:Label>
                                                                <asp:DropDownList ID="typeList" runat="server" CssClass="form-control cmmn_fnt_empList" Font-Names="Times New Roman" Width="100%"></asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label9" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Category: "></asp:Label>
                                                                <asp:TextBox ID="categoryList" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label10" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Amount: "></asp:Label>
                                                                <asp:TextBox ID="amountList" TextMode="Number" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-3 comm_css_col">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Label ID="Label11" class="form-label commn_fnt_size" runat="server" Font-Size="12" Text="Reason: "></asp:Label>
                                                                <asp:TextBox ID="strReasonList" runat="server" CssClass="txtBox form-control cmmn_fnt_empList"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row comm_css_row">
                                                        <div class="col-12 text-right comm_css_col">
                                                            <div class="btn-group btn-group-sm" role="group" aria-label="Basic mixed styles example">
                                                                <asp:Button ID="adjAddBtnID" runat="server" class="btn btn-sm btn-info cmmn_fnt_empList" Text="Add" OnClick="adjaddBtnClick" />
                                                                <asp:Button ID="adjSubBtnID" runat="server" class="btn btn-sm btn-success cmmn_fnt_empList" Text="Submit" OnClick="adjsubBtnClick" />
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
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Font-Size="10px" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                                        BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" ShowFooter="true" FooterStyle-Font-Bold="true" FooterStyle-BackColor="#999999" FooterStyle-HorizontalAlign="Right" OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />

                                                        <Columns>

                                                            <asp:BoundField DataField="FromDate" HeaderText="From Date." SortExpression="FromDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="ToDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="fromAddress" HeaderText="From Addr." SortExpression="fromAddress" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="toAddress" HeaderText="To Address" SortExpression="toAddress" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="typeID" Visible="false" HeaderText="Type ID" SortExpression="typeID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="typeName" HeaderText="Type Name" SortExpression="typeName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="catName" HeaderText="Category" SortExpression="catName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />

                                                            <asp:BoundField DataField="strReason" HeaderText="Reason" SortExpression="strReason" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />



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
