using IT_BLL.DBInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.ClassFiles;
using Utility;

namespace UI.HR.TADA
{
    public partial class TADADetailsSupApprv : System.Web.UI.Page
    {
        AssetReturnBLL bll = new AssetReturnBLL();
        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void showData()
        {
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
            DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;

            dt = bll.GetTadaPendingApprv(1, dteFromDate, dteToDate, actionBy);
            dgvlist.DataSource = dt;
            dgvlist.DataBind();
        }

        [Obsolete]
        protected void showBtnClick(object sender, EventArgs e)
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
        protected void rejectBtnClick(object sender, EventArgs e)
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
                if (checkedBoxCount > 0)
                {
                    autoIDCSV = autoIDCSV.Substring(1);
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    dt = bll.GetTADAAproved(0, actionBy, autoIDCSV);
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
        protected void subBtnClick(object sender, EventArgs e)
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
                if (checkedBoxCount > 0)
                {
                    autoIDCSV = autoIDCSV.Substring(1);
                    int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
                    dt = bll.GetTADAAproved(1, actionBy, autoIDCSV);
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
        protected void editBtnClick(object sender, EventArgs e)
        {
            try
            {
                char[] delimiterChars = { '^' };
                string temp = ((Button)sender).CommandArgument.ToString();
                string[] datas = temp.Split(delimiterChars);
                //String param = "Viewdetails('"+datas[0].ToString()+"');";

                String param = "Viewdetails('" + datas[0].ToString() + "')";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "StartupScript", param, true);

                int data = 0;

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