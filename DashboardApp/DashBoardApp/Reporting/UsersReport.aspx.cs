using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reporting
{
    public partial class UsersReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserID"] != null)
                    {
                        ViewState["SortExpression"] = "UserName";
                        ViewState["SortDirection"] = "ASC";
                        BindUsers(string.Empty);
                        if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                            lbAddUser.Visible = true;
                    }
                    else
                    {
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultURL"].ToString(), true);
                    }
                }
                catch (Exception ex)
                {
                    lblErrUser.Text = ex.Message;
                }
            }
        }

        private void BindUsers(string searchValue)
        {
            try
            {
                List<UserReport> users;
                
                if(searchValue == "")
                    users = (new BOUsers()).GetUsersReport(int.Parse(Session["UserID"].ToString()));
                else
                    users = (new BOUsers()).GetUsersReport(int.Parse(Session["UserID"].ToString()), searchValue);

                if (users == null)
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                }

                List<UserReport> usersSort = null;

                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                {
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "UserName":
                                    usersSort = users.OrderBy(o => o.UserName).ToList();
                                    break;
                                case "FirstName":
                                    usersSort = users.OrderBy(o => o.FirstName).ToList();
                                    break;
                                case "LastName":
                                    usersSort = users.OrderBy(o => o.LastName).ToList();
                                    break;
                                case "Email":
                                    usersSort = users.OrderBy(o => o.Email).ToList();
                                    break;
                                case "StateName":
                                    usersSort = users.OrderBy(o => o.StateName).ToList();
                                    break;
                                case "TeamName":
                                    usersSort = users.OrderBy(o => o.TeamName).ToList();
                                    break;
                            }
                        }
                        else
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "UserName":
                                    usersSort = users.OrderByDescending(o => o.UserName).ToList();
                                    break;
                                case "FirstName":
                                    usersSort = users.OrderByDescending(o => o.FirstName).ToList();
                                    break;
                                case "LastName":
                                    usersSort = users.OrderByDescending(o => o.LastName).ToList();
                                    break;
                                case "Email":
                                    usersSort = users.OrderByDescending(o => o.Email).ToList();
                                    break;
                                case "StateName":
                                    usersSort = users.OrderByDescending(o => o.StateName).ToList();
                                    break;
                                case "TeamName":
                                    usersSort = users.OrderByDescending(o => o.TeamName).ToList();
                                    break;
                            }
                        }
                        gvUsers.DataSource = usersSort;
                        gvUsers.DataBind();
                    }
                }
                else
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
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
                gvUsers.EditIndex = -1;
                BindUsers(txtSearchUser.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditUser")
                {
                    if(Session["Role"] != null && Session["Role"].ToString() == "ADMIN") 
                        ucAddEditUser.Popup(int.Parse(e.CommandArgument.ToString()), false);
                    else
                        ucAddEditUser.Popup(int.Parse(e.CommandArgument.ToString()), true);
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
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hfActive = (HiddenField)e.Row.FindControl("hfActive");
                    if (Convert.ToBoolean(hfActive.Value) == false)
                        e.Row.BackColor = System.Drawing.Color.Gray;

                }
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvUsers_RowCreated(object sender, GridViewRowEventArgs e)
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
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUsers.PageIndex = e.NewPageIndex;
                BindUsers(txtSearchUser.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void lbAddUser_Click(object sender, EventArgs e)
        {
            ucAddEditUser.Popup(0, false);
        }

        protected void lbSearchUser_Click(object sender, EventArgs e)
        {
            BindUsers(txtSearchUser.Text.Trim());
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchUser.Text = string.Empty;
            BindUsers(string.Empty);
        }

        protected void ucAddEditUser_SaveButtonClicked(object sender, EventArgs e)
        {
            txtSearchUser.Text = string.Empty;
            BindUsers(string.Empty);
        }
    }
}