using Microsoft.Reporting.WebForms;
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
    public partial class GrowingStockmain : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string divisionid = Session["DivisionId"].ToString();
            string divisionname = Session["DivisionName"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Growingstock"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;


                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@operation", "Divisionwise");
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportParameter rp1 = new ReportParameter("division", divisionname);
                            ReportParameter rp2 = new ReportParameter("heading", "Growing Stock");
                            ReportParameter rp3 = new ReportParameter("level", "Plot-Wise");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Shan_shimp.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("Shanon_simpson", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "Shan_shimp.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1,rp2 , rp3 });
                            ReportViewer1.LocalReport.Refresh();
                        }
                    }
                }
            }
        }

        protected void btnrangewise_Click(object sender, EventArgs e)
        {
            string divisionid = Session["DivisionId"].ToString();
            string divisionname = Session["DivisionName"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Growingstock"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;


                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@operation", "Rangewise");
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportParameter rp1 = new ReportParameter("division", "adilabad");
                           
                            ReportParameter rp2 = new ReportParameter("heading", "Growing Stock");
                            ReportParameter rp3 = new ReportParameter("level", "Range-Wise");
                            //ReportParameter rp4 = new ReportParameter("minheading", "Min_Growing Stock");
                            //ReportParameter rp5 = new ReportParameter("maxheading", "Max_Growing Stock");
                            //ReportParameter rp6 = new ReportParameter("avgheading", "Avg_Growing Stock");
                            //ReportParameter rp7 = new ReportParameter("SDheading", "SD_Growing Stock");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("GrowingRange.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("Growingrange", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "GrowingRange.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                            ReportViewer1.LocalReport.Refresh();
                        }
                    }
                }
            }
        }

        protected void btnblockwise_Click(object sender, EventArgs e)
        {
            string divisionid = Session["DivisionId"].ToString();
            string divisionname = Session["DivisionName"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Growingstock"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;


                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@operation", "Blockwise");
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportParameter rp1 = new ReportParameter("division", "adilabad");
                           
                            ReportParameter rp2 = new ReportParameter("heading", "Growing Stock");
                            ReportParameter rp3 = new ReportParameter("level", "Range-Wise");
                            //ReportParameter rp4 = new ReportParameter("minheading", "Min_Growing Stock");
                            //ReportParameter rp5 = new ReportParameter("maxheading", "Max_Growing Stock");
                            //ReportParameter rp6 = new ReportParameter("avgheading", "Avg_Growing Stock");
                            //ReportParameter rp7 = new ReportParameter("SDheading", "SD_Growing Stock");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("GrowingBlock.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("growingblock", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "GrowingBlock.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                            ReportViewer1.LocalReport.Refresh();
                            
                        }
                    }
                }
            }
        }

        protected void btncompwise_Click(object sender, EventArgs e)
        {
            string divisionid = Session["DivisionId"].ToString();
            string divisionname = Session["DivisionName"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Growingstock"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;


                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@operation", "Compartmentwise");
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportParameter rp1 = new ReportParameter("division", "adilabad");

                            ReportParameter rp2 = new ReportParameter("heading", "Growing Stock");
                            ReportParameter rp3 = new ReportParameter("level", "Range-Wise");
                            //ReportParameter rp4 = new ReportParameter("minheading", "Min_Growing Stock");
                            //ReportParameter rp5 = new ReportParameter("maxheading", "Max_Growing Stock");
                            //ReportParameter rp6 = new ReportParameter("avgheading", "Avg_Growing Stock");
                            //ReportParameter rp7 = new ReportParameter("SDheading", "SD_Growing Stock");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Compatmentwise.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("compartmentwise", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "Compatmentwise.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                            ReportViewer1.LocalReport.Refresh();

                        }
                    }
                }
            }
        }
    }
}