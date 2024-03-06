using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TADA_BLL.HRTADA;
using UI.ClassFiles;

namespace UI.HR.TADA
{
    public partial class TADAAdminApprove : System.Web.UI.Page
    {
        EmployeeTADA_BLL bll = new EmployeeTADA_BLL();
        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rvOne.ProcessingMode = ProcessingMode.Remote;
                rvTwo.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = rvOne.ServerReport;
                rvOne.ShowPromptAreaButton = false;

                ServerReport serverReport2 = rvTwo.ServerReport;
                rvTwo.ShowPromptAreaButton = false;

                serverReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                serverReport.ReportServerUrl = new Uri("https://report.akijassets.com/ReportServer");
                serverReport.ReportPath = "/ERP_REPORTS/HR/PUBLIC/Tada_Status_Report";

                serverReport2.ReportServerCredentials = new ReportServerNetworkCredentials();
                serverReport2.ReportServerUrl = new Uri("https://report.akijassets.com/ReportServer");
                serverReport2.ReportPath = "/ERP_REPORTS/HR/PUBLIC/Tada_Status_Report";

                //int enroll = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());

                rvOne.ServerReport.SetParameters(new ReportParameter[]
                {
                    new ReportParameter("type", "5")
                });
                rvTwo.ServerReport.SetParameters(new ReportParameter[]
                {
                    new ReportParameter("type", "4")
                });
            }
        }

        


      

        private void showData()
        {
            DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
            DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            decimal dcVl = decimal.Parse("0");
            dt = bll.GetTADAUserReq(7, 0, dteFromDate, dteToDate, " ", " ", dcVl, " ", 0, "");
            dgvlist.DataSource = dt;
            dgvlist.DataBind();
        }

        [Obsolete]
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                showData();
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
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string autoIDCSV = "";
                int checkedBoxCount = 0;

                for (int i = 0; i < dgvlist.Rows.Count; i++)
                {
                    CheckBox cv = (CheckBox)dgvlist.Rows[i].FindControl("chkbx");
                    if (cv.Checked)
                    {
                        Label autoIDlbl = (Label)dgvlist.Rows[i].FindControl("intAutoID");
                        autoIDCSV = autoIDCSV + "," + autoIDlbl.Text.Trim();

                        checkedBoxCount++;
                    }

                }

                //Label autoID = (Label)dgvlist.Rows[clickedRow].FindControl("intAutoId");
                if (checkedBoxCount > 0)
                {
                    autoIDCSV = autoIDCSV.Substring(1);
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    decimal dcVl = decimal.Parse("0");
                    dt = bll.GetTADAUserReq(8, actionBy, DateTime.Now, DateTime.Now, " ", " ", dcVl, " ", 0, autoIDCSV);
                    showData();
                    //nullData();
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
                             "alert('Sucessfully All Data Approved');" +
                         "</script>");
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
                             "alert('Please select atleast one!');" +
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
        protected void apprvBtnClick(object sender, EventArgs e)
        {
            try
            {
                string clickHist = ((Button)sender).ClientID.ToString();
                string[] chars;
                chars = clickHist.Split('_');
                int clickedRow = int.Parse(chars[2]);
                int srid = int.Parse(((Button)sender).CommandArgument.ToString());
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());

                TextBox etmatAmnt = (TextBox)dgvlist.Rows[clickedRow].FindControl("estAmount");
                string estimateAmount = etmatAmnt.Text.Trim();
                decimal estDecAmount = decimal.Parse(estimateAmount);

                TextBox reson = (TextBox)dgvlist.Rows[clickedRow].FindControl("reason");
                string txtReason = reson.Text.Trim();

                DateTime stDT = DateTime.Now;
                DateTime enDT = DateTime.Now;

                dt = bll.GetTADAUserReq(9, actionBy, stDT, enDT, " ", " ", estDecAmount, txtReason, srid, "");
                showData();

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
                            "alert('Updated Successfully');" +
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
        protected void rejectBtnClick(object sender, EventArgs e)
        {
            try
            {
                string clickHist = ((Button)sender).ClientID.ToString();
                string[] chars;
                chars = clickHist.Split('_');
                int clickedRow = int.Parse(chars[2]);
                int srid = int.Parse(((Button)sender).CommandArgument.ToString());
                int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                TextBox reson = (TextBox)dgvlist.Rows[clickedRow].FindControl("reason");
                string txtReason = reson.Text.Trim();
                DateTime stDT = DateTime.Now;
                DateTime enDT = DateTime.Now;
                decimal dcVl = decimal.Parse("0");
                dt = bll.GetTADAUserReq(10, actionBy, stDT, enDT, " ", " ", dcVl, txtReason, srid, "");
                showData();
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
                            "alert('Data Rejected Successfully');" +
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