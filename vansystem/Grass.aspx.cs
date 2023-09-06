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
    public partial class Grass : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("sp_grass"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectgrass");
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
                            GVGrass.DataSource = dt;
                            GVGrass.DataBind();
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

        protected void GVGrass_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVGrass.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVGrass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            int id = Convert.ToInt32(GVGrass.DataKeys[e.RowIndex].Value.ToString());

            //string grassdatadirection = ((TextBox)GVGrass.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            string grassdatadirection = (GVGrass.Rows[e.RowIndex].FindControl("ddlgrassdatadirection") as DropDownList).SelectedValue;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_grass] SET grassdatadirection = @grassdatadirection WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@grassdatadirection", grassdatadirection);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }


            // string grasslocalname = ((TextBox)GVGrass.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string grasslocalname = (GVGrass.Rows[e.RowIndex].FindControl("ddlLocal_Name") as DropDownList).SelectedValue;


            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_grass] SET grasslocalname = @grasslocalname WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@grasslocalname", grasslocalname);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }


            string grassnum = ((TextBox)GVGrass.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            //string grassdensity = ((TextBox)GVGrass.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string grassdensity = (GVGrass.Rows[e.RowIndex].FindControl("ddlgrassdensity") as DropDownList).SelectedValue;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_grass] SET grassdensity = @grassdensity WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@grassdensity", grassdensity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }



            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_grass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updategrass");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@grassnum ", grassnum);
                    cmd.Parameters.AddWithValue("@grassdensity", grassdensity);
                    cmd.Parameters.AddWithValue("@grasslocalname", grasslocalname);
                    cmd.Parameters.AddWithValue("@grassdatadirection", grassdatadirection);





                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVGrass.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();
                }
            }
        }

        protected void GVGrass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVGrass.EditIndex = -1;
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

        protected void GVGrass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVGrass.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {


                    DropDownList ddlgrassdatadirection = (DropDownList)e.Row.FindControl("ddlgrassdatadirection");
                    string sql2 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 10";
                    string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlgrassdatadirection.DataSource = dt;
                                ddlgrassdatadirection.DataTextField = "value_eng";
                                ddlgrassdatadirection.DataValueField = "id";
                                ddlgrassdatadirection.DataBind();
                                string selectedntfp = DataBinder.Eval(e.Row.DataItem, "direction_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlgrassdatadirection.Items)
                                {
                                    item.Selected = selectedntfp.Contains(item.Text);
                                }
                            }
                        }
                    }


                    DropDownList ddlLocal_Name = (DropDownList)e.Row.FindControl("ddlLocal_Name");
                    string sql = "SELECT SpeciesId, sc_name + ' - ' + local_name AS Local_Name FROM [VanIT].[dbo].[tblspecies] where habitid =13";
                    string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString2))
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

                    DropDownList ddlgrassdensity = (DropDownList)e.Row.FindControl("ddlgrassdensity");
                    string sql3 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 293";
                    string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString3))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql3, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlgrassdensity.DataSource = dt;
                                ddlgrassdensity.DataTextField = "value_eng";
                                ddlgrassdensity.DataValueField = "id";
                                ddlgrassdensity.DataBind();
                                string selectedntfp = DataBinder.Eval(e.Row.DataItem, "grassdensity_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlgrassdensity.Items)
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