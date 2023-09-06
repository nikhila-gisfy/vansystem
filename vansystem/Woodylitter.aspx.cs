using Antlr.Runtime.Tree;
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
    public partial class Woodylitter : System.Web.UI.Page
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
            /*where [PlotEnumerationId]='"+ id+" '*/
            string plotenumid = Session["plotenumid"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_woodylitter"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectwoody");
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.AddWithValue("@plotenumid", plotenumid);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVWoodylitter.DataSource = dt;
                            GVWoodylitter.DataBind();
                        }
                        int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
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

        protected void GVWoodylitter_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GVWoodylitter.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVWoodylitter_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVWoodylitter.DataKeys[e.RowIndex].Value.ToString());
            string direction = ((TextBox)GVWoodylitter.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string woody_litter_weight_in_grams = ((TextBox)GVWoodylitter.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string sub_sample_weight = ((TextBox)GVWoodylitter.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string sample_number = ((TextBox)GVWoodylitter.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string remarks = ((TextBox)GVWoodylitter.Rows[e.RowIndex].Cells[5].Controls[0]).Text;


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_woodylitter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updatewoody");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@direction ", direction);
                    cmd.Parameters.AddWithValue("@woody_litter_weight_in_grams", woody_litter_weight_in_grams);
                    cmd.Parameters.AddWithValue("@sub_sample_weight", sub_sample_weight);
                    cmd.Parameters.AddWithValue("@sample_number", sample_number);
                    cmd.Parameters.AddWithValue("@remarks", remarks);




                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVWoodylitter.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();
                }
            }
        }

        protected void GVWoodylitter_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GVWoodylitter.EditIndex = -1;
            BindGrid(1);
        }

        protected void masterdata_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)

            {

                Response.Redirect("PlotEnumeration.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Tree_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)

            {

                Response.Redirect("Tree.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Bamboo_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)

            {

                Response.Redirect("Bamboo.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Shurb_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)

            {

                Response.Redirect("Shurb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Sapling_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Sapling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Herb_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Herb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Seedling_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Seedling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Grass_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Grass.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Climber_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Climber.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void LeafLitter_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Leaflitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void WoodyLitter_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Woodylitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void Soil_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("Soil.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }

        protected void DeadWood_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                Response.Redirect("DeadWood.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());
            }
        }
    }
}