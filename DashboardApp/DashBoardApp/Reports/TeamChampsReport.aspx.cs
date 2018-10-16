using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reports
{
    public partial class TeamChampsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["team"] != null)
                        BindGrid(int.Parse(Request.QueryString["team"].ToString()));
                    else
                        BindGrid(0);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void dlReport_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {

        }

        protected void PdfExport_Click(object sender, EventArgs e)
        {

        }

        protected void txtExport_Click(object sender, EventArgs e)
        {

        }

        private void BindGrid(int teamId)
        {
            try
            {
                if (teamId != 0)
                {
                    lblTeam.Text = (new BOTeams()).GetTeamByID(teamId).TeamName;
                }
                else
                {
                    lblTeam.Text = "All Types";
                }

                var teamChamps = (new BOTeams()).GetTeamChampions(teamId);
                if (teamChamps != null && teamChamps.Count > 0)
                {
                    dlReport.DataSource = teamChamps;
                    dlReport.DataBind();
                    lblDataMsg.Text = "";
                }
                else
                {
                    dlReport.DataSource = null;
                    dlReport.DataBind();
                    lblDataMsg.Text = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}