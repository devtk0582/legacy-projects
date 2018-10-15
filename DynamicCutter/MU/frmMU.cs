using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Configuration;

namespace MU
{
    public partial class frmMU : Form
    {
        private SerialPort serialPort = new SerialPort();

        private string currentReceivedCmd = "";

        public frmMU()
        {
            InitializeComponent();
        }

        private void frmMU_Load(object sender, EventArgs e)
        {

            serialPort.PortName = ConfigurationManager.AppSettings["PortName"].ToString();
            serialPort.BaudRate = 2400;
            serialPort.Parity = Parity.Even;
            serialPort.DataBits = 7;
            serialPort.StopBits = StopBits.One;
            serialPort.NewLine = "\r";
            serialPort.ReadTimeout = 1000;
            try
            {
                serialPort.Open();
                this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Open: " + ex.Message);
            }
        }


        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                try
                {
                    string receivedCmd = serialPort.ReadLine();

                    currentReceivedCmd = receivedCmd;
                    if (receivedCmd.Length > 3 && receivedCmd.Length < 9)
                    {
                        if (receivedCmd.Substring(3, 1) == "<" || receivedCmd.Substring(3, 1) == ">")
                            serialPort.WriteLine(receivedCmd.Substring(0, 3) + "$");
                    }

                    this.Invoke(new EventHandler(UpdateUI));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SerialPort1_DataReceived: " + ex.Message);
                }
        }

        public void UpdateUI(object sender, System.EventArgs e)
        {
            lblMsg.Text = currentReceivedCmd;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            serialPort.WriteLine(txtInput.Text.Trim());
        }
    }
}
