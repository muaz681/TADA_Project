using Flogging.Core;
using GLOBAL_BLL;
using IT_BLL.DBInfo;
using Org.BouncyCastle.Utilities;
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
using Utility;

namespace UI.HR.TADA
{
    public partial class TADADetailsEntry : System.Web.UI.Page
    {
        AssetReturnBLL blls = new AssetReturnBLL();
        private DataTable dt = new DataTable();

        string filePathForXML;
        string adjFilePathForXML;
        string xmlString = "";
        string AdjxmlString = "";
        SAD_BLL.Customer.Report.StatementC bll = new SAD_BLL.Customer.Report.StatementC();



        SeriLog log = new SeriLog();
        string location = "SAD";
        string start = "starting SAD\\Order\\TA_DA_NoBike_Gb";
        string stop = "stopping SAD\\Order\\TA_DA_NoBike_Gb";

        protected void Page_Load(object sender, EventArgs e)
        {
            filePathForXML = Server.MapPath(HttpContext.Current.Session[SessionParams.USER_ID].ToString() + "_" + "remotetadanobike.xml");
            adjFilePathForXML = Server.MapPath(HttpContext.Current.Session[SessionParams.USER_ID].ToString() + "_" + "remotetadaadjnobike.xml");
            
            if (!IsPostBack)
            {
                //pnlUpperControl.DataBind();
                ////---------xml----------
                try { File.Delete(filePathForXML); }
                catch { }
                try { File.Delete(adjFilePathForXML); }
                catch { }
                ////-----**----------//
                LoadType();
                AdjLoadType();
                AdvTadaList();
            }
        }


        //=========================== Regular Tab Part ========================\\

        private void LoadType()
        {
            dt = blls.GetTADAData();
            typeID.LoadWithSelect(dt, "ExpenceseCategoryID", "ExpenceseCategory");
        }
        [Obsolete]
        protected void TADAExpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
                DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;
                dt = blls.GetTadaDesigByLimitMoney(dteFromDate, dteToDate, actionBy);
                if (int.Parse(typeID.SelectedValue) == 1)
                {
                    transID.Text = "Transport Limit: " + dt.Rows[0]["numDA"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numDA"].ToString();
                }
                else if (int.Parse(typeID.SelectedValue) == 2)
                {
                    transID.Text = "Accomodation Limit: " + dt.Rows[0]["numAccommodation"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numAccommodation"].ToString();
                }
                else if (int.Parse(typeID.SelectedValue) == 3)
                {
                    transID.Text = "Food Limit: " + dt.Rows[0]["numPocketExpenses"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numPocketExpenses"].ToString();
                }
                else if (int.Parse(typeID.SelectedValue) == 4)
                {
                    transID.Text = "Others Limit: " + dt.Rows[0]["numHQDA"].ToString();
                    hiddenTALimitAmount.Value = dt.Rows[0]["numHQDA"].ToString();
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
                            "alert('Please Select From Date and to To Date');" +
                        "</script>");
            }




        }
        private void LoadGridwithXml()
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(filePathForXML);
                System.Xml.XmlNode dSftTm = doc.SelectSingleNode("Remotetadanobike");
                xmlString = dSftTm.InnerXml;
                xmlString = "<Remotetadanobike>" + xmlString + "</Remotetadanobike>";
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



        private void CreateVoucherXml(string FromDate, string ToDate, string fromAddress, string toAddress, string typeID, string typeName, string catID, string Amount, string strReason)
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(filePathForXML))
            {
                doc.Load(filePathForXML);
                XmlNode rootNode = doc.SelectSingleNode("Remotetadanobike");
                XmlNode addItem = CreateItemNode(doc, FromDate, ToDate, fromAddress, toAddress, typeID, typeName, catID, Amount, strReason);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(xmldeclerationNode);
                XmlNode rootNode = doc.CreateElement("Remotetadanobike");
                XmlNode addItem = CreateItemNode(doc, FromDate, ToDate, fromAddress, toAddress, typeID, typeName, catID, Amount, strReason);
                rootNode.AppendChild(addItem);
                doc.AppendChild(rootNode);
            }
            doc.Save(filePathForXML);
            LoadGridwithXml();
            //Clear();
        }



