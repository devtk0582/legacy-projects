using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using DynamicCutterBusinessLogic;
using System.Configuration;
using System.Threading;

namespace DynamicCutter
{
    public partial class ConnectMU : Form
    {
        private SerialPort serialPort;
        private BOTransmitLogs boTransmitLogs = null;
        private BOExamRollLogs boExamRollLogs = null;
        private string Prefix;
        private RollExam frmRollExam;
        private int enableCounter = 0;
        private int disableCounter = 0;

        public bool IsEnableCmdReceived
        {
            get;
            set;
        }

        public bool IsDisableCmdReceived
        {
            get;
            set;
        }

        public string Side
        {
            get;
            set;
        }

        public char CarriageReturn
        {
            get { return (char)0x0D; }
        }

        public bool IsPortOpen
        { get; set; }

        public ConnectMU()
        {
            InitializeComponent();
        }

        public ConnectMU(SerialPort sp, string prefix, string side, RollExam frm)
        {
            InitializeComponent();
            serialPort = sp;
            boTransmitLogs = new BOTransmitLogs(frm.ConnectionString);
            boExamRollLogs = new BOExamRollLogs(frm.ConnectionString);
            Prefix = prefix;
            Side = side;
            frmRollExam = frm;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.serialPort.Close();
            this.Close();
        }

        private void ConnectMU_Load(object sender, EventArgs e)
        {
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
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
                serialPort.WriteTimeout = 1000;
                try
                {
                    serialPort.Open();
                    IsPortOpen = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Open: " + ex.Message);
                }
            }
            timer.Enabled = true;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsPortOpen)
            {
                try
                {
                    string receivedCmd = serialPort.ReadLine();

                    if (receivedCmd.Length > 3 && receivedCmd.Length < 9)
                    {
                        if (receivedCmd.Substring(0, 3) == Prefix + disableCounter)
                            IsDisableCmdReceived = true;

                        if (receivedCmd.Substring(0, 3) == Prefix + enableCounter)
                            IsEnableCmdReceived = true;

                        (new BOTransmitLogs(frmRollExam.ConnectionString)).SaveTransmitLog(0, receivedCmd, 'A', "RX");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("serialPort_DataReceived: " + ex.Message);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Dispose();
            try
            {
                if (frmRollExam.ReturnCounter >= 49 && frmRollExam.ReturnCounter < 99)
                    frmRollExam.ReturnCounter++;
                else
                    frmRollExam.ReturnCounter = 50;
                IsDisableCmdReceived = false;
                disableCounter = frmRollExam.ReturnCounter;
                string disableCmd = Prefix + frmRollExam.ReturnCounter + "<";
                serialPort.WriteLine(disableCmd);
                boTransmitLogs.SaveTransmitLog(0, disableCmd, Side == "EAST" ? 'E' : 'W', "TX");

                Thread.Sleep(1000);
                IsEnableCmdReceived = false;
                if (frmRollExam.ReturnCounter >= 49 && frmRollExam.ReturnCounter < 99)
                    frmRollExam.ReturnCounter++;
                else
                    frmRollExam.ReturnCounter = 50;
                enableCounter = frmRollExam.ReturnCounter;
                string enableCmd = Prefix + frmRollExam.ReturnCounter + ">";
                serialPort.WriteLine(enableCmd);
                boTransmitLogs.SaveTransmitLog(0, enableCmd, Side == "EAST" ? 'E' : 'W', "TX");

                Thread.Sleep(1000);
                bool MUReady = false;
                for (int count = 0; count < 3; count++)
                {
                    if (IsEnableCmdReceived && IsDisableCmdReceived)
                    {
                        MUReady = true;
                        break;
                    }
                    else
                        Thread.Sleep(3000);
                }

                if (MUReady)
                {
                    serialPort.DataReceived -= serialPort_DataReceived;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMsg.Text = "Could not connect with MU. Please contact Shift Supervisor.";
                }
            }
            catch (InvalidOperationException ex)
            {
                IsPortOpen = false;
                lblMsg.Text = "Could not connect with MU. Please contact Shift Supervisor.";
            }
            catch (TimeoutException ex)
            {
                IsPortOpen = false;
                lblMsg.Text = "Could not connect with MU. Please contact Shift Supervisor.";
            }
            catch (Exception ex)
            {
                IsPortOpen = false;
                lblMsg.Text = "Could not connect with MU. Please contact Shift Supervisor.";
            }
        }
    }
}
