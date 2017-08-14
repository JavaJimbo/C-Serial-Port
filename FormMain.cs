/* C# Serial Simple
* Adapted from CodeProjectSerialComms and COMPortTerminal programs, I think.
* 
* 8-14-17 JBS:  Got basic sending and receiving working with Hyperterminal.
*               Saves Port number and Baud Rate settings entered in PortSettingsDialog box and restores settings at startup.
*               Simplified basic code, does not create interrupt events when changes are made in Dilog box.
*/ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using CodeProjectSerialComms.Properties;



namespace CodeProjectSerialComms
// namespace COMPortTerminal
{
    public partial class FormMain : Form    
    {
        Timer timer = new Timer();        
        public int counter = 0;
        SerialPort ComPort1 = new SerialPort();                
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        delegate void SetTextCallback(string text);
        string InputData = String.Empty;
        string InputTextString = String.Empty;
        Boolean commandTimeout = false;

        internal PortSettingsDialog MyPortSettingsDialog;        
        private bool savedOpenPortOnStartup;
        
        public FormMain()
        {
            InitializeComponent();            
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            //myTimer.Tick += delegate (object sender, EventArgs e) {
            //    MessageBox.Show("Hello world!");
            //};
            ComPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
        }

        /// <summary>
        /// Create an instance of the ComPorts class.
        /// Initialize port settings and other parameters. 
        /// specify behavior on events.
        /// </summary>       
        private void FormMain_Load(object sender, System.EventArgs e)
        {
            Show();

            MyPortSettingsDialog = new PortSettingsDialog();


            GetPreferences();
            //  Select a COM port and bit rate using stored preferences if available.
            if (ComPorts.comPortExists)
            {
                InitializePortSettingsDialogBox();
                if (MyPortSettingsDialog.chkOpenComPortOnStartup.Checked) ComPort1.Open();
                DisplayPortSettings();
            }
        }




        /// <summary>
        /// Set the user preferences or default values in the combo boxes and ports array
        /// using stored preferences or default values.
        /// </summary>
        private void InitializePortSettingsDialogBox()
        {
            // int myPortIndex = 0;
            // myPortIndex = MyPortSettingsDialog.SelectComPort(ComPort1.PortName);
            MyPortSettingsDialog.SelectComPort(ComPort1.PortName);
            MyPortSettingsDialog.SelectBitRate(ComPort1.BaudRate);
            MyPortSettingsDialog.SelectHandshaking(ComPort1.Handshake);
            MyPortSettingsDialog.chkOpenComPortOnStartup.Checked = savedOpenPortOnStartup;
        }


        /// <summary>
        /// Get user preferences for the COM port and parameters.
        /// See SetPreferences for more information.
        /// </summary>

        private void GetPreferences() // $$$$
        {            
            ComPort1.PortName = Settings.Default.ComPort;
            ComPort1.BaudRate = Settings.Default.BitRate;
            ComPort1.Handshake = System.IO.Ports.Handshake.None;
            savedOpenPortOnStartup = Settings.Default.OpenComPortOnStartup;
        }
        

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = ComPort1.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
            }
        }

        private void SetText(string text)
        {            
            InputTextString += text;
            counter = 0;            
            int Start = InputTextString.IndexOf("\r", 0);
            if (Start > 0)
            {                
                InputTextString = "";
            }
            rtbIncoming.Text = InputTextString;
        }



        private void rtbOutgoing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // enter key  
            {
                ComPort1.Write("\r\n");
                rtbOutgoing.Text = "";
            }
            else if (e.KeyChar < 32 || e.KeyChar > 126)
            {
                e.Handled = true; // ignores anything else outside printable ASCII range  
            }
            else
            {
                ComPort1.Write(e.KeyChar.ToString());
            }
        }


        private void rtbIncoming_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbOutgoing_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnTimer.Text == "Start")
            {
                counter = 0;
                timer.Start();
                btnTimer.Text = "Stop";
                lblPortStatus.Text = "RUNNING";
            }
            else
            {
                timer.Stop();
                btnTimer.Text = "Start";
                DisplayPortSettings();
            }
            commandTimeout = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > 100)
            {
                timer.Stop();                                    
                lblTimer.Text = counter.ToString();
                commandTimeout = true;
                lblPortStatus.Text = "TIMEOUT";
            }
        }

        /// <summary>
        /// Look for COM ports and display them in the combo box.
        /// </summary>
        private void btnPort_Click(object sender, EventArgs e)
        { 
            ComPorts.FindComPorts();

            MyPortSettingsDialog.DisplayComPorts();
            MyPortSettingsDialog.SelectComPort(ComPort1.PortName);

            InitializePortSettingsDialogBox();

            //  Display the combo boxes for setting port parameters.
            MyPortSettingsDialog.ShowDialog();

            if (MyPortSettingsDialog.settingsChanged)
            {
                try
                {
                    if (ComPort1.IsOpen) ComPort1.Close();
                    ComPort1.PortName = MyPortSettingsDialog.cmbPort.SelectedItem.ToString();
                    lblPortNumber.Text = ComPort1.PortName;

                    String strBaudRate = MyPortSettingsDialog.cmbBitRate.SelectedValue.ToString();
                    if (strBaudRate != "")
                    {
                        int intBaudRate = Convert.ToInt32(strBaudRate);
                        ComPort1.BaudRate = intBaudRate;
                    }

                    ComPort1.DataBits = 8;
                    ComPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
                    ComPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None");
                    ComPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");

                    ComPort1.Open();
                    DisplayPortSettings();
                    SavePreferences();
                }
                catch
                {
                    lblPortStatus.Text = "ERROR: NO COM PORTS FOUND";
                }
            }
        }

        private void btnPortState_Click(object sender, EventArgs e)
        {
            if (ComPorts.comPortExists)
            {
                if (ComPort1 != null)
                {
                    if (!ComPort1.IsOpen)
                    {
                        try
                        {
                            btnPortState.Text = "Open";
                            ComPort1.Open();
                        }
                        catch
                        {
                            lblPortStatus.Text = "ERROR: CAN'T OPEN COM PORT";
                            btnPortState.Text = "Closed";
                        }
                    }
                    else
                    {
                        try
                        {                            
                            ComPort1.Close();
                        }
                        catch
                        {
                            lblPortStatus.Text = "ERROR: CAN'T CLOSE COM PORT";
                        }
                        btnPortState.Text = "Closed";
                    }
                }
                else btnPortState.Text = "None";                
            }            
            DisplayPortSettings();
        }


        /// <summary>
        /// Display the current port parameters on the form.
        /// </summary>
        private void DisplayPortSettings()
        {
            string strPortStatusText = "";

            if (ComPorts.comPortExists)
            {
                if ((!((ComPort1 == null))))
                {
                    if (ComPort1.IsOpen)
                    {
                        strPortStatusText = ComPort1.PortName + " " + System.Convert.ToString(ComPort1.BaudRate) + " N 8 1 OPEN";
                        btnPortState.Text = "OPEN";
                    }
                    else
                    {
                        strPortStatusText = ComPort1.PortName + " " + System.Convert.ToString(ComPort1.BaudRate) + " N 8 1 CLOSED";
                        btnPortState.Text = "CLOSED";
                    }
                    lblPortStatus.Text = strPortStatusText;
                }
                else lblPortStatus.Text = "ERROR: NO COM PORT SELECTED";
            }
            else lblPortStatus.Text = "ERROR: NO COM PORTS FOUND";
        }

        /// <summary> 
        /// Save user preferences for the COM port and parameters.
        /// </summary>  
        private void SavePreferences()
        {
            // To define additional settings, in the Visual Studio IDE go to
            // Solution Explorer > right click on project name > Properties > Settings.
            if (MyPortSettingsDialog.cmbPort.SelectedIndex > -1)
            {
                // The system has at least one COM port.
                Settings.Default.ComPort = ComPort1.PortName; //  MyPortSettingsDialog.cmbPort.SelectedItem.ToString();
                Settings.Default.BitRate = ComPort1.BaudRate; // (int)MyPortSettingsDialog.cmbBitRate.SelectedItem;
                Settings.Default.OpenComPortOnStartup = MyPortSettingsDialog.chkOpenComPortOnStartup.Checked;
                Settings.Default.Save();
            }
            else lblPortStatus.Text = "SAVE ERROR: NO COM PORTS FOUND!";
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (ComPort1.IsOpen)
            {
                ComPort1.Close();
                ComPort1.DiscardInBuffer();
                ComPort1.DiscardOutBuffer();                
            }
        }

    }
}
