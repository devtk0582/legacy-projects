using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtoTechScan
{
    public partial class MainMenu : Form
    {
        private UserLogin mainForm;

        string m_UserName = string.Empty;
        string m_UserID = string.Empty;
        string m_CustName = string.Empty;
        string m_LocationName = string.Empty;
        string m_ItemActivity = string.Empty;

        #region "Property"
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        public string CustName
        {
            get { return m_CustName; }
            set { m_CustName = value; }
        }

        public string LocationName
        {
            get { return m_LocationName; }
            set { m_LocationName = value; }
        }

        public string ItemActivity
        {
            get { return m_ItemActivity; }
            set { m_ItemActivity = value; }
        }

        #endregion
       
        public MainMenu()
        {
            InitializeComponent();
        }

        public MainMenu(UserLogin mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                lbUser.Text += UserName;
                lbCustName.Text += CustName;
                lbLocationName.Text += LocationName;
                lbActivity.Text += ItemActivity;
            }
            catch(Exception ex)
            {
                MessageBox.Show("MainMenu_Load: " + ex.Message);
            }
        }

        private void btnChgCust_Click(object sender, EventArgs e)
        {
            try
            {
                Customer fCustomer = new Customer(this.mainForm);
                fCustomer.UserName = UserName;
                fCustomer.UserID = UserID;
                fCustomer.CustName = CustName;
                fCustomer.LocationName = LocationName;
                fCustomer.ItemActivity = ItemActivity;
                //fCustomer.MyReader = this.MyReader;
                fCustomer.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangeCust_Click: " + ex.Message);
            }
        }

        private void btnChgLocation_Click(object sender, EventArgs e)
        {
            try
            {
                Location fLocation = new Location(this.mainForm);
                fLocation.UserName = UserName;
                fLocation.UserID = UserID;
                fLocation.CustName = CustName;
                fLocation.LocationName = LocationName;
                fLocation.ItemActivity = ItemActivity;
                //fLocation.MyReader = this.MyReader;
                fLocation.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangeLocation_Click: " + ex.Message);
            }
        }

        private void btnChgActivity_Click(object sender, EventArgs e)
        {
            try
            {
                Activity fActivity = new Activity(this.mainForm);
                fActivity.UserName = UserName;
                fActivity.UserID = UserID;
                fActivity.CustName = CustName;
                fActivity.LocationName = LocationName;
                fActivity.ItemActivity = ItemActivity;
                //fLocation.MyReader = this.MyReader;
                fActivity.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangeActivity_Click: " + ex.Message);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                ScanItem fScanItem = new ScanItem(this.mainForm);
                fScanItem.UserID = UserID;
                fScanItem.UserName = UserName;
                fScanItem.CustName = CustName;
                fScanItem.LocationName = LocationName;
                fScanItem.ItemActivity = ItemActivity;
                //fScanItem.MyReader = this.MyReader;
                fScanItem.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Scan_Click: " + ex.Message);
            }
        }

        private void llLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UserID = string.Empty;
                UserName = string.Empty;
                CustName = string.Empty;
                LocationName = string.Empty;
                ItemActivity = string.Empty;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Main_Logout: " + ex.Message);
            }
        }

    }
}