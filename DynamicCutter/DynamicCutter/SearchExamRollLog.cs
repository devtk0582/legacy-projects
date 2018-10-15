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
    public partial class SearchExamRollLog : Form
    {
        private RollExam parentForm;

        public SearchExamRollLog()
        {
            InitializeComponent();
        }

        public SearchExamRollLog(RollExam form)
        {
            InitializeComponent();
            parentForm = form;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int seqNum = 0;
            if (txtSEQNum.Text.Trim() != string.Empty && int.TryParse(txtSEQNum.Text.Trim(), out seqNum))
            {
                parentForm.BindExamRollLogData(seqNum);
                this.Close();
            }
            else
                MessageBox.Show("Invalid SEQ Number");
        }

        private void SearchExamRollLog_Load(object sender, EventArgs e)
        {
            txtSEQNum.Focus();
        }
    }
}
