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
    public partial class Shrubfull : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(1);

            }
        }
        int divisionid = 148;
        private void BindGrid(int pageIndex)
        {
            /*where [PlotEnumerationId]='"+ id+" '*/
            string plotenumid = Session["divisionid"].ToString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_shurbfull"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectshurb");
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
                            GVShurb.DataSource = dt;
                            GVShurb.DataBind();
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

        protected void GVShurb_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVShurb.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }
        protected void GVShurb_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            int id = Convert.ToInt32(GVShurb.DataKeys[e.RowIndex].Value.ToString());

            string shrubdatadirection = (GVShurb.Rows[e.RowIndex].FindControl("ddlshrubdatadirection") as DropDownList).SelectedValue;
            // string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_shrub] SET shrubdatadirection = @shrubdatadirection WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@shrubdatadirection", shrubdatadirection);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            //string shrublocalname = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string shrublocalname = (GVShurb.Rows[e.RowIndex].FindControl("ddlLocal_Name") as DropDownList).SelectedValue;


            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_shrub] SET shrublocalname = @shrublocalname WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@shrublocalname", shrublocalname);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string shrubnum = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string girth = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string height = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string ntfp = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string approx = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            string presenceofgrowth = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
            string remark = ((TextBox)GVShurb.Rows[e.RowIndex].Cells[9].Controls[0]).Text;



            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_shurb", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updateshurb");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@shrubnum ", shrubnum);
                    cmd.Parameters.AddWithValue("@shrubdatadirection", shrubdatadirection);
                    cmd.Parameters.AddWithValue("@shrublocalname", shrublocalname);
                    cmd.Parameters.AddWithValue("@girth", girth);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@ntfp", ntfp);
                    cmd.Parameters.AddWithValue("@approx", approx);
                    cmd.Parameters.AddWithValue("@presenceofgrowth", presenceofgrowth);
                    cmd.Parameters.AddWithValue("@remark", remark);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVShurb.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();
                }
            }
        }
        protected void GVShurb_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GVShurb.EditIndex = -1;
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

        protected void GVShurb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVShurb.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {


                    DropDownList ddlshrubdatadirection = (DropDownList)e.Row.FindControl("ddlshrubdatadirection");
                    string sql2 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 10";
                    string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlshrubdatadirection.DataSource = dt;
                                ddlshrubdatadirection.DataTextField = "value_eng";
                                ddlshrubdatadirection.DataValueField = "id";
                                ddlshrubdatadirection.DataBind();
                                string selectedntfp = DataBinder.Eval(e.Row.DataItem, "direction_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlshrubdatadirection.Items)
                                {
                                    item.Selected = selectedntfp.Contains(item.Text);
                                }
                            }
                        }
                    }


                    DropDownList ddlLocal_Name = (DropDownList)e.Row.FindControl("ddlLocal_Name");
                    string sql = "SELECT SpeciesId, sc_name + ' - ' + local_name AS Local_Name FROM [VanIT].[dbo].[tblspecies] where habitid =2";
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
                                foreach (ListItem item in ddlLocal_Name.Items)
                                {
                                    if (item.Text == selectedLocal_Name)
                                    {
                                        item.Selected = true;
                                        break;
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