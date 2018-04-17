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
        delegate void setControlPropertyValueCallback(Control control, object value, string property_name, bool update);
        delegate void setTextColorCallback(string txt, Color forecolor, Color backcolor);
        delegate void appendTextCallback(Control control, string txt);

        public WCodingUtil()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Files_listBox.Items.Clear();

            if (Properties.Settings.Default.Files == null)
                Properties.Settings.Default.Files = new System.Collections.Specialized.StringCollection();
            string[] files = new string[Properties.Settings.Default.Files.Count];
            Properties.Settings.Default.Files.CopyTo(files, 0);
            Files_listBox.Items.AddRange(files);

            device_comboBox.Text = Properties.Settings.Default.Device;
            mfgStr_textBox.Text = Properties.Settings.Default.MFgString;
            dbaip_textBox.Text = Properties.Settings.Default.DBA_ip;
            rtmip_textBox.Text = Properties.Settings.Default.RTM_ip;
            channel_numericUpDown.Value = Properties.Settings.Default.Channel;

            setRunStatus("Ready", Color.Black, Color.White);


            run_button.Select();

        }

        void setControlPropertyValue(Control control, object value, string property_name = "Text", bool update = true)
        {
            if (control.InvokeRequired)
            {
                setControlPropertyValueCallback d = new setControlPropertyValueCallback(setControlPropertyValue);
                this.Invoke(d, new object[] { control, value, property_name, update });
            }
            else
            {
                var property = control.GetType().GetProperty(property_name);
                if (property != null)
                {
                    property.SetValue(control, value);
                }

                if (update)
                {
                    var method = control.GetType().GetMethod("Update");
                    if (method != null)
                    {
                        method.Invoke(control, null);
                    }
                }
            }
        }


        void appendText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                appendTextCallback d = new appendTextCallback(appendText);
                this.Invoke(d, new object[] { control, text });
            }
            else
            {
                string line = $"{DateTime.Now}: {text}\r\n";

                var method = control.GetType().GetMethod("AppendText");
                if (method != null)
                {
                    method.Invoke(control, new object[] { line });
                }
                method = control.GetType().GetMethod("Update");
                if (method != null)
                {
                    method.Invoke(control, null);
                }
            }
        }

        void setOutputStatus(string text)
        {
            appendText(output_textBox, text);
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
                runStatus_textBox.ForeColor = forecolor;
                runStatus_textBox.BackColor = backcolor;
                runStatus_textBox.Text = text;
                //runStatus_textBox.SelectionStart = 0;
                runStatus_textBox.Update();
            }
        }


        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Properties.Settings.Default.Working_Dir;
            dlg.CheckFileExists = true;
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
            output_textBox.Update();
            settings_groupBox.Enabled = false;
            output_textBox.Focus();

            Task<int> task = new Task<int>(() => Code());

            task.ContinueWith(a =>
           {
               setOutputStatus(a.Exception.InnerException.Message + "\r\n" + a.Exception.InnerException.StackTrace);
               setRunStatus("FAIL", Color.White, Color.Red);

               setControlPropertyValue(settings_groupBox, true, "Enabled");

               run_button.Select();


           }, TaskContinuationOptions.OnlyOnFaulted);

            task.ContinueWith(a =>
           {

               if (task.Result == 0)
               {
                   setOutputStatus("Coding sequence completed without error");
                   setRunStatus("PASS", Color.White, Color.Green);
               }
               else
               {
                   setOutputStatus($"Coding sequence completed with error code: {task.Result}");
                   setRunStatus("FAIL", Color.White, Color.Red);
               }

               setControlPropertyValue(settings_groupBox, true, "Enabled");
               run_button.Select();

           }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.Start();

        }

        int Code()
        {
            setRunStatus("Code Device");

            Commander.Device = Properties.Settings.Default.Device;
            Commander.IP = Properties.Settings.Default.DBA_ip;
            Commander.SetDgbMode("OUT");

            string[] files = new string[Properties.Settings.Default.Files.Count];
            Properties.Settings.Default.Files.CopyTo(files, 0);
            Commander.Flash(files, mfgStr_textBox.Text, false);

            while (true)
            {
                setRunStatus("Range Test Start");

                IRangeTester rangeTester = new EFR32xRT();
                rangeTester.Server_Host = Properties.Settings.Default.RTM_ip;
                rangeTester.Server_Port = Properties.Settings.Default.RTM_port;
                rangeTester.Client_Host = Properties.Settings.Default.DBA_ip;
                rangeTester.Client_Port = Properties.Settings.Default.RTC_port;
                rangeTester.Channel = Properties.Settings.Default.Channel;

                rangeTester.Server_Init();
                Thread.Sleep(500);
                RtPingResults ping = rangeTester.Ping(Properties.Settings.Default.PingCount);

                string msg = "";
                msg += $"TxLqi:{ping.TxLqi} ({Properties.Settings.Default.TxLqi})\r\n";
                msg += $"TxRssi:{ping.TxRssi} ({Properties.Settings.Default.TxRssi})\r\n";
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
                    if (MessageBox.Show(msg, "Range Test Failed", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        return -1;
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
            Properties.Settings.Default.DBA_ip = dbaip_textBox.Text;
            Properties.Settings.Default.Save();
        }

        private void RTM_ip_textBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RTM_ip = rtmip_textBox.Text;
            Properties.Settings.Default.Save();
        }

        private void channel_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Channel = Convert.ToInt32(channel_numericUpDown.Value);
            Properties.Settings.Default.Save();
        }

        private void device_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Device = device_comboBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
