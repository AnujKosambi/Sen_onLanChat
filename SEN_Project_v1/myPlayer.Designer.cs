namespace SEN_Project_v1
{
    partial class myPlayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.b_stop = new System.Windows.Forms.Button();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.AutoSize = true;
            this.buttonsPanel.Controls.Add(this.trackBar);
            this.buttonsPanel.Controls.Add(this.b_stop);
            this.buttonsPanel.Location = new System.Drawing.Point(0, 208);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(298, 79);
            this.buttonsPanel.TabIndex = 1;
            // 
            // b_stop
            // 
            this.b_stop.AllowDrop = true;
            this.b_stop.AutoSize = true;
            this.b_stop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.b_stop.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_stop.Font = new System.Drawing.Font("Webdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.b_stop.Location = new System.Drawing.Point(0, 0);
            this.b_stop.Name = "b_stop";
            this.b_stop.Size = new System.Drawing.Size(298, 34);
            this.b_stop.TabIndex = 0;
            this.b_stop.Text = "4";
            this.b_stop.UseVisualStyleBackColor = true;
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar.Location = new System.Drawing.Point(0, 34);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(298, 45);
            this.trackBar.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(301, 290);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // myPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.pictureBox);
            this.Name = "myPlayer";
            this.Size = new System.Drawing.Size(301, 290);
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button b_stop;
        private System.Windows.Forms.TrackBar trackBar;
        public System.Windows.Forms.PictureBox pictureBox;
    }
}
