using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCodingUtil
{
    public partial class Form1 : Form
    {
        delegate void setTextColorCallback(string txt, Color forecolor, Color backcolor);

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            run_button.Focus();
            runStatus_Init();
        }

        void runStatus_Init()
        {
            runStatus_textBox.BackColor = Color.White;
            runStatus_textBox.ForeColor = Color.Black;
            runStatus_textBox.Clear();
            runStatus_textBox.Text = "Ready";
            runStatus_textBox.Update();


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
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                foreach(string name in dlg.FileNames)
                {
                    if(!Files_listBox.Items.Contains(name))
                        Files_listBox.Items.Add(name);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (int i in Files_listBox.SelectedIndices)
                Files_listBox.Items.RemoveAt(i);
        }

    }
}
