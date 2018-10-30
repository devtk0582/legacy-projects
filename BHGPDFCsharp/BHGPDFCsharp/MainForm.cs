using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace BHGPDFCsharp
   {
       public partial class MainForm : Form
       {
        
           private StringBuilder sb = new StringBuilder(); 

           //new WCF service code , WCF client instance - andy
           private BHGService.BHGServiceClient m_serviceClient = new BHGService.BHGServiceClient();

           public MainForm()
           {
               InitializeComponent();
           }


           private void Form1_Load(object sender, EventArgs e)
           {
               //ListFieldNames();
               //FillForm();
           }
   
   
           /// <summary>
           /// List all of the form fields into a textbox.  The
           /// form fields identified can be used to map each of the
           /// fields in a PDF.
           /// </summary>
           private void ListFieldNames()
           {
               string pdfTemplate = @"c:\Temp\PDF\SBAForm1846.PDF";
               
   
               // title the form
               this.Text += " - " + pdfTemplate;
   
               // create a new PDF reader based on the PDF template document
               
               PdfReader pdfReader = new PdfReader(pdfTemplate);
   
               // create and populate a string builder with each of the 
               // field names available in the subject PDF
               StringBuilder sb = new StringBuilder();
               //foreach (DictionaryEntry de in pdfReader.AcroFields.Fields)
               //{
               //    sb.Append(de.Key.ToString() + Environment.NewLine);
               //}

               foreach (KeyValuePair<string, AcroFields.Item> de in pdfReader.AcroFields.Fields)
               {
                   sb.Append(de.Key.ToString() + Environment.NewLine);
               }
               // Write the string builder's content to the form's textbox
               textBox1.Text = sb.ToString();
               textBox1.SelectionStart = 0;
           }

           /// <summary>
           /// List all of the form fields into a textbox.  The
           /// form fields identified can be used to map each of the
           /// fields in a PDF.
           /// </summary>
           private void ListFieldNames1624()
           {
               string pdfTemplate = @"c:\Temp\PDF\SBAForm1624.PDF";


               // title the form
               this.Text += " - " + pdfTemplate;

               // create a new PDF reader based on the PDF template document

               PdfReader pdfReader = new PdfReader(pdfTemplate);

               // create and populate a string builder with each of the 
               // field names available in the subject PDF
               StringBuilder sb = new StringBuilder();
               //foreach (DictionaryEntry de in pdfReader.AcroFields.Fields)
               //{
               //    sb.Append(de.Key.ToString() + Environment.NewLine);
               //}

               foreach (KeyValuePair<string, AcroFields.Item> de in pdfReader.AcroFields.Fields)
               {
                   sb.Append(de.Key.ToString() + Environment.NewLine);
               }
               // Write the string builder's content to the form's textbox
               textBox1.Text = sb.ToString();
               textBox1.SelectionStart = 0;
           }
   
           private void FillForm()
           {
               string pdfTemplate = @"c:\Temp\PDF\SBAForm1846.pdf";
               string newFile = @"c:\Temp\PDF\completed_SBAForm1846.pdf";
   
               PdfReader pdfReader = new PdfReader(pdfTemplate);
               PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                           newFile, FileMode.Create));
               
               AcroFields pdfFormFields = pdfStamper.AcroFields;
   
               DataSet ds =  new DataSet();
        
                //old business layer code - without WCF service - commented Andy
               //ds = (new Form1846_BAL().Forms_BAL(null, null));
               
               //new WCF service code added to do the same - andy
               ds = m_serviceClient.GetForms1846_BAL(null, null);
               
               
               // set form pdfFormFields
               pdfFormFields.SetField("Date", ds.Tables[0].Rows[0]["DateEstablished"].ToString().Trim());
               pdfFormFields.SetField("Nametitle", ds.Tables[0].Rows[0]["CoName"].ToString().Trim());
               pdfFormFields.SetField("signature", ds.Tables[0].Rows[0]["CoDBA"].ToString().Trim());

               
              // report by reading values from completed PDF
               string sTmp = "SBAForm1846 Completed for " + pdfFormFields.GetField("Nametitle") + " " +
                  pdfFormFields.GetField("signature");
              MessageBox.Show(sTmp, "Finished");
  
              // flatten the form to remove editting options, set it to false
              // to leave the form open to subsequent manual edits
              pdfStamper.FormFlattening = false;
  
              // close the pdf
              pdfStamper.Close();
          }


           private void FillForm1624()
           {
               string pdfTemplate = @"c:\Temp\PDF\SBAForm1624.pdf";
               string newFile = @"c:\Temp\PDF\completed_SBAForm1624.pdf";

               PdfReader pdfReader = new PdfReader(pdfTemplate);
               PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                           newFile, FileMode.Create));

               AcroFields pdfFormFields = pdfStamper.AcroFields;

               DataSet ds = new DataSet();

               //old business layer code - without WCF service - commented Andy
               //ds = (new Form1846_BAL().Forms_BAL(null, null));

               //new WCF service code added to do the same - andy
               ds = m_serviceClient.GetForms1624_BAL(null, null);

               // set form pdfFormFields
               pdfFormFields.SetField("Date", ds.Tables[0].Rows[0]["DateEstablished"].ToString().Trim());
               pdfFormFields.SetField("businessname", ds.Tables[0].Rows[0]["CoName"].ToString().Trim());
               pdfFormFields.SetField("nametitle", ds.Tables[0].Rows[0]["CoBusinessType"].ToString().Trim());
               pdfFormFields.SetField("signature", ds.Tables[0].Rows[0]["CoName"].ToString().Trim());

               // report by reading values from completed PDF
               string sTmp = "SBAForm1624 Completed for " + pdfFormFields.GetField("Nametitle") + " " +
                  pdfFormFields.GetField("signature");
               MessageBox.Show(sTmp, "Finished");

               // flatten the form to remove editting options, set it to false
               // to leave the form open to subsequent manual edits
               pdfStamper.FormFlattening = false;

               // close the pdf
               pdfStamper.Close();
           }

           private void button1_Click(object sender, EventArgs e)
           {
               ListFieldNames();
               FillForm();
           }

           private void button2_Click(object sender, EventArgs e)
           {
               ListFieldNames1624();
               FillForm1624();
           }
  
      }
  }
