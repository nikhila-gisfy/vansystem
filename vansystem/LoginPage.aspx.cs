using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vansystem;

namespace vansystem
{
    public partial class LoginPage : System.Web.UI.Page
    {

        clsConnnection clscon = new clsConnnection();
        clsJson objjson = new clsJson();
        NameValueCollection nvc = new NameValueCollection();
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;


            if (!IsPostBack)
            {

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Username = txtUserName.Text;
            string Password = txtPassword.Text;
            string msg = "Logged in successfully.";
            Session["msg"] = msg;
            nvc.Clear();
            nvc.Add("@Operation", "GetLogindetails");
            nvc.Add("@Username", Username);
            nvc.Add("@Password", Password);

            DataTable data = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpLogin]", nvc);
            DataSet ds = new DataSet();

            //if (data != null && data.Rows.Count > 0)
            if (ds != null && data.Rows.Count > 0)
            {
                if (Username == "admin")
                {
                    string username = data.Rows[0]["user_id"].ToString();
                    string name = data.Rows[0]["name"].ToString();
                    string designation = data.Rows[0]["designation"].ToString();
                    //int divisionid = Convert.ToInt32(data.Rows[0]["DivisionId"]);
                    string divisionid = data.Rows[0]["DivisionId"].ToString();

                    Session["user_id"] = username;
                    Session["designation"] = designation;
                    Session["name"] = name;
                    Session["DivisionId"] = divisionid;
                    Session["Password"] = Password;
                    Response.Redirect("AdminAbout.aspx");
                }
                else if (Username == "telangana")
                {
                    string username = data.Rows[0]["user_id"].ToString();
                    string name = data.Rows[0]["name"].ToString();
                    string designation = data.Rows[0]["designation"].ToString();
                    //int divisionid = Convert.ToInt32(data.Rows[0]["DivisionId"]);
                    string divisionid = data.Rows[0]["DivisionId"].ToString();
                    string user_uuid = data.Rows[0]["uuid"].ToString();

                    Session["user_id"] = username;
                    Session["designation"] = designation;
                    Session["name"] = name;
                    Session["DivisionId"] = divisionid;
                    Session["uuid"] = user_uuid;
                    Session["Password"] = Password;
                    Response.Redirect("StateDashboard.aspx");

                }
                else
                {
                    string username = data.Rows[0]["user_id"].ToString();
                    string name = data.Rows[0]["name"].ToString();
                    string designation = data.Rows[0]["designation"].ToString();
                    //int divisionid = Convert.ToInt32(data.Rows[0]["DivisionId"]);
                    string divisionid = data.Rows[0]["DivisionId"].ToString();
                    string create_by = data.Rows[0]["create_by"].ToString();

                    Session["user_id"] = username;
                    Session["designation"] = designation;
                    Session["name"] = name;
                    Session["DivisionId"] = divisionid;
                    Session["Password"] = Password;
                    Session["create_by"] = create_by;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("[GetDivisionById]"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;


                                sda.SelectCommand = cmd;
                                cmd.CommandTimeout = 120;

                                cmd.Parameters.AddWithValue("@user_id", username);



                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    Session["DivisionName"] = dt.Rows[0]["DivisionName"].ToString();

                                }

                                Response.Redirect("dashboard.aspx");
                                //Response.Redirect("CarbonEstimation.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert(' Invalid User Name / Password.','warning')", true);
            }

        }
    }
}