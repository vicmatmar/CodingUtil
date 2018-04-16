namespace WCodingUtil
{
    partial class Form1
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
            this.textBoxOutputStatus = new System.Windows.Forms.TextBox();
            this.AddFiles_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 14);
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
            this.device_comboBox.Location = new System.Drawing.Point(365, 11);
            this.device_comboBox.Name = "device_comboBox";
            this.device_comboBox.Size = new System.Drawing.Size(168, 21);
            this.device_comboBox.TabIndex = 1;
            this.device_comboBox.Text = "EFR32MG13P732F512GM48";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DBA ip:";
            // 
            // DBA_ip_textBox
            // 
            this.DBA_ip_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DBA_ip_textBox.Location = new System.Drawing.Point(588, 11);
            this.DBA_ip_textBox.Name = "DBA_ip_textBox";
            this.DBA_ip_textBox.Size = new System.Drawing.Size(88, 20);
            this.DBA_ip_textBox.TabIndex = 2;
            // 
            // RTM_ip_textBox
            // 
            this.RTM_ip_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RTM_ip_textBox.Location = new System.Drawing.Point(733, 11);
            this.RTM_ip_textBox.Name = "RTM_ip_textBox";
            this.RTM_ip_textBox.Size = new System.Drawing.Size(88, 20);
            this.RTM_ip_textBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(682, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "RTM ip:";
            // 
            // run_button
            // 
            this.run_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.run_button.Location = new System.Drawing.Point(827, 8);
            this.run_button.Name = "run_button";
            this.run_button.Size = new System.Drawing.Size(75, 23);
            this.run_button.TabIndex = 5;
            this.run_button.Text = "&Run";
            this.run_button.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 15);
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
            this.Files_listBox.Location = new System.Drawing.Point(52, 9);
            this.Files_listBox.Name = "Files_listBox";
            this.Files_listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.Files_listBox.Size = new System.Drawing.Size(257, 30);
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
            this.runStatus_textBox.Location = new System.Drawing.Point(12, 45);
            this.runStatus_textBox.Name = "runStatus_textBox";
            this.runStatus_textBox.ReadOnly = true;
            this.runStatus_textBox.Size = new System.Drawing.Size(890, 35);
            this.runStatus_textBox.TabIndex = 6;
            // 
            // textBoxOutputStatus
            // 
            this.textBoxOutputStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutputStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxOutputStatus.Location = new System.Drawing.Point(12, 86);
            this.textBoxOutputStatus.Multiline = true;
            this.textBoxOutputStatus.Name = "textBoxOutputStatus";
            this.textBoxOutputStatus.ReadOnly = true;
            this.textBoxOutputStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutputStatus.Size = new System.Drawing.Size(886, 396);
            this.textBoxOutputStatus.TabIndex = 7;
            this.textBoxOutputStatus.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 494);
            this.Controls.Add(this.textBoxOutputStatus);
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
            this.Name = "Form1";
            this.Text = "WCodeUtil";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.AddFiles_contextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBoxOutputStatus;
    }
}

