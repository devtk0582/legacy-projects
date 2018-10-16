using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.UserControls
{
    public partial class AddEditTeam : System.Web.UI.UserControl
    {
        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindTeamData(int teamId, bool readOnly)
        {
            try
            {
                Team team = (new BOTeams()).GetTeamByID(teamId);

                if (team != null)
                {
                    if (readOnly == true)
                    {
                        lblTeamName.Text = team.TeamName;
                        lblTeamState.Text = team.State == null ? "N / A" : team.State.StateName;
                        lblTeamStars.Text = team.TeamStars;
                        lblArena.Text = team.Arena;
                        lblHeadCoach.Text = team.HeadCoach;
                        lblChampions.Text = team.Champions.ToString();
                        lblFounded.Text = team.Founded.ToString();
                        
                        lbCancel.Text = "OK";
                        lblTitle.Text = "View Team Information";
                        lbSave.Visible = false;
                        addEditTbl.Visible = false;
                        readOnlyTbl.Visible = true;
                    }
                    else
                    {
                        txtTeamName.Text = team.TeamName;
                        ddlStates.DataSource = (new BOStates()).GetDDLStates();
                        ddlStates.DataTextField = "StateCode";
                        ddlStates.DataValueField = "StateID";
                        ddlStates.DataBind();
                        ddlStates.Items.Insert(0, new ListItem("No State", "0"));
                        if (team.State != null)
                            ddlStates.SelectedValue = team.State.StateID.ToString();
                        txtTeamStars.Text = team.TeamStars;
                        txtArena.Text = team.Arena;
                        txtHeadCoach.Text = team.HeadCoach;
                        txtChampions.Text = team.Champions.ToString();
                        txtFounded.Text = team.Founded.ToString();


                        lblTitle.Text = "Update Team";
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

        public void Popup(int teamId, bool readOnly)
        {
            try
            {
                upAddEditTeam.Visible = true;

                if (teamId == 0)
                {
                    hfTeamID.Value = "";
                    lblTitle.Text = "Add Team";
                    lbSave.Text = "Add";

                    ddlStates.DataSource = (new BOStates()).GetDDLStates();
                    ddlStates.DataTextField = "StateCode";
                    ddlStates.DataValueField = "StateID";
                    ddlStates.DataBind();
                    ddlStates.Items.Insert(0, new ListItem("No State", "0"));

                    addEditTbl.Visible = true;
                    readOnlyTbl.Visible = false;
                }
                else
                {
                    hfTeamID.Value = teamId.ToString();
                    BindTeamData(teamId, readOnly);
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
                if (txtTeamName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Team name should not be empty";
                    mpePopup.Show();
                    return;
                }

                int champions = 0, founded = 0;
                if (txtChampions.Text.Trim() == "")
                    txtChampions.Text = "0";

                if (txtFounded.Text.Trim() == "")
                    txtFounded.Text = "0";

                if (int.TryParse(txtChampions.Text.Trim(), out champions) == false)
                {
                    lblErrorMsg.Text = "Invalid champions number";
                    mpePopup.Show();
                    return;
                }

                if (int.TryParse(txtFounded.Text.Trim(), out founded) == false)
                {
                    lblErrorMsg.Text = "Invalid founded year";
                    mpePopup.Show();
                    return;
                }

                BOTeams boTeams = new BOTeams();
                if (hfTeamID.Value != "")
                {
                    boTeams.UpdateTeam(int.Parse(hfTeamID.Value), txtTeamName.Text.Trim(), int.Parse(ddlStates.SelectedValue), txtTeamStars.Text.Trim(),
                         txtArena.Text.Trim(), txtHeadCoach.Text.Trim(), champions, founded);
                }
                else
                {
                    boTeams.SaveTeam(txtTeamName.Text.Trim(), int.Parse(ddlStates.SelectedValue), txtTeamStars.Text.Trim(),
                         txtArena.Text.Trim(), txtHeadCoach.Text.Trim(), champions, founded);
                }

                ClearPanel();
                upAddEditTeam.Visible = false;
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
                upAddEditTeam.Visible = false;
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
            txtTeamName.Text = "";
            ddlStates.DataSource = null;
            ddlStates.DataBind();
            txtTeamStars.Text = "";
            txtArena.Text = "";
            txtHeadCoach.Text = "";
            txtChampions.Text = "";
            txtFounded.Text = "";

            lblTeamName.Text = "";
            lblTeamState.Text = "";
            lblTeamStars.Text = "";
            lblArena.Text = "";
            lblHeadCoach.Text = "";
            lblChampions.Text = "";
            lblFounded.Text = "";

            hfTeamID.Value = "";
            lblErrorMsg.Text = "";
        }
    }
}