using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class Admingetupdateuser : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        DBErrorLog db = new DBErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                select();
            }
           
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            string uid = Session["uid"].ToString();
            con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_Admingetnewuser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "UpdateForm");
            cmd.Parameters.AddWithValue("@name", name.Value);
            cmd.Parameters.AddWithValue("@mobilenum", mob_number.Value);
            cmd.Parameters.AddWithValue("@email", email.Value);
            cmd.Parameters.AddWithValue("@designation", ddlrole.Value);
            cmd.Parameters.AddWithValue("@uid", uid);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void select()
        {
            string uid = Session["uid"].ToString();
            SqlConnection con = new SqlConnection(constr);


            string query = "SELECT [user_id],[name],[designation],[mob_number],[email] FROM [VanIT].[dbo].[usermaster] where [uid] =" + uid;



            DataSet ds = db.getResultset(query, "", "", "");
            if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
            {
                name.Value = ds.Tables[0].Rows[0]["name"].ToString();
                mob_number.Value = ds.Tables[0].Rows[0]["mob_number"].ToString();
                email.Value = ds.Tables[0].Rows[0]["email"].ToString();
                ddlrole.Value = ds.Tables[0].Rows[0]["designation"].ToString();

            }


        }
    }
}