using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DataTable = System.Data.DataTable;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml.Style;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace vansystem
{
    public partial class TabularReports : System.Web.UI.Page
    {
        DBErrorLog db = new DBErrorLog();
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        private PdfPTable headerTable;
        private string headerText = "Your Header Text Here";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCH1_Click(object sender, EventArgs e)
        {
            GenerateExcelFile();
        }

        protected void btn2A_Click(object sender, EventArgs e)
        {
            GeneratePDF2A();
        }
        protected void GeneratePDF2A()
        {
            DataTable dataTable = new DataTable();
            string create_by = Session["create_by"].ToString();
            string DivisionName = Session["name"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("[sp_CH2a]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@create_by", create_by);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count > 0)
                    {

                        //string headerText = "CH Form 2-B Number of Tree individuals and DBH Classes";

                        // Create the table for table headings
                        PdfPTable headerTable = new PdfPTable(8);
                        AddTableHeaders(headerTable);

                        // Create an instance of the custom PdfPageEventHelper
                        CustomPdfPageEventHelper1 eventHelper = new CustomPdfPageEventHelper1(headerText, DivisionName);
                        eventHelper.AddHeaderTable(headerTable);

                       
                        Document doc = new Document(PageSize.A4);
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, memStream);

                        writer.PageEvent = eventHelper;

                        doc.Open();
                        PdfPTable table = new PdfPTable(8);
                        AddTableHeaders2A(table);
                        int rowIndex = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            AddDataRow2A(table, row, rowIndex);
                            rowIndex++;
                        }
                        doc.Add(table);
                        doc.Close();
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Compartment History Reporting Format 2A_" + DivisionName + ".pdf");
                        Response.Buffer = true;
                        Response.OutputStream.Write(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                        Response.OutputStream.Flush();
                        Response.End();
                    }
                    else
                    {
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            iTextSharp.text.Document doc = new iTextSharp.text.Document();
                            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, memStream);
                            doc.Open();
                            PdfPTable table = new PdfPTable(1);
                            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase("No data available for the specified criteria.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD)));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                            doc.Add(table);
                            doc.Close();
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=Compartment History Reporting Format 2A_" + DivisionName + ".pdf");
                            Response.Buffer = true;
                            Response.OutputStream.Write(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                            Response.OutputStream.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }
        private void AddTableHeaders2A(PdfPTable table)
        {
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            AddHeaderCell2A(table, "Serial No");
            AddHeaderCell2A(table, "Range Name");
            AddHeaderCell2A(table, "Block Name");
            AddHeaderCell2A(table, "Compartment");
            AddHeaderCell2A(table, "Total Area (Ha)");
            AddHeaderCell2A(table, "Area Enumerated");
            AddHeaderCell2A(table, "Sampling Method If Partial");
            AddHeaderCell2A(table, "Year of Enumeration");

        }
        private void AddHeaderCell2A(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.Padding = 3f;
            table.AddCell(cell);
        }
        private void AddDataRow2A(PdfPTable table, DataRow row, int rowIndex)
        {
            AddDataCell2A(table, row["S.No"].ToString(), rowIndex);
            AddDataCell2A(table, row["Range"].ToString(), rowIndex);
            AddDataCell2A(table, row["Block"].ToString(), rowIndex);
            AddDataCell2A(table, row["Compartment"].ToString(), rowIndex);
            AddDataCell2A(table, row["Total_area_in_hac"].ToString(), rowIndex);
            AddDataCell2A(table, row["Area_enumerated"].ToString(), rowIndex);
            AddDataCell2A(table, row["Sampling_method_if_partial"].ToString(), rowIndex);
            AddDataCell2A(table, row["year_of_enumeration"].ToString(), rowIndex);
        }
        private void AddDataCell2A(PdfPTable table, string text, int rowIndex)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8)));
            if (rowIndex % 2 == 0)
            {
                cell.BackgroundColor = BaseColor.WHITE;
            }
            else
            {
                //cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.BackgroundColor = new BaseColor(225, 243, 168);

            }
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            cell.FixedHeight = 20f;
            cell.Padding = 4f;
            table.AddCell(cell);
        }
        protected void btn2B_Click(object sender, EventArgs e)
        {
            GeneratePDF();

        }
        protected void GeneratePDF()
        {
            DataTable dataTable = new DataTable();
            string create_by = Session["create_by"].ToString();          
            string DivisionName = Session["name"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CH2b", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@create_by", create_by);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count > 0)
                    {

                        //string headerText = "CH Form 2-B Number of Tree individuals and DBH Classes";

                        // Create the table for table headings
                        PdfPTable headerTable = new PdfPTable(22);
                        AddTableHeaders(headerTable);

                        // Create an instance of the custom PdfPageEventHelper
                        CustomPdfPageEventHelper eventHelper = new CustomPdfPageEventHelper(headerText, DivisionName);
                        eventHelper.AddHeaderTable(headerTable);
                     
                        Document doc = new Document(PageSize.A3.Rotate());
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, memStream);

                        writer.PageEvent = eventHelper;

                        doc.Open();
                        PdfPTable table = new PdfPTable(22);
                        AddTableHeaders(table);
                        int rowIndex = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            AddDataRow(table, row, rowIndex);
                            rowIndex++;
                        }

                        doc.Add(table);
                        doc.Close();
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Compartment History Reporting Format 2B_" + DivisionName + ".pdf");
                        Response.Buffer = true;
                        Response.OutputStream.Write(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                        Response.OutputStream.Flush();
                        Response.End();
                    }
                    else
                    {
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            iTextSharp.text.Document doc = new iTextSharp.text.Document();
                            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, memStream);
                            doc.Open();
                            PdfPTable table = new PdfPTable(1);
                            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase("No data available for the specified criteria.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD)));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                            doc.Add(table);
                            doc.Close();
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=Compartment History Reporting Format 2B_" + DivisionName + ".pdf");
                            Response.Buffer = true;
                            Response.OutputStream.Write(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                            Response.OutputStream.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }
        private void AddTableHeaders(PdfPTable table)
        {
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            AddHeaderCell(table, "Serial No", colspan: 0);
            //AddHeaderCell(table, "Species Name");
            AddHeaderCell(table, "Species Name", colspan: 3);
            AddHeaderCell(table, "DBH Class 0-10", colspan: 0);
            AddHeaderCell(table, "DBH Class 10-20", colspan: 0);
            AddHeaderCell(table, "DBH Class 20-30", colspan: 0);
            AddHeaderCell(table, "DBH Class 30-40", colspan: 0);
            AddHeaderCell(table, "DBH Class 40-50", colspan: 0);
            AddHeaderCell(table, "DBH Class 50-60", colspan: 0);
            AddHeaderCell(table, "DBH Class 60-70", colspan: 0);
            AddHeaderCell(table, "DBH Class 70-80", colspan: 0);
            AddHeaderCell(table, "DBH Class 80-90", colspan: 0);
            AddHeaderCell(table, "DBH Class 90-100", colspan: 0);
            AddHeaderCell(table, "DBH Class 100-110", colspan: 0);
            AddHeaderCell(table, "DBH Class 110-120", colspan: 0);
            AddHeaderCell(table, "DBH Class 120-130", colspan: 0);
            AddHeaderCell(table, "DBH Class 130 & above", colspan: 0);
            AddHeaderCell(table, "Total Species", colspan: 0);
            AddHeaderCell(table, "Total Plots", colspan: 0);
            AddHeaderCell(table, "Total sampled area", colspan: 0);
            AddHeaderCell(table, "No. of Trees per Hac", colspan: 0);
        }
        private void AddHeaderCell(PdfPTable table, string text, int colspan)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.Padding = 3f;
            cell.Colspan = colspan;
            table.AddCell(cell);
        }
        private void AddDataRow(PdfPTable table, DataRow row, int rowIndex)
        {

            AddDataCell(table, row["S.No"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["Name"].ToString(), rowIndex, colspan: 3);
            AddDataCell(table, row["0-10 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["10-20 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["20-30 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["30-40 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["40-50 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["50-60 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["60-70 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["70-80 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["80-90 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["90-100 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["100-110 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["110-120 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["120-130 gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["130 & above gbh"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["Total"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["Total Plots"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["Total sampled area"].ToString(), rowIndex, colspan: 0);
            AddDataCell(table, row["No. of Trees per Hac"].ToString(), rowIndex, colspan: 0);
        }
        private void AddDataCell(PdfPTable table, string text, int rowIndex, int colspan)
        {

            PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8)));
            if (rowIndex % 2 == 0)
            {
                cell.BackgroundColor = BaseColor.WHITE;
            }
            else
            {
                //cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.BackgroundColor = new BaseColor(225, 243, 168);

            }
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            cell.FixedHeight = 20f;
            cell.Padding = 5f;
            cell.MinimumHeight = 20f;
            cell.Colspan = colspan;
            table.AddCell(cell);
        }

        protected void GenerateExcelFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a worksheet to the package
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("CH_Form_1");

                worksheet.Cells[1, 1].Value = "Category";
                worksheet.Cells[1, 2].Value = "Compartment No";
                worksheet.Cells[2, 1].Value = "Location";
                worksheet.Cells[2, 2].Value = "";

                worksheet.Cells[9, 1].Value = "";
                worksheet.Cells[10, 1].Value = "Land use and Topography";
                worksheet.Cells[15, 1].Value = "Soil";
                worksheet.Cells[19, 1].Value = "Crop Status";
                worksheet.Cells[22, 1].Value = "Grazing";
                worksheet.Cells[23, 1].Value = "Bamboo";
                worksheet.Cells[26, 1].Value = "Grass";
                worksheet.Cells[27, 1].Value = "";
                worksheet.Cells[28, 1].Value = "Plantation Details";
                worksheet.Cells[33, 1].Value = "Water Body";
                worksheet.Cells[37, 1].Value = "Degradation";
                worksheet.Cells[38, 1].Value = "Faunal and Floral sightings";

                worksheet.Cells[2, 2].Value = "Compartment Area (ha)";
                worksheet.Cells[3, 2].Value = "Latitude in DMS";
                worksheet.Cells[4, 2].Value = "Longitude in DMS";
                worksheet.Cells[5, 2].Value = "Range";
                worksheet.Cells[6, 2].Value = "Block";
                worksheet.Cells[7, 2].Value = "Village";
                worksheet.Cells[8, 2].Value = "Plot No";
                worksheet.Cells[9, 2].Value = "";

                worksheet.Cells[10, 2].Value = "Legal Status";
                worksheet.Cells[11, 2].Value = "Land use type";
                worksheet.Cells[12, 2].Value = "Type of rock";
                worksheet.Cells[13, 2].Value = "Topography of the plot";
                worksheet.Cells[14, 2].Value = "Slope of the plot (deg)";
                worksheet.Cells[15, 2].Value = "Soil Depth in cm";
                worksheet.Cells[16, 2].Value = "Soil Texture";
                worksheet.Cells[17, 2].Value = "Soil Permeability";
                worksheet.Cells[18, 2].Value = "Soil Erosion";
                worksheet.Cells[19, 2].Value = "Crop composition of the plot";
                worksheet.Cells[20, 2].Value = "Regeneration status of the plot";
                worksheet.Cells[21, 2].Value = "Damage by";
                worksheet.Cells[22, 2].Value = "Grazing incidence";
                worksheet.Cells[23, 2].Value = "Presence of Bamboo in plot";

                worksheet.Cells[24, 2].Value = "Bamboo quality";
                worksheet.Cells[25, 2].Value = "Bamboo regeneration";
                worksheet.Cells[26, 2].Value = "Presence of Grasses";
                worksheet.Cells[27, 2].Value = "Name of Weed";
                worksheet.Cells[28, 2].Value = "Plantation Species";
                worksheet.Cells[29, 2].Value = "Area of Plantation (ha)";
                worksheet.Cells[30, 2].Value = "Year of Plantation";
                worksheet.Cells[31, 2].Value = "Spacement in meter";
                worksheet.Cells[32, 2].Value = "Average Crop Diameter (cm)";
                worksheet.Cells[33, 2].Value = "Type of Water body";
                worksheet.Cells[34, 2].Value = "Status of Water body";
                worksheet.Cells[35, 2].Value = "Seasonality of Water body";
                worksheet.Cells[36, 2].Value = "Potability of Water body";
                worksheet.Cells[37, 2].Value = "Drivers of Plot Degradation";
                worksheet.Cells[38, 2].Value = "Mammals";
                worksheet.Cells[39, 2].Value = "Birds";
                worksheet.Cells[40, 2].Value = "Reptiles";
                worksheet.Cells[41, 2].Value = "Amphibians";
                worksheet.Cells[19, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[28, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Row(19).Style.WrapText = true;
                worksheet.Row(19).CustomHeight = true;
                worksheet.Row(19).Height = 80;
                worksheet.Row(28).Style.WrapText = true;
                worksheet.Row(28).CustomHeight = true;
                worksheet.Row(28).Height = 80;
                worksheet.Column(1).Style.WrapText = true;

                DataTable dataTable = GetDataFromDatabase();

                int row = 3; // Start from row 3 for data
                int column = 1; // Replace with the actual column index where data starts

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    int columnIndex = column;
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        worksheet.Cells[columnIndex, row].Value = dataRow[dataColumn].ToString();
                        columnIndex++;
                    }

                    row++;
                }
                for (int rowIndex = 1; rowIndex <= worksheet.Dimension.End.Row; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= worksheet.Dimension.End.Column; columnIndex++)
                    {
                        if (columnIndex != 2) // Skip setting alignment for Column B
                        {
                            worksheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[rowIndex, columnIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        }
                    }
                }


                using (ExcelRange range = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column])
                {
                    // Set border style
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    // Set text to bold and text size to 13 for the entire range
                    //range.Style.Font.Bold = true;
                    //range.Style.Font.Size = 13;
                }
                worksheet.Column(1).Width = 15.89;

                // Set the width of Column B (2nd column) to 20 pixels
                worksheet.Column(2).Width = 28.89;

                // Merge Ranges for categories
                worksheet.Cells["A2:A8"].Merge = true;
                worksheet.Cells["A10:A14"].Merge = true;
                worksheet.Cells["A15:A18"].Merge = true;
                worksheet.Cells["A19:A21"].Merge = true;
                worksheet.Cells["A23:A25"].Merge = true;
                worksheet.Cells["A28:A32"].Merge = true;
                worksheet.Cells["A33:A36"].Merge = true;
                worksheet.Cells["A38:A41"].Merge = true;


                int totalColumns = worksheet.Dimension.End.Column;

                for (int columnNumber = 3; columnNumber <= totalColumns; columnNumber++)
                {
                    worksheet.Column(columnNumber).Width = 66.89;
                }

                worksheet.View.FreezePanes(2, 2);
                worksheet.View.FreezePanes(2, 3);

                var columnToColor = worksheet.Column(1);
                columnToColor.Style.Font.Bold = true;
                columnToColor.Style.Font.Size = 13;
                Color customColor = Color.FromArgb(255, 237, 179);
                columnToColor.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                columnToColor.Style.Fill.BackgroundColor.SetColor(customColor);
                //columnToColor.Style.Fill.BackgroundColor.SetColor(Color.LightYellow);

                var columnToColor2 = worksheet.Column(2);
                columnToColor2.Style.Font.Bold = false;
                columnToColor2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                columnToColor2.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var rowToColor2 = worksheet.Row(1);
                rowToColor2.Style.Font.Bold = true;
                rowToColor2.Style.Font.Size = 13;
                //rowToColor2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Color customColor1 = Color.FromArgb(255, 237, 179);
                rowToColor2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rowToColor2.Style.Fill.BackgroundColor.SetColor(customColor1);
               


                // Save the Excel file to a MemoryStream
                using (MemoryStream stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    // Download the Excel file
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=CH_Form_1_Compartment_description_format.xlsx");
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            string divisionid = Session["DivisionId"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("CH1", con)) // Set the connection for the command
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Assuming "CH1" is a stored procedure

                    cmd.Parameters.AddWithValue("@divisionid", divisionid);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}