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
using System.Xml.Linq;
using CodingUtil;
using CommanderLib;
using RangeTester;

namespace WCodingUtil
{
    public partial class WCodingUtilForm : Form
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        delegate void setControlPropertyValueCallback(Control control, object value, string property_name, bool update);
        delegate void setTextColorCallback(string txt, Color forecolor, Color backcolor);
        delegate void appendTextCallback(Control control, string txt);

        #region Model
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

                try
                {
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
                        if (MessageBoxEx.Show(msg, "Range Test Failed", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                        {
                            return -1;
                        }
                    }
                }
                catch(Exception ex)
                {
                    setOutputStatus($"Range Test Failed:\r\n{ex.Message}\r\n{ex.StackTrace}");
                    if (MessageBoxEx.Show(ex.Message, "Range Test Failed", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        return -1;
                    }

                }
            }

            setRunStatus("GetEUI");
            long eui = Commander.GetEUI();
            setRunStatus($"EUI = {eui.ToString("X16")}");

            setRunStatus("Insert EUI");
            int euiid = DatabaseUtils.InsertEUI(eui);
            setRunStatus($"EUI id = {euiid}");

            // Dummy insert needed for barscan
            int tdid = DatabaseUtils.AddTargetDevice(euiid);

            return 0;
        }
        #endregion

        #region GUI Init
        public WCodingUtilForm()
        {
            InitializeComponent();

            Version version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            Text += $" {version.ToString()}";

            Icon = Properties.Resources.Oxygen_Icons_org_Oxygen_Apps_preferences_web_browser_cache;
        }

        private void WCodingUtilForm_Load(object sender, EventArgs e)
        {

            loadPropertySettings();
            setRunStatus("Ready", Color.Black, Color.White);
            run_button.Select();

        }
        #endregion

        #region GUI helpers
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
        #endregion

        #region GUI Actions

        private void run_button_Click(object sender, EventArgs e)
        {
            output_textBox.Text = "";
            output_textBox.Update();
            settings_groupBox.Enabled = false;
            output_textBox.Focus();

            Task<int> task = new Task<int>(() => Code());

            // OnlyOnFaulted
            task.ContinueWith(a =>
           {
               _logger.Debug(a.Exception, "Error coding");

               setOutputStatus(a.Exception.InnerException.Message + "\r\n" + a.Exception.InnerException.StackTrace);
               setRunStatus("FAIL", Color.White, Color.Red);

               setControlPropertyValue(settings_groupBox, true, "Enabled");

               run_button.Select();


           }, TaskContinuationOptions.OnlyOnFaulted);

            // OnlyOnRanToCompletion
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

            saveSettingstoFile("CurrentSettings.xml");

        }
        #endregion

        #region Settings
        void loadPropertySettings()
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
        }

        void restoreSettingstoFile(string fileloc)
        {
            XDocument xdoc = XDocument.Load(fileloc);
            XElement settings = xdoc.Element("CodeSettings");

            if (!settings.HasElements)
                return;

            var properties = Properties.Settings.Default;

            var files = settings.Elements("Files");
            if(files.Any())
            {
                Properties.Settings.Default.Files = new System.Collections.Specialized.StringCollection();
                foreach (var file in files.Elements("File"))
                    properties.Files.Add(file.Value);
            }

            var s = settings.Element("MFGStr");
            if (s != null) properties.MFgString = s.Value;
            s = settings.Element("Device");
            if (s != null) properties.Device = s.Value;
            s = settings.Element("DBAip");
            if (s != null) properties.DBA_ip = s.Value;
            s = settings.Element("RTMip");
            if (s != null) properties.RTM_ip = s.Value;
            s = settings.Element("Channel");
            if (s != null) properties.Channel = Convert.ToInt32(s.Value);

            properties.Save();
            loadPropertySettings();

        }

        void saveSettingstoFile(string fileloc)
        {
            XDocument xdoc = new XDocument(new XElement("CodeSettings"));

            var properties = Properties.Settings.Default;

            var files = new XElement("Files");
            foreach (string file in properties.Files)
                files.Add(new XElement("File", file));

            xdoc.Root.Add(files);

            xdoc.Root.Add(new XElement("MFGStr", properties.MFgString));
            xdoc.Root.Add(new XElement("Device", properties.Device));
            xdoc.Root.Add(new XElement("DBAip", properties.DBA_ip));
            xdoc.Root.Add(new XElement("RTMip", properties.RTM_ip));
            xdoc.Root.Add(new XElement("Channel", properties.Channel));

            xdoc.Save(fileloc);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "*.xml|*.xml|All Files *.*|*.*";
            //dlg.Filter = "(*.xml)|.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                restoreSettingstoFile(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "*.xml|*.xml|All Files *.*|*.*";
            //dlg.Filter = "(*.xml)|.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                saveSettingstoFile(dlg.FileName);
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


        #endregion

    }
}
