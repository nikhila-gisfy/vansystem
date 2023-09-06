
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace vansystem
{
    public partial class AdmingetNewuser : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDState();
                //string stateid = ddlselectstate.SelectedValue.ToString();
                //if (stateid != null)
                //{
                //  DDDivision();
                //}
            }
               

        }
        public void DDState()
        {
            con= new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_Admingetnewuser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "GetDllInfo");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count > 0)

            {

                // string combine = dt.Rows[0]["StateId"].ToString() + "|" + dt.Rows[0]["mc"].ToString();
                dt.Columns.Add("CombinedValue", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    string combine = row["StateId"].ToString() + "|" + row["mc"].ToString();
                    row["CombinedValue"] = combine;
                }

                ddlselectstate.DataSource = dt;
                    ddlselectstate.DataTextField = "StateName";
                    ddlselectstate.DataValueField = "CombinedValue";
                    ddlselectstate.DataBind();
                    ddlselectstate.Items.Insert(0, "Select State");
                
            }

            con.Close();
        }
       
       

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {

            string selectedValue = ddlselectstate.SelectedValue;
            string statecode = string.Empty;
            string stateid = string.Empty;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Split the selected value based on the delimiter, if applicable
                string[] values = selectedValue.Split('|');

                // Extract the "OtherValue" based on the index or property name
                stateid = values[0];
                statecode = values[1]; // Assuming "OtherValue" is the second value after splitting
            }
            //string stateid = ddlselectstate.SelectedValue.ToString();
            string divisionid = ddldivision.SelectedValue.ToString();
            con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_Admingetnewuser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "InsertForm");
            cmd.Parameters.AddWithValue("@name", name.Value);
            cmd.Parameters.AddWithValue("@mobilenum", mob_number.Value);
            cmd.Parameters.AddWithValue("@email", email.Value);
            cmd.Parameters.AddWithValue("@designation", ddlrole.Value);
            cmd.Parameters.AddWithValue("@StateId", stateid);
            cmd.Parameters.AddWithValue("@user_id", user_id.Value);
            cmd.Parameters.AddWithValue("@password", password.Value);
            cmd.Parameters.AddWithValue("@DivisionId", divisionid);
            cmd.Parameters.AddWithValue("@statecode", statecode);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void ddlselectstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlselectstate.SelectedValue;
            string statecode = string.Empty;
            string stateid = string.Empty;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Split the selected value based on the delimiter, if applicable
                string[] values = selectedValue.Split('|');

                // Extract the "OtherValue" based on the index or property name
                stateid = values[0];
                statecode = values[1]; // Assuming "OtherValue" is the second value after splitting
            }
            con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_Admingetnewuser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "GetDllInfodivision");
            cmd.Parameters.AddWithValue("@StateId", stateid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count > 0)

            {
             

                    ddldivision.DataSource = dt;
                    ddldivision.DataTextField = "DivisionName";
                    ddldivision.DataValueField = "DivisionId";
                    ddldivision.DataBind();
                    ddldivision.Items.Insert(0, "Select division");
                
            }

            con.Close();
        }
    }
}