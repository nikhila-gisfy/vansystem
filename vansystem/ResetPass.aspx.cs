using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace vansystem
{
    public partial class ResetPass : System.Web.UI.Page
    {
        DBErrorLog db = new DBErrorLog();
        string email = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["user_id"] != null)
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["email"] != null && Request.QueryString["email"] != string.Empty)
                    {
                        email = Request.QueryString["email"];
                    }
                    Session["email"] = email;



                }
            }
            else
            {
                if (Request.QueryString["email"] != null && Request.QueryString["email"] != string.Empty)
                {
                    email = Request.QueryString["email"];
                }
                Session["email"] = email;


            }
        }

        protected void btnRPass_Click(object sender, EventArgs e)
        {
            email = Session["email"].ToString();
            string Query = "update usermaster set password = '" + txtConPass.Text + "' where CONVERT(VARCHAR,email) = '" + Session["email"] + "'";

            if (db.UpdateQuery(Query, "", "", "") > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('Password Updated successfully.','success')", true);


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('Something Went Wrong Please Try Again.','warning')", true);
            }
        }
    }
}