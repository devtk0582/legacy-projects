using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reporting
{
    public partial class MoviesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserID"] != null)
                    {
                        ViewState["SortExpression"] = "MovieName";
                        ViewState["SortDirection"] = "ASC";
                        BindMovies(string.Empty);
                        if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                            lbAddMovie.Visible = true;
                    }
                    else
                    {
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultURL"].ToString(), true);
                    }
                }
                catch (Exception ex)
                {
                    lblErrMovie.Text = ex.Message;
                }
            }
        }

        private void BindMovies(string searchValue)
        {
            try
            {
                List<MovieReport> movies;

                if (searchValue == "")
                    movies = (new BOMovies()).GetMoviesReport();
                else
                    movies = (new BOMovies()).GetMoviesReport(searchValue);

                if (movies == null)
                {
                    gvMovies.DataSource = null;
                    gvMovies.DataBind();
                }

                List<MovieReport> moviesSort = null;

                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                {
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "MovieName":
                                    moviesSort = movies.OrderBy(o => o.MovieName).ToList();
                                    break;
                                case "TypeName":
                                    moviesSort = movies.OrderBy(o => o.TypeName).ToList();
                                    break;
                                case "MovieDate":
                                    moviesSort = movies.OrderBy(o => o.MovieDate).ToList();
                                    break;
                                case "MovieComment":
                                    moviesSort = movies.OrderBy(o => o.MovieComment).ToList();
                                    break;
                                case "Rating":
                                    moviesSort = movies.OrderBy(o => o.Rating).ToList();
                                    break;

                            }
                        }
                        else
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "MovieName":
                                    moviesSort = movies.OrderByDescending(o => o.MovieName).ToList();
                                    break;
                                case "TypeName":
                                    moviesSort = movies.OrderByDescending(o => o.TypeName).ToList();
                                    break;
                                case "MovieDate":
                                    moviesSort = movies.OrderByDescending(o => o.MovieDate).ToList();
                                    break;
                                case "MovieComment":
                                    moviesSort = movies.OrderByDescending(o => o.MovieComment).ToList();
                                    break;
                                case "Rating":
                                    moviesSort = movies.OrderByDescending(o => o.Rating).ToList();
                                    break;
                            }
                        }
                        gvMovies.DataSource = moviesSort;
                        gvMovies.DataBind();
                    }
                }
                else
                {
                    gvMovies.DataSource = null;
                    gvMovies.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void gvMovies_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortDirection"] != null)
                    if (ViewState["SortDirection"].ToString() == "DESC")
                    {
                        e.SortDirection = SortDirection.Ascending;
                        ViewState["SortDirection"] = "ASC";
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Descending;
                        ViewState["SortDirection"] = "DESC";
                    }
                else
                {
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        e.SortDirection = SortDirection.Descending;
                        ViewState["SortDirection"] = "DESC";
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Ascending;
                        ViewState["SortDirection"] = "ASC";
                    }
                }

                ViewState["SortExpression"] = e.SortExpression.ToString();
                gvMovies.EditIndex = -1;
                BindMovies(txtSearchMovie.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void gvMovies_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditMovie")
                {
                    if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                        ucAddEditMovie.Popup(int.Parse(e.CommandArgument.ToString()), false);
                    else
                        ucAddEditMovie.Popup(int.Parse(e.CommandArgument.ToString()), true);
                }
                else if (e.CommandName == "SetStatus")
                {
                    //hfCurrentID.Value = e.CommandArgument.ToString();
                    //bool currentStatus = ((LinkButton)e.CommandSource).Text == "Active" ? true : false;
                    //(new BOCountries()).SetCountryStatus(int.Parse(e.CommandArgument.ToString()), !currentStatus);
                    //txtSearchCountry.Text = string.Empty;
                    //BindCountries(string.Empty);
                }
            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void gvMovies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }
            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void gvMovies_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.Header)
                    foreach (TableCell tc in e.Row.Cells)
                    {
                        if (tc.HasControls())
                        {
                            LinkButton lnk = (LinkButton)tc.Controls[0];
                            if (ViewState["SortExpression"].ToString() == lnk.CommandArgument)
                            {
                                Image img = new Image();
                                img.Width = 10;
                                img.Height = 10;
                                img.ImageUrl = "~/images/arrow_" + (ViewState["SortDirection"].ToString() == "ASC" ? "up" : "down") + ".gif";
                                tc.Controls.Add(new LiteralControl(" "));
                                tc.Controls.Add(img);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void gvMovies_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMovies.PageIndex = e.NewPageIndex;
                BindMovies(txtSearchMovie.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrMovie.Text = ex.Message;
            }
        }

        protected void lbAddMovie_Click(object sender, EventArgs e)
        {
            ucAddEditMovie.Popup(0, false);
        }

        protected void lbSearchMovie_Click(object sender, EventArgs e)
        {
            BindMovies(txtSearchMovie.Text.Trim());
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchMovie.Text = string.Empty;
            BindMovies(string.Empty);
        }

        protected void ucAddEditMovie_SaveButtonClicked(object sender, EventArgs e)
        {
            txtSearchMovie.Text = string.Empty;
            BindMovies(string.Empty);
        }
    }
}