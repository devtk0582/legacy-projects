using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.UserControls
{
    public partial class AddEditState : System.Web.UI.UserControl
    {
        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindStateData(int stateId, bool readOnly)
        {
            try
            {
                State state = (new BOStates()).GetStateByID(stateId);

                if (state != null)
                {
                    if (readOnly == true)
                    {
                        lblStateName.Text = state.StateName;
                        lblStateCode.Text = state.StateCode;
                        lblArea.Text = state.Area.ToString();
                        lblPopulation.Text = state.Population.ToString();

                        lbCancel.Text = "OK";
                        lblTitle.Text = "View State Information";
                        lbSave.Visible = false;
                        addEditTbl.Visible = false;
                        readOnlyTbl.Visible = true;
                    }
                    else
                    {
                        txtStateName.Text = state.StateName;
                        txtStateCode.Text = state.StateCode;
                        txtArea.Text = state.Area.ToString();
                        txtPopulation.Text = state.Population.ToString();

                        
                        lblTitle.Text = "Update State";
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

        public void Popup(int stateId, bool readOnly)
        {
            try
            {
                upAddEditState.Visible = true;

                if (stateId == 0)
                {
                    hfStateID.Value = "";
                    lblTitle.Text = "Add State";
                    lbSave.Text = "Add";

                    
                    addEditTbl.Visible = true;
                    readOnlyTbl.Visible = false;
                }
                else
                {
                    hfStateID.Value = stateId.ToString();
                    BindStateData(stateId, readOnly);
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
                if (txtStateName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "State name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtStateCode.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "State code should not be empty";
                    mpePopup.Show();
                    return;
                }


                if (txtPopulation.Text.Trim() == "")
                {
                    txtPopulation.Text = "0";
                }

                int population = 0;

                if (int.TryParse(txtPopulation.Text.Trim(), out population) == false)
                {
                    lblErrorMsg.Text = "Invalid population number";
                    mpePopup.Show();
                    return;
                }

               
                BOStates boStates = new BOStates();
                if (hfStateID.Value != "")
                {
                    boStates.UpdateState(int.Parse(hfStateID.Value), txtStateName.Text.Trim(),
                        txtStateCode.Text.Trim(), txtArea.Text.Trim(), population);
                }
                else
                {
                    boStates.SaveState(txtStateName.Text.Trim(),
                       txtStateCode.Text.Trim(), txtArea.Text.Trim(), population);
                }

                ClearPanel();
                upAddEditState.Visible = false;
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
                upAddEditState.Visible = false;
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
            txtStateName.Text = "";
            txtStateCode.Text = "";
            txtArea.Text = "";
            txtPopulation.Text = "";

            lblStateName.Text = "";
            lblStateCode.Text = "";
            lblArea.Text = "";
            lblPopulation.Text = "";

            hfStateID.Value = "";
            lblErrorMsg.Text = "";
        }
    }
}