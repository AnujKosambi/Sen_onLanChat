namespace SEN_Project_v1
{
    partial class AudioControl
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
            this.audioDevices = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // audioDevices
            // 
            this.audioDevices.Dock = System.Windows.Forms.DockStyle.Top;
            this.audioDevices.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.audioDevices.FormattingEnabled = true;
            this.audioDevices.Location = new System.Drawing.Point(0, 0);
            this.audioDevices.Margin = new System.Windows.Forms.Padding(10);
            this.audioDevices.Name = "audioDevices";
            this.audioDevices.Size = new System.Drawing.Size(317, 21);
            this.audioDevices.TabIndex = 0;
            // 
            // AudioControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.audioDevices);
            this.Name = "AudioControl";
            this.Size = new System.Drawing.Size(317, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox audioDevices;

    }
}