        private XmlNode CreateItemNode(XmlDocument doc, string FromDate, string ToDate, string fromAddress, string toAddress, string typeID, string typeName, string catID, string Amount, string strReason)
        {
            XmlNode node = doc.CreateElement("items");

            XmlAttribute FROMDATE = doc.CreateAttribute("FromDate");
            FROMDATE.Value = FromDate;

            XmlAttribute TODATE = doc.CreateAttribute("ToDate");
            TODATE.Value = ToDate;

            XmlAttribute FRMADDR = doc.CreateAttribute("fromAddress");
            FRMADDR.Value = fromAddress;

            XmlAttribute TOADDR = doc.CreateAttribute("toAddress");
            TOADDR.Value = toAddress;


            XmlAttribute TYPE = doc.CreateAttribute("typeID");
            TYPE.Value = typeID;

            XmlAttribute TYPENAME = doc.CreateAttribute("typeName");
            TYPENAME.Value = typeName;


            XmlAttribute CATEGORY = doc.CreateAttribute("catName");
            CATEGORY.Value = catID;

            XmlAttribute AMOUNT = doc.CreateAttribute("Amount");
            AMOUNT.Value = Amount;

            XmlAttribute REASON = doc.CreateAttribute("strReason");
            REASON.Value = strReason;

            node.Attributes.Append(FROMDATE);
            node.Attributes.Append(TODATE);
            node.Attributes.Append(FRMADDR);
            node.Attributes.Append(TOADDR);

            node.Attributes.Append(TYPE);
            node.Attributes.Append(TYPENAME);
            node.Attributes.Append(CATEGORY);
            node.Attributes.Append(AMOUNT);

            node.Attributes.Append(REASON);
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
                DateTime fDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
                DateTime tDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;
                dt = blls.GetTadaRangeData(fDate, tDate, actionBy);
                string msg = dt.Rows[0]["strMsg"].ToString();
                //string msg2 = dt.Rows[0]["strMsg"].ToString();
                int dateValidityStatus = int.Parse(dt.Rows[0]["isValid"].ToString());

                //if (dt.Rows.Count == GridView1.Rows.Count)
                if (dateValidityStatus != 0)
                {

                    string strfromAddress = txtStartLc.Text;
                    string strmovementAddress = txtJrnDest.Text;
                    decimal limitAmount = decimal.Parse(hiddenTALimitAmount.Value.ToString());
                    string strAmount = Convert.ToString(txtAmnt.Text);
                    decimal amount = decimal.Parse(strAmount);
                    string category = cateName.Text;

                    if (strfromAddress == "")
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
                                "alert('Please Type From address');" +
                            "</script>");
                    }
                    else if (strmovementAddress == "")
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
                                "alert('Please Type your Destination');" +
                            "</script>");
                    }
                    else if (category == "")
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
                                "alert('Please enter your Type Category');" +
                            "</script>");
                    }

                    else
                    {

                        if (limitAmount >= amount)
                        {
                            string dteFromDate = DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd");
                            string dteToDate = DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");

                            string type = typeID.SelectedValue.Trim();
                            string typeName = typeID.SelectedItem.ToString();

                            string strReason = txtReason.Text;


                            CreateVoucherXml(dteFromDate, dteToDate, strfromAddress, strmovementAddress, type, typeName, category, strAmount, strReason);



                            decimal currentLimit = limitAmount - amount;
                            hiddenTALimitAmount.Value = currentLimit.ToString();
                            transID.Text = "Transport Limit: " + currentLimit.ToString();



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
            //fd = log.GetFlogDetail(stop, location, "Add", null);
            //Flogger.WriteDiagnostic(fd);
            //// ends
            //tracker.Stop();
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

                if (GridView1.Rows.Count > 0)
                {
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.Load(filePathForXML);
                        XmlNode dSftTm = doc.SelectSingleNode("Remotetadanobike");
                        string xmlString = dSftTm.InnerXml;
                        xmlString = "<Remotetadanobike>" + xmlString + "</Remotetadanobike>";
                        dt = blls.GetTadaInsertation(0, actionBy, xmlString);

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
                            "alert('Inserted Successfully');" +
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
                totalSum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = String.Format("{0:0}", totalSum);
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


        //==================== Adjustment Tab Part ==============================\\

        private void AdvTadaList()
        {
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            dt = blls.GetTadaAdvData(actionBy);
            TadaList.LoadWithSelect(dt, "AdvTadaID", "TadaList");
        }
        [Obsolete]
        protected void TADAList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intId = int.Parse(TadaList.SelectedValue.ToString());
            dt = blls.GetTadaAdvDetlsData(intId);
            if (dt.Rows.Count > 0)
            {
                fromDateID.Text = Convert.ToDateTime(dt.Rows[0]["JourneyStartDate"].ToString()).ToString("yyyy-MM-dd");
                ToDateID.Text = Convert.ToDateTime(dt.Rows[0]["JourneyEndDate"].ToString()).ToString("yyyy-MM-dd");
                FromAddressID.Text = dt.Rows[0]["JourneyStartLocation"].ToString();
                ToAddressID.Text = dt.Rows[0]["JourneyDestination"].ToString();
                AdjAmountID.Text = dt.Rows[0]["HRApprovedAmount"].ToString();
                AdjHiidenAmount.Value = dt.Rows[0]["HRApprovedAmount"].ToString();
            }
            
            
            //AdjAmountID.Text = Convert.ToDecimal(dt.Rows[0]["HRApprovedAmount"].ToString()).ToString();
            //adjTran.Text = "Your Previous: " + dt.Rows[0]["HRApprovedAmount"].ToString();
            //HiddenFieldAmount2.Value = dt.Rows[0]["HRApprovedAmount"].ToString();

        }

        private void AdjLoadType()
        {
            dt = blls.GetTADAData();
            typeList.LoadWithSelect(dt, "ExpenceseCategoryID", "ExpenceseCategory");
        }
        [Obsolete]
        protected void adjaddBtnClick(object sender, EventArgs e)
        {
            var fd = log.GetFlogDetail(start, location, "Add", null);
            Flogger.WriteDiagnostic(fd);

            // starting performance tracker
            var tracker = new PerfTracker("Performance on  SAD\\Order\\TA_DA_NoBike_Gb Add ", "", fd.UserName, fd.Location,
                fd.Product, fd.Layer);
            try
            {
                string dteFromDate = fromDateID.Text;
                string dteToDate = ToDateID.Text;
                string strfromAddress = FromAddressID.Text;
                string strmovementAddress = ToAddressID.Text;
                string type = typeList.SelectedValue.Trim();
                string category = categoryList.Text;
                decimal limitAmount = decimal.Parse(AdjHiidenAmount.Value.ToString());
                string strAmount = Convert.ToString(amountList.Text);
                decimal amount = decimal.Parse(strAmount);

                if (dteFromDate == "" && dteToDate == "" && strfromAddress == "" && strmovementAddress == "")
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
                            "alert('Please Select Advance TADA List');" +
                        "</script>");
                }
                else if (category == "")
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
                            "alert('Please Select Type & Category');" +
                        "</script>");
                }
                else if (strAmount == "")
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
                            "alert('Please Enter Your Amount');" +
                        "</script>");
                }
                else
                {
                    if (limitAmount >= amount)
                    {
                        string typeName = typeList.SelectedItem.ToString();

                        string strReason = strReasonList.Text;

                        AdjCreateVoucherXml(dteFromDate, dteToDate, strfromAddress, strmovementAddress, type, typeName, category, strAmount, strReason);

                        decimal currentLimit = limitAmount - amount;
                        AdjHiidenAmount.Value = currentLimit.ToString();
                        AdjAmountID.Text = currentLimit.ToString();
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
                            "alert('Your Adjustment amount limitation already crossed');" +
                        "</script>");
                    }

                        
                }


            }
            catch (Exception ex)
            {
                var efd = log.GetFlogDetail(stop, location, "Add", ex);
                Flogger.WriteError(efd);
            }
            fd = log.GetFlogDetail(stop, location, "Add", null);
            Flogger.WriteDiagnostic(fd);
            // ends
            tracker.Stop();
        }

        [Obsolete]
        protected void adjsubBtnClick(object sender, EventArgs e)
        {
            var fd = log.GetFlogDetail(start, location, "Save", null);
            Flogger.WriteDiagnostic(fd);

            // starting performance tracker
            var tracker = new PerfTracker("Performance on  SAD\\Order\\TA_DA_NoBike_Gb Save ", "", fd.UserName, fd.Location,
                fd.Product, fd.Layer);
            try
            {

                if (GridView2.Rows.Count > 0)
                {
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.Load(adjFilePathForXML);
                        XmlNode dSftTm = doc.SelectSingleNode("Remotetadanobike");
                        string xmlString = dSftTm.InnerXml;
                        xmlString = "<Remotetadanobike>" + xmlString + "</Remotetadanobike>";
                        int intId = int.Parse(TadaList.SelectedValue.ToString());
                        dt = blls.GetTadaInsertation(intId, actionBy, xmlString);

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
                            "alert('Inserted Successfully');" +
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
                    File.Delete(adjFilePathForXML);
                    GridView2.DataSource = "";
                    GridView2.DataBind();
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
        private void AdjLoadGridwithXml()
        {

            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(adjFilePathForXML);
                System.Xml.XmlNode dSftTm = doc.SelectSingleNode("Remotetadanobike");
                AdjxmlString = dSftTm.InnerXml;
                AdjxmlString = "<Remotetadanobike>" + AdjxmlString + "</Remotetadanobike>";
                StringReader sr = new StringReader(AdjxmlString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                if (ds.Tables[0].Rows.Count > 0)
                { GridView2.DataSource = ds; }
                else { GridView2.DataSource = ""; }

                GridView2.DataBind();

            }
            catch { }
        }
        private void AdjCreateVoucherXml(string AdjFromDate, string AdjToDate, string AdjfromAddress, string AdjtoAddress, string AdjtypeID, string AdjtypeName, string AdjcatID, string AdjAmount, string AdjstrReason)
        {

            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(adjFilePathForXML))
            {
                doc.Load(adjFilePathForXML);
                XmlNode rootNode = doc.SelectSingleNode("Remotetadanobike");
                XmlNode addItem = AdjCreateItemNode(doc, AdjFromDate, AdjToDate, AdjfromAddress, AdjtoAddress, AdjtypeID, AdjtypeName, AdjcatID, AdjAmount, AdjstrReason);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(xmldeclerationNode);
                XmlNode rootNode = doc.CreateElement("Remotetadanobike");
                XmlNode addItem = AdjCreateItemNode(doc, AdjFromDate, AdjToDate, AdjfromAddress, AdjtoAddress, AdjtypeID, AdjtypeName, AdjcatID, AdjAmount, AdjstrReason);
                rootNode.AppendChild(addItem);
                doc.AppendChild(rootNode);
            }
            doc.Save(adjFilePathForXML);
            AdjLoadGridwithXml();
            //Clear();
        }

        private XmlNode AdjCreateItemNode(XmlDocument doc, string AdjFromDate, string AdjToDate, string AdjfromAddress, string AdjtoAddress, string AdjtypeID, string AdjtypeName, string AdjcatID, string AdjAmount, string AdjstrReason)
        {
            XmlNode node = doc.CreateElement("items");

            XmlAttribute FROMDATE = doc.CreateAttribute("FromDate");
            FROMDATE.Value = AdjFromDate;

            XmlAttribute TODATE = doc.CreateAttribute("ToDate");
            TODATE.Value = AdjToDate;

            XmlAttribute FRMADDR = doc.CreateAttribute("fromAddress");
            FRMADDR.Value = AdjfromAddress;

            XmlAttribute TOADDR = doc.CreateAttribute("toAddress");
            TOADDR.Value = AdjtoAddress;


            XmlAttribute TYPE = doc.CreateAttribute("typeID");
            TYPE.Value = AdjtypeID;

            XmlAttribute TYPENAME = doc.CreateAttribute("typeName");
            TYPENAME.Value = AdjtypeName;


            XmlAttribute CATEGORY = doc.CreateAttribute("catName");
            CATEGORY.Value = AdjcatID;

            XmlAttribute AMOUNT = doc.CreateAttribute("Amount");
            AMOUNT.Value = AdjAmount;

            XmlAttribute REASON = doc.CreateAttribute("strReason");
            REASON.Value = AdjstrReason;

            node.Attributes.Append(FROMDATE);
            node.Attributes.Append(TODATE);
            node.Attributes.Append(FRMADDR);
            node.Attributes.Append(TOADDR);

            node.Attributes.Append(TYPE);
            node.Attributes.Append(TYPENAME);
            node.Attributes.Append(CATEGORY);
            node.Attributes.Append(AMOUNT);

            node.Attributes.Append(REASON);
            return node;
        }

        private decimal totalSumAm = (decimal)0.0;
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalSumAm += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = String.Format("{0:0}", totalSumAm);
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //AdjxmlString adjFilePathForXML
            try
            {
                AdjLoadGridwithXml();
                DataSet dsGrid = (DataSet)GridView2.DataSource;
                dsGrid.Tables[0].Rows[GridView2.Rows[e.RowIndex].DataItemIndex].Delete();
                dsGrid.WriteXml(adjFilePathForXML);
                DataSet dsGridAfterDelete = (DataSet)GridView2.DataSource;
                if (dsGridAfterDelete.Tables[0].Rows.Count <= 0)
                { File.Delete(adjFilePathForXML); GridView2.DataSource = ""; GridView2.DataBind(); }
                else { AdjLoadGridwithXml(); }
            }
            catch { }
        }
    }
}