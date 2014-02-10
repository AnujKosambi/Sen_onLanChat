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
            this.startChatButton = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sendBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startChatButton
            // 
            this.startChatButton.Location = new System.Drawing.Point(12, 287);
            this.startChatButton.Name = "startChatButton";
            this.startChatButton.Size = new System.Drawing.Size(316, 23);
            this.startChatButton.TabIndex = 0;
            this.startChatButton.Text = "Chat";
            this.startChatButton.UseVisualStyleBackColor = true;
            // 
            // statusText
            // 
            this.statusText.Location = new System.Drawing.Point(13, 13);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(315, 20);
            this.statusText.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.5F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(13, 130);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(315, 151);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // sendBox
            // 
            this.sendBox.Location = new System.Drawing.Point(13, 39);
            this.sendBox.Name = "sendBox";
            this.sendBox.Size = new System.Drawing.Size(315, 20);
            this.sendBox.TabIndex = 4;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(12, 65);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(316, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // MainIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 322);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.sendBox);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.startChatButton);
            this.Name = "MainIP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPMsg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainIP_FormClosing);
            this.Load += new System.EventHandler(this.MainIP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startChatButton;
        private System.Windows.Forms.TextBox statusText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox sendBox;
        private System.Windows.Forms.Button sendButton;
    }
}

