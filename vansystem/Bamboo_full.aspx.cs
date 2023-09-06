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
    public partial class Bamboo_full : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("sp_bamboo_full"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectbamboo");
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVBamboo.DataSource = dt;
                            GVBamboo.DataBind();
                        }

                        var recordCount = (cmd.Parameters["@RecordCount"].Value);
                        if (!(recordCount is DBNull))
                            recordCount = Convert.ToInt32(recordCount);


                    }
                }
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid(1);
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

        protected void lnkPage_Click(object sender, EventArgs e)
        {

            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
        }

        protected void GVBamboo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVBamboo.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVBamboo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVBamboo.EditIndex = -1;
            BindGrid(1);
        }

        protected void GVBamboo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GVBamboo.Rows[e.RowIndex];
            int id = Convert.ToInt32(GVBamboo.DataKeys[e.RowIndex].Values[0]);
            //string bamboonumber = Convert.ToInt32(GVBamboo.DataKeys[e.RowIndex].Value.ToString());
            //string bambooname = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string bambooname = (GVBamboo.Rows[e.RowIndex].FindControl("ddlLocal_Name") as DropDownList).SelectedValue;

            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_bamboo] SET bambooname = @bambooname WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@bambooname", bambooname);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string bamboonumber = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string bamboodiameter = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string bambooculms = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string bamboogbh = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string bambooheight = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string bambooremarks = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            //string bamboonumber = ((TextBox)GVBamboo.Rows[e.RowIndex].Cells[7].Controls[0]).Text;


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_bamboo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updatebamboo");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@bambooname", bambooname);
                    cmd.Parameters.AddWithValue("@bamboonumber", bamboonumber);
                    cmd.Parameters.AddWithValue("@bamboodiameter", bamboodiameter);
                    cmd.Parameters.AddWithValue("@bambooculms", bambooculms);
                    cmd.Parameters.AddWithValue("@bamboogbh", bamboogbh);
                    cmd.Parameters.AddWithValue("@bambooheight", bambooheight);
                    cmd.Parameters.AddWithValue("@bambooremarks", bambooremarks);
                    //cmd.Parameters.AddWithValue("@bamboonumber", bamboonumber);




                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVBamboo.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();

                }
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
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)
            {

                Response.Redirect("Leaflitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void WoodyLitter_Command(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)
            {

                Response.Redirect("Woodylitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Soil_Command(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)
            {

                Response.Redirect("Soil.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void DeadWood_Command(object sender, CommandEventArgs e)
        {
            int PlotEnumerationId = Convert.ToInt32(e.CommandArgument);
            Session["plotenumid"] = PlotEnumerationId;
            if (e.CommandArgument != null)
            {

                Response.Redirect("DeadWood.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }
        protected void GVBamboo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVBamboo.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlLocal_Name = (DropDownList)e.Row.FindControl("ddlLocal_Name");
                    string sql = "SELECT SpeciesId, sc_name + ' - ' + local_name AS Local_Name FROM [VanIT].[dbo].[tblspecies] where habitid =6";
                    string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlLocal_Name.DataSource = dt;
                                ddlLocal_Name.DataTextField = "Local_Name";
                                ddlLocal_Name.DataValueField = "SpeciesId";
                                ddlLocal_Name.DataBind();
                                string selectedLocal_Name = DataBinder.Eval(e.Row.DataItem, "Local_Name").ToString();
                                ddlLocal_Name.Items.FindByText(selectedLocal_Name).Selected = true;
                            }
                        }
                    }

                }
            }
        }
    }
}
