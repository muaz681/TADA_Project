using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.ClassFiles;

namespace UI.HR.TADA
{
    public partial class TADAUserReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rvOne.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = rvOne.ServerReport;
                rvOne.ShowPromptAreaButton = false;

                serverReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                serverReport.ReportServerUrl = new Uri("https://report.akijassets.com/ReportServer");
                serverReport.ReportPath = "/ERP_REPORTS/HR/PUBLIC/TADA/TaDa_User_Reports";

                int enroll = int.Parse(HttpContext.Current.Session[SessionParams.USER_ID].ToString());

                rvOne.ServerReport.SetParameters(new ReportParameter[]
                {
                    //new ReportParameter("token", enroll.ToString())
                });
            }
        }
    }
}