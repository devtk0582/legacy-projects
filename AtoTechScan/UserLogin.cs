using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.Barcode2;
using System.Xml.Linq;

namespace AtoTechScan
{
    public partial class UserLogin : Form
    {
        Helper objHelper;
        string strUserResult = string.Empty;
        string strUserName = string.Empty;
        string strUserActive = string.Empty;
        string strUserInfo = string.Empty;
        string strCustomer = string.Empty;
        XDocument xmlMessage = XDocument.Load(@"\Program Files\AtoTechScan\Message.xml");

        public UserLogin()
        {
            InitializeComponent();
        }
       
        private void UserLogin_Load(object sender, EventArgs e)
        {
            try
            {
                objHelper = new Helper();
                txtUserID.Text = "";
                txtPwd.Text = "";
                txtUserID.Focus();
              
            }
            catch (Exception ex)
            {
                Helper.LogError(ex,"UserLogin_Load");
            }
        }

        //private void MyReader_OnScan(ScanDataCollection sd)
        //{
        //    try
        //    {
        //        if (this.InvokeRequired)
        //        {
        //            this.BeginInvoke(new Barcode2.OnScanHandler(MyReader_OnScan), new object[] { sd });
        //        }
        //        else
        //        {
        //            foreach (ScanData data in sd)
        //            {
        //                if (data.Result == Results.SUCCESS)
        //                {
        //                    strUserBarcode = data.Text;

        //                    strUserInfo = objHelper.checkUserBarcode(strUserBarcode);
        //                    if (strUserInfo != String.Empty)
        //                    {
        //                        MyReader.OnScan -= eventHandler;
        //                        if (MyReader != null)
        //                            MyReader.Dispose();
        //                        Customer fCustomer = new Customer(this);
        //                        fCustomer.UserName = strUserInfo.Split('-')[0];
        //                        fCustomer.UserID = strUserInfo.Split('-')[1];
        //                        fCustomer.Show();
        //                        //this.txtUserID.Text = string.Empty;
        //                        this.txtPwd.Text = string.Empty;
        //                    }
        //                    else
        //                    {
        //                        var q = (from Message in xmlMessage.Elements("Message")
        //                                 where Message.Attribute("type").Value == "notValidUser"
        //                                 select Message).FirstOrDefault();

        //                        MessageBox.Show(q.Element("text").Value);
        //                        //MessageBox.Show("Not a valid User. Please try it again.");
        //                        MyReader.ScanBufferStart();
        //                    }
        //                }
        //                else
        //                {
        //                    MyReader.ScanBufferStart();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("UserLogin_Scan: " + ex.Message);
        //    }
        //}

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                strUserResult = objHelper.checkUserID(txtUserID.Text, txtPwd.Text);
                strUserName = strUserResult.Substring(0, strUserResult.IndexOf("/"));
                strUserActive = strUserResult.Substring(strUserResult.IndexOf("/") + 1);
                if (strUserName != String.Empty)
                {
                    if (strUserActive == "1")
                    {
                        Activity fActivity = new Activity(this);
                        fActivity.UserName = strUserName;
                        fActivity.UserID = txtUserID.Text;
                        strCustomer = objHelper.getCustomerInfo();
                        fActivity.CustName = strCustomer.Substring(0, strCustomer.IndexOf(","));
                        fActivity.LocationName = strCustomer.Substring(strCustomer.IndexOf(",") + 1);
                        Cursor.Current = Cursors.Default;
                        fActivity.Show();
                        this.txtPwd.Text = string.Empty;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The user ID you entered is a inactive user.");
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not a valid User. Please try it again.");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Helper.LogError(ex, "btnSubmit_Click");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtPwd.Text = "";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                base.OnClosing(e);
                Application.Exit();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex,"OnClosing");
            }
        }

    }
}