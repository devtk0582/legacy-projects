using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;
using System.IO;
using System.Net.Mail;

namespace DashBoardApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text == string.Empty)
                {
                    lblErrMessage.Text = "Please enter user name.";
                    return;
                }
                if (txtPWD.Text == string.Empty)
                {
                    lblErrMessage.Text = "Please enter password.";
                    return;
                }

                User user = (new BOUsers()).Authenticate(txtUserID.Text.Trim(), txtPWD.Text.Trim());

                lblErrMessage.Text = "";
                if (user == null)
                {
                    lblErrMessage.Text = "Username or password is invalid.";
                    txtPWD.Focus();
                    return;
                }
                else
                {
                    Session["UserID"] = user.UserID.ToString();
                    Session["UserName"] = user.FirstName + " " + user.LastName;
                    if (user.UserID.ToString() == System.Configuration.ConfigurationManager.AppSettings["AdminID"].ToString())
                        Session["Role"] = "ADMIN";
                    Response.Redirect("~/Home.aspx", false);

                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPWD.Text = "";
            txtUserID.Text = "";
        }

        protected void lbRetrival_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim() == "")
                {
                    lblpopupErrorMsg.Text = "Email address should not be empty.";
                    mpeForgotPW.Show();
                    return;
                }

                if (!BOValidation.IsEmail(txtEmail.Text.Trim()))
                {
                    lblpopupErrorMsg.Text = "Email address is not valid.";
                    mpeForgotPW.Show();
                    return;
                }


                RetrievePassword();
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }

        private void RetrievePassword()
        {
            try
            {
                User user = (new BOUsers()).GetUserByEmail(txtEmail.Text.Trim());
                if (user != null)
                {
                    SendEmail(user.FirstName + " " + user.LastName, user.Password, user.UserName);
                    ClearPopupPanel();
                    mpeForgotPW.Hide();
                }
                else
                {
                    lblpopupErrorMsg.Text = "The email you entered could not be found";
                    mpeForgotPW.Show();
                }
            }

            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }

        private void SendEmail(string name, string password, string username)
        {
            try
            {
                string fromAddress = "ketang@mydomain.com";
                string toAddress = txtEmail.Text.Trim();
                string subject = "";
                string body = "";
                MailMessage message;


                subject = "Password Request";
                body = GetMailBody(Server.MapPath("~/MailTemplates/ForgotPW.htm"), name, username, password);
                if (body != string.Empty)
                {
                    message = new MailMessage(fromAddress, toAddress, subject, body);
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.Normal;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
                mpeForgotPW.Show();
            }

        }

        private string GetTemplateContent(string templatePath)
        {
            try
            {
                string mailBody = String.Empty;

                using (StreamReader sr = new StreamReader(templatePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mailBody += line;
                    }
                }
                return mailBody;
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
                return string.Empty;
            }
        }

        public string GetMailBody(string templatePath, string name, string username, string password)
        {
            try
            {
                string mailBody = GetTemplateContent(templatePath);

                if (mailBody != string.Empty)
                {
                    mailBody = mailBody.Replace("[[txtName]]", name);
                    mailBody = mailBody.Replace("[[txtLogin]]", username);
                    mailBody = mailBody.Replace("[[txtPassword]]", password);
                }

                return mailBody;
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
                return string.Empty;
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            ClearPopupPanel();
            mpeForgotPW.Hide();
        }

        private void ClearPopupPanel()
        {
            txtEmail.Text = string.Empty;
            lblpopupErrorMsg.Text = string.Empty;
        }

        protected void lbRegister_Click(object sender, EventArgs e)
        {
            ucRegister.Popup();
        }

    }
}