using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashBoardApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblWelcome.Text = "Welcome, " + Session["UserName"].ToString();
                lblDate.Text = DateTime.Now.ToLongDateString();
            }
        }

        protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            //lblHeader.Text = e.Item.Value;
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultURL"].ToString(), true); 
        }
    }
}