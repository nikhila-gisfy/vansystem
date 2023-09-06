using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;

namespace vansystem
{
    public partial class PlotEnumeration : System.Web.UI.Page
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
            // string constr = ConfigurationManager.ConnectionStrings["ConnStringStr1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_plotenumeration"))
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
                            GVPlotEnumeration.DataSource = dt;
                            GVPlotEnumeration.DataBind();
                        }
                        int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
                    }
                }
            }
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





        protected void Tree_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            string form_id = "";
            if (e.CommandArgument != null)

            {

                //Response.Redirect("Tree.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
                Response.Redirect("Tree.aspx?PlotEnumerationId=" + PlotEnumerationId.ToString() + "&form_id=" + form_id.ToString());

            }



        }

        protected void Shrub_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Shurb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Bamboo_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Bamboo.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Sapling_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Sapling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }

        }

        protected void Herb_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Herb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Seedling_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Seedling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Grass_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Grass.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }

        }

        protected void Climber_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Climber.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Leaflitter_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Leaflitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }

        }

        protected void Woodylitter_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Woodylitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Soil_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("Soil.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Deadwood_link(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)

            {

                Response.Redirect("DeadWood.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }





        protected void masterdata_Command(object sender, CommandEventArgs e)
        {
           
            if (e.CommandArgument != null)

            {

                Response.Redirect("PlotEnumeration.aspx");
            }
        }

        protected void Tree_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandArgument != null)

            {

                Response.Redirect("Treefull.aspx");
            }
        }

        protected void Bamboo_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandArgument != null)

            {

                Response.Redirect("Bamboo_full.aspx");
            }
        }

        protected void Shurb_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandArgument != null)

            {

                Response.Redirect("Shrubfull.aspx");
            }
        }

        protected void Sapling_Command(object sender, CommandEventArgs e)
        {


            if (e.CommandArgument != null)
            {

                Response.Redirect("Saplingfull.aspx");
            }
        }

        protected void Herb_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandArgument != null)
            {

                Response.Redirect("Herbfull.aspx");
            }
        }

        protected void Seedling_Command(object sender, CommandEventArgs e)
        {


            if (e.CommandArgument != null)
            {

                Response.Redirect("Seedlingfull.aspx");
            }
        }

        protected void Grass_Command(object sender, CommandEventArgs e)
        {


            if (e.CommandArgument != null)
            {

                Response.Redirect("Grassfull.aspx");
            }
        }

        protected void Climber_Command(object sender, CommandEventArgs e)
        {


            if (e.CommandArgument != null)
            {

                Response.Redirect("Climberfull.aspx");
            }
        }

        protected void LeafLitter_Command(object sender, CommandEventArgs e)
        {
          
            if (e.CommandArgument != null)
            {

                Response.Redirect("Leaflitter_full.aspx");
            }
        }

        protected void WoodyLitter_Command(object sender, CommandEventArgs e)
        {
          
            if (e.CommandArgument != null)
            {

                Response.Redirect("WoodyLitter_Full.aspx");
            }
        }

        protected void Soil_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Soil_full.aspx");
            }
        }

        protected void DeadWood_Command(object sender, CommandEventArgs e)
        {
           
            if (e.CommandArgument != null)
            {

                Response.Redirect("DeadWood_full.aspx");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid(1);
        }
        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
        }
        protected void GVPlotEnumeration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVPlotEnumeration.PageIndex = e.NewPageIndex;
            BindGrid(1);
        }
        private void GetDataForExceldrydata()
        {
            string divisionid = Session["DivisionId"].ToString();
            string DivisionName = Session["name"].ToString();
            try
            {
                using (SqlConnection con1 = new SqlConnection(constr))
                {
                    //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                    // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                    using (SqlCommand cmd1 = new SqlCommand("get_all_plotenumeration_dry_data"))
                    {
                        using (SqlDataAdapter sda1 = new SqlDataAdapter())
                        {
                            cmd1.Connection = con1;
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandTimeout = 0;
                            cmd1.Parameters.AddWithValue("@divisionid", divisionid);
                            sda1.SelectCommand = cmd1;
                            cmd1.CommandTimeout = 3600;

                            using (DataTable dt1 = new DataTable())
                            {
                                sda1.Fill(dt1);
                                using (XLWorkbook wb1 = new XLWorkbook())
                                {
                                    wb1.Worksheets.Add(dt1, "Customers");

                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=PlotEnumeration_drydata" + DivisionName + "_" + DateTime.Now + ".xlsx" + "");
                                    // Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                                    using (MemoryStream MyMemoryStream1 = new MemoryStream())
                                    {
                                        wb1.SaveAs(MyMemoryStream1);
                                        MyMemoryStream1.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
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



                    using (SqlCommand cmd = new SqlCommand("Get_All_plotenumeration"))
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
                                    Response.AddHeader("content-disposition", "attachment;filename=PlotEnumeration_" + DivisionName + "_" + DateTime.Now + ".xlsx" + "");
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
     
        protected void btnExport_Click(object sender, EventArgs e)
        {
            GetDataForExcel();
        }

        protected void btnExport1_Click(object sender, EventArgs e)
        {
            GetDataForExceldrydata();
        }
    }

}
