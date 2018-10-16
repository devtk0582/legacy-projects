using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;

namespace DashBoardApp.Reports
{
    public partial class StatePopulationReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["state"] != null)
                        BindGrid(int.Parse(Request.QueryString["state"].ToString()));
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

        private void BindGrid(int stateId)
        {
            try
            {
                if (stateId != 0)
                {
                    lblState.Text = (new BOStates()).GetStateByID(stateId).StateName;
                }
                else
                {
                    lblState.Text = "All States";
                }

                var statePopulations = (new BOStates()).GetStatePopulations(stateId);
                if (statePopulations != null && statePopulations.Count > 0)
                {
                    dlReport.DataSource = statePopulations;
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