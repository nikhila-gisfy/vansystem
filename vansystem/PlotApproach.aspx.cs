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
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;
using ClosedXML.Excel;
using vansystem.Models;

namespace vansystem
{
    public partial class PlotApproach : System.Web.UI.Page
    {

        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(1);

            }


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



                    using (SqlCommand cmd = new SqlCommand("Get_All_sp_plotapproach"))
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
                                    Response.AddHeader("content-disposition", "attachment;filename=PlotApproach_" + DivisionName + "_" + DateTime.Now + ".xlsx" + "");
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
        private void BindGrid(int pageIndex)
        {
            string divisionid = Session["DivisionId"].ToString();
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                    // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                    using (SqlCommand cmd = new SqlCommand("sp_plotapproach"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
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
                                GVPlotApproach.DataSource = dt;
                                GVPlotApproach.DataBind();
                            }
                            int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                            this.PopulatePager(recordCount, pageIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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


        //protected void GVPlotApproach_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GVPlotApproach.PageIndex = e.NewPageIndex;
        //    BindGrid(1);
        //}

        //protected void GVPlotApproach_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    try
        //    {
        //        GVPlotApproach.EditIndex = e.NewEditIndex;
        //        BindGrid(GVPlotApproach.PageIndex); // Rebind with the current page index
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        protected void GVPlotApproach_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVPlotApproach.DataKeys[e.RowIndex].Value.ToString());
            string name = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string phonenumber = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string mammalscomments = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[39].Controls[0]).Text;
            string birdscomments = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[41].Controls[0]).Text;

            string reptilescomments = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[43].Controls[0]).Text;
            string amphibianscomments = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[45].Controls[0]).Text;
            string fuel = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[47].Controls[0]).Text;
            string fodder = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[48].Controls[0]).Text;
            string remark = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[49].Controls[0]).Text;





            //string village = ((TextBox)GVPlotApproach.Rows[e.RowIndex].Cells[10].Controls[0]).Text;

           
            string GeneralTopography = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlGeneralTopography") as DropDownList).SelectedValue;

            string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString1))
            {
                string query = "UPDATE tblPlotApproach SET GeneralTopography = @GeneralTopography WHERE PlotApproachId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@GeneralTopography", GeneralTopography);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string slope = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlSlope") as DropDownList).SelectedValue;

            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE tblPlotApproach SET Slope = @Slope WHERE PlotApproachId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Slope", slope);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            //string mammals = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstMammals") as ListBox).SelectedValue;
            ListBox lstMammals = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstMammals") as ListBox);
            List<string> selectedMammals = new List<string>();

            foreach (ListItem item in lstMammals.Items)
            {
                if (item.Selected)
                {
                    selectedMammals.Add(item.Value);
                }
            }
            string strMammals = string.Join(", ", selectedMammals);
            string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString2))
            {
                string query = "UPDATE tblPlotApproach SET Mammals = @Mammals WHERE PlotApproachId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Mammals", strMammals);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            // string birds = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlBirds") as DropDownList).SelectedValue;
            ListBox lstBirds = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstBirds") as ListBox);
            List<string> selectedBirds = new List<string>();

            foreach (ListItem item in lstBirds.Items)
            {
                if (item.Selected)
                {
                    selectedBirds.Add(item.Value);
                }
            }
            string birds = string.Join(", ", selectedBirds);
            string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString3))
            {
                string query = "UPDATE tblPlotApproach SET Birds = @Birds WHERE PlotApproachId = @id";
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
            //string reptiles = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlReptiles") as DropDownList).SelectedValue;
            ListBox lstReptiles = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstReptiles") as ListBox);
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
                string query = "UPDATE tblPlotApproach SET Reptiles = @Reptiles WHERE PlotApproachId = @id";
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
            //string amphibians = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlAmphibians") as DropDownList).SelectedValue;
            ListBox lstAmphibians = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstAmphibians") as ListBox);
            List<string> selectedAmphibians = new List<string>();

            foreach (ListItem item in lstAmphibians.Items)
            {
                if (item.Selected)
                {
                    selectedAmphibians.Add(item.Value);
                }
            }
            string amphibians = string.Join(", ", selectedAmphibians);
            string conString5 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString5))
            {
                string query = "UPDATE tblPlotApproach SET Amphibians = @Amphibians WHERE PlotApproachId = @id";
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
            ListBox lstPlants = (GVPlotApproach.Rows[e.RowIndex].FindControl("lstPlants") as ListBox);
            List<string> selectedPlants = new List<string>();
            foreach (ListItem item in lstPlants.Items)
            {
                if (item.Selected)
                {
                    selectedAmphibians.Add(item.Value);
                }
            }
            string plants = string.Join(", ", selectedPlants);

            string conString6 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString6))
            {
                string query = "UPDATE tblPlotApproach SET Plants = @Plants WHERE PlotApproachId = @id";
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
            string CharcoalMaking = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlCharcoalMaking") as DropDownList).SelectedValue;

            string conString0 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString0))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @CharcoalMaking WHERE PlotApproachId = @id";
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

            string Earthquake = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlEarthquake") as DropDownList).SelectedValue;

            string conString7 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString7))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Earthquake WHERE PlotApproachId = @id";
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
            string DevelopmentProjects = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlDevelopmentProjects") as DropDownList).SelectedValue;
            string conString8 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString8))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @DevelopmentProjects WHERE PlotApproachId = @id";
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

            string Fire = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlFire") as DropDownList).SelectedValue;

            string conString9 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString9))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Fire WHERE PlotApproachId = @id";
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
            string FirewoodExtraction = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlFirewoodExtraction") as DropDownList).SelectedValue;
            string conString10 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString10))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @FirewoodExtraction WHERE PlotApproachId = @id";
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

            string IllegalLogging = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlIllegalLogging") as DropDownList).SelectedValue;

            string conString12 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString12))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @IllegalLogging WHERE PlotApproachId = @id";
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
            string Mining = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlMining") as DropDownList).SelectedValue;
            string conString13 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString13))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Mining WHERE PlotApproachId = @id";
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

            string Grazing = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlGrazing") as DropDownList).SelectedValue;

            string conString11 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString11))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Grazing WHERE PlotApproachId = @id";
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
            string InvasiveSpecies = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlInvasiveSpecies") as DropDownList).SelectedValue;
            string conString14 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString14))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @InvasiveSpecies WHERE PlotApproachId = @id";
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

            string PathogenicAttack = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlPathogenicAttack") as DropDownList).SelectedValue;

            string conString15 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString15))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @PathogenicAttack WHERE PlotApproachId = @id";
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
            string Drought = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlDrought") as DropDownList).SelectedValue;
            string conString17 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString17))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Drought WHERE PlotApproachId = @id";
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

            string Flood = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlFlood") as DropDownList).SelectedValue;

            string conString16 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString16))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Flood WHERE PlotApproachId = @id";
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
            string Landslides = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlLandslides") as DropDownList).SelectedValue;
            string conString18 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString18))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Landslides WHERE PlotApproachId = @id";
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

            string Avalanche = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlAvalanche") as DropDownList).SelectedValue;

            string conString19 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString19))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Avalanche WHERE PlotApproachId = @id";
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
            string Storm = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlStorm") as DropDownList).SelectedValue;
            string conString20 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString20))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Storm WHERE PlotApproachId = @id";
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

            string Cyclone = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlCyclone") as DropDownList).SelectedValue;

            string conString21 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString21))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Cyclone WHERE PlotApproachId = @id";
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
            string HeavyRainfall = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlHeavyRainfall") as DropDownList).SelectedValue;
            string conString22 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString22))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @HeavyRainfall WHERE PlotApproachId = @id";
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

            string Other = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlOther") as DropDownList).SelectedValue;

            string conString24 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString24))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @Other WHERE PlotApproachId = @id";
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
            string HeavySnowfall = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlHeavySnowfall") as DropDownList).SelectedValue;
            string conString23 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString23))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @HeavySnowfall WHERE PlotApproachId = @id";
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

            string None = (GVPlotApproach.Rows[e.RowIndex].FindControl("ddlNone") as DropDownList).SelectedValue;

            string conString25 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString25))
            {
                string query = "UPDATE tblPlotApproach SET TypeOfDegradation = @None WHERE PlotApproachId = @id";
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

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_updateplotapproach", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlotApproachId ", id);
                    cmd.Parameters.AddWithValue("@Name ", name);
                    //cmd.Parameters.AddWithValue("@TypeOfDegradation ", typeofdregradation);
                    //cmd.Parameters.AddWithValue("@GeneralTopography ", GeneralTopography);
                    //cmd.Parameters.AddWithValue("@Slope", slope);
                    //cmd.Parameters.AddWithValue("@Mammals", strMammals);
                    //cmd.Parameters.AddWithValue("@Birds", birds);
                    //cmd.Parameters.AddWithValue("@Reptiles", reptiles);
                    //cmd.Parameters.AddWithValue("@Amphibians", amphibians);
                    //cmd.Parameters.AddWithValue("@Plants", plants);
                    cmd.Parameters.AddWithValue("@Fuel", fuel);
                    cmd.Parameters.AddWithValue("@Fodder", fodder);
                    cmd.Parameters.AddWithValue("@remark", remark);
                    //cmd.Parameters.AddWithValue("@Formname", formname);
                    //cmd.Parameters.AddWithValue("@device_id", deviceid);
                    //cmd.Parameters.AddWithValue("@Form_id", formid);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                    //cmd.Parameters.AddWithValue("@Version", version);
                    //cmd.Parameters.AddWithValue("@Synced", synced);
                    //cmd.Parameters.AddWithValue("@Sync_date", syncdate);
                    cmd.Parameters.AddWithValue("@Birds_comments", birdscomments);
                    cmd.Parameters.AddWithValue("@Mammals_comments", mammalscomments);
                    cmd.Parameters.AddWithValue("@Reptiles_comments", reptilescomments);
                    cmd.Parameters.AddWithValue("@Amphibians_comments", amphibianscomments);
                    //cmd.Parameters.AddWithValue("@Form_sync_date", formsyncdate);
                    //cmd.Parameters.AddWithValue("@TRIAL427", trial427);
                    //cmd.Parameters.AddWithValue("@CharcoalMaking ", CharcoalMaking);
                    //cmd.Parameters.AddWithValue("@Earthquake ", Earthquake);
                    //cmd.Parameters.AddWithValue("@DevelopmentProjects ", DevelopmentProjects);
                    //cmd.Parameters.AddWithValue("@Fire ", Fire);
                    //cmd.Parameters.AddWithValue("@FirewoodExtraction ", FirewoodExtraction);
                    //cmd.Parameters.AddWithValue("@IllegalLogging ", IllegalLogging);
                    //cmd.Parameters.AddWithValue("@Mining", Mining);
                    //cmd.Parameters.AddWithValue("@Grazing", Grazing);
                    //cmd.Parameters.AddWithValue("@InvasiveSpecies", InvasiveSpecies);
                    //cmd.Parameters.AddWithValue("@PathogenicAttack", PathogenicAttack);
                    //cmd.Parameters.AddWithValue("@Drought", Drought);
                    //cmd.Parameters.AddWithValue("@Flood", Flood);
                    //cmd.Parameters.AddWithValue("@Landslides", Landslides);
                    //cmd.Parameters.AddWithValue("@Avalanche", Avalanche);
                    //cmd.Parameters.AddWithValue("@Storm", Storm);
                    //cmd.Parameters.AddWithValue("@Cyclone", Cyclone);
                    //cmd.Parameters.AddWithValue("@HeavyRainfall", HeavyRainfall);
                    //cmd.Parameters.AddWithValue("@Other", Other);
                    //cmd.Parameters.AddWithValue("@None", None);
                    //cmd.Parameters.AddWithValue("@HeavySnowfall", HeavySnowfall);
                    //cmd.Parameters.AddWithValue("@village", village);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVPlotApproach.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();

                }
            }


        }

        protected void GVPlotApproach_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVPlotApproach.EditIndex = -1;
            BindGrid(1);
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
            string FileName = "PlotApproach" + DateTime.Now + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.ContentType = "File/Data.xls";
            System.IO.StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

            GVPlotApproach.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();


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
                GVPlotApproach.AllowPaging = false;
                this.BindGrid(1);

                GVPlotApproach.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GVPlotApproach.HeaderRow.Cells)
                {
                    cell.BackColor = GVPlotApproach.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GVPlotApproach.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GVPlotApproach.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GVPlotApproach.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GVPlotApproach.RenderControl(hw);

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

                        using (SqlCommand cmd = new SqlCommand("Get_AllData_sp_plotapproach"))
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
                                    GVPlotApproach.DataSource = dt;
                                    GVPlotApproach.DataBind();

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

        protected void GVPlotApproach_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVPlotApproach.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {


                    DropDownList ddlSlope = (DropDownList)e.Row.FindControl("ddlSlope");
                    if (ddlSlope != null)
                    {
                        string sql = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 37";
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
                    DropDownList ddlGeneralTopography = (DropDownList)e.Row.FindControl("ddlGeneralTopography");
                    string sql1 = "SELECT TOP (1000) [id],[value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] where attr_id = 42";
                    string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql1, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlGeneralTopography.DataSource = dt;
                                ddlGeneralTopography.DataTextField = "value_eng";
                                ddlGeneralTopography.DataValueField = "id";
                                ddlGeneralTopography.DataBind();
                             
                                string selectedGeneralTopography = DataBinder.Eval(e.Row.DataItem, "GeneralTopography")?.ToString();
                                if (!string.IsNullOrEmpty(selectedGeneralTopography))
                                {
                                    ListItem selectedItem = ddlGeneralTopography.Items.FindByText(selectedGeneralTopography);
                                    if (selectedItem != null)
                                    {
                                        selectedItem.Selected = true;
                                    }
                                }
                            }
                        }
                    }


                    ListBox lstMammals = (ListBox)e.Row.FindControl("lstMammals");
                    string sql2 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=10";
                    string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString2))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
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
                    string sql3 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=9";
                    string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString3))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql3, con))
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
                    //DropDownList ddlReptiles = (DropDownList)e.Row.FindControl("ddlReptiles");
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
                    string sql5 = "SELECT [SpeciesId],[habitid],[sc_name],[local_name] FROM [VanIT].[dbo].[tblspecies] where habitid=8";
                    string conString5 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString5))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql5, con))
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
                    DropDownList ddlCharcoalMaking = (DropDownList)e.Row.FindControl("ddlCharcoalMaking");
                    Label lblCharcoalMaking = (Label)e.Row.FindControl("CharcoalMaking");

                    if (lblCharcoalMaking != null)
                    {
                        if (lblCharcoalMaking.Text == "118,")
                            ddlCharcoalMaking.SelectedValue = "118,";
                        else if (lblCharcoalMaking.Text == "NO")
                            ddlCharcoalMaking.SelectedValue = "NO";
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
                }
            }
        }

        protected void GVPlotApproach_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int pageIndex = int.Parse(((GridView)sender).Rows[e.NewEditIndex].Cells[0].Text); // Assuming the pageIndex is in the first column
            //GVPlotApproach.EditIndex = e.NewEditIndex;
            BindGrid(pageIndex); // Rebind with the current page index
        }

        protected void GVPlotApproach_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVPlotApproach.PageIndex = e.NewPageIndex;
            BindGrid(e.NewPageIndex); // Rebind with the new page index
        }
    }
}
