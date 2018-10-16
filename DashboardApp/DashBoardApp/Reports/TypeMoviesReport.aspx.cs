using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reports
{
    public partial class TypeMoviesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["type"] != null)
                        BindGrid(int.Parse(Request.QueryString["type"].ToString()));
                    else
                        BindGrid(0);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void dlReport_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {

        }

        protected void PdfExport_Click(object sender, EventArgs e)
        {

        }

        protected void txtExport_Click(object sender, EventArgs e)
        {

        }

        private void BindGrid(int typeId)
        {
            try
            {
                if (typeId != 0)
                {
                    lblType.Text = (new BOMovies()).GetMovieTypeByID(typeId).TypeName;
                }
                else
                {
                    lblType.Text = "All Types";
                }

                var typeMovies = (new BOMovies()).GetTypeMovies(typeId);
                if (typeMovies != null && typeMovies.Count > 0)
                {
                    dlReport.DataSource = typeMovies;
                    dlReport.DataBind();
                    lblDataMsg.Text = "";
                }
                else
                {
                    dlReport.DataSource = null;
                    dlReport.DataBind();
                    lblDataMsg.Text = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}