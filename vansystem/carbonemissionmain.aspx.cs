﻿using Microsoft.Reporting.WebForms;
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
    public partial class carbonemissionmain : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }


       

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string divisionid = 148.ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_carbonemission"))
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
                            ReportParameter rp1 = new ReportParameter("division", "adilabad");
                            ReportParameter rp2 = new ReportParameter("login", "adilabad-DFO");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Division.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("Division", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "Division.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
                            ReportViewer1.LocalReport.Refresh();
                        }
                    }
                }
            }
        }

        protected void btnrangewise_Click(object sender, EventArgs e)
        {
            string divisionid = 148.ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_carbonemission"))
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
                            ReportParameter rp2 = new ReportParameter("login", "adilabad-DFO");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Range.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("Range", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "Range.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
                            ReportViewer1.LocalReport.Refresh();
                        }
                    }
                }
            }
        }

        protected void btnblockwise_Click(object sender, EventArgs e)
        {
            
                 string divisionid = 148.ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_carbonemission"))
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
                            ReportParameter rp2 = new ReportParameter("login", "adilabad-DFO");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Blockwise.rdlc");
                            ReportDataSource RDstblnames = new ReportDataSource("Block", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(RDstblnames);
                            ReportViewer1.LocalReport.ReportPath = "Blockwise.rdlc";
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
                            ReportViewer1.LocalReport.Refresh();
                        }
                    }
                }
            }
        }
    }
}