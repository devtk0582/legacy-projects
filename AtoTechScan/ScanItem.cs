using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.Barcode2;
using Symbol.ResourceCoordination;
using System.Data.SqlServerCe;

namespace AtoTechScan
{
   
    public partial class ScanItem : Form
    {

        public Barcode2 MyReader;
        public Barcode2.OnScanHandler eventHandler { get; set; }

        private UserLogin mainForm;
        private string strCurrentItem = "";
        private string strScannerID = new TerminalInfo().ESN;
        Helper objHelper;
        SqlCeConnection conn;

        string m_UserName = string.Empty;
        string m_UserID = string.Empty;
        string m_CustName = string.Empty;
        string m_LocationName = string.Empty;
        string m_ItemActivity = string.Empty;
        string m_ItemActivityID = string.Empty;
        string m_CurrentItemID = string.Empty;
      

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

        public string CurrentItemID
        {
            get { return m_CurrentItemID; }
            set { m_CurrentItemID = value; }
        }

        #endregion

        public ScanItem()
        {
            InitializeComponent();
        }

        public ScanItem(UserLogin mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }


        private void ScanItem_Load(object sender, EventArgs e)
        {
            try
            {
                objHelper = new Helper();
                lbCust.Text += CustName;
                lbLocation.Text += LocationName;
                lbActivity.Text += ItemActivity;
                lbQty.Visible = false;
                txtQty.Visible = false;
                txtQty.Text = String.Empty;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                //Demo
                //lbScanItem.Text = "ATO2202044-0055-4-000/NC1250";
                //lbQtyNum.Visible = true;
                //lbQtyNum.Text = "Qty: " + "1";

                //string ItemBarCode = string.Empty;
                //string ItemNum = string.Empty;
                //string BatchNum = string.Empty;
                //string strMasterAction = string.Empty;
                //string strNewItemID = string.Empty;
                //if (lbScanItem.Text.Substring(0, 3) == "ATO")
                //    ItemBarCode = lbScanItem.Text.Substring(3);
                //else
                //    ItemBarCode = lbScanItem.Text;
                //ItemNum = ItemBarCode.Substring(0, ItemBarCode.IndexOf("/"));
                //BatchNum = ItemBarCode.Substring(ItemBarCode.IndexOf("/") + 1);
                //strMasterAction = objHelper.checkScanItem(ItemNum, BatchNum, ItemActivityID);
                //if (strMasterAction == "E")
                //{
                //    MessageBox.Show("Invalid Item.");
                //}
                //else
                //{
                //    strNewItemID = objHelper.insertScanItem(ItemNum, BatchNum, CustName, LocationName, ItemActivityID, 1, strScannerID, UserID, strMasterAction);

                //    strCurrentItem = strNewItemID;
                //}

                conn = new SqlCeConnection(Helper.strConn);
                conn.Open();
                MyReader = new Barcode2();
                eventHandler = new Barcode2.OnScanHandler(MyReader_OnScan);
                MyReader.OnScan += eventHandler;
                MyReader.ScanBufferStart();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "ScanItem_Load");
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
                            strCurrentItem = saveScanItem(data.Text, strCurrentItem);
                            MyReader.ScanBufferStart();
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
                Helper.LogError(ex, "MyReader_OnScan");
            }
        }

        public string saveScanItem(string scanBarCode, string strLastItemID)
        {
            string ItemBarCode = string.Empty;
            string ItemNum = string.Empty;
            string BatchNum = string.Empty;
            string strMasterAction = string.Empty;
            string strNewItemID = string.Empty;

            try
            {
                if (scanBarCode.Substring(0, 3) == "ATO")
                    ItemBarCode = scanBarCode.Substring(3);
                else
                    ItemBarCode = scanBarCode;
                ItemNum = ItemBarCode.Substring(0, ItemBarCode.IndexOf("/"));
                BatchNum = ItemBarCode.Substring(ItemBarCode.IndexOf("/") + 1);

                strMasterAction = objHelper.checkScanItem(ItemNum, BatchNum, ItemActivityID, conn);
                if (strMasterAction == "E")
                {
                    MessageBox.Show("Invalid Item.");
                    strCurrentItem = strLastItemID;
                }
                else
                {
                    strNewItemID = objHelper.insertScanItem(ItemNum, BatchNum, CustName, LocationName, ItemActivityID, 1, strScannerID, UserID, strMasterAction, conn);
                    strCurrentItem = strNewItemID;
                    this.lbScanItem.Text = scanBarCode;
                    this.lbQtyNum.Text = "Qty: " + "1";        
                }
                return strCurrentItem;
            }
            catch(Exception ex)
            {
                strCurrentItem = strLastItemID;
                Helper.LogError(ex, "saveScanItem");
                return strCurrentItem;
                
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
        //        MyReader.OnScan -= eventHandler;
        //        if (MyReader != null)
        //            MyReader.Dispose();
        //        fMainMenu.Show();
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ScanItem_MainClick: " + ex.Message);
        //    }

        //}


        private void ScanItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Control == true) && (e.KeyCode == System.Windows.Forms.Keys.Q))  //Quantity update
                {
                    lbQty.Visible = true;
                    txtQty.Visible = true;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                    txtQty.Focus();
                }
                if ((e.Control == true) && (e.KeyCode == System.Windows.Forms.Keys.M))  //Manual entery
                {
                    popupManualEntry fManualEntry = new popupManualEntry(this);
                    fManualEntry.CurrentItemID = strCurrentItem;
                    DialogResult result = fManualEntry.ShowDialog();

                }
                if ((e.Control == true) && (e.KeyCode == System.Windows.Forms.Keys.R))  //Report
                {
                    ShowData fShowData = new ShowData(this, conn);
                    DialogResult result = fShowData.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "ScanItem_KeyDown");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (strCurrentItem != "")
                {
                    objHelper.updateItemQuantity(strCurrentItem, txtQty.Text.Trim(), conn);
                    lbQtyNum.Text = "Qty: " + txtQty.Text.Trim();
                    txtQty.Text = string.Empty;
                    lbQty.Visible = false;
                    txtQty.Visible = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    MessageBox.Show("There is no item to update quantity.");
                }
                
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "btnSave_Click");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lbQty.Visible = false;
            txtQty.Visible = false;
            txtQty.Text = string.Empty;
            btnSave.Visible = false;
            btnCancel.Visible = false;
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
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
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
                if (MyReader != null)
                    MyReader.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "OnClosing");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Activity fActivity = new Activity(mainForm);
                fActivity.UserName = UserName;
                fActivity.UserID = UserID;
                fActivity.CustName = CustName;
                fActivity.LocationName = LocationName;
                fActivity.ItemActivity = ItemActivity;
                fActivity.ItemActivityID = ItemActivityID;
                if (MyReader != null)
                    MyReader.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
                fActivity.Show();
                this.Close();
                
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "OnClosing");
            }
        }

      
    }
}