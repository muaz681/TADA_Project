using Dairy_BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TADA_BLL.HRTADA;
using Telerik.Web.UI;
using UI.ClassFiles;
using static iTextSharp.text.pdf.PdfDocument;

namespace UI.HR.TADA
{
    public partial class TADAUserRequest : System.Web.UI.Page
    {
        EmployeeTADA_BLL bll = new EmployeeTADA_BLL();
        private DataTable dt = new DataTable();
        int intEnroll;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //strName.Text = Session[SessionParams.USER_NAME].ToString();
                //strEmail.Text = Session[SessionParams.EMAIL].ToString();
                enrollID.Text = Session[SessionParams.USER_ID].ToString();
                loadView();
                loadAppOrRejView();

                //int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                //dt = bll.GetTADAUserReq(15, actionBy, DateTime.Now, DateTime.Now, "", "", 0, "", 0, "");
                //if (dt.Rows.Count > 1)
                //{
                //    Visible = false;
                //}


            }
        }
       

        private void loadView()
        {
            DateTime stDT = DateTime.Now;
            DateTime enDT = DateTime.Now;
            decimal dcVl = decimal.Parse("0");
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            dt = bll.GetTADAUserReq(2, actionBy, stDT, enDT, " ", " ", dcVl, " ", 0, "");
            dgvlist.DataSource = dt;
            dgvlist.DataBind();
        }

        //private void userPerm()
        //{
        //    DateTime stDT = DateTime.Now;
        //    DateTime enDT = DateTime.Now;
        //    decimal dcVl = decimal.Parse("0");
        //    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
        //    dt = bll.GetTADAUserReq(15, actionBy, stDT, enDT, " ", " ", dcVl, " ", 0, "");
        //    //txtSearchEmp.Text = dt.
        //}

        private void loadAppOrRejView()
        {
            DateTime stDT = DateTime.Now;
            DateTime enDT = DateTime.Now;
            decimal dcVl = decimal.Parse("0");
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            dt = bll.GetTADAUserReq(11, actionBy, stDT, enDT, " ", " ", dcVl, " ", 0, "");
            dgvlist2.DataSource = dt;
            dgvlist2.DataBind();
        }

        

        [Obsolete]
        protected void btnSbmt_Click(object sender, EventArgs e)
        {
            try
            {
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                DateTime startDate = DateTime.Parse(txtFromDate.Text);
                DateTime endDate = DateTime.Parse(txtToDate.Text);
                string startLocation = txtStartLc.Text.Trim();
                string destination = txtJrnDest.Text.Trim();
                decimal estimatTk = decimal.ToInt32(decimal.Parse(txtAmnt.Text));
                string reason = txtReason.Text.Trim();
                
                dt = bll.GetTADAUserReq(1, actionBy, startDate, endDate, startLocation, destination, estimatTk, reason, 0, "");
                string msg = dt.Rows[0]["strMsg"].ToString();
                loadView();
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
        protected void updBtnClick(object sender, EventArgs e)
        {
            try
            {
                string clickHist = ((Button)sender).ClientID.ToString();
                string[] chars;
                chars = clickHist.Split('_');
                int clickedRow = int.Parse(chars[2]);
                int srid = int.Parse(((Button)sender).CommandArgument.ToString());
                //int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());

                TextBox etmatAmnt = (TextBox)dgvlist.Rows[clickedRow].FindControl("estAmount");
                string estimateAmount = etmatAmnt.Text.Trim();
                decimal estDecAmount = decimal.Parse(estimateAmount);

                TextBox startDt = (TextBox)dgvlist.Rows[clickedRow].FindControl("strtDate");
                string startDate = startDt.Text.Trim();
                DateTime strtDateTime = Convert.ToDateTime(startDate);

                TextBox enDT = (TextBox)dgvlist.Rows[clickedRow].FindControl("enDate");
                string endDate = enDT.Text.Trim();
                DateTime endDateTime = Convert.ToDateTime(endDate);

                TextBox stLoc = (TextBox)dgvlist.Rows[clickedRow].FindControl("strtLocation");
                string startLocation = stLoc.Text.Trim();

                TextBox destID = (TextBox)dgvlist.Rows[clickedRow].FindControl("destination");
                string destination = destID.Text.Trim();

                TextBox reson = (TextBox)dgvlist.Rows[clickedRow].FindControl("reason");
                string txtReason = reson.Text.Trim();


                dt = bll.GetTADAUserReq(3, 0, strtDateTime, endDateTime, startLocation, destination, estDecAmount, txtReason, srid, "");
                loadView();
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
                            "alert('Your Data Updated Successfully');" +
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
                            "alert('Something wrong! Please try again');" +
                        "</script>");
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] GetSearchAssignedTo(string prefixText, int count)
        {
            Int32 intUnit = Convert.ToInt32(HttpContext.Current.Session[SessionParams.UNIT_ID].ToString());
            Global_BLL objAutoSearch_BLL = new Global_BLL();
            return objAutoSearch_BLL.AutoSearchEmpList(intUnit.ToString(), prefixText);
        }
        [Obsolete]
        protected void btnSbmt_Adm_CLick(object sender, EventArgs e)
        {
            try
            {
                char[] ch1 = { '[', ']' };
                string[] temp1 = enrlID.Text.Split(ch1, StringSplitOptions.RemoveEmptyEntries);
                intEnroll = int.Parse(temp1[1].ToString());
                DateTime startDate = DateTime.Parse(fromDate.Text);
                DateTime endDat = DateTime.Parse(endDate.Text);
                string startLocation = strLoc.Text.Trim();
                string destination = endLoc.Text.Trim();
                decimal estimatTk = decimal.ToInt32(decimal.Parse(totalAmount.Text));
                string reason = strReasn.Text.Trim();

                dt = bll.GetTADAUserReq(1, intEnroll, startDate, endDat, startLocation, destination, estimatTk, reason, 0, "");
                string msg = dt.Rows[0]["strMsg"].ToString();
                //loadView();
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
    }
}