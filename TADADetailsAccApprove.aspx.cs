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
    public partial class TADADetailsAccApprove : System.Web.UI.Page
    {
        AssetReturnBLL bll = new AssetReturnBLL();
        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                unitData();
            }
        }
        private void unitData()
        {
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            dt = bll.GetUnitData(actionBy);
            ddlUnit.LoadWithSelect(dt, "intUnitID", "strUnit");
        }
        private void showData()
        {
            int unitID = int.Parse(ddlUnit.SelectedValue.ToString());
            DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
            DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;

            dt = bll.GetTadaPendingApprv(5, dteFromDate, dteToDate, unitID);
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
                    dt = bll.GetTADAAproved(4, actionBy, autoIDCSV);
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
        private decimal totalSum = (decimal)0.0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalSum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AuditApprovedAmount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = String.Format("{0:0}", totalSum);
            }

        }
    }
}