﻿using System;
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
    public partial class Saplingfull : System.Web.UI.Page
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
            string divisionid = Session["DivisionId"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_saplingfull"))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "selectsapling");
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
                            GVSapling.DataSource = dt;
                            GVSapling.DataBind();
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
        protected void GVSapling_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVSapling.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }
        protected void GVSapling_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            GridViewRow row = GVSapling.Rows[e.RowIndex];
            int id = Convert.ToInt32(GVSapling.DataKeys[e.RowIndex].Values[0]);
            //string bamboonumber = Convert.ToInt32(GVBamboo.DataKeys[e.RowIndex].Value.ToString());


            //string saplingdatadirection = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string saplingdatadirection = (GVSapling.Rows[e.RowIndex].FindControl("ddlsaplingdatadirection") as DropDownList).SelectedValue;
            // string conString = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE static_sapling SET saplingdatadirection = @saplingdatadirection WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@saplingdatadirection", saplingdatadirection);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            // string saplinglocalname = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string saplinglocalname = (GVSapling.Rows[e.RowIndex].FindControl("ddlLocal_Name") as DropDownList).SelectedValue;


            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE static_sapling SET saplinglocalname = @saplinglocalname WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@saplinglocalname", saplinglocalname);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string saplingnum = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string collardiameter = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string height = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            // string presenceofgrowth = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string presenceofgrowth = (GVSapling.Rows[e.RowIndex].FindControl("ddlpresenceofgrowth") as DropDownList).SelectedValue;


            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE static_sapling SET presenceofgrowth = @presenceofgrowth WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@presenceofgrowth", presenceofgrowth);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            string remark = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            // string saplingtype = ((TextBox)GVSapling.Rows[e.RowIndex].Cells[8].Controls[0]).Text;



            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_saplingfull", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", "updatebamboo");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.Parameters.AddWithValue("@saplingnum", saplingnum);
                    cmd.Parameters.AddWithValue("@saplingdatadirection", saplingdatadirection);
                    cmd.Parameters.AddWithValue("@saplinglocalname", saplinglocalname);
                    cmd.Parameters.AddWithValue("@collardiameter", collardiameter);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@presenceofgrowth", presenceofgrowth);
                    cmd.Parameters.AddWithValue("@remark", remark);
                    //  cmd.Parameters.AddWithValue("@saplingtype", saplingtype);





                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVSapling.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();

                }
            }
        }
        protected void GVSapling_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVSapling.EditIndex = -1;
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
        protected void GVSapling_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVSapling.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlsaplingdatadirection = (DropDownList)e.Row.FindControl("ddlsaplingdatadirection");
                    string sql2 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 10";
                    string conString1 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlsaplingdatadirection.DataSource = dt;
                                ddlsaplingdatadirection.DataTextField = "value_eng";
                                ddlsaplingdatadirection.DataValueField = "id";
                                ddlsaplingdatadirection.DataBind();
                                string selecteddirection_names = DataBinder.Eval(e.Row.DataItem, "direction_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlsaplingdatadirection.Items)
                                {
                                    item.Selected = selecteddirection_names.Contains(item.Text);
                                }
                            }
                        }
                    }

                    DropDownList ddlLocal_Name = (DropDownList)e.Row.FindControl("ddlLocal_Name");
                    string sql = "SELECT SpeciesId, sc_name + ' - ' + local_name AS Local_Name FROM [VanIT].[dbo].[tblspecies] where habitid =7"; //habitid =3
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

                    DropDownList ddlpresenceofgrowth = (DropDownList)e.Row.FindControl("ddlpresenceofgrowth");
                    string sql3 = "SELECT id, [value_eng] FROM [VanIT].[dbo].[poss_val_lang_relations] WHERE [attr_id] = 30";
                    string conString3 = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString3))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(sql3, con))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                ddlpresenceofgrowth.DataSource = dt;
                                ddlpresenceofgrowth.DataTextField = "value_eng";
                                ddlpresenceofgrowth.DataValueField = "id";
                                ddlpresenceofgrowth.DataBind();
                                string selectedpresenceofgrowth = DataBinder.Eval(e.Row.DataItem, "Growth_names").ToString();
                                //ddlMammals.Items.FindByText(selectedMammals).Selected = true;
                                foreach (ListItem item in ddlpresenceofgrowth.Items)
                                {
                                    item.Selected = selectedpresenceofgrowth.Contains(item.Text);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}