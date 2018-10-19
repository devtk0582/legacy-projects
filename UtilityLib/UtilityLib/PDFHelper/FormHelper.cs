using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLib.PDFHelper
{
    public class FormHelper
    {
        public void FillForm(string templatePath, string targetPath, params string[] parameters)
        {
            if (parameters == null || parameters.Length < 19)
            {
                return;
            }

            PdfReader.unethicalreading = true;
            PdfReader pdfReader = new PdfReader(templatePath);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                        targetPath, FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfFormFields.SetField("form1[0].#subform[3].lastname[0]", parameters[0]);
            pdfFormFields.SetField("form1[0].#subform[3].firstname[0]", parameters[1]);
            pdfFormFields.SetField("form1[0].#subform[3].address[0]", parameters[2]);
            pdfFormFields.SetField("form1[0].#subform[3].apartmentnumber[0]", parameters[3]);
            pdfFormFields.SetField("form1[0].#subform[3].city[0]", parameters[4]);
            pdfFormFields.SetField("form1[0].#subform[3].state[0]", parameters[5]);
            pdfFormFields.SetField("form1[0].#subform[3].zipcode[0]", parameters[6]);
            pdfFormFields.SetField("form1[0].#subform[3].dateofbirth[0]", parameters[7]);
            pdfFormFields.SetField("form1[0].#subform[3].ssnum[0]", parameters[8]);
            pdfFormFields.SetField("form1[0].#subform[3].citizen1[0]", parameters[9]);
            pdfFormFields.SetField("form1[0].#subform[3].noncitizennational[0]", parameters[10]);
            pdfFormFields.SetField("form1[0].#subform[3].LPRAlienNumber[0]", parameters[11]);
            pdfFormFields.SetField("form1[0].#subform[3].LPRAlienNumber[1]", parameters[12]);
            pdfFormFields.SetField("form1[0].#subform[3].alienworknumber[0]", parameters[13]);
            pdfFormFields.SetField("form1[0].#subform[3].alienauthorizedtowork[0]", parameters[14]);
            pdfFormFields.SetField("form1[0].#subform[3].expirationdate[0]", parameters[15]);
            pdfFormFields.SetField("form1[0].#subform[3].signaturedate[0]", parameters[16]);
            pdfFormFields.SetField("form1[0].#subform[3].preparersaddress[0]", parameters[17]);
            pdfFormFields.SetField("form1[0].#subform[3].preparerssignaturedate[0]", parameters[18]);
            
            pdfStamper.FormFlattening = false;
            pdfStamper.Close();
        }
    }
}
