using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DynamicCutterBusinessLogic;
using System.IO.Ports;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Threading;
using System.Diagnostics;

namespace DynamicCutter
{
    public partial class RollExam : Form
    {
        #region Variables
        private LogIn loginForm;
        private SerialPort serialPort = new SerialPort();
        private SqlConnection connection = null;
        private SqlCommand cmdExamRollLogs = null;
        private SqlCommand cmdCurrentUsers = null;
        private SqlCommand cmdRollInfo = null;
        private DataSet dsExamRollLogs = null;
        private DataSet dsCurrentUsers = null;
        private DataSet dsRollInfo = null;
        private List<string> defectCodeList;
        private List<DC_ActionMaster> actionList;
        int firstLogin = 1;
        #endregion

        #region Properties
        public bool IsPortOpen
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"].ToString();
            }
        }

        public int ReturnCounter
        {
            get;
            set;
        }

        public char CarriageReturn
        {
            get { return (char)0x0D; }
        }

        public string Side
        {
            get { return lblOperatorSide.Text; }
        }

        public GetRollInfoResult RollInfo
        {
            get;
            set;
        }

        public string Prefix
        {
            get;
            set;
        }

        public int UpdateRowIndex
        {
            get;
            set;
        }

        public ExamRollLogView CurrentExamRollLog
        {
            get;
            set;
        }

        public bool MUStatus
        {
            get;
            set;
        }

        public RollCommandType CurrentCommandType
        {
            get;
            set;
        }

        public bool IsMasterSide
        {
            get;
            set;
        }

        public int ExamRollLogCounter
        { get; set; }
        #endregion


        #region public methods
        public void UpdateNextRoll(string rollNumber)
        {
            RollInfo.NextRoll = rollNumber;
            lblNextRoll.Text = rollNumber;
        }

        public void BindExamRollLogData(int logID)
        {
            int selectFlag = 0;

            foreach (DataGridViewRow row in dgvExamLogs.Rows)
            {
                if (row.Cells[0].Value.ToString() == logID.ToString())
                {
                    selectFlag = 1;
                    row.Selected = true;
                    dgvExamLogs.CurrentCell = row.Cells[0];
                    break;
                }
            }
            if (selectFlag == 0)
            {
                MessageBox.Show("No record found.");
            }
        }

        public void UpdateSide(string side)
        {
            lblSide.Text = side;
        }

        public void UpdateAction(string action, string id)
        {
            lblAction.Text = action;
            lblAction.Tag = id;
        }

        #endregion

        public RollExam()
        {
            InitializeComponent();
        }

        public RollExam(LogIn form)
        {
            InitializeComponent();
            loginForm = form;
            this.ControlBox = false;
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            this.Size = new System.Drawing.Size(resolution.Width, resolution.Height);
            dgvExamLogs.Focus();

        }

        #region Menu Events

        private void clearScreenF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblAction.Text = "";
            lblAction.Tag = "1";
            nudGrade.Value = 0;
            nudShade.Value = 0;
            nudSpot.Value = 0;
            txtDefect.Text = "";
        }

        private void runF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int seqNum = 0;

            if (!defectCodeList.Contains(txtDefect.Text.Trim()))
            {
                MessageBox.Show("Invalid Defect Code");
                return;
            }

            if (!string.IsNullOrEmpty(txtSeqNo.Text) && int.TryParse(txtSeqNo.Text, out seqNum))
            {
                if (defectCodeList.Contains(txtDefect.Text.Trim()))
                {

                    UpdateRowIndex = dgvExamLogs.CurrentRow.Index;
                    (new BOExamRollLogs(ConnectionString)).UpdateExamRollLog(seqNum,
                        txtLMR.Text.Trim(),
                        nudGrade.Value.ToString(),
                        txtDefect.Text.Trim(),
                        nudSpot.Value.ToString(),
                        lblSide.Text.Substring(0, 1),
                        nudShade.Value.ToString(),
                        lblAction.Tag.ToString());
                    dgvExamLogs.Focus();
                    if (dgvExamLogs.SelectedRows.Count > 0)
                    {
                        dgvExamLogs.CurrentCell = dgvExamLogs[0, dgvExamLogs.SelectedRows[0].Index];
                        dgvExamLogs.CurrentCell.Selected = true;
                    }
                }
                else
                    MessageBox.Show("Invalid Defect Code");
            }
            else
                MessageBox.Show("Invalid SEQ Number");
        }

        private void verifyDeleteF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iSeqNum = 0;
            if (dgvExamLogs.SelectedRows.Count > 0)
            {
                iSeqNum = Int16.Parse(dgvExamLogs.SelectedRows[0].Cells[0].Value.ToString());
          
                if (MessageBox.Show("Are you sure to remove this record? SEQ #: " + iSeqNum, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    (new BOExamRollLogs(ConnectionString)).DeleteExamRollLogByLogID(iSeqNum);
                    MessageBox.Show("Current record has been deleted!");
                }
            }
            else
                MessageBox.Show("You must select one record first!");
        }

        private void verifyAbortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RollInfo != null && RollInfo.CurrentRollID.HasValue)
            {
                if (MessageBox.Show("Are you sure to remove the entire defect data for the current Roll?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    (new BOExamRollLogs(ConnectionString)).DeleteExamRollLogsbyRollID(RollInfo.CurrentRollID.Value);
                    //Move to Next Roll
                    ChangeToNextRoll();
                    ClearScreen();
                }
            }
            else
                MessageBox.Show("You must select one record first!");
        }

        private void exitF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BOUsers boUsers = new BOUsers(ConnectionString);
            if (IsMasterSide && boUsers.HasOtherSideCurrentUser(Side))
                MessageBox.Show("You cannot exit because there is a user currently logged in the other side.");
            else
            {
                AppContext.Logout();
                this.Close();
            }
        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsMasterSide)
            {
                if (IsPortOpen && RollInfo.CurrentRollID.HasValue)
                {
                    if (ReturnCounter > 49 && ReturnCounter <= 99)
                        ReturnCounter++;
                    else
                        ReturnCounter = 50;

                    string enableCmd = Prefix + ReturnCounter.ToString("00") + ">" + CarriageReturn;

                    try
                    {
                        serialPort.WriteLine(enableCmd.Replace("\r", ""));
                        (new BOTransmitLogs(ConnectionString)).SaveTransmitLog(RollInfo.CurrentRollID.Value, enableCmd.Replace("\r", ""), Side == "EAST" ? 'E' : 'W', "TX");
                        MUStatus = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show("Port is not open");
            }
            else
                MessageBox.Show("Only master computer can send command to MU");
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsMasterSide)
            {
                if (IsPortOpen && RollInfo.CurrentRollID.HasValue)
                {
                    if (ReturnCounter > 49 && ReturnCounter <= 99)
                        ReturnCounter++;
                    else
                        ReturnCounter = 50;

                    string disableCmd = Prefix + ReturnCounter.ToString("00") + "<" + CarriageReturn;

                    try
                    {
                        serialPort.WriteLine(disableCmd.Replace("\r", ""));
                        (new BOTransmitLogs(ConnectionString)).SaveTransmitLog(RollInfo.CurrentRollID.Value, disableCmd.Replace("\r", ""), Side == "EAST" ? 'E' : 'W', "TX");
                        MUStatus = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show("Port is not open");
            }
            else
                MessageBox.Show("Only master computer can send command to MU");
        }

        private void openPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsMasterSide)
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = ConfigurationManager.AppSettings["PortName"].ToString();
                    serialPort.BaudRate = int.Parse(ConfigurationManager.AppSettings["BaudRate"]);
                    serialPort.Parity = Parity.Even;
                    serialPort.DataBits = int.Parse(ConfigurationManager.AppSettings["DataBits"]);
                    switch (ConfigurationManager.AppSettings["StopBits"].ToString())
                    {
                        case "1":
                            serialPort.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            serialPort.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort.StopBits = StopBits.Two;
                            break;
                        default:
                            serialPort.StopBits = StopBits.None;
                            break;
                    }
                    serialPort.NewLine = "\r";
                    serialPort.ReadTimeout = int.Parse(ConfigurationManager.AppSettings["ReadTimeOut"]);
                    try
                    {
                        serialPort.Open();
                        IsPortOpen = serialPort.IsOpen;
                        lblPortStatus.Text = "Open";
                        pbPortStatus.Image = Properties.Resources.port_open;
                    }
                    catch (Exception ex)
                    {
                        IsPortOpen = false;
                        MessageBox.Show("Error Open: " + ex.Message);
                    }
                }
                else
                    MessageBox.Show("Port is already open");
            }
            else
                MessageBox.Show("Only master computer can send command to MU");
        }

        private void searchSeqNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchExamRollLog frmSearchExamRollLog = new SearchExamRollLog(this);
            frmSearchExamRollLog.StartPosition = FormStartPosition.CenterParent;
            frmSearchExamRollLog.ShowDialog();
            btnChangeSide.Focus();
        }

        #endregion

        #region Gridview Events
        private void dgvExamLogs_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                if (row != null)
                {
                    txtLMR.Text = row.Cells[1].Value.ToString();
                    txtSeqNo.Text = row.Cells[0].Value.ToString();
                    switch (row.Cells[5].Value.ToString())
                    {
                        case "B": lblSide.Text = "BOTH"; break;
                        case "E": lblSide.Text = "EAST"; break;
                        case "W": lblSide.Text = "WEST"; break;
                        default: lblSide.Text = ""; break;
                    }
                    lblAction.Text = row.Cells[7].Value.ToString();
                    lblAction.Tag = row.Cells[8].Value.ToString();
                    nudGrade.Value = Decimal.Parse(row.Cells[2].Value.ToString() == ""? "0": row.Cells[2].Value.ToString() );
                    nudSpot.Value = Decimal.Parse(row.Cells[3].Value.ToString() == "" ? "0" : row.Cells[3].Value.ToString());
                    nudShade.Value = Decimal.Parse(row.Cells[6].Value.ToString() == "" ? "0" : row.Cells[6].Value.ToString());
                    txtDefect.Text = row.Cells[4].Value.ToString();
                }
            }
        }


        #endregion

        private void RollExam_Load(object sender, EventArgs e)
        {
            if (AppContext.Current.User != null)
            {
                if (!CanRequestNotifications())
                {
                    MessageBox.Show("Permission not granted to access database");
                    return;
                }

                firstLogin = 1;
                UpdateRowIndex = 0;
               

                GetUserMasterDataResult userMasterData = (new BOMaster(ConnectionString)).GetUserMasterData(AppContext.Current.User.UserId, ConfigurationManager.AppSettings["LocalIP"].ToString());

                if (userMasterData != null)
                {
                    lblLocation.Text = userMasterData.Location;
                    lblProductionLine.Text = userMasterData.Line;
                    lblShift.Text = userMasterData.Shift;
                    lblOperatorSide.Text = userMasterData.Side;
                    Prefix = userMasterData.Prefix;
                    IsMasterSide = userMasterData.Side == userMasterData.MasterSide;

                    (new BOUsers(ConnectionString)).UpdateCurrentUser(userMasterData.Side, AppContext.Current.User.UserId);
                }

                if (!IsMasterSide)
                {
                    pbPortStatus.Visible = false;
                    pbMUStatus.Visible = false;
                    lblPortStatus.Visible = false;
                    lblMUStatus.Visible = false;
                    lblPortStatusTitle.Visible = false;
                    lblMUStatusTitle.Visible = false;
                }

                RollInfo = (new BORolls(ConnectionString)).GetRollInfo();

                //Bind Defect Codes
                BOMaster boMaster = new BOMaster(ConnectionString);
                defectCodeList = boMaster.GetDefectCodes();
                AutoCompleteStringCollection defectCodeCollection = new AutoCompleteStringCollection();
                defectCodeCollection.AddRange(defectCodeList.ToArray());
                txtDefect.AutoCompleteCustomSource = defectCodeCollection;

                ////Bind Actions
                actionList = boMaster.GetActions();
                lblAction.Tag = "1";

                ReturnCounter = boMaster.GetLastestRollCounter();

                lblDate.Text = DateTime.Now.ToLongDateString();
                lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

                SqlDependency.Stop(ConnectionString);
                SqlDependency.Start(ConnectionString);

                if (connection == null)
                    connection = new SqlConnection(ConnectionString);

                if (cmdExamRollLogs == null)
                {
                    cmdExamRollLogs = new SqlCommand(GetExamRollLogSQL(), connection);
                    SqlParameter parameter = new SqlParameter("@Roll_ID", SqlDbType.Int);
                    parameter.Value = RollInfo.CurrentRollID.HasValue ? RollInfo.CurrentRollID.Value : 0;
                    cmdExamRollLogs.Parameters.Add(parameter);

                    if (dsExamRollLogs == null)
                        dsExamRollLogs = new DataSet();
                    LoadExamRollData();
                }

                if (cmdCurrentUsers == null)
                {
                    cmdCurrentUsers = new SqlCommand(GetCurrentUsersSQL(), connection);

                    if (dsCurrentUsers == null)
                        dsCurrentUsers = new DataSet();

                    LoadCurrentUsersData();
                }

                if (cmdRollInfo == null)
                {
                    cmdRollInfo = new SqlCommand(GetRollInfoSQL(), connection);

                    if (dsRollInfo == null)
                        dsRollInfo = new DataSet();

                    LoadRollInfoData();
                }

                if(IsMasterSide)
                    timerConnect.Enabled = true;
            }
            else
            {
                this.Close();
            }
        }

        #region SQL command

        private string GetExamRollLogSQL()
        {
            return "SELECT dbo.DC_ExamRollLog.LogID AS SEQ, dbo.DC_ExamRollLog.LMR, dbo.DC_ExamRollLog.Grade, "
            + "dbo.DC_ExamRollLog.Spot, dbo.DC_ExamRollLog.DefectCode, dbo.DC_ExamRollLog.Side, dbo.DC_ExamRollLog.Shade, "
            + "dbo.DC_ActionMaster.ActionDesc, dbo.DC_ExamRollLog.ActionID FROM dbo.DC_ExamRollLog INNER JOIN dbo.DC_ActionMaster ON dbo.DC_ExamRollLog.ActionID = dbo.DC_ActionMaster.ActionID "
            + "WHERE dbo.DC_ExamRollLog.RollID = @Roll_ID AND dbo.DC_ExamRollLog.Type = 'I' ORDER BY dbo.DC_ExamRollLog.LogID DESC;"
            + "SELECT dbo.DC_ExamRollLog.LogID AS SEQ, dbo.DC_ExamRollLog.LMR, dbo.DC_ExamRollLog.Grade, "
            + "dbo.DC_ExamRollLog.Spot, dbo.DC_ExamRollLog.DefectCode, dbo.DC_ExamRollLog.Side, dbo.DC_ExamRollLog.Shade, "
            + "dbo.DC_ExamRollLog.ActionID, dbo.DC_ExamRollLog.Type FROM dbo.DC_ExamRollLog WHERE dbo.DC_ExamRollLog.RollID = @Roll_ID ORDER BY dbo.DC_ExamRollLog.LogID DESC";
        }

        private string GetCurrentUsersSQL()
        {
            return "SELECT dbo.DC_CurrentUsers.Side, dbo.DC_Users.FirstName, dbo.DC_Users.LastName, "
            + "dbo.DC_Users.ClockNo FROM dbo.DC_CurrentUsers INNER JOIN dbo.DC_Users ON dbo.DC_CurrentUsers.UserID = dbo.DC_Users.UserId";
        }

        private string GetRollInfoSQL()
        {
            return "SELECT dbo.DC_RollNumbers.RollNumberID, dbo.DC_RollNumbers.RollID, dbo.DC_RollMaster.RollNumber, dbo.DC_RollNumbers.Status "
            + "FROM dbo.DC_RollNumbers INNER JOIN dbo.DC_RollMaster ON dbo.DC_RollNumbers.RollID = dbo.DC_RollMaster.RollID WHERE dbo.DC_RollNumbers.Status = 'C' OR dbo.DC_RollNumbers.Status = 'N'";
        }

        #endregion SQL command

        #region SQL Dependency OnChange
        private void cmdExamRollDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;

            if (i.InvokeRequired)
            {
                OnChangeEventHandler tempDelegate =
                    new OnChangeEventHandler(cmdExamRollDependency_OnChange);

                object[] args = { sender, e };

                i.BeginInvoke(tempDelegate, args);

                return;
            }

            SqlDependency cmdExamRollDependency =
                (SqlDependency)sender;

            cmdExamRollDependency.OnChange -= cmdExamRollDependency_OnChange;

            LoadExamRollData();
        }

        private void cmdCurrentUsersDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;

            if (i.InvokeRequired)
            {
                OnChangeEventHandler tempDelegate =
                    new OnChangeEventHandler(cmdCurrentUsersDependency_OnChange);

                object[] args = { sender, e };

                i.BeginInvoke(tempDelegate, args);

                return;
            }

            SqlDependency cmdCurrentUsersDependency =
                (SqlDependency)sender;

            cmdCurrentUsersDependency.OnChange -= cmdCurrentUsersDependency_OnChange;

            LoadCurrentUsersData();
        }

        private void cmdRollInfoDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;

            if (i.InvokeRequired)
            {
                OnChangeEventHandler tempDelegate =
                    new OnChangeEventHandler(cmdRollInfoDependency_OnChange);

                object[] args = { sender, e };

                i.BeginInvoke(tempDelegate, args);

                return;
            }

            SqlDependency cmdRollInfoDependency =
                (SqlDependency)sender;

            cmdRollInfoDependency.OnChange -= cmdRollInfoDependency_OnChange;

            LoadRollInfoData();
        }

        #endregion SQL Dependency OnChange

        private void LoadExamRollData()
        {
            dsExamRollLogs.Clear();
            cmdExamRollLogs.Notification = null;
            SqlDependency cmdExamRollDependency = new SqlDependency(cmdExamRollLogs);
            cmdExamRollDependency.OnChange += new OnChangeEventHandler(cmdExamRollDependency_OnChange);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdExamRollLogs))
            {
                adapter.Fill(dsExamRollLogs);

                DataTable dtExamRollLogs = null;
                dtExamRollLogs = dsExamRollLogs.Tables[0].DefaultView.ToTable();

                dgvExamLogs.DataSource = dtExamRollLogs;
               
                if (dsExamRollLogs.Tables[0].Rows.Count > 0 && dsExamRollLogs.Tables[0].Rows.Count > UpdateRowIndex)
                {
                    dgvExamLogs.Rows[UpdateRowIndex].Selected = true;
                    dgvExamLogs.CurrentCell = dgvExamLogs.Rows[UpdateRowIndex].Cells[0];
                }

                if (dsExamRollLogs.Tables[1].Rows.Count > 0)
                {
                    if (!IsMasterSide)
                    {
                        if (int.Parse(dsExamRollLogs.Tables[1].Rows[0]["SEQ"].ToString()) != ExamRollLogCounter)
                        {

                            switch (dsExamRollLogs.Tables[1].Rows[0]["Type"].ToString())
                            {
                                case "I":
                                    if (dsExamRollLogs.Tables[1].Rows[0]["Side"].ToString() == Side.Substring(0, 1))
                                    {
                                        txtSeqNo.Text = dsExamRollLogs.Tables[1].Rows[0]["SEQ"].ToString();
                                        txtLMR.Text = dsExamRollLogs.Tables[1].Rows[0]["LMR"].ToString();
                                        txtDefect.Text = "";
                                        switch (dsExamRollLogs.Tables[1].Rows[0]["Side"].ToString())
                                        {
                                            case "E":
                                                lblSide.Text = "EAST";
                                                break;
                                            case "W":
                                                lblSide.Text = "WEST";
                                                break;
                                            default:
                                                lblSide.Text = "";
                                                break;
                                        }
                                        nudGrade.Value = 0;
                                        nudShade.Value = 0;
                                        nudSpot.Value = 0;
                                        lblAction.Text = "";
                                        lblAction.Tag = "1";
                                    }
                                    ExamRollLogCounter = int.Parse(dsExamRollLogs.Tables[1].Rows[0]["SEQ"].ToString());

                                    break;
                                case "N":
                                    ExamRollLogCounter = int.Parse(dsExamRollLogs.Tables[1].Rows[0]["SEQ"].ToString());
                                    if (firstLogin == 0)
                                    {
                                        RollInfo.CurrentRollID = RollInfo.NextRollID;
                                        RollInfo.CurrentRoll = RollInfo.NextRoll;
                                    }
                                    ClearScreen();
                                    break;
                                case "Z":
                                    ExamRollLogCounter = int.Parse(dsExamRollLogs.Tables[1].Rows[0]["SEQ"].ToString());
                                    MessageBox.Show("End of roll has been reached");
                                    break;
                            }
                        }
                    }
                }
                firstLogin = 0;
            }

            dgvExamLogs.Focus();
            
        }

        private void LoadCurrentUsersData()
        {
            dsCurrentUsers.Clear();
            cmdCurrentUsers.Notification = null;
            SqlDependency cmdCurrentUsersDependency = new SqlDependency(cmdCurrentUsers);
            cmdCurrentUsersDependency.OnChange += new OnChangeEventHandler(cmdCurrentUsersDependency_OnChange);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdCurrentUsers))
                adapter.Fill(dsCurrentUsers);

            lblWestNo.Text = "";
            lblEastNo.Text = "";
            lblWestName.Text = "";
            lblEastName.Text = "";

            foreach (DataRow dr in dsCurrentUsers.Tables[0].Rows)
            {
                switch(dr["Side"].ToString())
                {
                    case "EAST":
                        lblEastName.Text = dr["FirstName"] + " " + dr["LastName"];
                        lblEastNo.Text = dr["ClockNo"].ToString();
                        break;
                    case "WEST":
                        lblWestName.Text = dr["FirstName"] + " " + dr["LastName"];
                        lblWestNo.Text = dr["ClockNo"].ToString();
                        break;
                }
            }
        }

        private void LoadRollInfoData()
        {
            try
            {
                dsRollInfo.Clear();
                cmdRollInfo.Notification = null;
                SqlDependency cmdRollInfoDependency = new SqlDependency(cmdRollInfo);
                cmdRollInfoDependency.OnChange += new OnChangeEventHandler(cmdRollInfoDependency_OnChange);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmdRollInfo))
                    adapter.Fill(dsRollInfo);

                foreach (DataRow dr in dsRollInfo.Tables[0].Rows)
                {
                    switch (dr["Status"].ToString())
                    {
                        case "C":
                            RollInfo.CurrentRoll = dr["RollNumber"].ToString();
                            RollInfo.CurrentRollID = Convert.ToInt32(dr["RollID"]);
                            lblCurrentRoll.Text = RollInfo.CurrentRoll;
                            ClearScreen();
                            
                            break;
                        case "N":
                            RollInfo.NextRoll = dr["RollNumber"].ToString();
                            RollInfo.NextRollID = Convert.ToInt32(dr["RollID"]);
                            lblNextRoll.Text = RollInfo.NextRoll;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsPortOpen && RollInfo.CurrentRollID.HasValue)
            {
                try
                {
                    string receivedCmd = serialPort.ReadLine();

                    BOTransmitLogs boTransmitLogsHelper = new BOTransmitLogs(ConnectionString);
                    BOExamRollLogs boExamRollLogsHelper = new BOExamRollLogs(ConnectionString);
                    if (receivedCmd.Length > 9)
                    {
                        char commandType = receivedCmd[9];

                        boTransmitLogsHelper.SaveTransmitLog(RollInfo.CurrentRollID.Value, receivedCmd, commandType, "RX");
                        switch (commandType)
                        {
                            case 'E':
                            case 'W':
                                if (CurrentExamRollLog == null)
                                    CurrentExamRollLog = new ExamRollLogView();

                                CurrentExamRollLog.LogID = boExamRollLogsHelper.SaveExamRollLog(RollInfo.CurrentRollID.Value, receivedCmd.Substring(3, 6), commandType.ToString(), "I");
                                CurrentExamRollLog.LMR = receivedCmd.Substring(3, 6);
                                CurrentExamRollLog.Grade = null;
                                CurrentExamRollLog.Spot = null;
                                CurrentExamRollLog.Shade = null;
                                CurrentExamRollLog.DefectCode = "";
                                CurrentExamRollLog.Side = commandType.ToString();
                                CurrentExamRollLog.ActionID = 1;
                                CurrentExamRollLog.ActionDesc = "";
                                CurrentExamRollLog.Type = "I";
                                CurrentExamRollLog.Transmitted = false;
                                CurrentCommandType = RollCommandType.OperatorInfo;
                                break;
                            case 'Z':
                                boExamRollLogsHelper.SaveExamRollLog(RollInfo.CurrentRollID.Value, "", "", "Z");
                                CurrentCommandType = RollCommandType.EndOfRoll;
                                break;
                            case 'N':
                                if (!RollInfo.NextRollID.HasValue)
                                {
                                    MessageBox.Show("No next roll exists. The new roll cannot be started.");
                                    return;
                                }
                                boExamRollLogsHelper.SaveExamRollLog(RollInfo.NextRollID.Value, "", "", "N");
                                
                                CurrentCommandType = RollCommandType.NewRoll;
                                break;
                            default:
                                CurrentCommandType = RollCommandType.Others;
                                break;
                        }

                        try
                        {
                            serialPort.WriteLine(receivedCmd.Substring(0, 3) + "$");
                            boTransmitLogsHelper.SaveTransmitLog(CurrentCommandType == RollCommandType.NewRoll ? RollInfo.NextRollID.Value : RollInfo.CurrentRollID.Value, receivedCmd.Substring(0, 3) + "$", Side == "EAST" ? 'E' : 'W', "TX");
                        }
                        catch (InvalidOperationException ioEx)
                        {
                            MessageBox.Show(ioEx.Message);
                        }
                        catch (TimeoutException toEx)
                        {
                            MessageBox.Show(toEx.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else if (receivedCmd.Length > 3)
                    {
                        CurrentCommandType = RollCommandType.EnableDisable;
                        boTransmitLogsHelper.SaveTransmitLog(RollInfo.CurrentRollID.Value, receivedCmd, 'A', "RX");
                    }
                    this.Invoke(new Action(() => UpdateUI(CurrentCommandType)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SerialPort1_DataReceived: " + ex.Message);
                }
            }
        }

        public void UpdateUI(RollCommandType commandType)
        {
            switch (CurrentCommandType)
            {
                case RollCommandType.OperatorInfo:
                    if (CurrentExamRollLog != null && CurrentExamRollLog.Side == (Side == "EAST" ? "E" : "W"))
                    {
                        txtSeqNo.Text = CurrentExamRollLog.LogID.ToString();
                        txtLMR.Text = CurrentExamRollLog.LMR;
                        txtDefect.Text = "";
                        switch (CurrentExamRollLog.Side)
                        {
                            case "E":
                                lblSide.Text = "EAST";
                                break;
                            case "W":
                                lblSide.Text = "WEST";
                                break;
                            default:
                                lblSide.Text = "";
                                break;
                        }
                        nudGrade.Value = 0;
                        nudShade.Value = 0;
                        nudSpot.Value = 0;
                        lblAction.Text = "";
                        lblAction.Tag = "1";
                    }
                    break;
                case RollCommandType.NewRoll:
                    ConfirmQueuedRoll frmConfirmRoll = new ConfirmQueuedRoll(this);

                    if (frmConfirmRoll.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        ChangeToNextRoll();
                        ClearScreen();
                    }
                    break;
                case RollCommandType.EnableDisable:
                    if (MUStatus)
                    {
                        lblMUStatus.Text = "Enabled";
                        pbMUStatus.Image = Properties.Resources.mu_enabled;
                    }
                    else
                    {
                        lblMUStatus.Text = "Disabled";
                        pbMUStatus.Image = Properties.Resources.mu_disabled;
                    }
                    break;
                case RollCommandType.Others:
                    break;
                case RollCommandType.EndOfRoll:
                    MessageBox.Show("End of roll has been reached");
                    break;
            }
        }

        private void ChangeToNextRoll()
        {
            ChangeRollResult changeRollResult = (new BORolls(ConnectionString)).ChangeRoll(RollInfo.CurrentRollID.Value, RollInfo.NextRollID.HasValue ? RollInfo.NextRollID.Value : 0);

            RollInfo.CurrentRollID = RollInfo.NextRollID;
            RollInfo.CurrentRoll = RollInfo.NextRoll;

            if (changeRollResult != null && changeRollResult.NextRollID > 0)
            {
                RollInfo.NextRollID = changeRollResult.NextRollID;
                RollInfo.NextRoll = changeRollResult.NextRoll;
            }
            else
            {
                RollInfo.NextRollID = null;
                RollInfo.NextRoll = "";
            }

        }

        private void ClearScreen()
        {
            dgvExamLogs.DataSource = null;
            txtLMR.Text = "";
            txtSeqNo.Text = "";
            txtDefect.Text = "";
            nudGrade.Value = 0;
            nudShade.Value = 0;
            nudSpot.Value = 0;
            lblSide.Text = "";
            lblAction.Text = "";
            lblAction.Tag = "0";
            lblCurrentRoll.Text = RollInfo.CurrentRoll;
            lblNextRoll.Text = RollInfo.NextRoll;
            if (cmdExamRollLogs != null)
                cmdExamRollLogs.Dispose();

            cmdExamRollLogs = new SqlCommand(GetExamRollLogSQL(), connection);
            SqlParameter parameter = new SqlParameter("@Roll_ID", SqlDbType.Int);
            parameter.Value = RollInfo.CurrentRollID.HasValue ? RollInfo.CurrentRollID.Value : 0;
            cmdExamRollLogs.Parameters.Add(parameter);

            if (dsExamRollLogs == null)
                dsExamRollLogs = new DataSet();
            LoadExamRollData();
          
        }

        private void timerConnect_Tick(object sender, EventArgs e)
        {
            timerConnect.Dispose();
            ConnectMU frmConnectMU = new ConnectMU(serialPort, Prefix, Side, this);
            frmConnectMU.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = frmConnectMU.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                IsPortOpen = serialPort.IsOpen;
                lblPortStatus.Text = "Open";
                pbPortStatus.Image = Properties.Resources.port_open;
                lblMUStatus.Text = "Enabled";
                pbMUStatus.Image = Properties.Resources.mu_enabled;
                this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
                this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            }
            else
            {
                this.Close();
            }
        }

        private void btnAddNewRoll_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo(ConfigurationManager.AppSettings["ServerWebApp"].ToString());
            Process.Start(psi);
        }

        private void serialPort_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void RollExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            (new BOUsers(ConnectionString)).UpdateCurrentUser(Side, 0);
            if (IsPortOpen)
            {
                serialPort.Close();
                serialPort.Dispose();
            }

            loginForm.Show();
        }

        #region Util
        private bool CanRequestNotifications()
        {
            try
            {
                SqlClientPermission perm =
                    new SqlClientPermission(
                    PermissionState.Unrestricted);
                perm.Demand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        private void btnChangeNextRoll_Click(object sender, EventArgs e)
        {
            if (RollInfo.NextRollID.HasValue)
            {
                ChangeNextRoll frmChangeNextRoll = new ChangeNextRoll(this);
                frmChangeNextRoll.StartPosition = FormStartPosition.CenterParent;
                frmChangeNextRoll.ShowDialog();
            }
            else
                MessageBox.Show("No next roll exists. Please contact supervisor.");
        }

        private void btnChangeSide_Click(object sender, EventArgs e)
        {
            ChangeSide frmChangeSide = new ChangeSide(this, lblSide.Text);
            frmChangeSide.ShowDialog();
        }

        private void btnChangeAction_Click(object sender, EventArgs e)
        {
            ChangeAction frmChangeAction = new ChangeAction(this, lblAction.Text, actionList);
            frmChangeAction.ShowDialog();
        }

        
    }
}
