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
    public partial class TADAAccApprove : System.Web.UI.Page
    {
        EmployeeTADA_BLL bll = new EmployeeTADA_BLL();
        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        private void showData()
        {
            DateTime dteFromDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtFromDate.Text).Value;
            DateTime dteToDate = GLOBAL_BLL.DateFormat.GetDateAtSQLDateFormat(txtToDate.Text).Value;
            int actionBy = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());
            decimal dcVl = decimal.Parse("0");
            dt = bll.GetTADAUserReq(12, 0, dteFromDate, dteToDate, " ", " ", dcVl, " ", 0, "");
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
                    dt = bll.GetTADAUserReq(13, actionBy, DateTime.Now, DateTime.Now, " ", " ", dcVl, " ", 0, autoIDCSV);
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

                DateTime stDT = DateTime.Now;
                DateTime enDT = DateTime.Now;
                decimal dcVl = decimal.Parse("0");
                dt = bll.GetTADAUserReq(14, actionBy, stDT, enDT, " ", " ", dcVl, " ", srid, "");
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

        private decimal totalSum = (decimal)0.0;

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalSum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "estimatAmount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = String.Format("{0:0}", totalSum);
            }

        }

    }
}