using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.UserControls
{
    public partial class AddEditUser : System.Web.UI.UserControl
    {
        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindUserData(int userId, bool readOnly)
        {
            try
            {
                User user = (new BOUsers()).GetUserByID(userId);

                if (user != null)
                {
                    if (readOnly == true)
                    {
                        lblUserName.Text = user.UserName;
                        lblFirstName.Text = user.FirstName;
                        lblLastName.Text = user.LastName;
                        lblEmail.Text = user.Email;
                        lblPhone.Text = user.Phone;
                        lblState.Text = user.State1 == null ? "N / A" : user.State1.StateName;
                        lblTeam.Text = user.Team1 == null ? "N / A" : user.Team1.TeamName;

                        lbCancel.Text = "OK";
                        lblTitle.Text = "View User Information";
                        lbSave.Visible = false;
                        addEditTbl.Visible = false;
                        readOnlyTbl.Visible = true;
                    }
                    else
                    {
                        txtUserName.Text = user.UserName;
                        txtFirstName.Text = user.FirstName;
                        txtLastName.Text = user.LastName;
                        txtEmail.Text = user.Email;
                        txtPhone.Text = user.Phone;

                        ddlStates.DataSource = (new BOStates()).GetDDLStates();
                        ddlStates.DataTextField = "StateName";
                        ddlStates.DataValueField = "StateID";
                        ddlStates.DataBind();
                        ddlStates.Items.Insert(0, new ListItem("No State", "0"));

                        if (user.State != null)
                            ddlStates.SelectedValue = user.State.ToString();

                        ddlTeams.DataSource = (new BOTeams()).GetDDLTeams();
                        ddlTeams.DataTextField = "TeamName";
                        ddlTeams.DataValueField = "TeamID";
                        ddlTeams.DataBind();
                        ddlTeams.Items.Insert(0, new ListItem("No Team", "0"));

                        if (user.Team != null)
                            ddlTeams.SelectedValue = user.Team.ToString();

                        lblTitle.Text = "Update User";
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

        public void Popup(int userId, bool readOnly)
        {
            try
            {
                upAddEditUser.Visible = true;

                if (userId == 0)
                {
                    hfUserID.Value = "";
                    lblTitle.Text = "Add User";
                    lbSave.Text = "Add";
                    
                    ddlStates.DataSource = (new BOStates()).GetDDLStates();
                    ddlStates.DataTextField = "StateName";
                    ddlStates.DataValueField = "StateID";
                    ddlStates.DataBind();
                    ddlStates.Items.Insert(0, new ListItem("No State", "0"));

                    ddlTeams.DataSource = (new BOTeams()).GetDDLTeams();
                    ddlTeams.DataTextField = "TeamName";
                    ddlTeams.DataValueField = "TeamID";
                    ddlTeams.DataBind();
                    ddlTeams.Items.Insert(0, new ListItem("No Team", "0"));

                    trPW.Visible = true;
                    trConfirmPW.Visible = true;
                    addEditTbl.Visible = true;
                    readOnlyTbl.Visible = false;
                }
                else
                {
                    hfUserID.Value = userId.ToString();
                    BindUserData(userId, readOnly);
                    trPW.Visible = false;
                    trConfirmPW.Visible = false;
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
                if (txtUserName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "User name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtFirstName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "First name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtLastName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Last name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Email should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (!BOValidation.IsEmail(txtEmail.Text.Trim()))
                {
                    lblErrorMsg.Text = "Invalid email address";
                    mpePopup.Show();
                    return;
                }

                BOUsers boUsers = new BOUsers();
                if (hfUserID.Value != "")
                {
                    boUsers.UpdateUser(int.Parse(hfUserID.Value), txtUserName.Text.Trim(), txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), int.Parse(ddlStates.SelectedValue), int.Parse(ddlTeams.SelectedValue));
                }
                else
                {
                    if (txtPW.Text.Trim() == "")
                    {
                        lblErrorMsg.Text = "Password should not be empty";
                        mpePopup.Show();
                        return;
                    }

                    boUsers.SaveUser(txtUserName.Text.Trim(), txtPW.Text.Trim(), txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), int.Parse(ddlStates.SelectedValue), int.Parse(ddlTeams.SelectedValue));
                }
                ClearPanel();
                upAddEditUser.Visible = false;
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
                upAddEditUser.Visible = false;
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
            txtUserName.Text = "";
            txtPW.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";

            ddlStates.DataSource = null;
            ddlStates.DataBind();
            ddlTeams.DataSource = null;
            ddlTeams.DataBind();

            lblUserName.Text = "";
            lblFirstName.Text = "";
            lblLastName.Text = "";
            lblEmail.Text = "";
            lblPhone.Text = "";
            lblState.Text = "";
            lblTeam.Text = "";

            hfUserID.Value = "";
            lblErrorMsg.Text = "";
        }

    }
}