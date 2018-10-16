using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reporting
{
    public partial class StatesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserID"] != null)
                    {
                        ViewState["SortExpression"] = "StateName";
                        ViewState["SortDirection"] = "ASC";
                        BindStates(string.Empty);
                        if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                            lbAddState.Visible = true;
                    }
                    else
                    {
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultURL"].ToString(), true);
                    }
                }
                catch (Exception ex)
                {
                    lblErrState.Text = ex.Message;
                }
            }
        }

        private void BindStates(string searchValue)
        {
            try
            {
                List<StateReport> states;

                if (searchValue == "")
                    states = (new BOStates()).GetStatesReport();
                else
                    states = (new BOStates()).GetStatesReport(searchValue);

                if (states == null)
                {
                    gvStates.DataSource = null;
                    gvStates.DataBind();
                }

                List<StateReport> statesSort = null;

                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                {
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "StateName":
                                    statesSort = states.OrderBy(o => o.StateName).ToList();
                                    break;
                                case "StateCode":
                                    statesSort = states.OrderBy(o => o.StateCode).ToList();
                                    break;
                                case "Area":
                                    statesSort = states.OrderBy(o => o.Area).ToList();
                                    break;
                                case "Population":
                                    statesSort = states.OrderBy(o => o.Population).ToList();
                                    break;
                                
                            }
                        }
                        else
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "StateName":
                                    statesSort = states.OrderByDescending(o => o.StateName).ToList();
                                    break;
                                case "StateCode":
                                    statesSort = states.OrderByDescending(o => o.StateCode).ToList();
                                    break;
                                case "Area":
                                    statesSort = states.OrderByDescending(o => o.Area).ToList();
                                    break;
                                case "Population":
                                    statesSort = states.OrderByDescending(o => o.Population).ToList();
                                    break;
                            }
                        }
                        gvStates.DataSource = statesSort;
                        gvStates.DataBind();
                    }
                }
                else
                {
                    gvStates.DataSource = null;
                    gvStates.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrState.Text = ex.Message;
            }
        }

        protected void gvStates_Sorting(object sender, GridViewSortEventArgs e)
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
                gvStates.EditIndex = -1;
                BindStates(txtSearchState.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrState.Text = ex.Message;
            }
        }

        protected void gvStates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditState")
                {
                    if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                        ucAddEditState.Popup(int.Parse(e.CommandArgument.ToString()), false);
                    else
                        ucAddEditState.Popup(int.Parse(e.CommandArgument.ToString()), true);
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
                lblErrState.Text = ex.Message;
            }
        }

        protected void gvStates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   

                }
            }
            catch (Exception ex)
            {
                lblErrState.Text = ex.Message;
            }
        }

        protected void gvStates_RowCreated(object sender, GridViewRowEventArgs e)
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
                lblErrState.Text = ex.Message;
            }
        }

        protected void gvStates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvStates.PageIndex = e.NewPageIndex;
                BindStates(txtSearchState.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrState.Text = ex.Message;
            }
        }

        protected void lbAddState_Click(object sender, EventArgs e)
        {
            ucAddEditState.Popup(0, false);
        }

        protected void lbSearchState_Click(object sender, EventArgs e)
        {
            BindStates(txtSearchState.Text.Trim());
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchState.Text = string.Empty;
            BindStates(string.Empty);
        }

        protected void ucAddEditState_SaveButtonClicked(object sender, EventArgs e)
        {
            txtSearchState.Text = string.Empty;
            BindStates(string.Empty);
        }
    }
}