using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicCutter
{
    public partial class ChangeSide : Form
    {
        private RollExam parentForm;

        public ChangeSide()
        {
            InitializeComponent();
        }

        public ChangeSide(RollExam form, string selectedSide)
        {
            InitializeComponent();
            parentForm = form;
            if (!string.IsNullOrEmpty(selectedSide))
                lbSide.Text = selectedSide;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            parentForm.UpdateSide(lbSide.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeSide_Load(object sender, EventArgs e)
        {
            lbSide.Focus();
        }


    }
}
