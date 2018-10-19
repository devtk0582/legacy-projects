using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLib.PDFHelper
{
    public class PDFHelper
    {
        public void MakePrintable(string sourcePath, string targetPath)
        {
            int pageNumber = 1;
            PdfReader reader = new PdfReader(sourcePath);
            Rectangle size = reader.GetPageSizeWithRotation(pageNumber);
            Document document = new Document(size);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(targetPath, FileMode.Create, FileAccess.Write));

            document.Open();
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(jAction);
            PdfContentByte cb = writer.DirectContent;
            document.NewPage();
            PdfImportedPage page = writer.GetImportedPage(reader, pageNumber);
            cb.AddTemplate(page, 0, 0);
            reader.Close();
            document.Close();
            writer.Close();
            document.Close();
        }
    }
}
