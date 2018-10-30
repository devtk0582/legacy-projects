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

    public partial class Activity : Form
    {

        private UserLogin mainForm;

        string m_UserName = string.Empty;
        string m_UserID = string.Empty;
        string m_CustName = string.Empty;
        string m_LocationName = string.Empty;
        string m_ItemActivity = string.Empty;
        string m_ItemActivityID = string.Empty;

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

        public string ItemActivityID
        {
            get { return m_ItemActivityID; }
            set { m_ItemActivityID = value; }
        }

        #endregion

        public Activity()
        {
            InitializeComponent();
        }

        public Activity(UserLogin mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Activity_Load(object sender, EventArgs e)
        {
            try
            {
                FillActivity();
                lbUser.Text += UserName;
                lbCust.Text += CustName;
                lbLocation.Text += LocationName;
                lbActivity.Text += ItemActivity;
                if(ItemActivity == "")
                lbActivity.Visible = false;
            }
            catch (Exception ex)
            {
                Helper.LogError(ex,"Activity_Load");
            }
        }

        private void FillActivity()
        {
            DataSet dsActivity = new DataSet();
            try
            {
                Helper objHelper = new Helper();
                dsActivity = objHelper.getActivityInfo();
                if (dsActivity.Tables[0].Rows.Count > 0)
                {

                    //DataRow myNewRow;
                    //myNewRow = dsActivity.Tables[0].NewRow();
                    //myNewRow["ActivityID"] = "0";
                    //myNewRow["ActivityDesc"] = "Select One";
                    //dsActivity.Tables[0].Rows.InsertAt(myNewRow, 0);
                    this.cbActivity.Items.Clear();
                    this.cbActivity.DataSource = dsActivity.Tables[0];
                    this.cbActivity.DisplayMember = "ActivityDesc";
                    this.cbActivity.ValueMember = "ActivityID";
                }

            }

            catch (Exception ex)
            {
                Helper.LogError(ex, "FillActivity");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                ScanItem fScanItem = new ScanItem(this.mainForm);
                fScanItem.UserID = UserID;
                fScanItem.UserName = UserName;
                fScanItem.CustName = CustName;
                fScanItem.LocationName = LocationName;
                fScanItem.ItemActivity = cbActivity.Text;
                fScanItem.ItemActivityID = cbActivity.SelectedValue.ToString();
                fScanItem.Show();
                this.Close();

            }
            catch(Exception ex)
            {
                Helper.LogError(ex, "btnNext_Click");
            }
        }

        //private void btnMain_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MainMenu fMainMenu = new MainMenu(this.mainForm);
        //        fMainMenu.UserName = UserName;
        //        fMainMenu.UserID = UserID;
        //        fMainMenu.CustName = CustName;
        //        fMainMenu.LocationName = LocationName;
        //        fMainMenu.ItemActivity = ItemActivity;
        //        fMainMenu.Show();
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Activity_MainClick:" + ex.Message);
        //    }
        //}

        private void llLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UserID = string.Empty;
                UserName = string.Empty;
                CustName = string.Empty;
                LocationName = string.Empty;
                ItemActivity = string.Empty;
                //mainForm.MyReader.ScanBufferStart();
               
                this.Close();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "llLogout_Click");
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "OnClosing");
            }
        }  
    }
}