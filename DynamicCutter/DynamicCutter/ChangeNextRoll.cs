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
    public partial class ChangeNextRoll : Form
    {
        private RollExam parentForm;

        public ChangeNextRoll()
        {
            InitializeComponent();
        }

        public ChangeNextRoll(RollExam form)
        {
            InitializeComponent();
            this.parentForm = form;
        }

        private void ChangeNextRoll_Load(object sender, EventArgs e)
        {
            lblScheduledNextRoll.Text = parentForm.RollInfo.NextRoll;
            txtNewNextRoll.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtNewNextRoll.Text.Trim()))
            {
                if ((new BORolls(parentForm.ConnectionString)).UpdateRollInfoByID(parentForm.RollInfo.NextRollID.Value, txtNewNextRoll.Text.Trim()))
                {
                    MessageBox.Show("Update Successfully");
                    //parentForm.UpdateNextRoll(txtNewNextRoll.Text.Trim());
                    this.Close();
                }
                else
                    MessageBox.Show("Update failed");
            }
            else
                MessageBox.Show("Please Enter New Next Roll");
        }
    }
}
