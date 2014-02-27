using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
namespace SEN_Project_v1
{
    public partial class VideoChatting : Form
    {
        FilterInfoCollection infos;
        VideoCaptureDevice capture = new VideoCaptureDevice();
        RTPHelper videoRTP;
        System.Net.IPEndPoint ip = null;
        private AudioControl audioControl;
        private RTPHelper audioRTP;
        public VideoChatting(System.Net.IPEndPoint ip)
        {
            this.ip = ip;
            InitializeComponent();
            pictureBox_Sender.BackColor = Color.Black;
            audioControl = new AudioControl();
            this.audioPanel.Controls.Add(audioControl);
        }

        private void VideoChatting_Load(object sender, EventArgs e)
        {
            infos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo info in infos)
            {
                deviceList.Items.Add(info.Name);

            }
            deviceList.SelectedIndex = 0;
            timer.Enabled = true;

     //      timer.Tick += timer_Tick;
            timer.Interval = 750;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            
            if (audioControl!=null  && audioControl.listBytes.Count > 0)
            {

                int oldsize = audioControl.listBytes.Count;
                System.Diagnostics.Debug.WriteLine(oldsize);
               
                audioRTP.rtpSender.Send(audioControl.listBytes.ToArray());
                audioControl.listBytes = audioControl.listBytes.Skip(oldsize).ToList();
           
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (capture.IsRunning == true)
            {
                b_start.Text = "Start";
                capture.Stop();
                audioControl.sourceStream.StopRecording();
         //       timer.Stop();
            }
            else
            {
//                timer.Start();

                videoRTP = new RTPHelper(this, ip, MSR.LST.Net.Rtp.PayloadType.Chat);
          //      audioRTP = new RTPHelper(this, new System.Net.IPEndPoint(ip.Address, ++ip.Port), MSR.LST.Net.Rtp.PayloadType.dynamicAudio);
                audioControl.sourceStream.StartRecording();
         
                capture = new VideoCaptureDevice(infos[deviceList.SelectedIndex].MonikerString);
                capture.NewFrame += capture_NewFrame;
                capture.Start();

                b_start.Text = "Stop";
            }
        }

        void capture_NewFrame(object sender, NewFrameEventArgs eventArgs) 
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
         

                BeginInvoke((Action)(() =>
                {
                    pictureBox_Sender.Image = bitmap;
                 
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MemoryStream withSize = new MemoryStream();
                    Byte[] length = new Byte[4];
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < BitConverter.GetBytes(ms.GetBuffer().Length).Length)
                            length[i] = BitConverter.GetBytes(ms.GetBuffer().Length)[i];
                    }
                    withSize.Write(length, 0, 4);
                    withSize.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            
                    if (audioControl != null && audioControl.listBytes.Count > 0)
                    {

                        int oldsize = audioControl.listBytes.Count;
                        withSize.Write(audioControl.listBytes.ToArray(), 0, oldsize);
                        audioControl.listBytes = audioControl.listBytes.Skip(oldsize).ToList();

                    }
                    videoRTP.rtpSender.Send(withSize.GetBuffer());
                }));



        }

        private void VideoChatting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
                capture.Stop();

            if (timer != null)
                timer.Stop();
       


        }
    }
}
