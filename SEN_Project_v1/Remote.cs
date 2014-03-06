using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
namespace SEN_Project_v1
{
    public partial class Remote : Form
    {
        /// <summary>
        /// For keystroke
        /// </summary>
        private static IntPtr _hookID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        /// <summary>
        /// 
        /// </summary>
        public int PORT = 12345;
        IPEndPoint ipendpoint;
        Thread serverThread = null;
        Thread clientThread = null;
        RTPHelper rtpHelper = null;
        static List<VKeys> onKeys = null;
        static Boolean[] dic_Keys = null;
         ScreenCapture screenCapture = null;
         private List<Process> processlist;

        public Remote(System.Net.IPEndPoint ip)
        {
            InitializeComponent();
            ipendpoint = ip;
        //    ipendpoint.Port = PORT;
            screenCapture = new ScreenCapture();
            onKeys = new List<VKeys>();
            dic_Keys = Enumerable.Repeat(false, 300).ToArray();
   
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Byte[] length = new Byte[4];
            length = BitConverter.GetBytes(screenCapture.GetDesktopBitmapBytes().Length);
            ms.Write(length, 0, length.Length);
            Byte[] image=screenCapture.GetWindowBitmapBytes("Untitled - Notepad");
            
            ms.Write(image, 0, image.Length);
            rtpHelper.rtpSender.Send(ms.GetBuffer());
          //  System.Diagnostics.Debug.WriteLine(string.Join("-", onKeys.Select(x => Convert.ToString(x)).ToArray()));
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void b_makeServer_Click(object sender, EventArgs e)
        {
            processlist = Process.GetProcesses().ToList();

  
           
            timer.Tick += timer_Tick;
            timer.Start();
        }
   
   
        private void b_connect_Click(object sender, EventArgs e)
        {
                  
       //    SetCursorPos(200, 100);
        //   mouse_event(0x00000002, 0, 0, 0, UIntPtr.Zero); /// left mouse button down
        //   mouse_event(0x00000004, 0, 0, 0, UIntPtr.Zero); /// left mouse button up
       //     IntPtr handle=System.Diagnostics.Process.GetProcessesByName("notepad")[0].MainWindowHandle;
     //       SendMessage(handle, (int)0x0102, (int)VKeys.VK_P, 1);
           // SendMessage(handle, WM_SETTEXT, 0, " key");
            
        //    SetForegroundWindow(handle);
         //   SendMessage(handle, (int)WMessages.WM_KEYDOWN, (int)VKeys.VK_P, 0);
        }

        private void client_proc()
        {

          
        }
        private void server_proc()
        {
            
            
            
            while (true)
            {
                RemoteData rd = new RemoteData();
                POINT point;
                if(GetCursorPos(out point))
                {
                    rd.mouseX = (UInt16)point.X;
                    rd.mouseY = (UInt16)point.Y;
                    rd.mouseStatus = (byte)MouseStatus.MOUSEEVENTF_MOVE ;
                    rd.keys = 10;
                    int size = Marshal.SizeOf(rd);
                    byte[] arr = new byte[size];
                    IntPtr ptr = Marshal.AllocHGlobal(size);

                    Marshal.StructureToPtr(rd, ptr, true);
                    Marshal.Copy(ptr, arr, 0, size);
                    Marshal.FreeHGlobal(ptr);
            //        System.Diagnostics.Debug.WriteLine(Marshal.SizeOf(rd));
           //         System.Diagnostics.Debug.WriteLine(string.Join("-",arr.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray()));

                }
             
                
                    


            }

        }
        #region DLLIMPORT
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")][return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);
        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202,  //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,   //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100,  //Key down
            WM_KEYUP = 0x101,   //Key up
        }
        public enum VKeys : int
        {
            VK_LBUTTON = 0x01,   //Left mouse button 
            VK_RBUTTON = 0x02,   //Right mouse button 
            VK_CANCEL = 0x03,   //Control-break processing 
            VK_MBUTTON = 0x04,   //Middle mouse button (three-button mouse) 
            VK_BACK = 0x08,   //BACKSPACE key 
            VK_TAB = 0x09,   //TAB key 
            VK_CLEAR = 0x0C,   //CLEAR key 
            VK_RETURN = 0x0D,   //ENTER key 
            VK_SHIFT = 0x10,   //SHIFT key 
            VK_CONTROL = 0x11,   //CTRL key 
            VK_MENU = 0x12,   //ALT key 
            VK_PAUSE = 0x13,   //PAUSE key 
            VK_CAPITAL = 0x14,   //CAPS LOCK key 
            VK_ESCAPE = 0x1B,   //ESC key 
            VK_SPACE = 0x20,   //SPACEBAR 
            VK_PRIOR = 0x21,   //PAGE UP key 
            VK_NEXT = 0x22,   //PAGE DOWN key 
            VK_END = 0x23,   //END key 
            VK_HOME = 0x24,   //HOME key 
            VK_LEFT = 0x25,   //LEFT ARROW key 
            VK_UP = 0x26,   //UP ARROW key 
            VK_RIGHT = 0x27,   //RIGHT ARROW key 
            VK_DOWN = 0x28,   //DOWN ARROW key 
            VK_SELECT = 0x29,   //SELECT key 
            VK_PRINT = 0x2A,   //PRINT key
            VK_EXECUTE = 0x2B,   //EXECUTE key 
            VK_SNAPSHOT = 0x2C,   //PRINT SCREEN key 
            VK_INSERT = 0x2D,   //INS key 
            VK_DELETE = 0x2E,   //DEL key 
            VK_HELP = 0x2F,   //HELP key
            VK_0 = 0x30,   //0 key 
            VK_1 = 0x31,   //1 key 
            VK_2 = 0x32,   //2 key 
            VK_3 = 0x33,   //3 key 
            VK_4 = 0x34,   //4 key 
            VK_5 = 0x35,   //5 key 
            VK_6 = 0x36,    //6 key 
            VK_7 = 0x37,    //7 key 
            VK_8 = 0x38,   //8 key 
            VK_9 = 0x39,    //9 key 
            VK_A = 0x41,   //A key 
            VK_B = 0x42,   //B key 
            VK_C = 0x43,   //C key 
            VK_D = 0x44,   //D key 
            VK_E = 0x45,   //E key 
            VK_F = 0x46,   //F key 
            VK_G = 0x47,   //G key 
            VK_H = 0x48,   //H key 
            VK_I = 0x49,    //I key 
            VK_J = 0x4A,   //J key 
            VK_K = 0x4B,   //K key 
            VK_L = 0x4C,   //L key 
            VK_M = 0x4D,   //M key 
            VK_N = 0x4E,    //N key 
            VK_O = 0x4F,   //O key 
            VK_P = 0x50,    //P key 
            VK_Q = 0x51,   //Q key 
            VK_R = 0x52,   //R key 
            VK_S = 0x53,   //S key 
            VK_T = 0x54,   //T key 
            VK_U = 0x55,   //U key 
            VK_V = 0x56,   //V key 
            VK_W = 0x57,   //W key 
            VK_X = 0x58,   //X key 
            VK_Y = 0x59,   //Y key 
            VK_Z = 0x5A,    //Z key
            VK_NUMPAD0 = 0x60,   //Numeric keypad 0 key 
            VK_NUMPAD1 = 0x61,   //Numeric keypad 1 key 
            VK_NUMPAD2 = 0x62,   //Numeric keypad 2 key 
            VK_NUMPAD3 = 0x63,   //Numeric keypad 3 key 
            VK_NUMPAD4 = 0x64,   //Numeric keypad 4 key 
            VK_NUMPAD5 = 0x65,   //Numeric keypad 5 key 
            VK_NUMPAD6 = 0x66,   //Numeric keypad 6 key 
            VK_NUMPAD7 = 0x67,   //Numeric keypad 7 key 
            VK_NUMPAD8 = 0x68,   //Numeric keypad 8 key 
            VK_NUMPAD9 = 0x69,   //Numeric keypad 9 key 
            VK_SEPARATOR = 0x6C,   //Separator key 
            VK_SUBTRACT = 0x6D,   //Subtract key 
            VK_DECIMAL = 0x6E,   //Decimal key 
            VK_DIVIDE = 0x6F,   //Divide key
            VK_F1 = 0x70,   //F1 key 
            VK_F2 = 0x71,   //F2 key 
            VK_F3 = 0x72,   //F3 key 
            VK_F4 = 0x73,   //F4 key 
            VK_F5 = 0x74,   //F5 key 
            VK_F6 = 0x75,   //F6 key 
            VK_F7 = 0x76,   //F7 key 
            VK_F8 = 0x77,   //F8 key 
            VK_F9 = 0x78,   //F9 key 
            VK_F10 = 0x79,   //F10 key 
            VK_F11 = 0x7A,   //F11 key 
            VK_F12 = 0x7B,   //F12 key
            VK_SCROLL = 0x91,   //SCROLL LOCK key 
            VK_LSHIFT = 0xA0,   //Left SHIFT key
            VK_RSHIFT = 0xA1,   //Right SHIFT key
            VK_LCONTROL = 0xA2,   //Left CONTROL key
            VK_RCONTROL = 0xA3,    //Right CONTROL key
            VK_LMENU = 0xA4,      //Left MENU key
            VK_RMENU = 0xA5,   //Right MENU key
            VK_PLAY = 0xFA,   //Play key
            VK_ZOOM = 0xFB, //Zoom key 
        }

