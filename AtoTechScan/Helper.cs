using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Xml;



namespace AtoTechScan
{
    class Helper
    {
       // string strConn = @"Data Source=\Program Files\AtoTech\Database\AtoTechScanDB.sdf;Persist Security Info=True;";
        public readonly static string strConn = @"Data Source=\Program Files\AtoTechScan\AtoTechScanDB.sdf;Persist Security Info=True;";
        readonly static string strLog = @"\Program Files\AtoTechScan\ScanLog.xml";

        public string checkUserID(string strUserID, string strPassword)
        {
            string strUserName = string.Empty;
            string userActive = string.Empty;
            try
            {
                using (SqlCeConnection con = new SqlCeConnection(strConn))
                {
                    con.Open();
                    string strcmd = "select FirstName, LastName, Active from Users where UserID= '" + strUserID + "' and UserPwd = '" + strPassword + "'";

                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    SqlCeDataReader dr = default(SqlCeDataReader);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr[0]!= DBNull.Value)
                        {
                            strUserName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                            userActive = dr["Active"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strUserName + "/" +userActive;
        }

        public DataSet getActivityInfo()
        {
            DataSet dsActivity = new DataSet();
            try
            {
                using (SqlCeConnection con = new SqlCeConnection(strConn))
                {
                    con.Open();
                    string strcmd = "select * from Activity order by ActivityDesc";
                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                    da.Fill(dsActivity);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                dsActivity = null;
                throw ex;
            }
            return dsActivity;
        }

        public string checkScanItem(string itemNum, string batchNum, string itemActivity, SqlCeConnection con)
        {
            string strMasterAction = string.Empty;
            try
            {
                    SqlCeDataReader dr1 = default(SqlCeDataReader);
                    string strcmd1 = "select ItemNum, BatchNum from ItemMaster where ItemNum = '" + itemNum + "' and BatchNum = '" + batchNum + "'" ;
                    SqlCeCommand cmd1 = new SqlCeCommand(strcmd1, con);
                    
                    dr1 = cmd1.ExecuteReader();

                    if (dr1.Read())
                    {
                        strMasterAction = "N";
                    }
                    else
                    {
                        string strcmd2 = "select ItemNum from ItemMaster where ItemNum = '" + itemNum + "'";
                        SqlCeCommand cmd2 = new SqlCeCommand(strcmd2, con);
                        SqlCeDataReader dr2 = default(SqlCeDataReader);
                        dr2 = cmd2.ExecuteReader();
                        if (dr2.Read())
                        {
                            if (itemActivity == "S")
                                strMasterAction = "A";
                            else
                                strMasterAction = "E";
                        }
                        else
                            strMasterAction = "E";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strMasterAction;
        }

        //public string checkScanItemNum(string itemNum)
        //{
        //    string strMasterAction = string.Empty;
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "select ItemNum from ItemMaster where ItemNum = '" + itemNum +  "'";

        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataReader dr = default(SqlCeDataReader);
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                if (dr[0] == DBNull.Value)
        //                    strMasterAction = "E";
        //                else
        //                    strMasterAction = "A";
        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return strMasterAction;
        //}

        public string insertScanItem(string itemNum, string batchNum,  string custName, string locationName, string itemActivity, int itemQuantity, string scannerID, string userID, string masterAction, SqlCeConnection con)
        {
            string strItemID = string.Empty;
            try
            {
                    string strcmd1
                        = "insert into HScanItems(Activity, Quantity, CustomerName, LocationName, ScannerID, UserID, ScanDate, ItemNum, BatchNum,MasterAction) values ('" + itemActivity + "','" + itemQuantity + "','" + custName + "','" + locationName + "','" + scannerID + "','" + userID + "','" + System.DateTime.Now + "','" + itemNum + "','" + batchNum + "','" + masterAction + "')";
                    SqlCeCommand cmd1 = new SqlCeCommand(strcmd1, con);
                    cmd1.ExecuteNonQuery();
                    //if (currentItemID == "")
                    //    strItemID = "1";
                    //else
                    //{
                    //    strItemID = (Int32.Parse(currentItemID) + 1).ToString();
                    //}
                    string strcmd2 = "SELECT @@IDENTITY";
                    SqlCeCommand cmd2 = new SqlCeCommand(strcmd2, con);
                    strItemID = cmd2.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                strItemID = string.Empty;
                throw ex;
            }
            return strItemID;
        }

        //public string getLatestItemID()
        //{
        //    string strCurrentItem = String.Empty;
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "select top(1) ItemID from HScanItems order by ItemID desc";
        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataReader dr = default(SqlCeDataReader);
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                if (dr[0] != DBNull.Value)
        //                {
        //                    strCurrentItem = dr["ItemID"].ToString();

        //                }
        //                else
        //                {
        //                    strCurrentItem = "0";
        //                }
        //            }
        //            else
        //            {
        //                strCurrentItem = "0";
        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return strCurrentItem;
        //}

        public void updateItemQuantity(string currentItem, string itemQuantity, SqlCeConnection con)
        {
            string strUserName = String.Empty;
            try
            {
                    string strcmd = "update HScanItems set Quantity = '" + itemQuantity + "' where ItemID = '" + currentItem + "'";

                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getCustomerInfo()
        {
            string strCustomer = String.Empty;
            try
            {
                using (SqlCeConnection con = new SqlCeConnection(strConn))
                {
                    con.Open();
                    string strcmd = "select CustomerName, LocationName from Customers";
                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    SqlCeDataReader dr = default(SqlCeDataReader);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr[0] != DBNull.Value)
                        {
                            strCustomer = dr["CustomerName"].ToString() + "," + dr["LocationName"].ToString();

                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strCustomer;
        }

        public DataSet getScanData(SqlCeConnection con)
        {
            DataSet dsScanData = new DataSet();
            try
            {
                    string strcmd = "select ItemID, ItemNum as MaterialNo, BatchNum as BatchNo, Quantity, ScanDate from HScanItems order by ItemID";
                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                    da.Fill(dsScanData);

            }
            catch (Exception ex)
            {
                dsScanData = null;
                throw ex;
            }
            return dsScanData;
        }

        public void deleteScanRecord(int itemID, SqlCeConnection con)
        {
            try
            {

                    string strcmd = "delete from HScanItems where ItemID = '" + itemID + "'";
                    SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static class opacity
        {

	        public struct BlendFunction
	        {
		        public byte BlendOp;
		        public byte BlendFlags;
		        public byte SourceConstantAlpha;
		        public byte AlphaFormat;
	        }
	        [DllImport("Coredll.dll", EntryPoint = "AlphaBlend", CharSet = CharSet.Auto, SetLastError = true)]
	        public static extern Int32 AlphaBlendCE(IntPtr hdcDest, Int32 xDest, Int32 yDest, Int32 cxDest, Int32 cyDest, IntPtr hdcSrc, Int32 xSrc, Int32 ySrc, Int32 cxSrc, Int32 cySrc,
	        BlendFunction blendFunction);


	        public enum BlendOperation : byte
	        {
		        AC_SRC_OVER = 0x0
	        }

	        public enum BlendFlags : byte
	        {
		        Zero = 0x0
	        }

	        public enum SourceConstantAlpha : byte
	        {
		        Transparent = 0x0,
		        Opaque = 0xff
	        }

	        public enum AlphaFormat : byte
	        {
		        AC_SRC_ALPHA = 0x0
	        }

	        public static void DrawAlpha(Graphics gx, Bitmap image, byte transp, int x, int y)
	        {
		        try {
			        using (Graphics gxSrc = Graphics.FromImage(image)) {
				        IntPtr hdcDst = gx.GetHdc();
				        IntPtr hdcSrc = gxSrc.GetHdc();
				        BlendFunction bf = new BlendFunction();
				        bf.BlendOp = Convert.ToByte(BlendOperation.AC_SRC_OVER);
				        bf.BlendFlags = Convert.ToByte(BlendFlags.Zero);
				        bf.SourceConstantAlpha = transp;
				        bf.AlphaFormat = Convert.ToByte(AlphaFormat.AC_SRC_ALPHA);

				        AlphaBlendCE(hdcDst, x, y, image.Width, image.Height, hdcSrc, 0, 0, image.Width, image.Height,
				        bf);

				        gx.ReleaseHdc(hdcDst);
				        gxSrc.ReleaseHdc(hdcSrc);
			        }
		        } catch (Exception ex) {
			        MessageBox.Show(ex.Message);
		        }
	        }

        }

        //public string checkUserBarcode(string strUserBarcode)
        //{
        //    string strUserInfo = String.Empty;
        //    try 
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn)) {
        //            con.Open();
        //            string strcmd = "select FirstName, LastName, UserID from Users where UserBarCode= '" + strUserBarcode + "'";

        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataReader dr = default(SqlCeDataReader);
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                if (dr[0] != DBNull.Value)
        //                {
        //                    strUserInfo = dr["FirstName"].ToString() + " " + dr["LastName"].ToString() + "-" + dr["UserID"];

        //                }

        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        throw ex;
        //    }
        //    return strUserInfo;
        //}

        //public void InsertConfig(string IP, string Action)
        //{
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string query = string.Empty;
        //            if (Action == "Add")
        //            {
        //                query = "insert into Config(IP) values('" + IP + "')";
        //            }
        //            else if (Action == "Update")
        //            {
        //                query = "update Config set IP ='" + IP + "'";
        //            }
        //            SqlCeCommand cmd = new SqlCeCommand(query, con);
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool HasConfig()
        //{
        //    bool flag = false;
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string query = "select * from Config";
        //            SqlCeCommand cmd = new SqlCeCommand(query, con);
        //            SqlCeDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                if (reader[0].ToString() != string.Empty)
        //                    flag = true;
        //            }
        //            con.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return flag;
        //}

        //public string GetConfig()
        //{
        //    string strHost = "";
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string query = "select * from Config";
        //            SqlCeCommand cmd = new SqlCeCommand(query, con);
        //            SqlCeDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                strHost = reader[0].ToString();
        //                //strUrl = @"http://" + reader[0].ToString() + @"/WebServices/AtoTechWebService.asmx";
        //            }
        //            con.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return strHost;
        //}

        //public DataSet getCustInfo()
        //{

        //    DataSet dsCust = new DataSet();
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "select * from Customer order by CustName";
        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
        //            da.Fill(dsCust);
        //            con.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        dsCust = null;
        //        throw ex;
        //        //strRetVal = "Exception" + "Error retrving in this method GetRankList() " + "," + ex.ToString();
        //    }
        //    return dsCust;
        //}

        //public DataSet getLocationInfo()
        //{
           
        //    DataSet dsLocation = new DataSet();
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "select * from Location order by LocationName";
        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
        //            da.Fill(dsLocation);
        //            con.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        dsLocation = null;
        //        throw ex;
        //        //strRetVal = "Exception" + "Error retrving in this method GetRankList() " + "," + ex.ToString();
        //    }
        //    return dsLocation;
        //}

        //public bool checkCustNum(string strCustNum)
        //{
        //    bool chkCustNum = false;
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "select * from Customer where CustNum= '" + strCustNum + "'";

        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
        //            SqlCeDataReader dr = default(SqlCeDataReader);
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                if (dr[0] != DBNull.Value)
        //                {
        //                    chkCustNum = true;
        //                }

        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //strRetVal = "Exception" + "Error retrving in this method GetItem() " + "," + ex.ToString();
        //    }
        //    return chkCustNum;
        //}

        //public void addCustNew(string strCustNum, string strCustName)
        //{
        //    try
        //    {
        //        using (SqlCeConnection con = new SqlCeConnection(strConn))
        //        {
        //            con.Open();
        //            string strcmd = "insert into Customer(CustNum, CustName) values '" + strCustNum + "'," + "'" + strCustName + "'";

        //            SqlCeCommand cmd = new SqlCeCommand(strcmd, con);
                    
        //            cmd.ExecuteNonQuery();
                   
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //strRetVal = "Exception" + "Error retrving in this method GetItem() " + "," + ex.ToString();
        //    }
           
        //}

        public static string LogError(Exception ex, string location)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(strLog))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Exception");
                    writer.WriteStartElement("Message");
                    writer.WriteElementString("Content", ex.Message);
                    writer.WriteEndElement();
                    writer.WriteStartElement("InnerException");
                    writer.WriteElementString("Content", ex.InnerException.Message);
                    writer.WriteEndElement();
                    writer.WriteStartElement("StackTrace");
                    writer.WriteElementString("Content", ex.StackTrace);
                    writer.WriteEndElement();
                    writer.WriteStartElement("ExceptionTime");
                    writer.WriteElementString("Time", ex.StackTrace);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Location");
                    writer.WriteElementString("Content", location);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                return "";
            }
            catch (Exception excep)
            {
                return excep.Message;
            }
        }
    }
}