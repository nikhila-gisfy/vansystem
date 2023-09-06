using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class IVICal : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call the method to populate the first dropdown (ddlRange)
                PopulateRangeDropDown();

                // Call the method to populate the second dropdown (ddlHabitType)
                PopulateHabitTypeDropDown();
            }
        }
        
          private void PopulateRangeDropDown()
          {
            string divisionid = Session["DivisionId"].ToString();
            // Provide your connection string here
            //string connectionString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("spGetIviCalBY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Operation", "RangeName");
                    cmd.Parameters.AddWithValue("@DivisionID", divisionid); // Set the DivisionID appropriately

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlRangeName.DataSource = reader;
                    ddlRangeName.DataTextField = "RangeName";
                    ddlRangeName.DataValueField = "RangeId";
                    ddlRangeName.DataBind();

                    ddlRangeName.Items.Insert(0, new ListItem("-- Select Range --", "0"));
                }
            }
          }
    
    
    

        private void PopulateHabitTypeDropDown()
        {
            // Provide your connection string here
            //string connectionString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("spGetIviCalBY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Operation", "habit_type");
                    //cmd.Parameters.AddWithValue("@DivisionID", 1);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlHabitType.DataSource = reader;
                    ddlHabitType.DataTextField = "name";
                    ddlHabitType.DataValueField = "id";
                    ddlHabitType.DataBind();

                    ddlHabitType.Items.Insert(0, new ListItem("-- Select Habit Type --", "0"));
                }
            }
        }
        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            // Get the selected values from the dropdown lists
            string selectedRangeId = ddlRangeName.SelectedValue;
            string selectedHabitTypeId = ddlHabitType.SelectedItem.ToString();
            string divisionname = Session["DivisionName"].ToString();
            string selectedrangename = ddlRangeName.SelectedItem.ToString();


            //// Do whatever processing you need with the selected values
            //// For example, you can concatenate them into a single string:
            //string result = "Selected Range ID: " + selectedRangeId + ", Selected Habit Type ID: " + selectedHabitTypeId;

            //// Now, you can use the 'result' string as needed, for example, displaying it in a label or redirecting to another page.
            if (selectedHabitTypeId == "Tree")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ivitree"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;


                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;
                            cmd.Parameters.AddWithValue("@rangeid", selectedRangeId);



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportParameter rp1 = new ReportParameter("division", divisionname);
                                ReportParameter rp2 = new ReportParameter("habittype", "Tree");
                                ReportParameter rp3 = new ReportParameter("rangename", selectedrangename);

                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("IVI.rdlc");
                                ReportDataSource RDstblnames = new ReportDataSource("ivi", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                                ReportViewer1.LocalReport.ReportPath = "IVI.rdlc";
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1,rp2,rp3 });
                                ReportViewer1.LocalReport.Refresh();
                            }
                        }
                    }
                }
            }
            if (selectedHabitTypeId == "Shrub")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ivishrub"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;


                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;
                            cmd.Parameters.AddWithValue("@rangeid", selectedRangeId);



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportParameter rp1 = new ReportParameter("division", divisionname);
                                ReportParameter rp2 = new ReportParameter("habittype", "Shrub");
                                ReportParameter rp3 = new ReportParameter("rangename", selectedrangename);
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("IVI.rdlc");
                                ReportDataSource RDstblnames = new ReportDataSource("ivi", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                                ReportViewer1.LocalReport.ReportPath = "IVI.rdlc";
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 , rp3 });
                                ReportViewer1.LocalReport.Refresh();
                            }
                        }
                    }
                }
            }
            if (selectedHabitTypeId == "Herb")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_iviherb"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;


                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;
                            cmd.Parameters.AddWithValue("@rangeid", selectedRangeId);



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportParameter rp1 = new ReportParameter("division", divisionname);
                                ReportParameter rp2 = new ReportParameter("habittype", "Herb");
                                ReportParameter rp3 = new ReportParameter("rangename", selectedrangename);
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("IVI.rdlc");
                                ReportDataSource RDstblnames = new ReportDataSource("ivi", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                                ReportViewer1.LocalReport.ReportPath = "IVI.rdlc";
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 , rp3 });
                                ReportViewer1.LocalReport.Refresh();
                            }
                        }
                    }
                }
            }
            if (selectedHabitTypeId == "Climber")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_iviclimber"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;


                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;
                            cmd.Parameters.AddWithValue("@rangeid", selectedRangeId);



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportParameter rp1 = new ReportParameter("division", divisionname);
                                ReportParameter rp2 = new ReportParameter("habittype", "Climber");
                                ReportParameter rp3 = new ReportParameter("rangename", selectedrangename);
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("IVI.rdlc");
                                ReportDataSource RDstblnames = new ReportDataSource("ivi", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                                ReportViewer1.LocalReport.ReportPath = "IVI.rdlc";
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 , rp3 });
                                ReportViewer1.LocalReport.Refresh();
                            }
                        }
                    }
                }
            }
            if (selectedHabitTypeId == "Grass")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ivigrass"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;


                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;
                            cmd.Parameters.AddWithValue("@rangeid", selectedRangeId);



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportParameter rp1 = new ReportParameter("division", divisionname);
                                ReportParameter rp2 = new ReportParameter("habittype", "Grass");
                                ReportParameter rp3 = new ReportParameter("rangename", selectedrangename);
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("IVI.rdlc");
                                ReportDataSource RDstblnames = new ReportDataSource("ivi", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                                ReportViewer1.LocalReport.ReportPath = "IVI.rdlc";
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 , rp3 });
                                ReportViewer1.LocalReport.Refresh();
                            }
                        }
                    }
                }
            }
        }




    }
}