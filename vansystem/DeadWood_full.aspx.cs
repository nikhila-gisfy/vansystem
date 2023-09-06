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
    public partial class DeadWood_full : System.Web.UI.Page
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
            string divisionid = Session["DivisionId"].ToString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deadwood_full"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectdeadwood");
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
                            GVDeadWood.DataSource = dt;
                            GVDeadWood.DataBind();
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

        protected void GVDeadWood_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVDeadWood.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVDeadWood_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVDeadWood.DataKeys[e.RowIndex].Value.ToString());
            //string climberdatadirection = ((TextBox)GVClimber.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string direction = (GVDeadWood.Rows[e.RowIndex].FindControl("ddldirection") as DropDownList).SelectedValue;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "UPDATE [static_dead_wood] SET direction = @direction WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@direction", direction);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            //int id = Convert.ToInt32(GVDeadWood.DataKeys[e.RowIndex].Value.ToString());
            //string direction = ((TextBox)GVDeadWood.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string dead_wood_weight_in_grams = ((TextBox)GVDeadWood.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string sub_sample_weight = ((TextBox)GVDeadWood.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string sample_plot_number = ((TextBox)GVDeadWood.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string remarks = ((TextBox)GVDeadWood.Rows[e.RowIndex].Cells[5].Controls[0]).Text;


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deadwood_full", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updatedeadwood");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@direction ", direction);
                    cmd.Parameters.AddWithValue("@dead_wood_weight_in_grams", dead_wood_weight_in_grams);
                    cmd.Parameters.AddWithValue("@sub_sample_weight", sub_sample_weight);
                    cmd.Parameters.AddWithValue("@sample_plot_number", sample_plot_number);
                    cmd.Parameters.AddWithValue("@remarks", remarks);




                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVDeadWood.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();
                }
            }
        }
        protected void GVDeadWood_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GVDeadWood.EditIndex = -1;
            BindGrid(1);
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
        protected void GVDeadWood_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVDeadWood.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {


                    DropDownList ddldirection = (DropDownList)e.Row.FindControl("ddldirection");
                    string sql2 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 10";
                    // string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddldirection.DataSource = dt;
                                ddldirection.DataTextField = "value_eng";
                                ddldirection.DataValueField = "id";
                                ddldirection.DataBind();
                                string selectedntfp = DataBinder.Eval(e.Row.DataItem, "direction_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddldirection.Items)
                                {
                                    item.Selected = selectedntfp.Contains(item.Text);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}