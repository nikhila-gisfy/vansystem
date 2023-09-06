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
    public partial class TimberExtraction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(1);
            }
        }

        private void BindGrid(int pageIndex)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("TimberExtraction_SELECT"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvActivity.DataSource = dt;
                            gvActivity.DataBind();
                        }
                        int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SpTimberInsert"; // Store procediure name
                cmd.Parameters.AddWithValue("Year", Convert.ToInt32(txtYear.Text));
                cmd.Parameters.AddWithValue("WoodQuality", txtwoodTQ.Text.ToString());
                cmd.Parameters.AddWithValue("Extraction", Convert.ToInt32(txtWoodExtraction.Text));
                cmd.Parameters.AddWithValue("SmallWoodTypeQuality", txtPoles.Text.ToString());
                cmd.Parameters.AddWithValue("SmallWoodExtraction", Convert.ToInt32(txtPolescmt.Text));
                cmd.Parameters.AddWithValue("TotalExtraction", Convert.ToInt32(txtTotalextraction.Text));
                cmd.Parameters.AddWithValue("UnAuthroized", Convert.ToInt32(txtTotalextractionunauthorized.Text));
                cmd.Parameters.AddWithValue("TotalExtractionTOF", Convert.ToInt32(txtTotalextractionfromToF.Text));
                cmd.Parameters.AddWithValue("CompareRosterNorm", Convert.ToInt32(txtComparedwiththeroster.Text));
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAlert('Added Successfully.','success')", true);

                    BindGrid(1);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert();", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "pop()", true);

                    //lboutput.Text = "Record inserted successfully";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        protected void gvActivity_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

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
    }
}