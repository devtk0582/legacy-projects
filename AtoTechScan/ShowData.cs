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
    public partial class ShowData : Form
    {

        Helper objHelper = new Helper();
        DataSet dsScanData = null;
        ScanItem objScanItem = new ScanItem();
        SqlCeConnection conn;

        public ShowData()
        {
            InitializeComponent();
        }

        public ShowData(ScanItem objScanItem, SqlCeConnection con)
        {
	        this.InitializeComponent();
            this.objScanItem = objScanItem;
            this.conn = con;
        }

        private void ShowData_Load(object sender, EventArgs e)
        {
            try
            {
                dsScanData = objHelper.getScanData(conn);
                dgData.DataSource = dsScanData.Tables[0];
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "ShowData_Load");
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgData.Focused != true)
                {
                    MessageBox.Show("Please select a row");
                }
                else
                {
                    DataGridCell currentCell = dgData.CurrentCell;
                    int rowId = int.Parse(dgData[currentCell.RowNumber, 0].ToString());
                    DialogResult confirmResult = MessageBox.Show("Are you sure to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (confirmResult == DialogResult.Yes)
                    {
                        objHelper.deleteScanRecord(rowId, conn);
                        dgData.DataSource = objHelper.getScanData(conn).Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "menuDelete_Click");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgData_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgData.CurrentRowIndex >= 0)
                {
                    dgData.Select(dgData.CurrentRowIndex);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, "dgData_CurrentCellChanged");
            }
        }
    }
}