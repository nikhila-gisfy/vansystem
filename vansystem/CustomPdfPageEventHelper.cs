using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

public class CustomPdfPageEventHelper : PdfPageEventHelper
{
    private PdfPTable headerTable;
    private string headerText;

    private string DivisionName;
    private PdfPTable _columnHeaderTable;

    // Constructor to set the header text
    public CustomPdfPageEventHelper(string headerText, string divisionName)
    {
        this.headerText = headerText;
        this.DivisionName = divisionName;
        // Initialize the headerTable
        headerTable = new PdfPTable(25);
        _columnHeaderTable = new PdfPTable(22);
        AddTableHeaders2A(_columnHeaderTable);
    }
    public void AddHeaderTable(PdfPTable table)
    {
        var headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD);

        // Add the header text to the header table
        var headerCell = new PdfPCell(new Phrase(headerText, headerFont))
        {
            Colspan = 20, // The number of columns in your data table
            HorizontalAlignment = Element.ALIGN_CENTER,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            BackgroundColor = BaseColor.LIGHT_GRAY
        };
        table.AddCell(headerCell);
    }
    private void AddTableHeaders2A(PdfPTable table)
    {
        table.WidthPercentage = 100;
        table.HorizontalAlignment = Element.ALIGN_LEFT;
        AddHeaderCell(table, "Serial No", colspan: 0);
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
    public override void OnStartPage(PdfWriter writer, Document document)
    {
        int headerTopOffset = 60;
        base.OnStartPage(writer, document);

        if (document.PageNumber == 1)
        {
            if (headerTable != null && headerTable.TotalWidth > 0)
            {

                headerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.GetTop(document.TopMargin), writer.DirectContent);
            }
            var headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD);

            // Create a phrase for the "CH Form" line
            var numberOfTreePhrase = new Phrase();
            numberOfTreePhrase.Add(new Chunk("CH Form ", headerFont));
            numberOfTreePhrase.Add(new Chunk("2-B", headerFont));

            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER,
                numberOfTreePhrase,
                 ////document.PageSize.Width / 2, document.PageSize.GetTop(document.TopMargin) + 25, 0);
                 document.PageSize.Width / 2, document.PageSize.GetTop(document.TopMargin) + headerTopOffset, 0);

            // Create a phrase for the "Number of Tree individuals and DBH Classes" line
            var dbhClassesPhrase = new Phrase();
            dbhClassesPhrase.Add(new Chunk("Number of Tree individuals ", headerFont));
            dbhClassesPhrase.Add(new Chunk("and DBH Classes", headerFont));

            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER,
                dbhClassesPhrase,
                document.PageSize.Width / 2, document.PageSize.GetTop(document.TopMargin) + 13, 0);

            if (!string.IsNullOrEmpty(DivisionName))
            {
                var divisionPhrase = new Phrase();
                divisionPhrase.Add(new Chunk("Division Name : ", headerFont));
                divisionPhrase.Add(new Chunk(DivisionName, headerFont));

                ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER,
                    divisionPhrase,
                    document.PageSize.Width / 2, document.PageSize.GetTop(document.TopMargin) + 1, 0);
            }


        }
        if (document.PageNumber != 1)
        {
            if (_columnHeaderTable != null)
            {
                _columnHeaderTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                // Write the column header table to each page
                _columnHeaderTable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.RightMargin + _columnHeaderTable.TotalHeight, writer.DirectContent);
            }
        }
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);

        // Add footer content here (displayed on every page)
        PdfPTable footerTable = new PdfPTable(2);
        footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        footerTable.HorizontalAlignment = Element.ALIGN_CENTER;
        string pageNumber = writer.PageNumber.ToString();
        //string totalPages = writer.PageNumber.ToString();

        PdfPCell pageNumberCell = new PdfPCell(new Phrase($"Page {pageNumber} ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f)));
        pageNumberCell.Border = Rectangle.TOP_BORDER;
        pageNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
        footerTable.AddCell(pageNumberCell);

        // Add current date
        string currentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
        PdfPCell dateCell = new PdfPCell(new Phrase($"{currentDate}", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f)));
        dateCell.Border = Rectangle.TOP_BORDER;
        dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        footerTable.AddCell(dateCell);

        footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin + footerTable.TotalHeight, writer.DirectContent);
    }
}