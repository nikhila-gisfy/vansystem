using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Text;
using ClosedXML.Excel;
using System.Collections.Specialized;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Presentation;

namespace vansystem
{
    public partial class VillageLevelInformation : System.Web.UI.Page
    {
        clsConnnection clscon = new clsConnnection();
        clsJson objjson = new clsJson();
        NameValueCollection nvc = new NameValueCollection();
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        string typeofvillage=string.Empty;
        public static int EditIndex { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(1);
            }
        }
        //SELECT* from static_village_details
        private void BindGrid(int pageIndex)
        {
            string divisionid = Session["DivisionId"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("VillageLevelInfo_SELECT"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@divisionid", divisionid);
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




        private void GetDataForExcel()
        {
            string divisionid = Session["DivisionId"].ToString();
            string DivisionName = Session["name"].ToString();
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //int divisionid = Convert.ToInt32(Session["DivisionId"].ToString());
                    // int divisionid = Convert.ToInt32(Session["DivisionId"]);



                    using (SqlCommand cmd = new SqlCommand("Get_All_VillageLevelInfo_SELECT"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@divisionid", divisionid);
                            sda.SelectCommand = cmd;
                            cmd.CommandTimeout = 3600;

                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    wb.Worksheets.Add(dt, "Customers");

                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=VillageLevelInformation_" + DivisionName + "_" + DateTime.Now + ".xlsx" + "");
                                    // Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                                    using (MemoryStream MyMemoryStream = new MemoryStream())
                                    {
                                        wb.SaveAs(MyMemoryStream);
                                        MyMemoryStream.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        Response.End();
                                    }
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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



        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            GetDataForExcel();

        }

        public string ds2json()
        {
            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("VillageLevelInfo_SELECT"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
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

        //protected void btnJson_Click(object sender, EventArgs e)
        //{
        //    string str = ds2json();
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(str);
        //    sb.Append("\r\n");

        //    string text = sb.ToString();

        //    Response.Clear();
        //    Response.ClearHeaders();

        //    Response.AppendHeader("Content-Length", text.Length.ToString());
        //    Response.ContentType = "text/plain";
        //    Response.AppendHeader("Content-Disposition", "attachment;filename=\"output.txt\"");

        //    Response.Write(text);
        //    Response.End();
        //}

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPageSize.SelectedValue == "All")
            {
                pagingnation.Visible = false;
                string divisionid = Session["DivisionId"].ToString();
                string DivisionName = Session["name"].ToString();
                try
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        using (SqlCommand cmd = new SqlCommand("Get_AllData_VillageLevelInfo_SELECT"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@divisionid", divisionid);
                                sda.SelectCommand = cmd;
                                cmd.CommandTimeout = 3600;

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
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                pagingnation.Visible = true;
                this.BindGrid(1);
            }
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindGrid(pageIndex);
        }




        protected void gvVLI_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvVLI.EditIndex = e.NewEditIndex;
            BindGrid(1);
        }

        protected void gvVLI_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            
                // Get the updated row from the GridView
                GridViewRow row = gvVLI.Rows[e.RowIndex];

                // Extract values from the updated row
                int id = Convert.ToInt32(gvVLI.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtSurveyorName") as TextBox).Text;
                string phonenumber = (row.FindControl("txtSurveyorPhoneNo") as TextBox).Text;
                string village = (row.FindControl("txtSurveyorVillageName") as TextBox).Text;
                string fuelwoodreq = (row.FindControl("ddlfuelwoodreq") as DropDownList).SelectedValue;
                string fuelwoodsell=(row.FindControl("ddlfuelwoodsell") as DropDownList).SelectedValue;
                string sellingplace=(row.FindControl("ddlsellingplace") as DropDownList).SelectedValue;
                string sellingprice = (row.FindControl("txtSurveyorsellingprice") as TextBox).Text;  
                string distance=(row.FindControl("txtSurveyordistance") as TextBox).Text;  
                string trips=(row.FindControl("txtSurveyortrips") as TextBox).Text;
                string weightcarriedbyman=(row.FindControl("txtSurveyorweightcarriedbyman") as TextBox).Text;
                string weightcarriedbywoman=(row.FindControl("txtSurveyorweightcarriedbywoman") as TextBox).Text;
                string weightcarriedbychild = (row.FindControl("txtSurveyorweightcarriedbychild") as TextBox).Text;
                string firstpriority=(row.FindControl("txtSurveyorfirstpriority") as TextBox).Text;
                string secondpriority = (row.FindControl("txtSurveyorsecondpriority") as TextBox).Text;
                string thirdpriority = (row.FindControl("txtSurveyorthirdpriority") as TextBox).Text;
                string fourthpriority = (row.FindControl("txtSurveyorfourthpriority") as TextBox).Text;
                string fodderreq=(row.FindControl("ddlfodderreq") as DropDownList).SelectedValue;
                string sellingplacefodder=(row.FindControl("ddlsellingplacefodder") as DropDownList).SelectedValue;
                string sellingpricefodder=(row.FindControl("txtSurveyorsellingpricefodder") as  TextBox).Text;
                string distancefodder=(row.FindControl("txtSurveyordistancefodder") as TextBox).Text;
                string tripsfodder=(row.FindControl("txtSurveyortripsfodder") as TextBox).Text;
                string fodderweightcarriedbyman= (row.FindControl("txtSurveyorfodderweightcarriedbyman") as TextBox).Text;
                string fodderweightcarriedbywoman = (row.FindControl("txtSurveyorfodderweightcarriedbywoman") as TextBox).Text;
                string fodderweightcarriedbychild = (row.FindControl("txtSurveyorfodderweightcarriedbychild") as TextBox).Text;
                string datisystem = (row.FindControl("ddldatisystem") as DropDownList).SelectedValue;
                string practiceofsillage = (row.FindControl("txtSurveyorpractice") as TextBox).Text;
                string foddergrassfirstpriority = (row.FindControl("txtSurveyorfoddergrassfirstpriority") as TextBox).Text;
                string foddergrasssecondpriority = (row.FindControl("txtSurveyorfoddergrasssecondpriority") as TextBox).Text;
                string foddergrassthirdpriority = (row.FindControl("txtSurveyorfoddergrassthirdpriority") as TextBox).Text;
                string foddergrassfourthpriority = (row.FindControl("txtSurveyorfoddergrassfourthpriority") as TextBox).Text;
                string measurestaken = (row.FindControl("ddlmeasurestaken") as DropDownList).SelectedValue;
                string village_type = (row.FindControl("ddlTypeofhamletvillage") as DropDownList).SelectedValue; 

            // Create a new SqlConnection using the connection string (constr)
            using (SqlConnection con = new SqlConnection(constr))
                {
                    // Create a new SqlCommand with the stored procedure name and connection
                    using (SqlCommand cmd = new SqlCommand("sp_update_village_level_form", con))
                    {
                        // Set the command type to stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Set the command timeout
                        cmd.CommandTimeout = 0;

                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
                        cmd.Parameters.AddWithValue("@village", village);
                        cmd.Parameters.AddWithValue("@fuelwoodreq", fuelwoodreq);
                        cmd.Parameters.AddWithValue("@fuelwoodsell", fuelwoodsell);
                        cmd.Parameters.AddWithValue("@sellingplace", sellingplace);
                        cmd.Parameters.AddWithValue("@sellingprice", sellingprice);
                        cmd.Parameters.AddWithValue("@distance", distance);
                        cmd.Parameters.AddWithValue("@trips", trips);
                        cmd.Parameters.AddWithValue("@weightcarriedbyman", weightcarriedbyman);
                        cmd.Parameters.AddWithValue("@weightcarriedbywoman ", weightcarriedbywoman);
                        cmd.Parameters.AddWithValue("@weightcarriedbychild", weightcarriedbychild);
                        cmd.Parameters.AddWithValue("@firstpriority", firstpriority);
                        cmd.Parameters.AddWithValue("@secondpriority", secondpriority);
                        cmd.Parameters.AddWithValue("@thirdpriority", thirdpriority);
                        cmd.Parameters.AddWithValue("@fourthpriority", fourthpriority);
                        cmd.Parameters.AddWithValue("@fodderreq", fodderreq);
                        cmd.Parameters.AddWithValue("@sellingplacefodder", sellingplacefodder);
                        cmd.Parameters.AddWithValue("@sellingpricefodder", sellingpricefodder);
                        cmd.Parameters.AddWithValue("@distancefodder", distancefodder);
                        cmd.Parameters.AddWithValue("@tripsfodder", tripsfodder);
                        cmd.Parameters.AddWithValue("@fodderweightcarriedbyman", fodderweightcarriedbyman);
                        cmd.Parameters.AddWithValue("@fodderweightcarriedbywoman", fodderweightcarriedbywoman);
                        cmd.Parameters.AddWithValue("@fodderweightcarriedbychild", fodderweightcarriedbychild);
                        cmd.Parameters.AddWithValue("@datisystem", datisystem);
                        cmd.Parameters.AddWithValue("@practiceofsillage", practiceofsillage);
                        cmd.Parameters.AddWithValue("@foddergrassfirstpriority", foddergrassfirstpriority);
                        cmd.Parameters.AddWithValue("@foddergrasssecondpriority", foddergrasssecondpriority);
                        cmd.Parameters.AddWithValue("@foddergrassthirdpriority", foddergrassthirdpriority);
                        cmd.Parameters.AddWithValue("@foddergrassfourthpriority", foddergrassfourthpriority);
                        cmd.Parameters.AddWithValue("@measurestaken", measurestaken);
                        cmd.Parameters.AddWithValue("@village_type", village_type);

                    // Open the database connection
                    con.Open();

                        // Execute the query and get the number of affected rows
                        int i = cmd.ExecuteNonQuery();

                        // If data was updated successfully
                        if (i > 0)
                        {
                            // Exit edit mode
                            gvVLI.EditIndex = -1;

                            // Rebind the GridView to display updated data
                            BindGrid(1);
                        }

                        // Close the database connection
                        con.Close();
                    }
                }
            
            
        }



        protected void gvVLI_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvVLI.EditIndex = -1;
            BindGrid(1);
        }

        protected void gvVLI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvVLI.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    nvc.Clear();
                    nvc.Add("@operation", "70");
                    // nvc.Add("@operation","41");
                    //gvVLI.ro
                    DropDownList dlllist = (DropDownList)e.Row.FindControl("ddlTypeofhamletvillage");
                    clscon.fnExecuteProcedureSelectSpcBindDDL(dlllist, "sp_dropdownsvillage", "value_eng", "id", nvc);
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    dlllist.Items.RemoveAt(0);  
                    dlllist.SelectedValue = dr["id"].ToString();
                   
                     typeofvillage = dlllist.SelectedValue;


                    //nvc.Clear();
                    //    nvc.Add("@operation", "41");
                    //    //gvVLI.ro
                    //    DropDownList downList = (DropDownList)e.Row.FindControl("ddlsellingplace");
                    //    clscon.fnExecuteProcedureSelectSpcBindDDL(downList, "sp_dropdownsvillage", "value_eng", "id", nvc);
                    //    DataRowView dr1 = e.Row.DataItem as DataRowView;
                    //    downList.SelectedValue = dr1["id"].ToString();
                }

            }
                if (e.Row.RowType == DataControlRowType.DataRow && gvVLI.EditIndex == e.Row.RowIndex)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {

                        nvc.Clear();
                        nvc.Add("@operation", "41");
                        //gvVLI.ro
                        DropDownList downList = (DropDownList)e.Row.FindControl("ddlsellingplace");
                        clscon.fnExecuteProcedureSelectSpcBindDDL(downList, "sp_dropdownsvillage", "value_eng", "id", nvc);
                        DataRowView dr1 = e.Row.DataItem as DataRowView;
                    downList.Items.RemoveAt(0);
                    downList.SelectedValue = dr1["id"].ToString();





                    }

                }
            if (e.Row.RowType == DataControlRowType.DataRow && gvVLI.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    nvc.Clear();
                    nvc.Add("@operation", "41-A");
                    //gvVLI.ro
                    DropDownList downList1 = (DropDownList)e.Row.FindControl("ddlsellingplacefodder");
                    clscon.fnExecuteProcedureSelectSpcBindDDL(downList1, "sp_dropdownsvillage", "value_eng", "id", nvc);
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    downList1.Items.RemoveAt(0);
                    downList1.SelectedValue = dr2["id"].ToString();

                    



                }

            }

        }
    }
}