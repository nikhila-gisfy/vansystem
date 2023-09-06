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
using vansystem.Models;

namespace vansystem
{
    public partial class Herb : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("sp_herb"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectherb");
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
                            GVHerb.DataSource = dt;
                            GVHerb.DataBind();
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

        protected void GVHerb_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVHerb.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVHerb_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            GridViewRow row = GVHerb.Rows[e.RowIndex];
            int id = Convert.ToInt32(GVHerb.DataKeys[e.RowIndex].Values[0]);
            //string bamboonumber = Convert.ToInt32(GVBamboo.DataKeys[e.RowIndex].Value.ToString());


            //string herbdatadirection = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string herbdatadirection = (GVHerb.Rows[e.RowIndex].FindControl("ddlherbdatadirection") as DropDownList).SelectedValue;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_herb] SET herbdatadirection = @herbdatadirection WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@herbdatadirection", herbdatadirection);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            // string herblocalname = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string herblocalname = (GVHerb.Rows[e.RowIndex].FindControl("ddlLocal_Name") as DropDownList).SelectedValue;


            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE [static_herb] SET herblocalname = @herblocalname WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@herblocalname", herblocalname);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string herbnum = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string averageherbs = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string height = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            //string ntfp = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            ListBox lstntfp = (GVHerb.Rows[e.RowIndex].FindControl("lstntfp") as ListBox);
            List<string> selectedntfp = new List<string>();

            foreach (ListItem item in lstntfp.Items)
            {
                if (item.Selected)
                {
                    selectedntfp.Add(item.Value);
                }
            }
            string ntfp = string.Join(", ", selectedntfp);
            //string conString2 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE static_herb SET ntfp = @ntfp WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ntfp", ntfp);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string remark = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            string presenceofgrowth = ((TextBox)GVHerb.Rows[e.RowIndex].Cells[8].Controls[0]).Text;



            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_herb", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updateherb");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.Parameters.AddWithValue("@herbnum", herbnum);
                    cmd.Parameters.AddWithValue("@herbdatadirection", herbdatadirection);
                    cmd.Parameters.AddWithValue("@herblocalname", herblocalname);
                    cmd.Parameters.AddWithValue("@averageherbs", averageherbs);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@ntfp", ntfp);
                    cmd.Parameters.AddWithValue("@remark", remark);
                    cmd.Parameters.AddWithValue("@presenceofgrowth", presenceofgrowth);





                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVHerb.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();

                }
            }
        }

        protected void GVHerb_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVHerb.EditIndex = -1;
            BindGrid(1);
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
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

        protected void masterdata_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("PlotEnumeration.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Tree_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Tree.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Bamboo_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Bamboo.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Shurb_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Shurb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Sapling_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Sapling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Herb_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Herb.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Seedling_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Seedling.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Grass_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Grass.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Climber_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Climber.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void LeafLitter_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Leaflitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void WoodyLitter_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Woodylitter.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void Soil_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("Soil.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }        protected void DeadWood_Command(object sender, CommandEventArgs e)        {            if (e.CommandArgument != null)            {                Response.Redirect("DeadWood.aspx?PlotEnumerationId=" + e.CommandArgument.ToString());            }        }
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

        protected void GVHerb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVHerb.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlherbdatadirection = (DropDownList)e.Row.FindControl("ddlherbdatadirection");
                    string sql2 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 10";
                    string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlherbdatadirection.DataSource = dt;
                                ddlherbdatadirection.DataTextField = "value_eng";
                                ddlherbdatadirection.DataValueField = "id";
                                ddlherbdatadirection.DataBind();
                                string selectedherb = DataBinder.Eval(e.Row.DataItem, "direction_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlherbdatadirection.Items)
                                {
                                    item.Selected = selectedherb.Contains(item.Text);
                                }
                            }
                        }
                    }

                    DropDownList ddlLocal_Name = (DropDownList)e.Row.FindControl("ddlLocal_Name");
                    string sql = "SELECT SpeciesId, sc_name + ' - ' + local_name AS Local_Name FROM [VanIT].[dbo].[tblspecies] where habitid =1";
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


                    ListBox lstntfp = (ListBox)e.Row.FindControl("lstntfp");
                    string sql3 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 4";
                    string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString3))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql3, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                lstntfp.DataSource = dt;
                                lstntfp.DataTextField = "value_eng";
                                lstntfp.DataValueField = "id";
                                lstntfp.DataBind();
                                string selectedntfp = DataBinder.Eval(e.Row.DataItem, "ntfp_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in lstntfp.Items)
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