using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;

namespace vansystem
{
    public partial class getUpdateUser : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        DBErrorLog db = new DBErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binddata();
            }
        }
        private void Binddata()
        {
            string divisionid = Session["DivisionId"].ToString();
            string uid = Session["uid"].ToString();
            SqlConnection con = new SqlConnection(constr);


            string query = "select name, mob_number, email, user_id, password, Range from usermaster where uid =" + uid;

             

            DataSet ds = db.getResultset(query, "", "", "");
            if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
            {
                name.Value = ds.Tables[0].Rows[0]["name"].ToString();
                mob_number.Value = ds.Tables[0].Rows[0]["mob_number"].ToString();
                email.Value = ds.Tables[0].Rows[0]["email"].ToString();
                user_id.Value= ds.Tables[0].Rows[0]["user_id"].ToString();
                password.Value = ds.Tables[0].Rows[0]["password"].ToString();
                range.Value = ds.Tables[0].Rows[0]["Range"].ToString();

            }
        }
        
        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                using (SqlCommand cmd = new SqlCommand("sp_getNewUser"))
                {
                    string uid = Session["uid"].ToString();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@StatementType", "Update");
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@name", name.Value);
                    cmd.Parameters.AddWithValue("@mobilenum", mob_number.Value);
                    cmd.Parameters.AddWithValue("@email", email.Value);
                    
                   
                    cmd.Parameters.AddWithValue("@Range", range.Value);
                    cmd.Parameters.AddWithValue("@password", password.Value);

                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}