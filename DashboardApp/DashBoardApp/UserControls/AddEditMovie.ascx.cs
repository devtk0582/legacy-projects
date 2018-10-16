using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.UserControls
{
    public partial class AddEditMovie : System.Web.UI.UserControl
    {
        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindMovieData(int movieId, bool readOnly)
        {
            try
            {
                Movie movie = (new BOMovies()).GetMovieByID(movieId);

                if (movie != null)
                {
                    if (readOnly == true)
                    {
                        lblMovieName.Text = movie.MovieName;
                        lblMovieType.Text = movie.MovieType1.TypeName;
                        lblMovieDate.Text = string.Format("{0:MM/dd/yyyyy}", movie.MovieDate);
                        lblMovieComment.Text = movie.MovieComment;
                        ratingMovieReviewRO.CurrentRating = movie.Review == null ? 0 : (int)movie.Review;
                        

                        lbCancel.Text = "OK";
                        lblTitle.Text = "View Movie Information";
                        lbSave.Visible = false;
                        addEditTbl.Visible = false;
                        readOnlyTbl.Visible = true;
                    }
                    else
                    {
                        txtMovieName.Text = movie.MovieName;
                        ddlMovieTypes.DataSource = (new BOMovies()).GetMovieTypes();
                        ddlMovieTypes.DataTextField = "TypeName";
                        ddlMovieTypes.DataValueField = "TypeID";
                        ddlMovieTypes.DataBind();
                        ddlMovieTypes.Items.Insert(0, new ListItem("Select Type", "0"));
                        
                        if(movie.MovieType1 != null)
                            ddlMovieTypes.SelectedValue = movie.MovieType1.TypeID.ToString();
                        txtMovieDate.Text = string.Format("{0:MM/dd/yyyyy}", movie.MovieDate);
                        txtMovieComment.Text = movie.MovieComment;
                        ratingMovieReview.CurrentRating = movie.Review == null ? 0 : (int)movie.Review;
                        
                        lblTitle.Text = "Update Movie";
                        lbSave.Text = "Update";
                        addEditTbl.Visible = true;
                        readOnlyTbl.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(int movieId, bool readOnly)
        {
            try
            {
                upAddEditMovie.Visible = true;

                if (movieId == 0)
                {
                    hfMovieID.Value = "";
                    lblTitle.Text = "Add Movie";
                    lbSave.Text = "Add";

                    ddlMovieTypes.DataSource = (new BOMovies()).GetMovieTypes();
                    ddlMovieTypes.DataTextField = "TypeName";
                    ddlMovieTypes.DataValueField = "TypeID";
                    ddlMovieTypes.DataBind();
                    ddlMovieTypes.Items.Insert(0, new ListItem("Select Type", "0"));

                    addEditTbl.Visible = true;
                    readOnlyTbl.Visible = false;
                }
                else
                {
                    hfMovieID.Value = movieId.ToString();
                    BindMovieData(movieId, readOnly);
                }

                mpePopup.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMovieName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Movie name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (ddlMovieTypes.SelectedIndex <= 0)
                {
                    lblErrorMsg.Text = "Please select movie type";
                    mpePopup.Show();
                    return;
                }

                if (txtMovieDate.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Movie date should not be empty";
                    mpePopup.Show();
                    return;
                }

                
                BOMovies boMovies = new BOMovies();
                if (hfMovieID.Value != "")
                {
                    boMovies.UpdateMovie(int.Parse(hfMovieID.Value),txtMovieName.Text.Trim(), int.Parse(ddlMovieTypes.SelectedValue),
                        txtMovieDate.Text.Trim(), txtMovieComment.Text.Trim(), ratingMovieReview.CurrentRating);
                }
                else
                {
                    boMovies.AddMovie(txtMovieName.Text.Trim(), int.Parse(ddlMovieTypes.SelectedValue),
                        txtMovieDate.Text.Trim(), txtMovieComment.Text.Trim(), ratingMovieReview.CurrentRating);
                }

                ClearPanel();
                upAddEditMovie.Visible = false;
                SaveButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPanel();
                upAddEditMovie.Visible = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        private void ClearPanel()
        {
            txtMovieName.Text = "";
            ddlMovieTypes.DataSource = null;
            ddlMovieTypes.DataBind();
            txtMovieDate.Text = "";
            txtMovieComment.Text = "";
            ratingMovieReview.CurrentRating = 0;

            lblMovieName.Text = "";
            lblMovieType.Text = "";
            lblMovieDate.Text = "";
            lblMovieComment.Text = "";
            ratingMovieReviewRO.CurrentRating = 0;

            hfMovieID.Value = "";
            lblErrorMsg.Text = "";
        }
    }
}