using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFHelper
{
    public class pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
   {
        private Paragraph headertext;
        public Paragraph HeaderText
        {
            get
            {
                return headertext;
            }
            set
            {
                headertext = value;
            }
        }

        private Paragraph footertext;
        public Paragraph FooterText
        {
            get
            {
                return footertext;
            }
            set
            {
                footertext = value;
            }
        }

        private Paragraph footertext2;
        public Paragraph FooterSubText
        {
            get
            {
                return footertext2;
            }
            set
            {
                footertext2 = value;
            }
        }

       //I create a font object to use within my footer
      protected Font footer
      {
          get
          {
               // create a basecolor to use for the footer font, if needed.
              BaseColor black = new BaseColor(0, 0, 0);
               Font font = FontFactory.GetFont("TimesNewRoman", 10, Font.NORMAL, black);
               return font;
          }
      }
       //override the OnStartPage event handler to add our header
       public override void OnStartPage(PdfWriter writer, Document doc)
       {
           base.OnStartPage(writer, doc);
            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.TotalWidth = doc.PageSize.Width;
            headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell = new PdfPCell(headertext);
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 10;
            headerTbl.AddCell(cell);

            if (doc.PageNumber == 1)
            {
                Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/HeaderLogo.gif"));
                logo.ScalePercent(50);
                cell = new PdfPCell(logo, false);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.PaddingRight = 20;
                cell.Border = 0;
            }
            else
            {
                cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = 0;
            }
            headerTbl.AddCell(cell);

            //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
            headerTbl.WriteSelectedRows(0,-1, 0, (doc.PageSize.Height-30), writer.DirectContent);
       }

       //override the OnPageEnd event handler to add our footer
       private PdfTemplate template;
       public override void OnEndPage(PdfWriter writer, Document doc)
       {
           base.OnEndPage(writer, doc);
           //I use a PdfPtable with 2 columns to position my footer where I want it
           PdfPTable footerTbl = new PdfPTable(3);

           //set the width of the table to be the same as the document
           footerTbl.TotalWidth = doc.PageSize.Width;
           footerTbl.DefaultCell.Indent = 20;
           footerTbl.HorizontalAlignment = 1;
           footerTbl.SpacingBefore = 5;
           //Center the table on the page
           footerTbl.HorizontalAlignment = 0;


           //Create a paragraph that contains the footer text
           //Paragraph para = new Paragraph(footertext, footer);

           //add a carriage return
           //para.Add(Environment.NewLine);
           //para.Add("Some more footer text");

           //create a cell instance to hold the text
           PdfPCell cell = new PdfPCell(footertext);

           //set cell border to 0
           cell.BorderWidth = 0;
           cell.BorderWidthTop = 1;
           cell.Padding = 0;
           cell.PaddingTop = 5;
           cell.PaddingLeft = 10;
           cell.BorderColorTop = BaseColor.BLACK;
           //add some padding to bring away from the edge

           //add cell to table
           footerTbl.AddCell(cell);

           //create new instance of Paragraph for 2nd cell text
           Paragraph para = new Paragraph(string.Format("{0:MMMM dd, yyyy}", System.DateTime.Now), footer);

           //create new instance of cell to hold the text
           cell = new PdfPCell(para);

           //align the text to the right of the cell
           cell.HorizontalAlignment = Element.ALIGN_CENTER;
           //set border to 0
           cell.BorderWidth = 0;
           cell.BorderWidthTop = 1;
           cell.BorderColorTop = BaseColor.BLACK;
           cell.Padding = 0;
           cell.PaddingTop = 5;
           footerTbl.AddCell(cell);

           //para = new Paragraph(FooterSubText.Content, new Font(Font.FontFamily.TIMES_ROMAN, 10));
           //para.Add(Environment.NewLine);
           //para.Add("Page " + doc.PageNumber.ToString());
           para = new Paragraph("Page " + doc.PageNumber.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 10));
           cell = new PdfPCell(para);
           cell.BorderWidth = 0;
           cell.BorderWidthTop = 1;
           cell.BorderColorTop = BaseColor.BLACK;
           cell.Padding = 0;
           cell.PaddingTop = 5;
           cell.HorizontalAlignment = Element.ALIGN_RIGHT;
           // add some padding to take away from the edge of the page
           cell.PaddingRight = 10;

           //add the cell to the table
           footerTbl.AddCell(cell);

           //write the rows out to the PDF output stream.
           footerTbl.WriteSelectedRows(0, -1, 0, 30, writer.DirectContent);
       }


       //}

       //public override void OnCloseDocument(PdfWriter writer, Document document)
       //{
       //    base.OnCloseDocument(writer, document);
       //    template.BeginText();
       //    template.SetTextMatrix(0,0);
       //    template.ShowText("" + (writer.PageNumber - 1));
       //    template.EndText();
       //}
   }
}


