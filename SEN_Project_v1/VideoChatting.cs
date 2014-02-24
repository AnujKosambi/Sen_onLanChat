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
        VideoCaptureDevice capture=new VideoCaptureDevice();
        RTPHelper rtpHelper;
        public VideoChatting()
        {
            InitializeComponent();
            pictureBox_Sender.BackColor = Color.Black;
        }

        private void VideoChatting_Load(object sender, EventArgs e)
        {
            infos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo info in infos)
            {
                deviceList.Items.Add(info.Name);

            }
            deviceList.SelectedIndex = 0;
            
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (capture.IsRunning == true)
            {
                b_start.Text = "Start";
                capture.Stop();

            }
            else
            {
                if(rtpHelper==null)
                rtpHelper=new RTPHelper(this,new System.Net.IPEndPoint(System.Net.IPAddress.Parse("192.168.1.105"),1762));
                
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
            
                MemoryStream ms  = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                rtpHelper.rtpSender.Send(ms.GetBuffer());
                
            }));
          
         
            
        }

        private void VideoChatting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
                capture.Stop();
        }


    }
    /*
    public class CamDevice
    {
        #region dllpart
        private const short WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;

        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);
        #endregion
        int index;
        int deviceHandle;

        public CamDevice(int index)
        {
            this.index = index;
           
        }
        public void Init(int windowHeight, int windowWidth, int handle)
        {
            string deviceIndex = Convert.ToString(this.index);
            deviceHandle = capCreateCaptureWindowA(ref deviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, windowWidth, windowHeight, handle, 0);
            int i=0;
            while (SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, this.index, 0) <= 0 )
            {
            }
                
                SendMessage(deviceHandle, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, 0xff, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, 1, 0);

                SetWindowPos(deviceHandle, 1, 0, 0, windowWidth, windowHeight, 6);
            
        }
        public void ShowWindow(global::System.Windows.Forms.Control windowsControl)
        {
            Init(windowsControl.Height, windowsControl.Width, windowsControl.Handle.ToInt32());
        }
        public void Capture()
        {
            SendMessage(deviceHandle, WM_CAP_EDIT_COPY, 0, 0);
            IDataObject data = Clipboard.GetDataObject();
            Image oImage;
            if(data!=null)
              if(data.GetDataPresent( typeof(System.Drawing.Bitmap))){
                  oImage=(Image)data.GetData(typeof(System.Drawing.Bitmap));
                    System.Diagnostics.Debug.Write(oImage);
              }
        }
        public void Stop()
        {
            SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, this.index, 0);

            DestroyWindow(deviceHandle);
        }
    }*/
}
