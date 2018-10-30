using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace AtoTechScan
{
    public partial class popupManualEntry : Form
    {
        string ItemBarCode = string.Empty;
        string ItemNum = string.Empty;
        string BatchNum = string.Empty;
        string strMasterAction = string.Empty;
        string strCurrentItem = "";
        string strMEBarCode = string.Empty;

        string m_CurrentItemID = string.Empty;

        ScanItem objScanItem = new ScanItem();

        #region "Property"
        public string CurrentItemID
        {
            get { return m_CurrentItemID; }
            set { m_CurrentItemID = value; }
        }

        #endregion


        public popupManualEntry()
        {
            InitializeComponent();
        }

        public popupManualEntry(ScanItem objScanItem)
        {
	        this.InitializeComponent();
            this.objScanItem = objScanItem;
        }

        private void popupManualEntry_Load(Object sender, EventArgs e)
        {
            txtMEItemNum.Focus();
        }


        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                using (Bitmap dimBackGround = new Bitmap(this.Width, this.Height))
                {
                    using (Graphics gxTemp = Graphics.FromImage(dimBackGround))
                    {
                        gxTemp.Clear(Color.Black);
                        AtoTechScan.Helper.opacity.DrawAlpha(e.Graphics, dimBackGround, 180, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "OnPaintBackground");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMEItemNum.Text != string.Empty && txtMEBatchNum.Text != string.Empty)
                {
                    strMEBarCode = txtMEItemNum.Text.Trim() + "/" + txtMEBatchNum.Text.Trim();
                    strCurrentItem = objScanItem.saveScanItem(strMEBarCode, CurrentItemID);
                    if (strCurrentItem != CurrentItemID)
                    {
                        txtMEItemNum.Text = string.Empty;
                        txtMEBatchNum.Text = string.Empty;
                        objScanItem.CurrentItemID = strCurrentItem;
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Please enter both Product Code and Batch Number.");
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "btnSubmit_Click");
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}