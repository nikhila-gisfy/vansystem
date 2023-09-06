using System;
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
    public partial class getNewrole : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_updateroles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "insert");
            cmd.Parameters.AddWithValue("@rolename", name.Value);
            cmd.Parameters.AddWithValue("@description", description.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}