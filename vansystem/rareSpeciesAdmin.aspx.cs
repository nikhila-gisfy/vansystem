﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class rareSpeciesAdmin : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("RareSpecies_SELECT"))
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
                            gvVLI.DataSource = dt;
                            gvVLI.DataBind();
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

        protected void gvVLI_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.ContentType = "File/Data.xls";
            System.IO.StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

            gvVLI.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();
        }


        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvVLI.AllowPaging = false;
                this.BindGrid(1);

                gvVLI.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvVLI.HeaderRow.Cells)
                {
                    cell.BackColor = gvVLI.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvVLI.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvVLI.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvVLI.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvVLI.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();

        }

        public string ds2json()
        {
            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("RareSpecies_SELECT"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvVLI.DataSource = dt;
                            gvVLI.DataBind();
                            ds.Tables.Add(dt);
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(ds, Formatting.Indented);
        }

        protected void btnJson_Click(object sender, EventArgs e)
        {
            string str = ds2json();
            StringBuilder sb = new StringBuilder();

            sb.Append(str);
            sb.Append("\r\n");

            string text = sb.ToString();

            Response.Clear();
            Response.ClearHeaders();

            Response.AppendHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"output.txt\"");

            Response.Write(text);
            Response.End();
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