using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reporting
{
    public partial class TeamsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserID"] != null)
                    {
                        ViewState["SortExpression"] = "TeamName";
                        ViewState["SortDirection"] = "ASC";
                        BindTeams(string.Empty);
                        if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                            lbAddTeam.Visible = true;
                    }
                    else
                    {
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultURL"].ToString(), true);
                    }
                }
                catch (Exception ex)
                {
                    lblErrTeam.Text = ex.Message;
                }
            }
        }

        private void BindTeams(string searchValue)
        {
            try
            {
                List<TeamReport> teams;

                if (searchValue == "")
                    teams = (new BOTeams()).GetTeamsReport();
                else
                    teams = (new BOTeams()).GetTeamsReport(searchValue);

                if (teams == null)
                {
                    gvTeams.DataSource = null;
                    gvTeams.DataBind();
                }

                List<TeamReport> teamsSort = null;

                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                {
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "TeamName":
                                    teamsSort = teams.OrderBy(o => o.TeamName).ToList();
                                    break;
                                case "StateName":
                                    teamsSort = teams.OrderBy(o => o.StateName).ToList();
                                    break;
                                case "Arena":
                                    teamsSort = teams.OrderBy(o => o.Arena).ToList();
                                    break;
                                case "HeadCoach":
                                    teamsSort = teams.OrderBy(o => o.HeadCoach).ToList();
                                    break;
                                case "Champions":
                                    teamsSort = teams.OrderBy(o => o.Champions).ToList();
                                    break;
                                case "Founded":
                                    teamsSort = teams.OrderBy(o => o.Founded).ToList();
                                    break;

                            }
                        }
                        else
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "TeamName":
                                    teamsSort = teams.OrderByDescending(o => o.TeamName).ToList();
                                    break;
                                case "StateName":
                                    teamsSort = teams.OrderByDescending(o => o.StateName).ToList();
                                    break;
                                case "Arena":
                                    teamsSort = teams.OrderByDescending(o => o.Arena).ToList();
                                    break;
                                case "HeadCoach":
                                    teamsSort = teams.OrderByDescending(o => o.HeadCoach).ToList();
                                    break;
                                case "Champions":
                                    teamsSort = teams.OrderByDescending(o => o.Champions).ToList();
                                    break;
                                case "Founded":
                                    teamsSort = teams.OrderByDescending(o => o.Founded).ToList();
                                    break;

                            }
                        }
                        gvTeams.DataSource = teamsSort;
                        gvTeams.DataBind();
                    }
                }
                else
                {
                    gvTeams.DataSource = null;
                    gvTeams.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void gvTeams_Sorting(object sender, GridViewSortEventArgs e)
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
                gvTeams.EditIndex = -1;
                BindTeams(txtSearchTeam.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void gvTeams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditTeam")
                {
                    if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                        ucAddEditTeam.Popup(int.Parse(e.CommandArgument.ToString()), false);
                    else
                        ucAddEditTeam.Popup(int.Parse(e.CommandArgument.ToString()), true);
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
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void gvTeams_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }
            }
            catch (Exception ex)
            {
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void gvTeams_RowCreated(object sender, GridViewRowEventArgs e)
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
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void gvTeams_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTeams.PageIndex = e.NewPageIndex;
                BindTeams(txtSearchTeam.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrTeam.Text = ex.Message;
            }
        }

        protected void lbAddTeam_Click(object sender, EventArgs e)
        {
            ucAddEditTeam.Popup(0, false);
        }

        protected void lbSearchTeam_Click(object sender, EventArgs e)
        {
            BindTeams(txtSearchTeam.Text.Trim());
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchTeam.Text = string.Empty;
            BindTeams(string.Empty);
        }

        protected void ucAddEditTeam_SaveButtonClicked(object sender, EventArgs e)
        {
            txtSearchTeam.Text = string.Empty;
            BindTeams(string.Empty);
        }
    }
}