           #endregion      

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RemoteData
        {
            public UInt16 mouseX;
            public UInt16 mouseY;
            public Byte mouseStatus;
            public int keys;
            
        

        }
        public enum MouseStatus
        {
            MOUSEEVENTF_ABSOLUTE=0x8000,
            MOUSEEVENTF_LEFTDOWN=0x0002,
            MOUSEEVENTF_LEFTUP=0x0004,
            MOUSEEVENTF_MIDDLEDOWN=0x0020,
            MOUSEEVENTF_MIDDLEUP=0x0040,
            MOUSEEVENTF_MOVE=0x0001,
            MOUSEEVENTF_RIGHTDOWN=0x0008,
            MOUSEEVENTF_RIGHTUP=0x0010,
            MOUSEEVENTF_WHEEL=0x0800,
            MOUSEEVENTF_XDOWN=0x0080,
            MOUSEEVENTF_XUP=0x0100,

            MOUSEEVENTF_HWHEEL=0x01000
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
     
         private void Remote_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            if (clientThread != null && clientThread.IsAlive )
            {
                clientThread.Abort();

            }
            if (serverThread != null && serverThread.IsAlive)
            {
                serverThread.Abort();

            }
        }

        #region KeyStroke DLL
        [DllImport("kernel32.dll")]static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)][return: MarshalAs(UnmanagedType.Bool)]private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
        return SetWindowsHookEx(WH_KEYBOARD_LL, proc, LoadLibrary("User32"), 0);
          
        }

        #endregion
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN))
            {
                VKeys vkCode = (VKeys)Marshal.ReadInt32(lParam);
                if (!dic_Keys[(int)vkCode])
                    onKeys.Add(vkCode);
                dic_Keys[(int)vkCode] = true;
              /*  IntPtr handle = System.Diagnostics.Process.GetProcessesByName("notepad")[0].MainWindowHandle;
                      SendMessage(handle, (int)0x0102, (int)VKeys.VK_P, 1);
               //SendMessage(handle, WM_SETTEXT, 0, " key");
             
                  //  SetForegroundWindow(handle);
                   SendMessage(handle, (int)WMessages.WM_KEYDOWN, (int)VKeys.VK_P, 0);*/
                IntPtr handle = System.Diagnostics.Process.GetProcessesByName("notepad")[0].MainWindowHandle;
                SetForegroundWindow(handle);
                PostMessage(handle, WM_KEYDOWN, (IntPtr)(vkCode), IntPtr.Zero);
              
            }
            else if (nCode >= 0 && (wParam == (IntPtr)0x101))
            {
                VKeys vkCode =(VKeys) Marshal.ReadInt32(lParam);
                if(onKeys.Contains(vkCode))
                 onKeys.Remove(vkCode);
                dic_Keys[(int)vkCode]=false;

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        private void Remote_Load(object sender, EventArgs e)
        {
            rtpHelper = new RTPHelper(this.pictureBox, ipendpoint, MSR.LST.Net.Rtp.PayloadType.JPEG);
           _hookID = SetHook(_proc);
        
           
        }
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    }
}

