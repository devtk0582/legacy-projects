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
    public partial class ChangeAction : Form
    {
        private RollExam parentForm;

        public ChangeAction()
        {
            InitializeComponent();
        }

        public ChangeAction(RollExam form, string selectedAction, List<DC_ActionMaster> actionList)
        {
            InitializeComponent();
            parentForm = form;
            lbAction.DataSource = actionList;
            lbAction.DisplayMember = "ActionDesc";
            lbAction.ValueMember = "ActionID";
            if (!string.IsNullOrEmpty(selectedAction))
                lbAction.SelectedValue = selectedAction;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            parentForm.UpdateAction(lbAction.Text, lbAction.SelectedValue.ToString());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeAction_Load(object sender, EventArgs e)
        {
            lbAction.Focus();
        }

    }
}
