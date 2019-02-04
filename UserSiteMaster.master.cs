using System;
using System.Web.Security;

namespace SecureDataSharing
{
    public partial class UserSiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "Current Time : " + DateTime.Now.ToString("hh:mm:ss tt");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}