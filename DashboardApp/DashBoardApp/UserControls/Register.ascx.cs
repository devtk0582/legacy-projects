using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.UserControls
{
    public partial class Register : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblErrorMsg.Text = "";
            }
        }

        public void Popup()
        {
            try
            {
                upRegister.Visible = true;
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "First Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtLastName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Last Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtUserName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "User Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtPassword.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Password should not be empty";
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
                    lblErrorMsg.Text = "Email is not valid";
                    mpePopup.Show();
                    return;
                }


                (new BOUsers()).Register(txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim());

                ClearPanel();
                upRegister.Visible = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPanel();
                upRegister.Visible = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void ClearPanel()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            lblErrorMsg.Text = "";
        }
    }
}