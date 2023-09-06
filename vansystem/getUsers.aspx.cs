using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class getUsers : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string divisionid = Session["DivisionId"].ToString();
            

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsers"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@divisionid", divisionid);
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVgetuser.DataSource = dt;
                            GVgetuser.DataBind();
                        }

                    }
                }
            }
        }
        protected void GVgetuser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int uid = Convert.ToInt32(GVgetuser.DataKeys[e.NewEditIndex].Value);
            Session["uid"] = uid;
            Response.Redirect("getUpdateUser.aspx?uid=" + uid);
        }
    }
}