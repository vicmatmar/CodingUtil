using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommanderLib;
using RangeTester;

namespace WCodingUtil
{
    public partial class WCodingUtil : Form
    {
        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);  // Set object text
        delegate void setTextColorCallback(string txt, Color forecolor, Color backcolor);

        public WCodingUtil()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            run_button.Focus();

            runStatus_Init();

            Files_listBox.Items.Clear();

            if (Properties.Settings.Default.Files == null)
                Properties.Settings.Default.Files = new System.Collections.Specialized.StringCollection();
            string[] files = new string[Properties.Settings.Default.Files.Count];
            Properties.Settings.Default.Files.CopyTo(files, 0);
            Files_listBox.Items.AddRange(files);

            mfgStr_textBox.Text = Properties.Settings.Default.MFgString;
            DBA_ip_textBox.Text = Properties.Settings.Default.DBA_ip;
            RTM_ip_textBox.Text = Properties.Settings.Default.RTM_ip;
            channel_numericUpDown.Value = Properties.Settings.Default.Channel;

        }


        void runStatus_Init()
        {
            runStatus_textBox.BackColor = Color.White;
            runStatus_textBox.ForeColor = Color.Black;
            runStatus_textBox.Clear();
            runStatus_textBox.Text = "Ready";
            runStatus_textBox.Update();


        }

        void controlSetText(Control control, object value, string property_name = "Text")
        {
            if (control.InvokeRequired)
            {
                setControlPropertyValueCallback d = new setControlPropertyValueCallback(controlSetText);
                this.Invoke(d, new object[] { control, value, property_name });
            }
            else
            {
                var property = control.GetType().GetProperty(property_name);
                if (property != null)
                {
                    property.SetValue(control, value);
                }
                var method = control.GetType().GetMethod("Update");
                if (method != null)
                {
                    method.Invoke(control, null);
                }
            }
        }

        void setOutputStatus(string text)
        {
            string line = output_textBox.Text + $"{DateTime.Now}: {text}\r\n";
            controlSetText(output_textBox, line);
        }

        void setRunStatus(string text)
        {
            setRunStatus(text, Color.Black, Color.White);
            setOutputStatus(text);
        }

        void setRunStatus(string text, Color forecolor, Color backcolor)
        {
            if (this.runStatus_textBox.InvokeRequired)
            {
                setTextColorCallback d = new setTextColorCallback(setRunStatus);
                this.Invoke(d, new object[] { text, forecolor, backcolor });
            }
            else
            {
                runStatus_textBox.Text = text;
                runStatus_textBox.ForeColor = forecolor;
                runStatus_textBox.BackColor = backcolor;
                runStatus_textBox.Update();
            }
        }


        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Properties.Settings.Default.Working_Dir;
            dlg.CheckFileExists = true;
            dlg.SupportMultiDottedExtensions = true;
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string name in dlg.FileNames)
                {
                    if (!Files_listBox.Items.Contains(name))
                    {
                        Files_listBox.Items.Add(name);
                        Properties.Settings.Default.Files.Add(name);
                    }
                }
                Properties.Settings.Default.Save();
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (int i in Files_listBox.SelectedIndices)
                Files_listBox.Items.RemoveAt(i);
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            output_textBox.Text = "";
            run_button.Enabled = false;
            int rc = -1;
            try
            {
                rc = Code();
            }
            catch (Exception ex)
            {
                setOutputStatus(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                run_button.Enabled = true;
            }

            if (rc != 0)
            {
                setRunStatus("FAIL", Color.Black, Color.Red);
            }
            else
            {
                setRunStatus("PASS", Color.Black, Color.Green);
            }

        }

        int Code()
        {
            setRunStatus("Code Device");

            Commander.Device = device_comboBox.Text;
            Commander.IP = DBA_ip_textBox.Text;
            Commander.SetDgbMode("OUT");

            string[] files = new string[Files_listBox.Items.Count];
            Files_listBox.Items.CopyTo(files, 0);

            Commander.Flash(files, mfgStr_textBox.Text, false);

            while (true)
            {
                setRunStatus("Range Test Start");

                IRangeTester rangeTester = new EFR32xRT();
                rangeTester.Server_Host = RTM_ip_textBox.Text;
                rangeTester.Server_Port = Properties.Settings.Default.RTM_port;
                rangeTester.Client_Host = DBA_ip_textBox.Text;
                rangeTester.Client_Port = Properties.Settings.Default.RTC_port;
                rangeTester.Channel = Convert.ToInt32(channel_numericUpDown.Value);

                rangeTester.Server_Init();
                Thread.Sleep(500);
                RtPingResults ping = rangeTester.Ping(Properties.Settings.Default.PingCount);

                string msg = "";
                msg += $"TxLqi:{ping.TxLqi} ({Properties.Settings.Default.TxLqi})\r\n";
                msg += $"TxSsi:{ping.TxRssi} ({Properties.Settings.Default.TxRssi})\r\n";
                msg += $"RxLqi:{ping.RxLqi} ({Properties.Settings.Default.RxLqi})\r\n";
                msg += $"RxRssi:{ping.RxRssi} ({Properties.Settings.Default.RxRssi})";

                if (ping.TxLqi >= Properties.Settings.Default.TxLqi && ping.TxRssi >= Properties.Settings.Default.TxRssi &&
                    ping.RxLqi >= Properties.Settings.Default.RxLqi && ping.RxRssi >= Properties.Settings.Default.RxRssi)
                {
                    setOutputStatus($"Range Test Passed:\r\n{msg}");
                    break;
                }
                else
                {
                    setOutputStatus($"Range Test Failed:\r\n{msg}");
                    if (MessageBox.Show(msg, "Range Test Failed", MessageBoxButtons.RetryCancel) == DialogResult.Abort)
                    {
                        break;
                    }
                }
            }

            return 0;

        }

        private void mfgStr_textBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MFgString = mfgStr_textBox.Text;
            Properties.Settings.Default.Save();
        }

        private void DBA_ip_textBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DBA_ip = DBA_ip_textBox.Text;
            Properties.Settings.Default.Save();
        }

        private void RTM_ip_textBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RTM_ip = RTM_ip_textBox.Text;
            Properties.Settings.Default.Save();
        }

        private void channel_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Channel = Convert.ToInt32(channel_numericUpDown.Value);
            Properties.Settings.Default.Save();
        }
    }
}
