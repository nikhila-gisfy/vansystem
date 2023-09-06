using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace vansystem
{
   
    public partial class getNewUser : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }

      

       

        protected void Unnamed_ServerClick1(object sender, EventArgs e)
        {
            string divisionid = Session["DivisionId"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
               



                // Check if a user with the specified user_id already exists
                using (SqlCommand cmdCheckUser = new SqlCommand("sp_getNewUser"))
                {
                    cmdCheckUser.Connection = con;
                    cmdCheckUser.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmdCheckUser.Parameters.AddWithValue("@StatementType", "checkuserid");
                    cmdCheckUser.Parameters.AddWithValue("@user_id", user_id.Value);

                    int existingUserCount = (int)cmdCheckUser.ExecuteScalar();

                    con.Close();

                    if (existingUserCount > 0)
                    {
                        // User with the provided user_id already exists, show a message or take appropriate action
                        checkuserid.Text = "User with user_id already exists.";
                    }
                    else
                    {
                        // User with the provided user_id doesn't exist, proceed with insertion
                        using (SqlCommand cmdInsertUser = new SqlCommand("sp_getNewUser"))
                        {
                            cmdInsertUser.Connection = con;
                            cmdInsertUser.CommandType = CommandType.StoredProcedure;
                            con.Open();

                            cmdInsertUser.Parameters.AddWithValue("@StatementType", "Insert");
                            cmdInsertUser.Parameters.AddWithValue("@name", name.Value);
                            cmdInsertUser.Parameters.AddWithValue("@mobilenum", mob_number.Value);
                            cmdInsertUser.Parameters.AddWithValue("@email", email.Value);
                            cmdInsertUser.Parameters.AddWithValue("@user_id", user_id.Value);
                            cmdInsertUser.Parameters.AddWithValue("@password", password.Value);
                            cmdInsertUser.Parameters.AddWithValue("@Range", range.Value);
                            cmdInsertUser.Parameters.AddWithValue("@divisionid", divisionid);

                            int i = cmdInsertUser.ExecuteNonQuery();

                            con.Close();

                            if (i == 1)
                            {
                                insertbox.Text = "Record Inserted Successfully";
                            }
                        }
                    }
                }





            }
        }
    }
}