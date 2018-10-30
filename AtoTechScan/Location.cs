using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.Barcode2;

namespace AtoTechScan
{
    public partial class Location : Form
    {
        public Barcode2 MyReader;
        public Barcode2.OnScanHandler eventHandler { get; set; }

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

        public Location()
        {
            InitializeComponent();
        }

        public Location(UserLogin mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }


        private void Location_Load(object sender, EventArgs e)
        {
            try
            {
                lbUser.Text += UserName;
                lbCustName.Text += CustName;
                lbLocationName.Text += LocationName;
                lbActivity.Text += ItemActivity;

                MyReader = new Barcode2();
                eventHandler = new Barcode2.OnScanHandler(MyReader_OnScan);
                MyReader.OnScan += eventHandler;
                MyReader.ScanBufferStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_Load: " + ex.Message);
            }
        }

        private void MyReader_OnScan(ScanDataCollection sd)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Barcode2.OnScanHandler(MyReader_OnScan), new object[] { sd });
                }
                else
                {
                    foreach (ScanData data in sd)
                    {
                        if (data.Result == Results.SUCCESS)
                        {
                            this.LocationName = data.Text;

                            MyReader.OnScan -= eventHandler;
                            if (MyReader != null)
                                MyReader.Dispose();

                            Activity fActivity = new Activity(this.mainForm);
                            fActivity.UserName = UserName;
                            fActivity.UserID = UserID;
                            fActivity.CustName = CustName;
                            fActivity.LocationName = LocationName;
                            fActivity.ItemActivity = ItemActivity;
                            fActivity.Show();

                            this.Close();
                        }
                        else if (data.Result == Results.CANCELED)
                        {
                            MyReader.ScanBufferStart();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_Scan: " + ex.Message);
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Activity fActivity = new Activity(this.mainForm);
                MyReader.OnScan -= eventHandler;
                if (MyReader != null)
                    MyReader.Dispose();
                fActivity.UserName = UserName;
                fActivity.UserID = UserID;
                fActivity.CustName = CustName;
                fActivity.LocationName = txtLocationName.Text;
                fActivity.ItemActivity = ItemActivity;
                fActivity.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_NextClick: " + ex.Message);
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            try
            {
                MainMenu fMainMenu = new MainMenu(this.mainForm);
                MyReader.OnScan -= eventHandler;
                if (MyReader != null)
                    MyReader.Dispose();
                fMainMenu.UserName = UserName;
                fMainMenu.UserID = UserID;
                fMainMenu.CustName = CustName;
                fMainMenu.LocationName = LocationName;
                fMainMenu.ItemActivity = ItemActivity;

                fMainMenu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_MainClick: " + ex.Message);
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
                if (MyReader != null)
                    MyReader.Dispose();
                //mainForm.MyReader.ScanBufferStart();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_Logout: " + ex.Message);
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                if (MyReader != null)
                    MyReader.Dispose();
                //mainForm.MyReader.ScanBufferStart();
                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location_Closing: " + ex.Message);
            }
        }

    }
}