using System;
using SECDS.BAL;
using SECDS.Entities;
using System.Web.Security;
using System.Data;

namespace SecureDataSharing
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Request.Cookies["UserName"] != null)
                    txtUserName.Text = Request.Cookies["UserName"].Value;
                if (Request.Cookies["Password"] != null)
                    txtPassword.Attributes.Add("value", Request.Cookies["Password"].Value);
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    chkRememberMe.Checked = true;
                txtUserName.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            lblMsg.Text = string.Empty;
            UserEntity entity = new UserEntity();
            entity.UserName = userName;
            entity.Password = password;

            BusinessLogic bl = new BusinessLogic();
            DataTable dt = bl.Get_LoginDetails(entity);

            if (userName != string.Empty && password != string.Empty)
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Is_Active"].ToString()) == true)
                    {
                        Session["UserName"] = userName;
                        Session["UserId"] = dt.Rows[0]["USER_Id"].ToString();
                        Session["GroupId"] = dt.Rows[0]["Group_Id"].ToString();
                        if (chkRememberMe.Checked == true)
                        {
                            Response.Cookies["UserName"].Value = userName;
                            Response.Cookies["Password"].Value = password;
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(15);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(15);
                        }
                        else
                        {
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                        }
                        FormsAuthentication.RedirectFromLoginPage(FormsAuthentication.GetRedirectUrl(userName, false), false);

                        if(dt.Rows[0]["User_Role"].ToString()== "Admin")
                        {
                            Response.Redirect("~/Admin/AdminHome.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/User/UserHome.aspx");
                        }
                        
                    }
                    else
                    {
                        lblMsg.Text = "Unauthorized user is not allowed.";
                        txtUserName.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = "YOU HAVE ENTERED WRONG CREDENTIAL";
                    lblMsg.ForeColor = System.Drawing.Color.WhiteSmoke;
                    
                }
            }
        }
    }
}