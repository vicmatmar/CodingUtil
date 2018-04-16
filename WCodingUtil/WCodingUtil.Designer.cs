namespace WCodingUtil
{
    partial class WCodingUtil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.device_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DBA_ip_textBox = new System.Windows.Forms.TextBox();
            this.RTM_ip_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.run_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Files_listBox = new System.Windows.Forms.ListBox();
            this.AddFiles_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runStatus_textBox = new System.Windows.Forms.TextBox();
            this.output_textBox = new System.Windows.Forms.TextBox();
            this.mfgStr_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.channel_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AddFiles_contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.channel_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device:";
            // 
            // device_comboBox
            // 
            this.device_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.device_comboBox.FormattingEnabled = true;
            this.device_comboBox.Items.AddRange(new object[] {
            "EFR32MG13P732F512GM48"});
            this.device_comboBox.Location = new System.Drawing.Point(408, 24);
            this.device_comboBox.Name = "device_comboBox";
            this.device_comboBox.Size = new System.Drawing.Size(168, 21);
            this.device_comboBox.TabIndex = 1;
            this.device_comboBox.Text = "EFR32MG13P732F512GM48";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DBA:";
            // 
            // DBA_ip_textBox
            // 
            this.DBA_ip_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DBA_ip_textBox.Location = new System.Drawing.Point(582, 25);
            this.DBA_ip_textBox.Name = "DBA_ip_textBox";
            this.DBA_ip_textBox.Size = new System.Drawing.Size(88, 20);
            this.DBA_ip_textBox.TabIndex = 2;
            this.DBA_ip_textBox.TextChanged += new System.EventHandler(this.DBA_ip_textBox_TextChanged);
            // 
            // RTM_ip_textBox
            // 
            this.RTM_ip_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RTM_ip_textBox.Location = new System.Drawing.Point(676, 24);
            this.RTM_ip_textBox.Name = "RTM_ip_textBox";
            this.RTM_ip_textBox.Size = new System.Drawing.Size(88, 20);
            this.RTM_ip_textBox.TabIndex = 3;
            this.RTM_ip_textBox.TextChanged += new System.EventHandler(this.RTM_ip_textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(673, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "RTM:";
            // 
            // run_button
            // 
            this.run_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.run_button.Location = new System.Drawing.Point(838, 21);
            this.run_button.Name = "run_button";
            this.run_button.Size = new System.Drawing.Size(75, 23);
            this.run_button.TabIndex = 5;
            this.run_button.Text = "&Run";
            this.run_button.UseVisualStyleBackColor = true;
            this.run_button.Click += new System.EventHandler(this.run_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fiiles:";
            // 
            // Files_listBox
            // 
            this.Files_listBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Files_listBox.ContextMenuStrip = this.AddFiles_contextMenuStrip;
            this.Files_listBox.FormattingEnabled = true;
            this.Files_listBox.Location = new System.Drawing.Point(12, 25);
            this.Files_listBox.Name = "Files_listBox";
            this.Files_listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.Files_listBox.Size = new System.Drawing.Size(311, 30);
            this.Files_listBox.TabIndex = 0;
            // 
            // AddFiles_contextMenuStrip
            // 
            this.AddFiles_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.AddFiles_contextMenuStrip.Name = "AddFiles_contextMenuStrip";
            this.AddFiles_contextMenuStrip.Size = new System.Drawing.Size(123, 48);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.addFilesToolStripMenuItem.Text = "&Add Files";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.addFilesToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // runStatus_textBox
            // 
            this.runStatus_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runStatus_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runStatus_textBox.Location = new System.Drawing.Point(12, 61);
            this.runStatus_textBox.Name = "runStatus_textBox";
            this.runStatus_textBox.ReadOnly = true;
            this.runStatus_textBox.Size = new System.Drawing.Size(901, 35);
            this.runStatus_textBox.TabIndex = 6;
            // 
            // output_textBox
            // 
            this.output_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output_textBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.output_textBox.Location = new System.Drawing.Point(12, 102);
            this.output_textBox.Multiline = true;
            this.output_textBox.Name = "output_textBox";
            this.output_textBox.ReadOnly = true;
            this.output_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output_textBox.Size = new System.Drawing.Size(901, 380);
            this.output_textBox.TabIndex = 7;
            this.output_textBox.WordWrap = false;
            // 
            // mfgStr_textBox
            // 
            this.mfgStr_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mfgStr_textBox.Location = new System.Drawing.Point(329, 25);
            this.mfgStr_textBox.Name = "mfgStr_textBox";
            this.mfgStr_textBox.Size = new System.Drawing.Size(73, 20);
            this.mfgStr_textBox.TabIndex = 8;
            this.mfgStr_textBox.TextChanged += new System.EventHandler(this.mfgStr_textBox_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "MFG String:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(767, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Channel:";
            // 
            // channel_numericUpDown
            // 
            this.channel_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.channel_numericUpDown.Location = new System.Drawing.Point(770, 25);
            this.channel_numericUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.channel_numericUpDown.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.channel_numericUpDown.Name = "channel_numericUpDown";
            this.channel_numericUpDown.ReadOnly = true;
            this.channel_numericUpDown.Size = new System.Drawing.Size(45, 20);
            this.channel_numericUpDown.TabIndex = 12;
            this.channel_numericUpDown.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.channel_numericUpDown.ValueChanged += new System.EventHandler(this.channel_numericUpDown_ValueChanged);
            // 
            // WCodingUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 494);
            this.Controls.Add(this.channel_numericUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mfgStr_textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.output_textBox);
            this.Controls.Add(this.runStatus_textBox);
            this.Controls.Add(this.Files_listBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.run_button);
            this.Controls.Add(this.RTM_ip_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DBA_ip_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.device_comboBox);
            this.Controls.Add(this.label1);
            this.Name = "WCodingUtil";
            this.Text = "WCodeUtil";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.AddFiles_contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.channel_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox device_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DBA_ip_textBox;
        private System.Windows.Forms.TextBox RTM_ip_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button run_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox Files_listBox;
        private System.Windows.Forms.ContextMenuStrip AddFiles_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.TextBox runStatus_textBox;
        private System.Windows.Forms.TextBox output_textBox;
        private System.Windows.Forms.TextBox mfgStr_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown channel_numericUpDown;
    }
}

