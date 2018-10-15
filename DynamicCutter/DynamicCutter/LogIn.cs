using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DynamicCutterBusinessLogic;

namespace DynamicCutter
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtLoginID.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user id");
                return;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter password");
            }

            BOUsers boUsers = new BOUsers(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            DC_User user = boUsers.AuthenticateUser(txtLoginID.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {
                AppContext.Login(user);
                RollExam examForm = new RollExam(this);
                examForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Record not found");
        }

    }
}
