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
    public partial class samplingPlots : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetMapLayers();
            int count = GetInerSectionGridCount();
            txtgrid.Text = count >0 ? count.ToString() : "0";
        }
        int GetInerSectionGridCount()
        {
            int count = 0;
            int intDivisionId = !string.IsNullOrEmpty(Session["DivisionId"].ToString()) ? Convert.ToInt32(Session["DivisionId"].ToString()) : 0;
           // string divisionid = Session["DivisionId"].ToString();
            try
            {
               
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GridCount"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@divisionid", intDivisionId);
                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 120;

                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    count = Convert.ToInt32(dt.Rows[0]["InterSectionGridCount"].ToString());

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return count;
            
        }
        private void GetMapLayers()
        {
            string divisionid = Session["DivisionId"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_layernames"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

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
                            if (dt.Rows.Count > 0)
                            {
                                string x = dt.Rows[0]["Layer_Name"].ToString();
                                string[] layers = x.Split(':');
                                string lon = dt.Rows[0]["divLongitude"].ToString();
                                string lat = dt.Rows[0]["divLattitude"].ToString();
                                for (int i = 0; i < layers.Length; i++)
                                {
                                    string layer = layers[i];
                                    //vw_division_medak
                                    string[] layerss = layer.Split('_');



                                    if (layerss.Length > 0)
                                    {
                                       
                                        if (layerss[1].ToString() == "division")
                                        {
                                            hdndivision.Value = layer;
                                        }
                                       
                                        if (layerss[1].ToString() == "block")
                                        {
                                            hdnblock.Value = layer;
                                        }
                                        if (layerss[1].ToString() == "compartment")
                                        {
                                            hdncompartment.Value = layer;
                                        }
                                        if (layerss[1].ToString() == "plot")
                                        {
                                            hdnplots.Value = layer;
                                        }
                                        if (layerss[1].ToString() == "grid")
                                        {
                                            hdngrid.Value = layer;
                                        }

                                    }
                                }
                                hdnlon.Value = lon;
                                hdnlat.Value = lat;

                            }

                        }

                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            double CV = Convert.ToDouble(txtCV.Text); // Coefficient of variation
            double AE = 10; // Allowable error (%)
            double tv = 1.96; // Value of t-statistic with v degrees of freedom and 5% significance level
            double N = Convert.ToDouble(txtgrid.Text); // Total number of plots of optimum size of the main characteristic in the population

            double part1 = Math.Pow(tv, 1);
            double part2 = Math.Pow(CV / AE, 2);
            double part3 = (1 + 1) / (N * part2);

            double n = (part1 * part2) / part3;

            // Display the result in the label
            NLabel.Text = "sample size(n): " + n.ToString("0");
        }
    }
}