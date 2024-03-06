using Flogging.Core;
using GLOBAL_BLL;
using IT_BLL.DBInfo;
using SAD_BLL.Customer.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UI.ClassFiles;
using UI.SAD.Order;
using Utility;

namespace UI.HR.TADA
{
    public partial class TADAVehicleUser : System.Web.UI.Page
    {
        private DataTable dt = new DataTable();
        StatementC fuelBll = new StatementC();
        AssetReturnBLL bll = new AssetReturnBLL();

        string filePathForXML;
        string xmlString = "";
        string SecxmlString = "";
        string secFilePathForXML;

        SeriLog log = new SeriLog();
        string location = "SAD";
        string start = "starting SAD\\Order\\TA_DA_NoBike_Gb";
        string stop = "stopping SAD\\Order\\TA_DA_NoBike_Gb";
        protected void Page_Load(object sender, EventArgs e)
        {
            filePathForXML = Server.MapPath(HttpContext.Current.Session[SessionParams.USER_ID].ToString() + "_" + "remotetadavehicle.xml");
            secFilePathForXML = Server.MapPath(HttpContext.Current.Session[SessionParams.USER_ID].ToString() + "_" + "remotetadavehicleothers.xml");

            if (!IsPostBack)
            {
                int unitid = int.Parse(HttpContext.Current.Session[SessionParams.UNIT_ID].ToString());
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                try { File.Delete(filePathForXML); }
                catch { }
                try { File.Delete(secFilePathForXML); }
                catch { }
                loadStationName();
                loadService();
                loadFuelType();
                stationNameID.Visible = false;
                stationNam.Visible = false;
                //strFuelStationName.Visible = false;
            }
        }
        private void loadStationName()
        {
            int unitid = int.Parse(HttpContext.Current.Session[SessionParams.UNIT_ID].ToString());
            dt = fuelBll.getFuelStationList(unitid);
            stationNameID.LoadWithSelect(dt, "intFuelStationID", "strFuelStationName");
        }
        private void loadFuelType()
        {
            dt = fuelBll.getFuelType();
            fuelTypeID.LoadWithSelect(dt, "FuelTypeID", "FuelType");
        }
        private void loadService()
        {
            dt = bll.GetTADACarData();
            serviceTypeID.LoadWithSelect(dt, "ExpenceseCategoryID", "ExpenceseCategory");
        }
        protected void paymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paymentID.SelectedIndex == 1)
            {
                stationNameID.SelectedIndex = 1;
                stationNameID.Visible = true;
                stationNam.Visible = true;
            }
            else if (paymentID.SelectedIndex == 2)
            {
                stationNameID.Visible = true;
                stationNameID.SelectedIndex = 0;
                stationNam.Visible = true;
            }
        }

        //=========================== FUEL Station Part ========================\\

        private void LoadGridwithXml()
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(filePathForXML);
                System.Xml.XmlNode dSftTm = doc.SelectSingleNode("Remotetadavehicle");
                xmlString = dSftTm.InnerXml;
                xmlString = "<Remotetadavehicle>" + xmlString + "</Remotetadavehicle>";
                StringReader sr = new StringReader(xmlString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                if (ds.Tables[0].Rows.Count > 0)
                { GridView1.DataSource = ds; }
                else { GridView1.DataSource = ""; }

                GridView1.DataBind();

            }
            catch { }
        }
        private void CreateVoucherXml(string fuelID, string strFuel, string paymentID, string strPayment, string stationID, string strStation, string fuelPrc)
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(filePathForXML))
            {
                doc.Load(filePathForXML);
                XmlNode rootNode = doc.SelectSingleNode("Remotetadavehicle");
                XmlNode addItem = CreateItemNode(doc, fuelID, strFuel, paymentID, strPayment, stationID, strStation, fuelPrc);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(xmldeclerationNode);
                XmlNode rootNode = doc.CreateElement("Remotetadavehicle");
                XmlNode addItem = CreateItemNode(doc, fuelID, strFuel, paymentID, strPayment, stationID, strStation, fuelPrc);
                rootNode.AppendChild(addItem);
                doc.AppendChild(rootNode);
            }
            doc.Save(filePathForXML);
            LoadGridwithXml();
            //Clear();
        }
        private XmlNode CreateItemNode(XmlDocument doc, string fuelID, string strFuel, string paymentID, string strPayment, string stationID, string strStation, string fuelPrc)
        {
            XmlNode node = doc.CreateElement("items");


            XmlAttribute FUELID = doc.CreateAttribute("fuelID");
            FUELID.Value = fuelID;

            XmlAttribute FUELNAME = doc.CreateAttribute("strFuel");
            FUELNAME.Value = strFuel;


            XmlAttribute PAYMENTID = doc.CreateAttribute("paymentID");
            PAYMENTID.Value = paymentID;

            XmlAttribute PAYMENTNAME = doc.CreateAttribute("strPayment");
            PAYMENTNAME.Value = strPayment;

            XmlAttribute STATIONID = doc.CreateAttribute("stationID");
            STATIONID.Value = stationID;

            XmlAttribute STATIONNAME = doc.CreateAttribute("strStation");
            STATIONNAME.Value = strStation;

            XmlAttribute FUELPRICE = doc.CreateAttribute("fuelPrc");
            FUELPRICE.Value = fuelPrc;


            node.Attributes.Append(FUELID);
            node.Attributes.Append(FUELNAME);
            node.Attributes.Append(PAYMENTID);
            node.Attributes.Append(PAYMENTNAME);
            node.Attributes.Append(STATIONID);
            node.Attributes.Append(STATIONNAME);
            node.Attributes.Append(FUELPRICE);


            return node;
        }


        [Obsolete]
        protected void addBtnClick(object sender, EventArgs e)
        {
            var fd = log.GetFlogDetail(start, location, "Add", null);
            Flogger.WriteDiagnostic(fd);

            // starting performance tracker
            var tracker = new PerfTracker("Performance on  SAD\\Order\\TA_DA_NoBike_Gb Add ", "", fd.UserName, fd.Location,
                fd.Product, fd.Layer);

            try
            {
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                DateTime fDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                DateTime tDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                dt = bll.GetTadaRangeData(fDate, tDate, actionBy);
                string msg = dt.Rows[0]["strMsg"].ToString();
                //string msg2 = dt.Rows[0]["strMsg"].ToString();
                int dateValidityStatus = int.Parse(dt.Rows[0]["isValid"].ToString());
                if (dateValidityStatus != 0)
                {
                    //string date = DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd");
                    //string strtTim = strtTime.Text;
                    //string endTim = endTime.Text;
                    string strMovArea = mvmntArea.Text;
                    string milStrtTo = milStart.Text;
                    string milToEnd = milEnd.Text;

                    string fuel = fuelTypeID.SelectedValue.Trim();
                    string fuelName = fuelTypeID.SelectedItem.ToString();

                    string payment = paymentID.SelectedValue.Trim();
                    string paymentName = paymentID.SelectedItem.ToString();

                    string station = stationNameID.SelectedValue.Trim();
                    string stationName = stationNameID.SelectedItem.ToString();

                    string strFuel = Convert.ToString(fuelPriceID.Text);

                    if (strMovArea == "")
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please Type your Movement Area');" +
                            "</script>");
                    }
                    else if (milStrtTo == "" && milToEnd == "")
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please enter your vehicle mileage start to end');" +
                            "</script>");
                    }
                    else if (fuelTypeID.SelectedIndex == 0)
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please select fuel, payment, and station type or enter your vehicle Fuel price');" +
                            "</script>");
                    }
                    else if (paymentID.SelectedIndex == 0)
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please select payment type');" +
                            "</script>");
                    }
                    else if (stationNameID.SelectedIndex == 0)
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please select fuel station type');" +
                            "</script>");
                    }
                    else if (strFuel == "")
                    {
                        Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please enter your vehicle fuel amount');" +
                            "</script>");
                    }
                    else
                    {
                        CreateVoucherXml(fuel, fuelName, payment, paymentName, station, stationName, strFuel);

                    }


                }
                else
                {
                    Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('" + msg + "');" +
                        "</script>");
                }




            }
            catch
            {
                Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Something wrong! Please try again');" +
                        "</script>");
            }
        }
        [Obsolete]
        protected void subBtnClick(object sender, EventArgs e)
        {
            var fd = log.GetFlogDetail(start, location, "Save", null);
            Flogger.WriteDiagnostic(fd);

            // starting performance tracker
            var tracker = new PerfTracker("Performance on  SAD\\Order\\TA_DA_NoBike_Gb Save ", "", fd.UserName, fd.Location,
                fd.Product, fd.Layer);
            try
            {
                if (GridView1.Rows.Count > 0 && GridView2.Rows.Count > 0)
                {
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    XmlDocument doc = new XmlDocument();
                    XmlDocument docTwo = new XmlDocument();
                    try
                    {
                        DateTime date = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                        //TimeSpan strtTime = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(strtTime.Days).Value.ToTimeSpan();
                        string starttime = strtTime.Text.Trim();
                        string endtime = endTime.Text.Trim();
                        string movArea = mvmntArea.Text.Trim();
                        decimal mileageStart = Convert.ToDecimal(milStart.Text);
                        decimal mileageEnd = Convert.ToDecimal(milEnd.Text);

                        doc.Load(filePathForXML);
                        XmlNode dSftTm = doc.SelectSingleNode("Remotetadavehicle");
                        string xmlString = dSftTm.InnerXml;
                        xmlString = "<Remotetadavehicle>" + xmlString + "</Remotetadavehicle>";

                        docTwo.Load(secFilePathForXML);
                        XmlNode dfsTwoTm = docTwo.SelectSingleNode("Remotetadavehicleothers");
                        string secXmlString = dfsTwoTm.InnerXml;
                        secXmlString = "<Remotetadavehicleothers>" + secXmlString + "</Remotetadavehicleothers>";

                        dt = bll.GetTadaCarInsertation(actionBy, date, TimeSpan.Parse(starttime), TimeSpan.Parse(endtime), movArea, mileageStart, mileageEnd, xmlString, secXmlString);


                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Car TADA & TADA Details Inserted Successfully');" +
                        "</script>");
                    }
                    catch
                    {
                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Sorry-- wrong format data. plz check');" +
                        "</script>");
                    }
                    GridView1.DataBind();
                    File.Delete(filePathForXML);
                    GridView1.DataSource = "";
                    GridView1.DataBind();

                    GridView2.DataBind();
                    File.Delete(secFilePathForXML);
                    GridView2.DataSource = "";
                    GridView2.DataBind();

                    nullValue();
                }
                else if (GridView1.Rows.Count > 0)
                {
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        DateTime date = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                        //TimeSpan strtTime = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(strtTime.Days).Value.ToTimeSpan();
                        string starttime = strtTime.Text.Trim();
                        string endtime = endTime.Text.Trim();
                        string movArea = mvmntArea.Text.Trim();
                        decimal mileageStart = Convert.ToDecimal(milStart.Text);
                        decimal mileageEnd = Convert.ToDecimal(milEnd.Text);

                        doc.Load(filePathForXML);
                        XmlNode dSftTm = doc.SelectSingleNode("Remotetadavehicle");
                        string xmlString = dSftTm.InnerXml;
                        xmlString = "<Remotetadavehicle>" + xmlString + "</Remotetadavehicle>";

                        //doc.Load(secFilePathForXML);
                        //XmlNode dfsTwoTm = doc.SelectSingleNode("Remotetadavehicleothers");
                        //string secXmlString = dfsTwoTm.InnerXml;
                        //secXmlString = "<Remotetadavehicleothers>" + secXmlString + "</Remotetadavehicleothers>";


                        dt = bll.GetTadaCarInsertation(actionBy, date, TimeSpan.Parse(starttime), TimeSpan.Parse(endtime), movArea, mileageStart, mileageEnd, xmlString, "");


                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Car TADA Inserted Successfully');" +
                        "</script>");
                    }
                    catch
                    {
                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Sorry-- wrong format data. plz check');" +
                        "</script>");
                    }
                    GridView1.DataBind();
                    File.Delete(filePathForXML);
                    GridView1.DataSource = "";
                    GridView1.DataBind();
                    nullValue();
                }
                else if (GridView2.Rows.Count > 0)
                {
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        DateTime date = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                        //TimeSpan strtTime = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(strtTime.Days).Value.ToTimeSpan();
                        string starttime = strtTime.Text.Trim();
                        string endtime = endTime.Text.Trim();
                        string movArea = mvmntArea.Text.Trim();
                        decimal mileageStart = Convert.ToDecimal(milStart.Text);
                        decimal mileageEnd = Convert.ToDecimal(milEnd.Text);

                        doc.Load(secFilePathForXML);
                        XmlNode dfsTwoTm = doc.SelectSingleNode("Remotetadavehicleothers");
                        string secXmlString = dfsTwoTm.InnerXml;
                        secXmlString = "<Remotetadavehicleothers>" + secXmlString + "</Remotetadavehicleothers>";
                        dt = bll.GetTadaCarInsertation(actionBy, date, TimeSpan.Parse(starttime), TimeSpan.Parse(endtime), movArea, mileageStart, mileageEnd, "", secXmlString);

                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('TADA Details Inserted Successfully');" +
                        "</script>");
                    }
                    catch
                    {
                        Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Sorry-- wrong format data. plz check');" +
                        "</script>");
                    }
                    GridView2.DataBind();
                    File.Delete(secFilePathForXML);
                    GridView2.DataSource = "";
                    GridView2.DataBind();
                    nullValue();
                }
            }
            catch (Exception ex)
            {
                var efd = log.GetFlogDetail(stop, location, "Save", ex);
                Flogger.WriteError(efd);
            }
            fd = log.GetFlogDetail(stop, location, "Save", null);
            Flogger.WriteDiagnostic(fd);
            // ends
            tracker.Stop();
        }

        private decimal totalSum = (decimal)0.0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalSum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "fuelPrc"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = String.Format("{0:0}", totalSum);
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LoadGridwithXml();
                DataSet dsGrid = (DataSet)GridView1.DataSource;
                dsGrid.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex].Delete();
                dsGrid.WriteXml(filePathForXML);
                DataSet dsGridAfterDelete = (DataSet)GridView1.DataSource;
                if (dsGridAfterDelete.Tables[0].Rows.Count <= 0)
                { File.Delete(filePathForXML); GridView1.DataSource = ""; GridView1.DataBind(); }
                else { LoadGridwithXml(); }
            }
            catch { }
        }

        private void nullValue()
        {
            txtDate.Text = null;
            mvmntArea.Text = null;
            milStart.Text = null;
            milEnd.Text = null;
            fuelTypeID.SelectedIndex = 0;
            paymentID.SelectedIndex = 0;
            stationNameID.SelectedIndex = 0;
            fuelPriceID.Text = null;
            serviceTypeID.SelectedIndex = 0;
            txtDtlsReason.Text = null;
            txtAmnt.Text = null;
        }
        //=========================== Others Expense Part ========================\\
        //SecxmlString secFilePathForXML;

        [Obsolete]
        protected void TADAExpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                dt = bll.GetTadaDesigByLimitMoney(dteFromDate, dteToDate, actionBy);
                if (int.Parse(serviceTypeID.SelectedValue) == 1)
                {
                    transID.Text = "Transport Limit: " + dt.Rows[0]["numDA"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numDA"].ToString();
                    transID.Visible = true;
                }
                else if (int.Parse(serviceTypeID.SelectedValue) == 2)
                {
                    transID.Text = "Accomodation Limit: " + dt.Rows[0]["numAccommodation"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numAccommodation"].ToString();
                    transID.Visible = true;
                }
                else if (int.Parse(serviceTypeID.SelectedValue) == 3)
                {
                    transID.Text = "Food Limit: " + dt.Rows[0]["numPocketExpenses"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numPocketExpenses"].ToString();
                    transID.Visible = true;
                }
                else if (int.Parse(serviceTypeID.SelectedValue) == 4)
                {
                    transID.Text = "Others Limit: " + dt.Rows[0]["numHQDA"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numHQDA"].ToString();
                    transID.Visible = true;
                }
                //else if (int.Parse(serviceTypeID.SelectedValue) == 5)
                //{
                //    transID.Text = string.Empty;
                //    transID.Visible = false;
                //}
                //else if (int.Parse(serviceTypeID.SelectedValue) == 6)
                //{
                //    transID.Text = string.Empty;
                //    transID.Visible = false;
                //}
                //else if (int.Parse(serviceTypeID.SelectedValue) == 7)
                //{
                //    transID.Text = string.Empty;
                //    transID.Visible = false;
                //}
                //else if (int.Parse(serviceTypeID.SelectedValue) == 8)
                //{
                //    transID.Text = string.Empty;
                //    transID.Visible = false;
                //}
                else
                {
                    transID.Text = string.Empty;
                    transID.Visible = false;
                }
            }
            catch
            {
                Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Please Select Date');" +
                        "</script>");
            }




        }



        private void SecLoadGridwithXml()
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(secFilePathForXML);
                System.Xml.XmlNode dSftTm = doc.SelectSingleNode("Remotetadavehicleothers");
                SecxmlString = dSftTm.InnerXml;
                SecxmlString = "<Remotetadavehicleothers>" + SecxmlString + "</Remotetadavehicleothers>";
                StringReader sr = new StringReader(SecxmlString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                if (ds.Tables[0].Rows.Count > 0)
                { GridView2.DataSource = ds; }
                else { GridView2.DataSource = ""; }

                GridView2.DataBind();

            }
            catch { }
        }
        //string DateAddress, string strtTime, string endTime, string strMovArea,
        private void SecCreateVoucherXml(string serviceID, string strService, string strReason, string Amount)
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(secFilePathForXML))
            {
                doc.Load(secFilePathForXML);
                XmlNode rootNode = doc.SelectSingleNode("Remotetadavehicleothers");
                XmlNode addItem = SecCreateItemNode(doc, serviceID, strService, strReason, Amount);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(xmldeclerationNode);
                XmlNode rootNode = doc.CreateElement("Remotetadavehicleothers");
                XmlNode addItem = SecCreateItemNode(doc, serviceID, strService, strReason, Amount);
                rootNode.AppendChild(addItem);
                doc.AppendChild(rootNode);
            }
            doc.Save(secFilePathForXML);
            SecLoadGridwithXml();
            //Clear();
        }

        private XmlNode SecCreateItemNode(XmlDocument doc, string serviceID, string strService, string strReason, string Amount)
        {
            XmlNode node = doc.CreateElement("items");

            //XmlAttribute DATE = doc.CreateAttribute("DateAddress");
            //DATE.Value = DateAddress;

            //XmlAttribute STARTTIME = doc.CreateAttribute("strtTime");
            //STARTTIME.Value = strtTime;

            //XmlAttribute ENDTIME = doc.CreateAttribute("endTime");
            //ENDTIME.Value = endTime;

            //XmlAttribute MOVEMENTAREA = doc.CreateAttribute("strMovArea");
            //MOVEMENTAREA.Value = strMovArea;


            XmlAttribute SERVICEID = doc.CreateAttribute("serviceID");
            SERVICEID.Value = serviceID;

            XmlAttribute SERVICENAME = doc.CreateAttribute("strService");
            SERVICENAME.Value = strService;

            XmlAttribute REASON = doc.CreateAttribute("strReason");
            REASON.Value = strReason;

            XmlAttribute AMOUNT = doc.CreateAttribute("Amount");
            AMOUNT.Value = Amount;

            //node.Attributes.Append(DATE);
            //node.Attributes.Append(STARTTIME);
            //node.Attributes.Append(ENDTIME);
            //node.Attributes.Append(MOVEMENTAREA);
            node.Attributes.Append(SERVICEID);
            node.Attributes.Append(SERVICENAME);
            node.Attributes.Append(REASON);
            node.Attributes.Append(AMOUNT);


            return node;
        }


        [Obsolete]
        protected void addBtnClickTwo(object sender, EventArgs e)
        {
            var fd = log.GetFlogDetail(start, location, "Add", null);
            Flogger.WriteDiagnostic(fd);

            // starting performance tracker
            var tracker = new PerfTracker("Performance on  SAD\\Order\\TA_DA_NoBike_Gb Add ", "", fd.UserName, fd.Location,
                fd.Product, fd.Layer);

            try
            {
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                DateTime fDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                DateTime tDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtDate.Text).Value;
                dt = bll.GetTadaRangeData(fDate, tDate, actionBy);
                string msg = dt.Rows[0]["strMsg"].ToString();
                //string msg2 = dt.Rows[0]["strMsg"].ToString();
                int dateValidityStatus = int.Parse(dt.Rows[0]["isValid"].ToString());

                //string date = DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd");
                //string strtTim = strtTime.Text;
                //string endTim = endTime.Text;
                if (dateValidityStatus != 0)
                {
                    string strMovArea = mvmntArea.Text;
                    if (serviceTypeID.SelectedIndex == 1 || serviceTypeID.SelectedIndex == 2 || serviceTypeID.SelectedIndex == 3 || serviceTypeID.SelectedIndex == 4)
                    {
                        decimal limitAmount = decimal.Parse(hiddenTALimitAmount.Value.ToString());
                        string strAmnt = Convert.ToString(txtAmnt.Text);
                        decimal amount = decimal.Parse(strAmnt);

                        if (limitAmount >= amount)
                        {
                            string service = serviceTypeID.SelectedValue.Trim();
                            string serviceName = serviceTypeID.SelectedItem.ToString();
                            string strReason = txtDtlsReason.Text;

                            if (serviceTypeID.SelectedIndex == 0)
                            {
                                Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Please select service type');" +
                            "</script>");
                            }
                            else
                            {
                                SecCreateVoucherXml(service, serviceName, strReason, strAmnt);

                                decimal currentLimit = limitAmount - amount;
                                hiddenTALimitAmount.Value = currentLimit.ToString();
                                transID.Text = "Transport Limit: " + currentLimit.ToString();
                            }


                        }
                        else
                        {
                            Page.RegisterStartupScript("captcha",
                            "<script language='javascript'>" +
                                "function disableSubmitButton() {" +
                                    "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                                "}" +
                                "if(window.addEventListener) {" +
                                "    window.addEventListener('load',disableSubmitButton,false);" +
                                "} else {" +
                                "    window.attachEvent('onload',disableSubmitButton);" +
                                "}" +
                                "alert('Your Amount Limitation is crossed!');" +
                            "</script>");
                        }
                    }
                    else
                    {
                        string service = serviceTypeID.SelectedValue.Trim();
                        string serviceName = serviceTypeID.SelectedItem.ToString();
                        string strReason = txtDtlsReason.Text;
                        string strFuel = Convert.ToString(txtAmnt.Text);
                        SecCreateVoucherXml(service, serviceName, strReason, strFuel);
                    }

                }
                else
                {
                    Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('" + msg + "');" +
                        "</script>");
                }

            }
            catch
            {
                Page.RegisterStartupScript("captcha",
                        "<script language='javascript'>" +
                            "function disableSubmitButton() {" +
                                "document.getElementById('***submitButtonID***').onclick = function(){return false;}" +
                            "}" +
                            "if(window.addEventListener) {" +
                            "    window.addEventListener('load',disableSubmitButton,false);" +
                            "} else {" +
                            "    window.attachEvent('onload',disableSubmitButton);" +
                            "}" +
                            "alert('Something wrong! Please try again');" +
                        "</script>");
            }
        }



        private decimal totalSumTwo = (decimal)0.0;
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalSumTwo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = String.Format("{0:0}", totalSumTwo);
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SecLoadGridwithXml();
                DataSet dsGrid = (DataSet)GridView2.DataSource;
                dsGrid.Tables[0].Rows[GridView2.Rows[e.RowIndex].DataItemIndex].Delete();
                dsGrid.WriteXml(secFilePathForXML);
                DataSet dsGridAfterDelete = (DataSet)GridView2.DataSource;
                if (dsGridAfterDelete.Tables[0].Rows.Count <= 0)
                { File.Delete(secFilePathForXML); GridView2.DataSource = ""; GridView2.DataBind(); }
                else { SecLoadGridwithXml(); }
            }
            catch { }
        }


    }
}