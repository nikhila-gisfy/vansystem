using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class VillageLI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }

        }
        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("VillageLevelInfo_SELECT"))
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
                        }
                    }
                }
            }
        }

        protected void gvVLI_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvVLI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
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
                this.BindGrid();

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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}