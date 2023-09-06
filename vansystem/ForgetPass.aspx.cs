using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class ForgetPass : System.Web.UI.Page
    {
        DBErrorLog db = new DBErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {

            }
        }

        protected void btnuSbmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string CheckEmail = "select user_id,name,email,mob_number,password from usermaster where CONVERT(VARCHAR,email)='" + email + "'";
            DataSet ds = db.getResultset(CheckEmail, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string body = "<a href=http://3.7.34.230/VanIt/ResetPass.aspx?email=" + txtEmail.Text + ">Click here to change your password</a>";
                string subject = "Reset Your Password";
                SendEmail se = new SendEmail();
                string respose = se.sendEmailMsg(subject, "", body, email);

                if (respose == "Success")
                {
                    txtEmail.Text = "";
                    ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('Dear " + ds.Tables[0].Rows[0]["name"].ToString() + ", we have sent a password to your registor Email, Please check your Email.','warning');", true);
                }
                else if (respose == "Error Occured")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('Something went wrong. Please try again later.','error');", true);
                }

            }
            else
            {
                txtEmail.Text = "";
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('The entered Email address is not registor with VanIT.','warning');", true);
            }
        }
    }
}