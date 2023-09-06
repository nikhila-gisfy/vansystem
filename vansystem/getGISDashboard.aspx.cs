using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace vansystem
{
    public partial class getGISDashboard : System.Web.UI.Page
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

        protected void divisionlink_ServerClick(object sender, EventArgs e)
        {
            string href = ((HtmlAnchor)sender).ID;

            string layername = href;
            string divisionname = "";
            string minx = "";
            string miny = "";
            string maxx = "";
            string maxy = "";
            string userid = Session["user_id"].ToString();
            //string userid = "adilabad@van";
            try
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Clear();

                nvc.Add("@user_id", userid);


                DataTable dt = new clsConnnection().fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[GetDivisionById]", nvc);
                using (dt)
                {
                    divisionname = dt.Rows[0]["DivisionName"].ToString();
                    minx = dt.Rows[0]["minx"].ToString();
                    miny = dt.Rows[0]["miny"].ToString();
                    maxx = dt.Rows[0]["maxx"].ToString();
                    maxy = dt.Rows[0]["maxy"].ToString();
                }

            }
            catch (Exception ex) { }
            Session["layername"] = layername;
            Session["division"] = divisionname;
            Session["minx"] = minx;
            Session["miny"] = miny;
            Session["maxx"] = maxx;
            Session["maxy"] = maxy;
            Response.Redirect("GISReportGenerator.aspx");
        }
    }
}