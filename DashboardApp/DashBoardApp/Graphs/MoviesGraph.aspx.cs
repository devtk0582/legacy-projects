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
    public partial class MoviesGraph : System.Web.UI.Page
    {
        Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnStartDate.Attributes.Add("onMouseOver", "this.style.cursor='hand';");
                    btnEndDate.Attributes.Add("onMouseOver", "this.style.cursor='hand';");

                    lbTitle.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                    lbTitle.Attributes.Add("onclick", "javascript:makeNewWindow('../Reports/TypeMoviesReport.aspx');");

                    OkButton.Attributes.Add("onclick", "javascript:return Validate('" + txtStartDate.ClientID + "','" + txtEndDate.ClientID + "','" + lblPopupErrMsg.ClientID + "');");

                    Bind(txtStartDate.Text.Trim(),txtEndDate.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }

        }

        private void Bind(string startDate, string endDate)
        {
            try
            {
                string strXml = "";
                FCChart.Text = "";
                FCChart.Visible = false;
                lblEmtMsg.Text = "No Data Found";

                List<TypeMovie> typeMovies = (new BOMovies()).GetMoviesGraph(startDate, endDate);
                if (typeMovies != null && typeMovies.Count > 0)
                {
                    strXml = "<graph caption='' rotateNames='0'  xAxisName='Type'   yAxisName='Movies Count' showNames='1' canvasBorderThickness='2' baseFontSize='12'  formatNumberScale='0' decimalPrecision='0' showValue='1'>";
                    FCChart.Visible = true;
                    lblEmtMsg.Text = "";
                    foreach (TypeMovie tm in typeMovies)
                    {
                        strXml = strXml + "<set name='" + tm.TypeName + "' value='" + tm.MoviesCount.ToString() + "' color='" + RandomRGBColor() + "' link='n-../Reports/TypeMoviesReport.aspx?type=" + tm.TypeID + "'  />";
                    }
                    strXml += "</graph>";
                    FCChart.Text = FusionCharts.RenderChart("../Charts/" + GraphTypeCheck(), "", strXml, "moviesGraph", "800", "400", false, true);

                    dgChart.DataSource = typeMovies;
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
                Bind(txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            Bind(txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
            popupChooseDate.Hide();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            popupChooseDate.Hide();
        }

        protected void lbChooseDate_Click(object sender, EventArgs e)
        {
            popupChooseDate.Show();
        }
    }
}