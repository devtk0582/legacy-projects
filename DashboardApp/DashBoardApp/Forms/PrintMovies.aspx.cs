using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;
using System.IO;

namespace DashBoardApp.Forms
{
    public partial class PrintMovies : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindMovieTypes();
                    BindYears();
                    BindMonths();
                    BindReviews();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindMovieTypes()
        {
            try
            {
                var types = (new BOMovies()).GetMovieTypes();

                ddlMovieTypes.DataSource = types;
                ddlMovieTypes.DataTextField = "TypeName";
                ddlMovieTypes.DataValueField = "TypeID";
                ddlMovieTypes.DataBind();
                ddlMovieTypes.Items.Insert(0, new ListItem("Select Type", "0"));
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindYears()
        {
            try
            {
                ddlYear.Items.Insert(0, new ListItem("2012", "2"));
                ddlYear.Items.Insert(0, new ListItem("2011", "1"));
                ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindMonths()
        {
            try
            {
                Dictionary<string, string> months = new Dictionary<string, string>();
                months.Add("JAN", "1");
                months.Add("FEB", "2");
                months.Add("MAR", "3");
                months.Add("APR", "4");
                months.Add("MAY", "5");
                months.Add("JUN", "6");
                months.Add("JUL", "7");
                months.Add("AUG", "8");
                months.Add("SEP", "9");
                months.Add("OCT", "10");
                months.Add("NOV", "11");
                months.Add("DEC", "12");

                ddlMonth.DataSource = months;
                ddlMonth.DataTextField = "Key";
                ddlMonth.DataValueField = "Value";
                ddlMonth.DataBind();

                ddlMonth.Items.Insert(0, new ListItem("Select Month", "0"));
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindReviews()
        {
            try
            {
                Dictionary<string, string> points = new Dictionary<string, string>();
                points.Add("1", "1");
                points.Add("2", "2");
                points.Add("3", "3");
                points.Add("4", "4");
                points.Add("5", "5");
                points.Add("6", "6");
                points.Add("7", "7");
                points.Add("8", "8");
                points.Add("9", "9");
                points.Add("10", "10");

                ddlReview.DataSource = points;
                ddlReview.DataTextField = "Key";
                ddlReview.DataValueField = "Value";
                ddlReview.DataBind();

                ddlReview.Items.Insert(0, new ListItem("Select Review", "0"));
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void lbGetPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> fileList = GeneratePDF();
                if (fileList != null)
                {
                    DLForm.DataSource = fileList;
                    DLForm.DataBind();
                    imgPreview.Visible = false;
                    iframePreview.Attributes["src"] = "../PDFFiles/" + Session["UserName"].ToString() + "/" + fileList.First().Value;
                    iframePreview.Visible = true;
                }
            }
            catch (Exception ex)
            {       
                lblErr.Text = ex.Message;
            }
        }

        private Dictionary<string, string> GeneratePDF()
        {
            try
            {
                Dictionary<string, string> fileList = new Dictionary<string,string>();
                List<string> files = new List<string>();
                string file = (new PDFGenerator()).PrintMovies(int.Parse(ddlMovieTypes.SelectedValue), int.Parse(ddlYear.SelectedItem.Text), int.Parse(ddlMonth.SelectedValue), int.Parse(ddlReview.SelectedValue), Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["PDFPath"].ToString()) + "\\" + Session["UserName"].ToString());
                //files.Add(file);
                //files.Add(file);
                //string mergedFile = (new PDFGenerator()).PrintMergedFile(files, Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["PDFPath"].ToString()) + "\\" + Session["UserName"].ToString());
                //fileList.Add("Movie Report", file);
                fileList.Add("Movie Report",file);
                //fileList.Add("Merged Report", mergedFile);
                return fileList;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return null;
            }
        }

        protected void DLForm_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Preview")
                {
                    imgPreview.Visible = false;
                    iframePreview.Attributes["src"] = "../PDFFiles/" + Session["UserName"].ToString() + "/" + ((HiddenField)e.Item.FindControl("hfPath")).Value;
                    iframePreview.Visible = true;
                    //lblHead.Text = ((Label)e.Item.FindControl("lblDoc_Type_Name")).Text.Trim();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void lbDownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                (new PDFGenerator()).PrintMovies(int.Parse(ddlMovieTypes.SelectedValue), int.Parse(ddlYear.SelectedItem.Text), int.Parse(ddlMonth.SelectedValue), int.Parse(ddlReview.SelectedValue), stream);

                byte[] _buffer = stream.ToArray();

                Response.Clear();

                Response.Buffer = true;
                Response.Cache.SetExpires(DateTime.Now.AddDays(1));
                Response.Cache.SetCacheability(HttpCacheability.Private);

                Response.AddHeader("Content-Disposition",
                                   "attachment; filename=Movie_Report_" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".pdf");

                Response.ContentType = "application/pdf";

                Response.BinaryWrite(_buffer);
                Response.End();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}