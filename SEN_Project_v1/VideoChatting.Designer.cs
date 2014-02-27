namespace SEN_Project_v1
{
    partial class VideoChatting
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
            this.pictureBox_Sender = new System.Windows.Forms.PictureBox();
            this.b_start = new System.Windows.Forms.Button();
            this.deviceList = new System.Windows.Forms.ComboBox();
            this.pictureBox_rec = new System.Windows.Forms.PictureBox();
            this.audioPanel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Sender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rec)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Sender
            // 
            this.pictureBox_Sender.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Sender.Name = "pictureBox_Sender";
            this.pictureBox_Sender.Size = new System.Drawing.Size(196, 203);
            this.pictureBox_Sender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Sender.TabIndex = 1;
            this.pictureBox_Sender.TabStop = false;
            // 
            // b_start
            // 
            this.b_start.Location = new System.Drawing.Point(214, 221);
            this.b_start.Name = "b_start";
            this.b_start.Size = new System.Drawing.Size(214, 23);
            this.b_start.TabIndex = 3;
            this.b_start.Text = "Start";
            this.b_start.UseVisualStyleBackColor = true;
            this.b_start.Click += new System.EventHandler(this.Start_Click);
            // 
            // deviceList
            // 
            this.deviceList.FormattingEnabled = true;
            this.deviceList.Location = new System.Drawing.Point(12, 221);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(183, 21);
            this.deviceList.TabIndex = 4;
            // 
            // pictureBox_rec
            // 
            this.pictureBox_rec.Location = new System.Drawing.Point(214, 12);
            this.pictureBox_rec.Name = "pictureBox_rec";
            this.pictureBox_rec.Size = new System.Drawing.Size(215, 203);
            this.pictureBox_rec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_rec.TabIndex = 5;
            this.pictureBox_rec.TabStop = false;
            // 
            // audioPanel
            // 
            this.audioPanel.AutoSize = true;
            this.audioPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.audioPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.audioPanel.Location = new System.Drawing.Point(0, 326);
            this.audioPanel.Name = "audioPanel";
            this.audioPanel.Size = new System.Drawing.Size(431, 0);
            this.audioPanel.TabIndex = 7;
            // 
            // VideoChatting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 326);
            this.Controls.Add(this.audioPanel);
            this.Controls.Add(this.pictureBox_rec);
            this.Controls.Add(this.deviceList);
            this.Controls.Add(this.b_start);
            this.Controls.Add(this.pictureBox_Sender);
            this.Name = "VideoChatting";
            this.Text = "VideoChatting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoChatting_FormClosing);
            this.Load += new System.EventHandler(this.VideoChatting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Sender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Sender;
        private System.Windows.Forms.Button b_start;
        private System.Windows.Forms.ComboBox deviceList;
        public System.Windows.Forms.PictureBox pictureBox_rec;
        private System.Windows.Forms.Panel audioPanel;
        private System.Windows.Forms.Timer timer;

    }
}