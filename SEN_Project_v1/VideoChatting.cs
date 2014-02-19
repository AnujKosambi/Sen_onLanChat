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

namespace SEN_Project_v1
{
    public partial class VideoChatting : Form
    {
        public VideoChatting()
        {
            InitializeComponent();
           CamDevice c= new CamDevice(0);
            c.ShowWindow(pictureBox);
             Thread t = new Thread(new ThreadStart(()=>{
             while(true)
             {
                 c.Capture();
                 if (Clipboard.GetDataObject() != null)
                 if (Clipboard.GetDataObject().GetDataPresent(typeof(System.Drawing.Bitmap)))
                 {
                     Bitmap b =(Bitmap)Clipboard.GetDataObject().GetData(typeof(System.Drawing.Bitmap));
                     System.Diagnostics.Debug.WriteLine(b.Size);
                 }


             }

            }));
             t.Start();
            
        }
    }
    
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
            SendMessage(deviceHandle, WM_CAP_EDIT_COPY, 640, 480);
        }
        public void Stop()
        {
            SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, this.index, 0);

            DestroyWindow(deviceHandle);
        }
    }
}
