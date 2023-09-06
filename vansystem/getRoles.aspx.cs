using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class getRoles : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid(1);

            }
        }
        private void BindGrid(int pageIndex)
        {
            //string divisionid = Session["DivisionId"].ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                    // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                    using (SqlCommand cmd = new SqlCommand("sp_getroles"))
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
                            
                            sda.SelectCommand = cmd;
                            //cmd.CommandTimeout = 120;



                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GVgetroles.DataSource = dt;
                                GVgetroles.DataBind();
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
    

        protected void GVgetroles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVgetroles.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void GVgetroles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVgetroles.DataKeys[e.RowIndex].Value.ToString());
            string name = ((TextBox)GVgetroles.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string description = ((TextBox)GVgetroles.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_updateroles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@operation","update");
                    cmd.Parameters.AddWithValue("@roleId", id);
                    cmd.Parameters.AddWithValue("@roleName", name);
                    cmd.Parameters.AddWithValue("@description ", description);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        GVgetroles.EditIndex = -1;
                        BindGrid(1);

                    }
                    con.Close();
                }
            }
        }

        protected void GVgetroles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVgetroles.EditIndex = -1;
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
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid(1);
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
        }
    }
}