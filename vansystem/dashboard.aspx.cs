using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace vansystem
{
    public partial class dashboard1 : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTabels();
                GetTables1();
                DropDown();
                GetMapLayers();
            }

        }
        private void GetTabels()
        {
            string username = Session["user_id"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SpDashboard"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 4);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVvillagelevelinformation.DataSource = dt;
                            GVvillagelevelinformation.DataBind();
                        }


                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 5);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVplotapproach.DataSource = dt;
                            GVplotapproach.DataBind();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 2);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVplotdiscription.DataSource = dt;
                            GVplotdiscription.DataBind();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 3);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVplotenumeration.DataSource = dt;
                            GVplotenumeration.DataBind();
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 6);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVhousehold.DataSource = dt;
                            GVhousehold.DataBind();
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 7);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVtimberextarction.DataSource = dt;
                            GVtimberextarction.DataBind();
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetTableInfo");
                        cmd.Parameters.AddWithValue("@f_id", 8);
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVNTFPextraction.DataSource = dt;
                            GVNTFPextraction.DataBind();
                        }

                    }
                }
            }
        }
        private void GetTables1()
        {
            string username = Session["user_id"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_dashboard1"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "GetProvisionservices");
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVprovisioningservices.DataSource = dt;
                            GVprovisioningservices.DataBind();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetHydrologicalservices");
                        //cmd.Parameters.AddWithValue("@f_id", "");
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVhydrologicalservices.DataSource = dt;
                            GVhydrologicalservices.DataBind();
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@operation", "GetCulturalservices");
                        //cmd.Parameters.AddWithValue("@f_id", "");
                        cmd.Parameters.AddWithValue("@user_id", username);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVculturalservices.DataSource = dt;
                            GVculturalservices.DataBind();
                        }



                    }
                }
            }
        }
        private void DropDown()
        {

            SqlCommand cmd = new SqlCommand("SpDashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "GetDllInfo");
            cmd.Parameters.AddWithValue("@f_id", "");
            cmd.Parameters.AddWithValue("@user_id", "");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count > 0)

            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ddlselectform.DataSource = dt;
                    ddlselectform.DataTextField = "description";
                    ddlselectform.DataValueField = "f_id";
                    ddlselectform.DataBind();
                    //ddlselectform.Items.Insert(0, "Select Form");
                    ddlselectform.Items.Insert(0, new ListItem("-- Select Form --", "0"));
                }
            }

            con.Close();
        }

        protected void ddlselectform_SelectedIndexChanged(object sender, EventArgs e)
        {
            string username = Session["user_id"].ToString();
            string fid = ddlselectform.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand("SpDashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation", "GetTotalSurveys");
            cmd.Parameters.AddWithValue("@f_id", fid);
            cmd.Parameters.AddWithValue("@user_id", username);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count > 0)

            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cell = dt.Rows[i]["Totalplotssurveyed"].ToString();

                    lbltsc2.Text = cell;






                }
                lbltsc2.DataBind();
            }
            con.Close();



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
                                        if (layerss[1].ToString() == "plot")
                                        {
                                            hdnplots.Value = layer;
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

    }
}