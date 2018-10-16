using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashBoardBLL;
using InfoSoftGlobal;
using System.Drawing;

namespace DashBoardApp.Graphs
{
    public partial class TeamsGraph : System.Web.UI.Page
    {
        Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lbTitle.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                    lbTitle.Attributes.Add("onclick", "javascript:makeNewWindow('../Reports/TeamChampsReport.aspx');");
                    Bind();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }

        }

        private void Bind()
        {
            try
            {
                string strXml = "";
                FCChart.Text = "";
                FCChart.Visible = false;
                lblEmtMsg.Text = "No Data Found";

                List<TeamChamp> teamChamps = (new BOTeams()).GetTeamChampions();
                if (teamChamps != null && teamChamps.Count > 0)
                {
                    strXml = "<graph caption='' rotateNames='0'  xAxisName='Team Name'   yAxisName='Champions Count' showNames='1' canvasBorderThickness='2' baseFontSize='12'  formatNumberScale='0' decimalPrecision='0' showValue='1'>";
                    FCChart.Visible = true;
                    lblEmtMsg.Text = "";
                    foreach (TeamChamp tc in teamChamps)
                    {
                        strXml = strXml + "<set name='" + tc.TeamName + "' value='" + tc.Champions.ToString() + "' color='" + RandomRGBColor() + "' link='n-../Reports/TeamChampsReport.aspx?team=" + tc.TeamID + "'  />";
                    }
                    strXml += "</graph>";
                    FCChart.Text = FusionCharts.RenderChart("../Charts/" + GraphTypeCheck(), "", strXml, "teamsGraph", "800", "400", false, true);

                    dgChart.DataSource = teamChamps;
                    dgChart.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected string RandomRGBColor()
        {
            Color a = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

            return ColorTranslator.ToHtml(a).ToString();
        }

        private string GraphTypeCheck()
        {
            try
            {
                string strChartType = "";
                switch (rblChartTypes.SelectedValue)
                {
                    case "1":
                        strChartType = "FCF_Column3D.swf";
                        break;
                    case "2":
                        strChartType = "FCF_Bar2D.swf";
                        break;
                    case "3":
                        strChartType = "FCF_Line.swf";
                        break;
                    case "4":
                        strChartType = "FCF_Pie3D.swf";
                        break;
                    case "5":
                        strChartType = "FCF_Doughnut2D.swf";
                        break;
                    case "6":
                        strChartType = "FCF_Funnel.swf";
                        break;
                }
                return strChartType;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return "";
            }
        }

        protected void rblChartTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}