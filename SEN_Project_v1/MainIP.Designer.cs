namespace SEN_Project_v1
{
    partial class MainIP
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
            this.statusText = new System.Windows.Forms.TextBox();
            this.sendBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileSelectButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusText
            // 
            this.statusText.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusText.Enabled = false;
            this.statusText.Location = new System.Drawing.Point(0, 0);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(422, 20);
            this.statusText.TabIndex = 1;
            // 
            // sendBox
            // 
            this.sendBox.AllowDrop = true;
            this.sendBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sendBox.Location = new System.Drawing.Point(0, 191);
            this.sendBox.Multiline = true;
            this.sendBox.Name = "sendBox";
            this.sendBox.Size = new System.Drawing.Size(422, 61);
            this.sendBox.TabIndex = 4;
            this.sendBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.sendBox_DragDrop);
            this.sendBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.sendBox_DragEnter);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.Location = new System.Drawing.Point(12, 10);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(257, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(422, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // fileSelectButton
            // 
            this.fileSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectButton.Location = new System.Drawing.Point(357, 10);
            this.fileSelectButton.Name = "fileSelectButton";
            this.fileSelectButton.Size = new System.Drawing.Size(53, 23);
            this.fileSelectButton.TabIndex = 9;
            this.fileSelectButton.Text = "File.";
            this.fileSelectButton.UseVisualStyleBackColor = true;
            this.fileSelectButton.Click += new System.EventHandler(this.fileSelectButton_Click);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.sendButton);
            this.buttonPanel.Controls.Add(this.fileSelectButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 276);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(422, 45);
            this.buttonPanel.TabIndex = 10;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.tableLayoutPanel);
            this.MainPanel.Controls.Add(this.sendBox);
            this.MainPanel.Controls.Add(this.statusText);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(422, 252);
            this.MainPanel.TabIndex = 11;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(422, 171);
            this.tableLayoutPanel.TabIndex = 9;
            // 
            // MainIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 321);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainIP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OnLanChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainIP_FormClosing);
            this.Load += new System.EventHandler(this.MainIP_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox statusText;
        private System.Windows.Forms.TextBox sendBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button fileSelectButton;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

