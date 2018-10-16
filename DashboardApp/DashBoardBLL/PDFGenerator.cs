using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PDFHelper;

namespace DashBoardBLL
{
    public enum PDFType { BlackWhite, Colors}

    public class PDFGenerator
    {
        public string PrintMovies(int typeId, string pdfPath)
        {
            try
            {
                Paragraph paragraph;
                var movies = (new BOMovies()).GetTypeMovies(typeId);
                var setHead = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24);
                var setSubHead = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18);
                var setboldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
                var setFont = FontFactory.GetFont(FontFactory.TIMES, 12);
                var setLine = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
                var setSubHead2 = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 12);

                if (!Directory.Exists(pdfPath))
                    Directory.CreateDirectory(pdfPath);

                Document doc = new Document();
                pdfPage page = new pdfPage();
                PdfPCell cell;
                string filename = "Movie_Report.pdf";
                string file = pdfPath + "\\" + filename;

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
                paragraph = new Paragraph("Your Movie List", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.HeaderText = paragraph;
                paragraph = new Paragraph("Movies Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.FooterText = paragraph;
                paragraph = new Paragraph("From Ke's Website", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                page.FooterSubText = paragraph;
                writer.PageEvent = page;
                doc.SetMargins(30, 30, 200, 30);
                doc.Open();
                doc.SetMargins(30, 30, 50, 30);

                paragraph = new Paragraph("The Movie List", setHead);
                paragraph.SpacingBefore = 100;
                paragraph.SpacingAfter = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("http://dashboard.jiushizhutk.com", setSubHead);
                paragraph.SpacingAfter = 5;
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("Ke's Dashboard Application", setSubHead);
                paragraph.SpacingAfter = 50;
                paragraph.SpacingBefore = 5;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph(DateTime.Now.ToShortDateString(), setSubHead2);
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);
                doc.NewPage();
                if (movies != null)
                {
                    paragraph = new Paragraph("Movies: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));
                    paragraph.Alignment = 0;
                    paragraph.SpacingBefore = 10;
                    paragraph.SpacingAfter = 10;
                    doc.Add(paragraph);

                    var movieTbl = new PdfPTable(4);
                    movieTbl.TotalWidth = doc.PageSize.Width;
                    float[] widths = new float[] { 25, 25, 20, 30 };
                    movieTbl.SetWidths(widths);
                    movieTbl.SpacingAfter = 10;
                    movieTbl.SpacingBefore = 10;
                    movieTbl.HorizontalAlignment = 0;
                    

                    cell = new PdfPCell(new Phrase("Movie Name", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Movie Type", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Show Date", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Comment", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    movieTbl.CompleteRow();

                    foreach (DB_GetTypeMoviesResult m in movies)
                    {
                        cell = new PdfPCell(new Phrase(m.MovieName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.TypeName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(string.Format("{0:MM/dd/yyyy}", m.MovieDate), setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.MovieComment, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);
                        movieTbl.CompleteRow();
                    }

                    doc.Add(movieTbl);
                }
                else
                {
                    paragraph = new Paragraph("No Movies");
                    paragraph.Alignment = 1;
                    paragraph.SpacingAfter = 100;
                    paragraph.SpacingBefore = 100;
                    doc.Add(paragraph);
                }

                doc.Close();
                return filename;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PrintMergedFile(List<string> files, string pdfPath)
        {
            try
            {
                if (!Directory.Exists(pdfPath))
                    Directory.CreateDirectory(pdfPath);

                string mergedFileName = "MergedFiles.pdf";
                string mergedPath = pdfPath + "\\" + mergedFileName;
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(mergedPath, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int n = 0;
                int rotation = 0;
                foreach (string filename in files)
                {
                    PdfReader reader = new PdfReader(pdfPath + filename);

                    n = reader.NumberOfPages;
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));

                        document.NewPage();

                        if (i == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(pdfPath + filename);
                            document.Add(fileRef);
                        }

                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }

                document.Close();
                return mergedFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PrintMovies(int movieType, int year, int month, int review, string pdfPath)
        {
            try
            {
                Paragraph paragraph;
                var movies = (new BOMovies()).GetMovieList(movieType, year, month, review);
                var setHead = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24);
                var setSubHead = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18);
                var setboldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
                var setFont = FontFactory.GetFont(FontFactory.TIMES, 12);
                var setLine = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
                var setSubHead2 = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 12);


                if (!Directory.Exists(pdfPath))
                    Directory.CreateDirectory(pdfPath);

                Document doc = new Document();
                pdfPage page = new pdfPage();
                PdfPCell cell;
                string filename = "Movie_Report.pdf";
                string file = pdfPath + "\\" + filename;

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
                paragraph = new Paragraph("Your Movie List", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.HeaderText = paragraph;
                paragraph = new Paragraph("Movies Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.FooterText = paragraph;
                paragraph = new Paragraph("From Ke's Website", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                page.FooterSubText = paragraph;
                writer.PageEvent = page;
                doc.SetMargins(30, 30, 200, 30);
                doc.Open();
                doc.SetMargins(30, 30, 50, 30);

                paragraph = new Paragraph("The Movie List", setHead);
                paragraph.SpacingBefore = 100;
                paragraph.SpacingAfter = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("http://dashboard.jiushizhutk.com", setSubHead);
                paragraph.SpacingAfter = 5;
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("Ke's Dashboard Application", setSubHead);
                paragraph.SpacingAfter = 50;
                paragraph.SpacingBefore = 5;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph(DateTime.Now.ToShortDateString(), setSubHead2);
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);
                doc.NewPage();
                if (movies != null)
                {
                    paragraph = new Paragraph("Movies: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));
                    paragraph.Alignment = 0;
                    paragraph.SpacingBefore = 10;
                    paragraph.SpacingAfter = 10;
                    doc.Add(paragraph);

                    var movieTbl = new PdfPTable(5);
                    movieTbl.TotalWidth = doc.PageSize.Width;
                    float[] widths = new float[] { 25, 25, 20, 20, 10 };
                    movieTbl.SetWidths(widths);
                    movieTbl.SpacingAfter = 10;
                    movieTbl.SpacingBefore = 10;
                    movieTbl.HorizontalAlignment = 0;


                    cell = new PdfPCell(new Phrase("Movie Name", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Movie Type", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Show Date", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Comment", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Review", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    movieTbl.CompleteRow();

                    foreach (Movie m in movies)
                    {
                        cell = new PdfPCell(new Phrase(m.MovieName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.MovieType1.TypeName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(string.Format("{0:MM/dd/yyyy}", m.MovieDate), setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.MovieComment, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.Review == null ? "0" : m.Review.ToString(), setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        movieTbl.CompleteRow();
                    }

                    doc.Add(movieTbl);
                }
                else
                {
                    paragraph = new Paragraph("No Movies");
                    paragraph.Alignment = 1;
                    paragraph.SpacingAfter = 100;
                    paragraph.SpacingBefore = 100;
                    doc.Add(paragraph);
                }

                doc.Close();
                return filename;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PrintMovies(int movieType, int year, int month, int review, Stream outputStream)
        {
            try
            {
                Paragraph paragraph;
                var movies = (new BOMovies()).GetMovieList(movieType, year, month, review);
                var setHead = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24);
                var setSubHead = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18);
                var setboldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
                var setFont = FontFactory.GetFont(FontFactory.TIMES, 12);
                var setLine = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
                var setSubHead2 = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 12);

                Document doc = new Document();
                pdfPage page = new pdfPage();
                PdfPCell cell;

                PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
                paragraph = new Paragraph("Your Movie List", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.HeaderText = paragraph;
                paragraph = new Paragraph("Movies Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                page.FooterText = paragraph;
                paragraph = new Paragraph("From Ke's Website", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                page.FooterSubText = paragraph;
                writer.PageEvent = page;
                doc.SetMargins(30, 30, 200, 30);
                doc.Open();
                doc.SetMargins(30, 30, 50, 30);

                paragraph = new Paragraph("The Movie List", setHead);
                paragraph.SpacingBefore = 100;
                paragraph.SpacingAfter = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("http://dashboard.jiushizhutk.com", setSubHead);
                paragraph.SpacingAfter = 5;
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph("Ke's Dashboard Application", setSubHead);
                paragraph.SpacingAfter = 50;
                paragraph.SpacingBefore = 5;
                paragraph.Alignment = 1;
                doc.Add(paragraph);

                paragraph = new Paragraph(DateTime.Now.ToShortDateString(), setSubHead2);
                paragraph.SpacingBefore = 50;
                paragraph.Alignment = 1;
                doc.Add(paragraph);
                doc.NewPage();
                if (movies != null)
                {
                    paragraph = new Paragraph("Movies: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));
                    paragraph.Alignment = 0;
                    paragraph.SpacingBefore = 10;
                    paragraph.SpacingAfter = 10;
                    doc.Add(paragraph);

                    var movieTbl = new PdfPTable(5);
                    movieTbl.TotalWidth = doc.PageSize.Width;
                    float[] widths = new float[] { 25, 25, 20, 20, 10 };
                    movieTbl.SetWidths(widths);
                    movieTbl.SpacingAfter = 10;
                    movieTbl.SpacingBefore = 10;
                    movieTbl.HorizontalAlignment = 0;


                    cell = new PdfPCell(new Phrase("Movie Name", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Movie Type", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Show Date", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Comment", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Review", setboldFont));
                    cell.BorderWidth = 0;
                    movieTbl.AddCell(cell);

                    movieTbl.CompleteRow();

                    foreach (Movie m in movies)
                    {
                        cell = new PdfPCell(new Phrase(m.MovieName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.MovieType1.TypeName, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(string.Format("{0:MM/dd/yyyy}", m.MovieDate), setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.MovieComment, setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase(m.Review == null ? "0" : m.Review.ToString(), setFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthTop = 1;
                        movieTbl.AddCell(cell);

                        movieTbl.CompleteRow();
                    }

                    doc.Add(movieTbl);
                }
                else
                {
                    paragraph = new Paragraph("No Movies");
                    paragraph.Alignment = 1;
                    paragraph.SpacingAfter = 100;
                    paragraph.SpacingBefore = 100;
                    doc.Add(paragraph);
                }

                doc.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string PrintMovies(int movieType, int year, int month, int review, string pdfPath, List<Movie> movies, PDFType pdfType )
        //{
        //    try
        //    {
        //        Paragraph paragraph;
        //        Font setHeader, setFooter, setTitle, setSubTitle, setFont, setBoldFont, setLine;

        //        if (pdfType == PDFType.BlackWhite)
        //        {
        //            setHeader = FontFactory.GetFont(FontFactory.COURIER_BOLD, 
        //            setHead = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24);
        //            setSubHead = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18);
        //            setboldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
        //            setFont = FontFactory.GetFont(FontFactory.TIMES, 12);
        //            setLine = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
        //            setSubHead2 = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 12);
        //        }


        //        if (!Directory.Exists(pdfPath))
        //            Directory.CreateDirectory(pdfPath);

        //        Document doc = new Document();
        //        pdfPage page = new pdfPage();
        //        PdfPCell cell;
        //        string filename = "Movie_Report.pdf";
        //        string file = pdfPath + "\\" + filename;

        //        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
        //        paragraph = new Paragraph("Your Movie List", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
        //        page.HeaderText = paragraph;
        //        paragraph = new Paragraph("Movies Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
        //        page.FooterText = paragraph;
        //        paragraph = new Paragraph("From Ke's Website", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
        //        page.FooterSubText = paragraph;
        //        writer.PageEvent = page;
        //        doc.SetMargins(30, 30, 200, 30);
        //        doc.Open();
        //        doc.SetMargins(30, 30, 50, 30);

        //        paragraph = new Paragraph("The Movie List", setHead);
        //        paragraph.SpacingBefore = 100;
        //        paragraph.SpacingAfter = 50;
        //        paragraph.Alignment = 1;
        //        doc.Add(paragraph);

        //        paragraph = new Paragraph("http://dashboard.jiushizhutk.com", setSubHead);
        //        paragraph.SpacingAfter = 5;
        //        paragraph.SpacingBefore = 50;
        //        paragraph.Alignment = 1;
        //        doc.Add(paragraph);

        //        paragraph = new Paragraph("Ke's Dashboard Application", setSubHead);
        //        paragraph.SpacingAfter = 50;
        //        paragraph.SpacingBefore = 5;
        //        paragraph.Alignment = 1;
        //        doc.Add(paragraph);

        //        paragraph = new Paragraph(DateTime.Now.ToShortDateString(), setSubHead2);
        //        paragraph.SpacingBefore = 50;
        //        paragraph.Alignment = 1;
        //        doc.Add(paragraph);
        //        doc.NewPage();

        //        if (movies != null)
        //        {
        //            paragraph = new Paragraph("Movies: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));
        //            paragraph.Alignment = 0;
        //            paragraph.SpacingBefore = 10;
        //            paragraph.SpacingAfter = 10;
        //            doc.Add(paragraph);

        //            var movieTbl = new PdfPTable(5);
        //            movieTbl.TotalWidth = doc.PageSize.Width;
        //            float[] widths = new float[] { 25, 25, 20, 20, 10 };
        //            movieTbl.SetWidths(widths);
        //            movieTbl.SpacingAfter = 10;
        //            movieTbl.SpacingBefore = 10;
        //            movieTbl.HorizontalAlignment = 0;


        //            cell = new PdfPCell(new Phrase("Movie Name", setboldFont));
        //            cell.BorderWidth = 0;
        //            movieTbl.AddCell(cell);

        //            cell = new PdfPCell(new Phrase("Movie Type", setboldFont));
        //            cell.BorderWidth = 0;
        //            movieTbl.AddCell(cell);

        //            cell = new PdfPCell(new Phrase("Show Date", setboldFont));
        //            cell.BorderWidth = 0;
        //            movieTbl.AddCell(cell);

        //            cell = new PdfPCell(new Phrase("Comment", setboldFont));
        //            cell.BorderWidth = 0;
        //            movieTbl.AddCell(cell);

        //            cell = new PdfPCell(new Phrase("Review", setboldFont));
        //            cell.BorderWidth = 0;
        //            movieTbl.AddCell(cell);

        //            movieTbl.CompleteRow();

        //            foreach (Movie m in movies)
        //            {
        //                cell = new PdfPCell(new Phrase(m.MovieName, setFont));
        //                cell.BorderWidth = 0;
        //                cell.BorderWidthTop = 1;
        //                movieTbl.AddCell(cell);

        //                cell = new PdfPCell(new Phrase(m.MovieType1.TypeName, setFont));
        //                cell.BorderWidth = 0;
        //                cell.BorderWidthTop = 1;
        //                movieTbl.AddCell(cell);

        //                cell = new PdfPCell(new Phrase(string.Format("{0:MM/dd/yyyy}", m.MovieDate), setFont));
        //                cell.BorderWidth = 0;
        //                cell.BorderWidthTop = 1;
        //                movieTbl.AddCell(cell);

        //                cell = new PdfPCell(new Phrase(m.MovieComment, setFont));
        //                cell.BorderWidth = 0;
        //                cell.BorderWidthTop = 1;
        //                movieTbl.AddCell(cell);

        //                cell = new PdfPCell(new Phrase(m.Review == null ? "0" : m.Review.ToString(), setFont));
        //                cell.BorderWidth = 0;
        //                cell.BorderWidthTop = 1;
        //                movieTbl.AddCell(cell);

        //                movieTbl.CompleteRow();
        //            }

        //            doc.Add(movieTbl);
        //        }
        //        else
        //        {
        //            paragraph = new Paragraph("No Movies");
        //            paragraph.Alignment = 1;
        //            paragraph.SpacingAfter = 100;
        //            paragraph.SpacingBefore = 100;
        //            doc.Add(paragraph);
        //        }

        //        doc.Close();
        //        return filename;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
