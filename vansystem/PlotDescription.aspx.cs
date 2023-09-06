using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using vansystem.Models;

namespace vansystem
{
    public partial class PlotDescription : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGrid(1);
            }
        }
        private void BindGrid(int pageIndex)
        {
            string divisionid = Session["DivisionId"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_PlotDescription"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 3600;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVPlotDescription.DataSource = dt;
                            GVPlotDescription.DataBind();
                        }
                        int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
                    }
                }
            }
        }

        public string TrimTo(object stringToTrim, int noOfChar)
        {
            if (stringToTrim != null)
            {
                return stringToTrim.ToString().TrimToString(noOfChar, true);
            }
            else
            {
                return "";
            }

        }

        protected void GVPlotDescription_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GVPlotDescription.PageIndex = e.NewPageIndex;
            BindGrid(1);
        }


        protected void GVPlotDescription_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVPlotDescription.EditIndex = -1;
            BindGrid(1);

        }

        protected void GVPlotDescription_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVPlotDescription.EditIndex = e.NewEditIndex;
            BindGrid(1);

        }

        protected void GVPlotDescription_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVPlotDescription.DataKeys[e.RowIndex].Value.ToString());

            string name = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            string Slope = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlSlope") as DropDownList).SelectedValue;
            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE tblPlotDesc SET Slope = @Slope WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Slope", Slope);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string General = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlGeneral") as DropDownList).SelectedValue;
            string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString1))
            {
                string query = "UPDATE tblPlotDesc SET General = @General WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@General", General);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Landuse = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlLanduse") as DropDownList).SelectedValue;
            string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString2))
            {
                string query = "UPDATE tblPlotDesc SET Landuse = @Landuse WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Landuse", Landuse);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Rocks = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[18].Controls[0]).Text;

            string Soiltexture = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlSoiltexture") as DropDownList).SelectedValue;
            string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString3))
            {
                string query = "UPDATE tblPlotDesc SET Soiltexture = @Soiltexture WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Soiltexture", Soiltexture);
                    //con.Open();
                    //cmd.ExecuteNonQuery();

                }
            }

            string Soildepth = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[21].Controls[0]).Text;

            string Soilpermeability = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlSoilpermeability") as DropDownList).SelectedValue;
            string conString5 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString5))
            {
                string query = "UPDATE tblPlotDesc SET Soilpermeability = @Soilpermeability WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Soilpermeability", Soilpermeability);
                    //con.Open();
                    //cmd.ExecuteNonQuery();

                }
            }

            string Soilerosion = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlSoilerosion") as DropDownList).SelectedValue;
            string conString6 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString6))
            {
                string query = "UPDATE tblPlotDesc SET Soilerosion = @Soilerosion WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Soilerosion", Soilerosion);
                    //con.Open();
                    //cmd.ExecuteNonQuery();

                }
            }


            ListBox lstMammals = (GVPlotDescription.Rows[e.RowIndex].FindControl("lstMammals") as ListBox);
            List<string> selectedMammals = new List<string>();
            foreach (ListItem item in lstMammals.Items)
            {
                if (item.Selected)
                {
                    selectedMammals.Add(item.Value);
                }
            }
            string mammals = string.Join(", ", selectedMammals);
            string conString7 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString7))
            {
                string query = "UPDATE tblPlotDesc SET Mammals = @Mammals WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Mammals", mammals);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            //string birds = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlBirds") as DropDownList).SelectedValue;
            ListBox lstBirds = (GVPlotDescription.Rows[e.RowIndex].FindControl("lstBirds") as ListBox);
            List<string> selectedBirds = new List<string>();
            foreach (ListItem item in lstBirds.Items)
            {
                if (item.Selected)
                {
                    selectedBirds.Add(item.Value);
                }
            }
            string birds = string.Join(", ", selectedBirds);
            string conString8 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString8))
            {
                string query = "UPDATE tblPlotDesc SET Birds = @Birds WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Birds", birds);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }


            ListBox lstReptiles = (GVPlotDescription.Rows[e.RowIndex].FindControl("lstReptiles") as ListBox);
            List<string> selectedReptiles = new List<string>();
            foreach (ListItem item in lstReptiles.Items)
            {
                if (item.Selected)
                {
                    selectedReptiles.Add(item.Value);
                }
            }
            string reptiles = string.Join(", ", selectedReptiles);
            string conString4 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString4))
            {
                string query = "UPDATE tblPlotDesc SET Reptiles = @Reptiles WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Reptiles", reptiles);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            ListBox lstAmphibians = (GVPlotDescription.Rows[e.RowIndex].FindControl("lstAmphibians") as ListBox);
            List<string> selectedAmphibians = new List<string>();
            foreach (ListItem item in lstAmphibians.Items)
            {
                if (item.Selected)
                {
                    selectedAmphibians.Add(item.Value);
                }
            }
            string amphibians = string.Join(", ", selectedAmphibians);
            string conString9 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString9))
            {
                string query = "UPDATE tblPlotDesc SET Amphibians = @Amphibians WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Amphibians", amphibians);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            ListBox lstPlants = (GVPlotDescription.Rows[e.RowIndex].FindControl("lstPlants") as ListBox);
            List<string> selectedPlants = new List<string>();
            foreach (ListItem item in lstPlants.Items)
            {
                if (item.Selected)
                {
                    selectedAmphibians.Add(item.Value);
                }
            }
            string plants = string.Join(", ", selectedPlants);
            string conString10 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString10))
            {
                string query = "UPDATE tblPlotDesc SET Plants = @Plants WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Plants", plants);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string CharcoalMaking = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlCharcoalMaking") as DropDownList).SelectedValue;
            string conString0 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString0))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @CharcoalMaking WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@CharcoalMaking", CharcoalMaking);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Earthquake = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlEarthquake") as DropDownList).SelectedValue;
            string conString11 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString11))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Earthquake WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Earthquake", Earthquake);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string DevelopmentProjects = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlDevelopmentProjects") as DropDownList).SelectedValue;
            string conString12 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString12))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @DevelopmentProjects WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DevelopmentProjects", DevelopmentProjects);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Fire = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlFire") as DropDownList).SelectedValue;
            string conString13 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString13))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Fire WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Fire", Fire);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string FirewoodExtraction = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlFirewoodExtraction") as DropDownList).SelectedValue;
            string conString14 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString14))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @FirewoodExtraction WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@FirewoodExtraction", FirewoodExtraction);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();FirewoodExtraction
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string IllegalLogging = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlIllegalLogging") as DropDownList).SelectedValue;
            string conString15 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString15))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @IllegalLogging WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@IllegalLogging", IllegalLogging);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Mining = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlMining") as DropDownList).SelectedValue;
            string conString16 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString16))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Mining WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Mining", Mining);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Grazing = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlGrazing") as DropDownList).SelectedValue;
            string conString17 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString17))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Grazing WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Grazing", Grazing);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string InvasiveSpecies = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlInvasiveSpecies") as DropDownList).SelectedValue;
            string conString26 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString26))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @InvasiveSpecies WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@InvasiveSpecies", InvasiveSpecies);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string PathogenicAttack = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlPathogenicAttack") as DropDownList).SelectedValue;
            string conString27 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString27))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @PathogenicAttack WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@PathogenicAttack", PathogenicAttack);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Drought = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlDrought") as DropDownList).SelectedValue;
            string conString28 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString28))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Drought WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Drought", Drought);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Flood = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlFlood") as DropDownList).SelectedValue;
            string conString29 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString29))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Flood WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Flood", Flood);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Landslides = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlLandslides") as DropDownList).SelectedValue;
            string conString18 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString18))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Landslides WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Landslides", Landslides);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Avalanche = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlAvalanche") as DropDownList).SelectedValue;
            string conString19 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString19))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Avalanche WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Avalanche", Avalanche);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Storm = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlStorm") as DropDownList).SelectedValue;
            string conString20 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString20))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Storm WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Storm", Storm);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Cyclone = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlCyclone") as DropDownList).SelectedValue;
            string conString21 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString21))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Cyclone WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Cyclone", Cyclone);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string HeavyRainfall = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlHeavyRainfall") as DropDownList).SelectedValue;
            string conString22 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString22))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @HeavyRainfall WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@HeavyRainfall", HeavyRainfall);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Other = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlOther") as DropDownList).SelectedValue;
            string conString24 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString24))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @Other WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Other", Other);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string HeavySnowfall = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlHeavySnowfall") as DropDownList).SelectedValue;
            string conString23 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString23))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @HeavySnowfall WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@HeavySnowfall", HeavySnowfall);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string None = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlNone") as DropDownList).SelectedValue;
            string conString25 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString25))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @None WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@None", None);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Crop = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[21].Controls[0]).Text;

            string Regeneration = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlRegeneration") as DropDownList).SelectedValue;
            string conString30 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString30))
            {
                string query = "UPDATE tblPlotDesc SET Regeneration = @Regeneration WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Regeneration", Regeneration);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Injury = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlInjury") as DropDownList).SelectedValue;
            string conString31 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString31))
            {
                string query = "UPDATE tblPlotDesc SET Injury = @Injury WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Injury", Injury);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string Cc_Grazing = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlCc_Grazing") as DropDownList).SelectedValue;
            string conString32 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString32))
            {
                string query = "UPDATE tblPlotDesc SET Grazing = @Grazing WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Grazing", Cc_Grazing);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string PresenceOfBamboo = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlPresenceOfBamboo") as DropDownList).SelectedValue;
            string conString33 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString33))
            {
                string query = "UPDATE tblPlotDesc SET TypeOfDegradation = @PresenceOfBamboo WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@PresenceOfBamboo", PresenceOfBamboo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string BambooQuality = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlBambooQuality") as DropDownList).SelectedValue;
            string conString34 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString34))
            {
                string query = "UPDATE tblPlotDesc SET BambooQuality = @BambooQuality WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@BambooQuality", BambooQuality);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string BambooRegeneration = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlBambooRegeneration") as DropDownList).SelectedValue;
            string conString35 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString35))
            {
                string query = "UPDATE tblPlotDesc SET BambooRegeneration = @BambooRegeneration WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@BambooRegeneration", BambooRegeneration);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string PresenceOfGrass = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlPresenceOfGrass") as DropDownList).SelectedValue;
            string conString36 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString36))
            {
                string query = "UPDATE tblPlotDesc SET PresenceOfGrass = @PresenceOfGrass WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@PresenceOfGrass", PresenceOfGrass);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string PresenceOfWeeds = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlPresenceOfWeeds") as DropDownList).SelectedValue;
            string conString37 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString37))
            {
                string query = "UPDATE tblPlotDesc SET PresenceOfWeeds = @PresenceOfWeeds WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@PresenceOfWeeds", PresenceOfWeeds);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }



            string Plantation = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[35].Controls[0]).Text;

            string WaterBodyType = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlWaterBodyType") as DropDownList).SelectedValue;
            string conString39 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString39))
            {
                string query = "UPDATE tblPlotDesc SET WaterBodyType = @WaterBodyType WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@WaterBodyType", WaterBodyType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string WaterBodyStatus = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlWaterBodyStatus") as DropDownList).SelectedValue;
            string conString40 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString40))
            {
                string query = "UPDATE tblPlotDesc SET WaterBodyStatus = @WaterBodyStatus WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@WaterBodyStatus", WaterBodyStatus);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string WaterBodySeasonality = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlWaterBodySeasonality") as DropDownList).SelectedValue;
            string conString41 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString41))
            {
                string query = "UPDATE tblPlotDesc SET WaterBodySeasonality = @WaterBodySeasonality WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@WaterBodySeasonality", WaterBodySeasonality);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

            string WaterBodyPotability = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddlWaterBodyPotability") as DropDownList).SelectedValue;
            string conString42 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString42))
            {
                string query = "UPDATE tblPlotDesc SET WaterBodyPotability = @WaterBodyPotability WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@WaterBodyPotability", WaterBodyPotability);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }




            string legalStatus = (GVPlotDescription.Rows[e.RowIndex].FindControl("ddllegalStatus") as DropDownList).SelectedValue;
            string conString43 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString43))
            {
                string query = "UPDATE tblPlotDesc SET legalStatus = @legalStatus WHERE PlotDescId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@legalStatus", legalStatus);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }



            string PhoneNumber = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string Plants_comments = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[70].Controls[0]).Text;
            string Birds_comments = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[64].Controls[0]).Text;
            string Mammals_comments = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[62].Controls[0]).Text;
            string Reptiles_comments = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[66].Controls[0]).Text;
            string Amphibians_comments = ((TextBox)GVPlotDescription.Rows[e.RowIndex].Cells[68].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_updateplotdescrip", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PlotDescId ", id);
                    cmd.Parameters.AddWithValue("@PlotDescName", name);

                    cmd.Parameters.AddWithValue("@Rocks", Rocks);

                    cmd.Parameters.AddWithValue("@Soildepth", Soildepth);

                    cmd.Parameters.AddWithValue("@Plantation", Plantation);

                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@Plants_comments", Plants_comments);

                    cmd.Parameters.AddWithValue("@Birds_comments", Birds_comments);
                    cmd.Parameters.AddWithValue("@Mammals_comments", Mammals_comments);
                    cmd.Parameters.AddWithValue("@Reptiles_comments", Reptiles_comments);
                    cmd.Parameters.AddWithValue("@Amphibians_comments", Amphibians_comments);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVPlotDescription.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();


                }
            }
        }
        protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));

                if (pageCount < 4)
                {
                    for (int i = 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else if (currentPage < 4)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                else if (currentPage > pageCount - 4)
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 2; i <= currentPage + 2; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
                }
                //for (int i = 1; i <= pageCount; i++)
                //{
                //    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                //}
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.ContentType = "File/Data.xls";
            System.IO.StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

            GVPlotDescription.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();
        }

        private void GetDataForExcel()
        {
            string divisionid = Session["DivisionId"].ToString();
            string DivisionName = Session["name"].ToString();
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                    // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                    using (SqlCommand cmd = new SqlCommand("Get_All_sp_PlotDescription"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@divisionid", divisionid);
                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 3600;

                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    wb.Worksheets.Add(dt, "Customers");

                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=PlotDescription_" + DivisionName + "_" + DateTime.Now + ".xlsx" + "");
                                    // Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                                    using (MemoryStream MyMemoryStream = new MemoryStream())
                                    {
                                        wb.SaveAs(MyMemoryStream);
                                        MyMemoryStream.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        Response.End();
                                    }
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GVPlotDescription.AllowPaging = false;
                this.BindGrid(1);

                GVPlotDescription.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GVPlotDescription.HeaderRow.Cells)
                {
                    cell.BackColor = GVPlotDescription.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GVPlotDescription.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GVPlotDescription.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GVPlotDescription.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GVPlotDescription.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //ExportGridToExcel();
            GetDataForExcel();

        }
        public string ds2json()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_PlotDescription"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVPlotDescription.DataSource = dt;
                            GVPlotDescription.DataBind();
                            ds.Tables.Add(dt);
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(ds, Formatting.Indented);
        }


        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPageSize.SelectedValue == "All")
            {
                pagingnation.Visible = false;
                string divisionid = Session["DivisionId"].ToString();
                string DivisionName = Session["name"].ToString();
                try
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        using (SqlCommand cmd = new SqlCommand("Get_AllDataPlotDescription"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@divisionid", divisionid);
                                sda.SelectCommand = cmd;
                                cmd.CommandTimeout = 3600;

                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    GVPlotDescription.DataSource = dt;
                                    GVPlotDescription.DataBind();

                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                pagingnation.Visible = true;
                this.BindGrid(1);
            }

        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
        }

        protected void GVPlotDescription_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVPlotDescription.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    //DropDownList ddlMammals = (DropDownList)e.Row.FindControl("ddlMammals");
                    ListBox lstMammals = (ListBox)e.Row.FindControl("lstMammals");
                    string sql7 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=10";
                    string conString7 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString7))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql7, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstMammals.DataSource = dt;
                                lstMammals.DataTextField = "sc_name";
                                lstMammals.DataValueField = "SpeciesId";
                                lstMammals.DataBind();
                                string selectedMammals = DataBinder.Eval(e.Row.DataItem, "Mammals").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in lstMammals.Items)
                                {
                                    item.Selected = selectedMammals.Contains(item.Text);
                                }
                            }
                        }
                    }



                    ListBox lstBirds = (ListBox)e.Row.FindControl("lstBirds");
                    string sql8 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=9";
                    string conString8 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString8))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql8, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstBirds.DataSource = dt;
                                lstBirds.DataTextField = "sc_name";
                                lstBirds.DataValueField = "SpeciesId";
                                lstBirds.DataBind();
                                string selectedBirds = DataBinder.Eval(e.Row.DataItem, "Birds").ToString();
                                //ddlBirds.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in lstBirds.Items)
                                {
                                    item.Selected = selectedBirds.Contains(item.Text);
                                }
                            }
                        }
                    }
                    ListBox lstReptiles = (ListBox)e.Row.FindControl("lstReptiles");
                    string sql4 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=11";
                    string conString4 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString4))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql4, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstReptiles.DataSource = dt;
                                lstReptiles.DataTextField = "sc_name";
                                lstReptiles.DataValueField = "SpeciesId";
                                lstReptiles.DataBind();
                                string selectedReptiles = DataBinder.Eval(e.Row.DataItem, "Reptiles").ToString();
                                //ddlReptiles.Items.FindByText(selectedReptiles).Selected = true;
                                foreach (ListItem item in lstReptiles.Items)
                                {
                                    item.Selected = selectedReptiles.Contains(item.Text);
                                }
                            }
                        }
                    }
                    //DropDownList ddlAmphibians = (DropDownList)e.Row.FindControl("ddlAmphibians");
                    ListBox lstAmphibians = (ListBox)e.Row.FindControl("lstAmphibians");
                    string sql9 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=8";
                    string conString9 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString9))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql9, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstAmphibians.DataSource = dt;
                                lstAmphibians.DataTextField = "sc_name";
                                lstAmphibians.DataValueField = "SpeciesId";
                                lstAmphibians.DataBind();
                                string selectedAmphibians = DataBinder.Eval(e.Row.DataItem, "Amphibians").ToString();
                                //ddlAmphibians.Items.FindByText(selectedAmphibians).Selected = true;
                                foreach (ListItem item in lstAmphibians.Items)
                                {
                                    item.Selected = selectedAmphibians.Contains(item.Text);
                                }
                            }
                        }
                    }

                    ListBox lstPlants = (ListBox)e.Row.FindControl("lstPlants");
                    string sql20 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=7";
                    string conString20 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString20))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql20, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstPlants.DataSource = dt;
                                lstPlants.DataTextField = "sc_name";
                                lstPlants.DataValueField = "SpeciesId";
                                lstPlants.DataBind();
                                string selectedPlants = DataBinder.Eval(e.Row.DataItem, "Plants").ToString();
                                //ddlAmphibians.Items.FindByText(selectedAmphibians).Selected = true;
                                foreach (ListItem item in lstPlants.Items)
                                {
                                    item.Selected = selectedPlants.Contains(item.Text);
                                }
                            }
                        }
                    }



                    DropDownList ddlSlope = (DropDownList)e.Row.FindControl("ddlSlope");
                    if (ddlSlope != null)
                    {
                        string sql = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 37";
                        string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
                            {
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    ddlSlope.DataSource = dt;
                                    ddlSlope.DataTextField = "value_eng";
                                    ddlSlope.DataValueField = "id";
                                    ddlSlope.DataBind();
                                    string selectedSlope = DataBinder.Eval(e.Row.DataItem, "Slope")?.ToString();
                                    if (!string.IsNullOrEmpty(selectedSlope))
                                    {
                                        ListItem selectedItem = ddlSlope.Items.FindByText(selectedSlope);
                                        if (selectedItem != null)
                                        {
                                            selectedItem.Selected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                        DropDownList ddlGeneral = (DropDownList)e.Row.FindControl("ddlGeneral");
                        if (ddlGeneral != null)
                        {
                            string sql1 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 42";
                            string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString1))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql1, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlGeneral.DataSource = dt;
                                        ddlGeneral.DataTextField = "value_eng";
                                        ddlGeneral.DataValueField = "id";
                                        ddlGeneral.DataBind();
                                        string selectedGeneral = DataBinder.Eval(e.Row.DataItem, "General")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedGeneral))
                                        {
                                            ListItem selectedItem = ddlGeneral.Items.FindByText(selectedGeneral);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DropDownList ddlLanduse = (DropDownList)e.Row.FindControl("ddlLanduse");
                        if (ddlLanduse != null)
                        {
                            string sql2 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 19";
                            string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString2))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlLanduse.DataSource = dt;
                                        ddlLanduse.DataTextField = "value_eng";
                                        ddlLanduse.DataValueField = "id";
                                        ddlLanduse.DataBind();
                                        string selectedLanduse = DataBinder.Eval(e.Row.DataItem, "Landuse")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedLanduse))
                                        {
                                            ListItem selectedItem = ddlLanduse.Items.FindByText(selectedLanduse);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DropDownList ddlSoiltexture = (DropDownList)e.Row.FindControl("ddlSoiltexture");
                        if (ddlSoiltexture != null)
                        {
                            string sql3 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 36";
                            string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString3))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql3, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlSoiltexture.DataSource = dt;
                                        ddlSoiltexture.DataTextField = "value_eng";
                                        ddlSoiltexture.DataValueField = "id";
                                        ddlSoiltexture.DataBind();
                                        string selectedSoiltexture = DataBinder.Eval(e.Row.DataItem, "Soiltexture")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedSoiltexture))
                                        {
                                            ListItem selectedItem = ddlSoiltexture.Items.FindByText(selectedSoiltexture);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlSoilpermeability = (DropDownList)e.Row.FindControl("ddlSoilpermeability");
                        if (ddlSoilpermeability != null)
                        {
                            string sql5 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 35";
                            string conString5 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString5))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql5, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {

                                        sda.Fill(dt);
                                        ddlSoilpermeability.DataSource = dt;
                                        ddlSoilpermeability.DataTextField = "value_eng";
                                        ddlSoilpermeability.DataValueField = "id";
                                        ddlSoilpermeability.DataBind();
                                        string selectedSoilpermeability = DataBinder.Eval(e.Row.DataItem, "Soilpermeability")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedSoilpermeability))
                                        {
                                            ListItem selectedItem = ddlSoilpermeability.Items.FindByText(selectedSoilpermeability);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DropDownList ddlSoilerosion = (DropDownList)e.Row.FindControl("ddlSoilerosion");
                        if (ddlSoilerosion != null)
                        {
                            string sql6 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 34";
                            string conString6 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString6))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql6, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlSoilerosion.DataSource = dt;
                                        ddlSoilerosion.DataTextField = "value_eng";
                                        ddlSoilerosion.DataValueField = "id";
                                        ddlSoilerosion.DataBind();
                                        string selectedSoilerosion = DataBinder.Eval(e.Row.DataItem, "Soilerosion")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedSoilerosion))
                                        {
                                            ListItem selectedItem = ddlSoilerosion.Items.FindByText(selectedSoilerosion);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlCharcoalMaking = (DropDownList)e.Row.FindControl("ddlCharcoalMaking");

                        Label lblCharcoalMaking = (Label)e.Row.FindControl("CharcoalMaking");

                        if (lblCharcoalMaking != null)
                        {
                            if (lblCharcoalMaking.Text == "118,")
                                ddlCharcoalMaking.SelectedValue = "118,";
                            else if (lblCharcoalMaking.Text == "No")
                                ddlCharcoalMaking.SelectedValue = "No";
                        }
                        string selectedCharcoalMaking = DataBinder.Eval(e.Row.DataItem, "CharcoalMaking")?.ToString();
                    if (!string.IsNullOrEmpty(selectedCharcoalMaking))
                    {
                        ListItem selectedItem = ddlCharcoalMaking.Items.FindByText(selectedCharcoalMaking);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlEarthquake = (DropDownList)e.Row.FindControl("ddlEarthquake");
                        Label lblEarthquake = (Label)e.Row.FindControl("Earthquake");

                        if (lblEarthquake != null)
                        {
                            if (lblEarthquake.Text == "133,")
                                ddlEarthquake.SelectedValue = "133,";
                            else if (lblEarthquake.Text == "No")
                                ddlEarthquake.SelectedValue = "No";
                        }
                        string selectedEarthquake = DataBinder.Eval(e.Row.DataItem, "Earthquake")?.ToString();
                    if (!string.IsNullOrEmpty(selectedEarthquake))
                    {
                        ListItem selectedItem = ddlEarthquake.Items.FindByText(selectedEarthquake);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlDevelopmentProjects = (DropDownList)e.Row.FindControl("ddlDevelopmentProjects");
                        Label lblDevelopmentProjects = (Label)e.Row.FindControl("DevelopmentProjects");

                        if (lblDevelopmentProjects != null)
                        {
                            if (lblDevelopmentProjects.Text == "119,")
                                ddlDevelopmentProjects.SelectedValue = "119,";
                            else if (lblDevelopmentProjects.Text == "No")
                                ddlDevelopmentProjects.SelectedValue = "No";
                        }
                        string selectedDevelopmentProjects = DataBinder.Eval(e.Row.DataItem, "DevelopmentProjects")?.ToString();
                    if (!string.IsNullOrEmpty(selectedDevelopmentProjects))
                    {
                        ListItem selectedItem = ddlDevelopmentProjects.Items.FindByText(selectedDevelopmentProjects);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlFire = (DropDownList)e.Row.FindControl("ddlFire");
                        Label lblFire = (Label)e.Row.FindControl("Earthquake");

                        if (lblFire != null)
                        {
                            if (lblFire.Text == "120,")
                                ddlFire.SelectedValue = "120,";
                            else if (lblFire.Text == "No")
                                ddlFire.SelectedValue = "No";
                        }
                        string selectedFire = DataBinder.Eval(e.Row.DataItem, "Fire")?.ToString();
                    if (!string.IsNullOrEmpty(selectedFire))
                    {
                        ListItem selectedItem = ddlFire.Items.FindByText(selectedFire);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlFirewoodExtraction = (DropDownList)e.Row.FindControl("ddlFirewoodExtraction");
                        Label lblFirewoodExtraction = (Label)e.Row.FindControl("FirewoodExtraction");

                        if (lblFirewoodExtraction != null)
                        {
                            if (lblFirewoodExtraction.Text == "121,")
                                ddlFirewoodExtraction.SelectedValue = "121,";
                            else if (lblFirewoodExtraction.Text == "No")
                                ddlFirewoodExtraction.SelectedValue = "No";
                        }
                        string selectedFirewoodExtraction = DataBinder.Eval(e.Row.DataItem, "FirewoodExtraction")?.ToString();
                    if (!string.IsNullOrEmpty(selectedFirewoodExtraction))
                    {
                        ListItem selectedItem = ddlFirewoodExtraction.Items.FindByText(selectedFirewoodExtraction);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlGrazing = (DropDownList)e.Row.FindControl("ddlGrazing");
                        Label lblGrazing = (Label)e.Row.FindControl("Grazing");

                        if (lblGrazing != null)
                        {
                            if (lblGrazing.Text == "122,")
                                ddlGrazing.SelectedValue = "122,";
                            else if (lblGrazing.Text == "No")
                                ddlGrazing.SelectedValue = "No";
                        }
                        string selectedGrazing = DataBinder.Eval(e.Row.DataItem, "Grazing")?.ToString();
                    if (!string.IsNullOrEmpty(selectedGrazing))
                    {
                        ListItem selectedItem = ddlGrazing.Items.FindByText(selectedGrazing);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlIllegalLogging = (DropDownList)e.Row.FindControl("ddlIllegalLogging");
                        Label lblIllegalLogging = (Label)e.Row.FindControl("IllegalLogging");

                        if (lblIllegalLogging != null)
                        {
                            if (lblIllegalLogging.Text == "123,")
                                ddlIllegalLogging.SelectedValue = "123,";
                            else if (lblIllegalLogging.Text == "No")
                                ddlIllegalLogging.SelectedValue = "No";
                        }
                        string selectedIllegalLogging = DataBinder.Eval(e.Row.DataItem, "IllegalLogging")?.ToString();
                    if (!string.IsNullOrEmpty(selectedIllegalLogging))
                    {
                        ListItem selectedItem = ddlIllegalLogging.Items.FindByText(selectedIllegalLogging);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlMining = (DropDownList)e.Row.FindControl("ddlMining");
                        Label lblMining = (Label)e.Row.FindControl("Mining");

                        if (lblMining != null)
                        {
                            if (lblMining.Text == "124,")
                                ddlMining.SelectedValue = "124,";
                            else if (lblMining.Text == "No")
                                ddlMining.SelectedValue = "No";
                        }
                        string selectedMining = DataBinder.Eval(e.Row.DataItem, "Mining")?.ToString();
                    if (!string.IsNullOrEmpty(selectedMining))
                    {
                        ListItem selectedItem = ddlMining.Items.FindByText(selectedMining);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlPathogenicAttack = (DropDownList)e.Row.FindControl("ddlPathogenicAttack");
                        Label lblPathogenicAttack = (Label)e.Row.FindControl("PathogenicAttack");

                        if (lblPathogenicAttack != null)
                        {
                            if (lblPathogenicAttack.Text == "125,")
                                ddlPathogenicAttack.SelectedValue = "125,";
                            else if (lblPathogenicAttack.Text == "No")
                                ddlPathogenicAttack.SelectedValue = "No";
                        }
                        string selectedPathogenicAttack = DataBinder.Eval(e.Row.DataItem, "PathogenicAttack")?.ToString();
                    if (!string.IsNullOrEmpty(selectedPathogenicAttack))
                    {
                        ListItem selectedItem = ddlPathogenicAttack.Items.FindByText(selectedPathogenicAttack);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlInvasiveSpecies = (DropDownList)e.Row.FindControl("ddlInvasiveSpecies");
                        Label lblInvasiveSpecies = (Label)e.Row.FindControl("InvasiveSpecies");

                        if (lblInvasiveSpecies != null)
                        {
                            if (lblInvasiveSpecies.Text == "126,")
                                ddlInvasiveSpecies.SelectedValue = "126,";
                            else if (lblInvasiveSpecies.Text == "No")
                                ddlInvasiveSpecies.SelectedValue = "No";
                        }
                        string selectedInvasiveSpecies = DataBinder.Eval(e.Row.DataItem, "InvasiveSpecies")?.ToString();
                    if (!string.IsNullOrEmpty(selectedInvasiveSpecies))
                    {
                        ListItem selectedItem = ddlInvasiveSpecies.Items.FindByText(selectedInvasiveSpecies);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlFlood = (DropDownList)e.Row.FindControl("ddlFlood");
                        Label lblFlood = (Label)e.Row.FindControl("Flood");

                        if (lblFlood != null)
                        {
                            if (lblFlood.Text == "127,")
                                ddlFlood.SelectedValue = "127,";
                            else if (lblFlood.Text == "No")
                                ddlFlood.SelectedValue = "No";
                        }
                        string selectedFlood = DataBinder.Eval(e.Row.DataItem, "Flood")?.ToString();
                    if (!string.IsNullOrEmpty(selectedFlood))
                    {
                        ListItem selectedItem = ddlFlood.Items.FindByText(selectedFlood);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlDrought = (DropDownList)e.Row.FindControl("ddlDrought");
                        Label lblDrought = (Label)e.Row.FindControl("Drought");

                        if (lblDrought != null)
                        {
                            if (lblDrought.Text == "128,")
                                ddlDrought.SelectedValue = "128,";
                            else if (lblDrought.Text == "No")
                                ddlDrought.SelectedValue = "No";
                        }
                        string selectedDrought = DataBinder.Eval(e.Row.DataItem, "Drought")?.ToString();
                    if (!string.IsNullOrEmpty(selectedDrought))
                    {
                        ListItem selectedItem = ddlDrought.Items.FindByText(selectedDrought);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlLandslides = (DropDownList)e.Row.FindControl("ddlLandslides");
                        Label lblLandslides = (Label)e.Row.FindControl("Landslides");

                        if (lblLandslides != null)
                        {
                            if (lblLandslides.Text == "129,")
                                ddlLandslides.SelectedValue = "129,";
                            else if (lblLandslides.Text == "No")
                                ddlLandslides.SelectedValue = "No";
                        }
                        string selectedLandslides = DataBinder.Eval(e.Row.DataItem, "Landslides")?.ToString();
                    if (!string.IsNullOrEmpty(selectedLandslides))
                    {
                        ListItem selectedItem = ddlLandslides.Items.FindByText(selectedLandslides);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlAvalanche = (DropDownList)e.Row.FindControl("ddlAvalanche");
                        Label lblAvalanche = (Label)e.Row.FindControl("Avalanche");

                        if (lblAvalanche != null)
                        {
                            if (lblAvalanche.Text == "130,")
                                ddlAvalanche.SelectedValue = "130,";
                            else if (lblAvalanche.Text == "No")
                                ddlAvalanche.SelectedValue = "No";
                        }
                        string selectedAvalanche = DataBinder.Eval(e.Row.DataItem, "Avalanche")?.ToString();
                    if (!string.IsNullOrEmpty(selectedAvalanche))
                    {
                        ListItem selectedItem = ddlAvalanche.Items.FindByText(selectedAvalanche);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlStorm = (DropDownList)e.Row.FindControl("ddlStorm");
                        Label lblStorm = (Label)e.Row.FindControl("Storm");

                        if (lblStorm != null)
                        {
                            if (lblStorm.Text == "131,")
                                ddlStorm.SelectedValue = "131,";
                            else if (lblStorm.Text == "No")
                                ddlStorm.SelectedValue = "No";
                        }
                        string selectedStorm = DataBinder.Eval(e.Row.DataItem, "Storm")?.ToString();
                    if (!string.IsNullOrEmpty(selectedStorm))
                    {
                        ListItem selectedItem = ddlStorm.Items.FindByText(selectedStorm);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlCyclone = (DropDownList)e.Row.FindControl("ddlCyclone");
                        Label lblCyclone = (Label)e.Row.FindControl("Cyclone");

                        if (lblCyclone != null)
                        {
                            if (lblCyclone.Text == "132,")
                                ddlCyclone.SelectedValue = "132,";
                            else if (lblCyclone.Text == "No")
                                ddlCyclone.SelectedValue = "No";
                        }
                        string selectedCyclone = DataBinder.Eval(e.Row.DataItem, "Cyclone")?.ToString();
                    if (!string.IsNullOrEmpty(selectedCyclone))
                    {
                        ListItem selectedItem = ddlCyclone.Items.FindByText(selectedCyclone);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlHeavyRainfall = (DropDownList)e.Row.FindControl("ddlHeavyRainfall");
                        Label lblHeavyRainfall = (Label)e.Row.FindControl("HeavyRainfall");

                        if (lblHeavyRainfall != null)
                        {
                            if (lblHeavyRainfall.Text == "134,")
                                ddlHeavyRainfall.SelectedValue = "134,";
                            else if (lblHeavyRainfall.Text == "No")
                                ddlHeavyRainfall.SelectedValue = "No";
                        }
                        string selectedHeavyRainfall = DataBinder.Eval(e.Row.DataItem, "HeavySnowfall")?.ToString();
                    if (!string.IsNullOrEmpty(selectedHeavyRainfall))
                    {
                        ListItem selectedItem = ddlHeavyRainfall.Items.FindByText(selectedHeavyRainfall);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlHeavySnowfall = (DropDownList)e.Row.FindControl("ddlHeavySnowfall");
                        Label lblHeavySnowfall = (Label)e.Row.FindControl("HeavySnowfall");

                        if (lblHeavySnowfall != null)
                        {
                            if (lblHeavySnowfall.Text == "135,")
                                ddlHeavySnowfall.SelectedValue = "135,";
                            else if (lblHeavySnowfall.Text == "No")
                                ddlHeavySnowfall.SelectedValue = "No";
                        }
                        string selectedHeavySnowfall = DataBinder.Eval(e.Row.DataItem, "HeavySnowfall")?.ToString();
                    if (!string.IsNullOrEmpty(selectedHeavySnowfall))
                    {
                        ListItem selectedItem = ddlHeavySnowfall.Items.FindByText(selectedHeavySnowfall);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlOther = (DropDownList)e.Row.FindControl("ddlOther");
                        Label lblOther = (Label)e.Row.FindControl("Other");

                        if (lblOther != null)
                        {
                            if (lblOther.Text == "136,")
                                ddlOther.SelectedValue = "136,";
                            else if (lblOther.Text == "No")
                                ddlOther.SelectedValue = "No";
                        }
                        string selectedOther = DataBinder.Eval(e.Row.DataItem, "Other")?.ToString();
                    if (!string.IsNullOrEmpty(selectedOther))
                    {
                        ListItem selectedItem = ddlOther.Items.FindByText(selectedOther);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlNone = (DropDownList)e.Row.FindControl("ddlNone");
                        Label lblNone = (Label)e.Row.FindControl("None");

                        if (lblNone != null)
                        {
                            if (lblNone.Text == "137,")
                                ddlNone.SelectedValue = "137,";
                            else if (lblNone.Text == "No")
                                ddlNone.SelectedValue = "No";
                        }
                        string selectedNone = DataBinder.Eval(e.Row.DataItem, "None")?.ToString();
                    if (!string.IsNullOrEmpty(selectedNone))
                    {
                        ListItem selectedItem = ddlNone.Items.FindByText(selectedNone);
                        if (selectedItem != null)
                        {
                            selectedItem.Selected = true;
                        }
                    }


                    DropDownList ddlInjury = (DropDownList)e.Row.FindControl("ddlInjury");
                        if (ddlInjury != null)
                        {
                            string sql31 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 32";
                            string conString31 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString31))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql31, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlInjury.DataSource = dt;
                                        ddlInjury.DataTextField = "value_eng";
                                        ddlInjury.DataValueField = "value_eng";
                                        ddlInjury.DataBind();
                                        string selectedInjury = DataBinder.Eval(e.Row.DataItem, "Injury")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedInjury))
                                        {
                                            ListItem selectedItem = ddlInjury.Items.FindByText(selectedInjury);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DropDownList ddlCc_Grazing = (DropDownList)e.Row.FindControl("ddlCc_Grazing");
                        if (ddlCc_Grazing != null)
                        {

                            string sql32 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 31";
                            string conString32 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString32))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql32, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlCc_Grazing.DataSource = dt;
                                        ddlCc_Grazing.DataTextField = "value_eng";
                                        ddlCc_Grazing.DataValueField = "id";
                                        ddlCc_Grazing.DataBind();
                                        string selectedCc_Grazing = DataBinder.Eval(e.Row.DataItem, "Cc_Grazing")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedCc_Grazing))
                                        {
                                            ListItem selectedItem = ddlCc_Grazing.Items.FindByText(selectedCc_Grazing);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlRegeneration = (DropDownList)e.Row.FindControl("ddlRegeneration");
                        if (ddlRegeneration != null)
                        {

                            string sql33 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 31";
                            string conString33 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString33))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql33, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlRegeneration.DataSource = dt;
                                        ddlRegeneration.DataTextField = "value_eng";
                                        ddlRegeneration.DataValueField = "id";
                                        ddlRegeneration.DataBind();
                                        string selectedRegeneration = DataBinder.Eval(e.Row.DataItem, "Regeneration")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedRegeneration))
                                        {
                                            ListItem selectedItem = ddlRegeneration.Items.FindByText(selectedRegeneration);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlPresenceOfBamboo = (DropDownList)e.Row.FindControl("ddlPresenceOfBamboo");
                        Label lblPresenceOfBamboo = (Label)e.Row.FindControl("PresenceOfBamboo");

                        if (lblPresenceOfBamboo != null)
                        {
                            if (lblPresenceOfBamboo.Text == "1,")
                                ddlPresenceOfBamboo.SelectedValue = "1,";
                            else if (lblPresenceOfBamboo.Text == "0")
                                ddlPresenceOfBamboo.SelectedValue = "0";
                        }
                        string selectedPresenceOfBamboo = DataBinder.Eval(e.Row.DataItem, "PresenceOfBamboo")?.ToString();
                        if (!string.IsNullOrEmpty(selectedPresenceOfBamboo))
                        {
                            ListItem selectedItem = ddlPresenceOfBamboo.Items.FindByText(selectedPresenceOfBamboo);
                            if (selectedItem != null)
                            {
                                selectedItem.Selected = true;
                            }
                        }


                        DropDownList ddlBambooQuality = (DropDownList)e.Row.FindControl("ddlBambooQuality");
                        if (ddlBambooQuality != null)
                        {

                            string sql34 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 30";
                            string conString34 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString34))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql34, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlBambooQuality.DataSource = dt;
                                        ddlBambooQuality.DataTextField = "value_eng";
                                        ddlBambooQuality.DataValueField = "id";
                                        ddlBambooQuality.DataBind();
                                        string selectedBambooQuality = DataBinder.Eval(e.Row.DataItem, "BambooQuality")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedBambooQuality))
                                        {
                                            ListItem selectedItem = ddlBambooQuality.Items.FindByText(selectedBambooQuality);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlBambooRegeneration = (DropDownList)e.Row.FindControl("ddlBambooRegeneration");
                        if (ddlBambooRegeneration != null)
                        {
                            string sql35 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 30";
                            string conString35 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString35))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql35, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlBambooRegeneration.DataSource = dt;
                                        ddlBambooRegeneration.DataTextField = "value_eng";
                                        ddlBambooRegeneration.DataValueField = "id";
                                        ddlBambooRegeneration.DataBind();
                                        string selectedBambooRegeneration = DataBinder.Eval(e.Row.DataItem, "BambooRegeneration")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedBambooRegeneration))
                                        {
                                            ListItem selectedItem = ddlBambooRegeneration.Items.FindByText(selectedBambooRegeneration);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }



                        DropDownList ddlPresenceOfGrass = (DropDownList)e.Row.FindControl("ddlPresenceOfGrass");
                        if (ddlPresenceOfGrass != null)
                        {

                            string sql36 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 28";
                            string conString36 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString36))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql36, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlPresenceOfGrass.DataSource = dt;
                                        ddlPresenceOfGrass.DataTextField = "value_eng";
                                        ddlPresenceOfGrass.DataValueField = "id";
                                        ddlPresenceOfGrass.DataBind();
                                        string selectedPresenceOfGrass = DataBinder.Eval(e.Row.DataItem, "PresenceOfGrass")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedPresenceOfGrass))
                                        {
                                            ListItem selectedItem = ddlPresenceOfGrass.Items.FindByText(selectedPresenceOfGrass);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DropDownList ddlPresenceOfWeeds = (DropDownList)e.Row.FindControl("ddlPresenceOfWeeds");
                        if (ddlPresenceOfWeeds != null)
                        {

                            string sql37 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 28";
                            string conString37 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString37))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql37, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlPresenceOfWeeds.DataSource = dt;
                                        ddlPresenceOfWeeds.DataTextField = "value_eng";
                                        ddlPresenceOfWeeds.DataValueField = "id";
                                        ddlPresenceOfWeeds.DataBind();
                                        string selectedPresenceOfWeeds = DataBinder.Eval(e.Row.DataItem, "PresenceOfWeeds")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedPresenceOfWeeds))
                                        {
                                            ListItem selectedItem = ddlPresenceOfWeeds.Items.FindByText(selectedPresenceOfWeeds);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }



                        DropDownList ddlWaterBodyType = (DropDownList)e.Row.FindControl("ddlWaterBodyType");
                        if (ddlWaterBodyType != null)
                        {

                            string sql39 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 26";
                            string conString39 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString39))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql39, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlWaterBodyType.DataSource = dt;
                                        ddlWaterBodyType.DataTextField = "value_eng";
                                        ddlWaterBodyType.DataValueField = "id";
                                        ddlWaterBodyType.DataBind();
                                        string selectedWaterBodyType = DataBinder.Eval(e.Row.DataItem, "WaterBodyType")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedWaterBodyType))
                                        {
                                            ListItem selectedItem = ddlWaterBodyType.Items.FindByText(selectedWaterBodyType);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        DropDownList ddlWaterBodySeasonality = (DropDownList)e.Row.FindControl("ddlWaterBodySeasonality");
                        if (ddlWaterBodySeasonality != null)
                        {

                            string sql40 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 24";
                            string conString40 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString40))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql40, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlWaterBodySeasonality.DataSource = dt;
                                        ddlWaterBodySeasonality.DataTextField = "value_eng";
                                        ddlWaterBodySeasonality.DataValueField = "id";
                                        ddlWaterBodySeasonality.DataBind();
                                        string selectedWaterBodySeasonality = DataBinder.Eval(e.Row.DataItem, "WaterBodySeasonality")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedWaterBodySeasonality))
                                        {
                                            ListItem selectedItem = ddlWaterBodySeasonality.Items.FindByText(selectedWaterBodySeasonality);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlWaterBodyPotability = (DropDownList)e.Row.FindControl("ddlWaterBodyPotability");
                        if (ddlWaterBodyPotability != null)
                        {
                            string sql41 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 23";
                            string conString41 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString41))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql41, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlWaterBodyPotability.DataSource = dt;
                                        ddlWaterBodyPotability.DataTextField = "value_eng";
                                        ddlWaterBodyPotability.DataValueField = "id";
                                        ddlWaterBodyPotability.DataBind();
                                        string selectedWaterBodyPotability = DataBinder.Eval(e.Row.DataItem, "WaterBodyPotability")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedWaterBodyPotability))
                                        {
                                            ListItem selectedItem = ddlWaterBodyPotability.Items.FindByText(selectedWaterBodyPotability);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        DropDownList ddlWaterBodyStatus = (DropDownList)e.Row.FindControl("ddlWaterBodyStatus");
                        if (ddlWaterBodyStatus != null)
                        {

                            string sql42 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 25";
                            string conString42 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString42))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql42, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddlWaterBodyStatus.DataSource = dt;
                                        ddlWaterBodyStatus.DataTextField = "value_eng";
                                        ddlWaterBodyStatus.DataValueField = "id";
                                        ddlWaterBodyStatus.DataBind();
                                        string selectedWaterBodyStatus = DataBinder.Eval(e.Row.DataItem, "WaterBodyStatus")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedWaterBodyStatus))
                                        {
                                            ListItem selectedItem = ddlWaterBodyStatus.Items.FindByText(selectedWaterBodyStatus);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        DropDownList ddllegalStatus = (DropDownList)e.Row.FindControl("ddllegalStatus");
                        if (ddllegalStatus != null)
                        {

                            string sql43 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 18";
                            string conString43 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString43))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(sql43, con))
                                {
                                    using (DataTable dt = new DataTable())
                                    {
                                        sda.Fill(dt);
                                        ddllegalStatus.DataSource = dt;
                                        ddllegalStatus.DataTextField = "value_eng";
                                        ddllegalStatus.DataValueField = "id";
                                        ddllegalStatus.DataBind();
                                        string selectedlegalStatus = DataBinder.Eval(e.Row.DataItem, "legalStatus")?.ToString();
                                        if (!string.IsNullOrEmpty(selectedlegalStatus))
                                        {
                                            ListItem selectedItem = ddllegalStatus.Items.FindByText(selectedlegalStatus);
                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }

                }

            }
        }
    }


