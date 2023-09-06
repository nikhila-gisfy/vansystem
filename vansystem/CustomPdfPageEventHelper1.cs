using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

public class CustomPdfPageEventHelper1 : PdfPageEventHelper
{
    private PdfPTable headerTable;
    private string headerText;

    private PdfPTable _columnHeaderTable;
    private string DivisionName;

    // Constructor to set the header text
    public CustomPdfPageEventHelper1(string headerText, string DivisionName)
    {
        this.headerText = headerText;
        this.DivisionName = DivisionName;

        // Initialize the headerTable
        headerTable = new PdfPTable(20);
        _columnHeaderTable = new PdfPTable(8);
        AddTableHeaders2A(_columnHeaderTable);
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

    // Method to add the header table content
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
            numberOfTreePhrase.Add(new Chunk("COMPARTMENT ENUMERATION ", headerFont));
            numberOfTreePhrase.Add(new Chunk("(CH Form 2)", headerFont));

            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER,
                numberOfTreePhrase,
                document.PageSize.Width / 2, document.PageSize.GetTop(document.TopMargin) + headerTopOffset, 0);

            // Create a phrase for the "Number of Tree individuals and DBH Classes" line
            var dbhClassesPhrase = new Phrase();
            dbhClassesPhrase.Add(new Chunk("(Based on Enumeration of Sample Plots ", headerFont));
            dbhClassesPhrase.Add(new Chunk("Falling In The Compartment)", headerFont));

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
        /*string totalPages = writer.PageNumber.ToString();*/  // Use writer.CurrentPageNumber to get the correct total number of pages.

        PdfPCell pageNumberCell = new PdfPCell(new Phrase($"Page {pageNumber}", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f)));
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
