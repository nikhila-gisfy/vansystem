using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace vansystem
{
    public partial class Rangewise : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
                BindGrid();

            
        }
        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("rangewise"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvrangewise.DataSource = dt;
                            gvrangewise.DataBind();
                           
                        }
                    }


                }
            }
        }  
    }
}