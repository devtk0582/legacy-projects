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
    public partial class ConfirmQueuedRoll : Form
    {
        private RollExam parentForm;

        public ConfirmQueuedRoll()
        {
            InitializeComponent();
        }

        public ConfirmQueuedRoll(RollExam form)
        {
            InitializeComponent();
            parentForm = form;
        }

        private void ConfirmQueuedRoll_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ConfirmQueuedRoll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }
    }
}
