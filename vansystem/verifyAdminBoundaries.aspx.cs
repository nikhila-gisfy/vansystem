using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace vansystem.DataVerification
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMapLayers();
            }
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
                            if(dt.Rows.Count > 0) 
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

                                //layerss[0]-vw
                                //layerss[1]-division
                                //layerss[2]-medak
                                if (layerss.Length > 0)
                                {
                                    if (layerss[1].ToString() == "division")
                                    {
                                        hdndivision.Value = layer;
                                    }
                                    if (layerss[1].ToString() == "range")
                                    {
                                        hdnrange.Value = layer;
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
                                }
                            }
                            hdnlon.Value = lon;
                            hdnlat.Value = lat;
                                // string[] layername = layers.Split();


                                //assign to a variable - in backend 
                                // Split
                            }
                        }

                    }
                }
            }
        }
    }
}