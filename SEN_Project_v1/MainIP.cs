using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebCam_Capture;
using System.Runtime.InteropServices;
using System.Security;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace SEN_Project_v1
{
    public partial class MainIP : Form
    {
        #region INIT
        public static UdpClient memberRetrive = null;
        static int PORT = 1716;
        IPEndPoint BROADCAST_SENDING = new IPEndPoint(IPAddress.Parse("255.255.255.255"), PORT);
        IPEndPoint BROADCAST_RECIVEING= new IPEndPoint(IPAddress.Any, PORT);
        public static VideoChatting _videoChatting;
        Thread tBroadCast_r=null;
        #endregion

        public MainIP()
        {
            memberRetrive = new UdpClient(PORT);
      
            InitializeComponent();
 
        }
        private void MainIP_Load(object sender, EventArgs e)
        {
            tBroadCast_r = new Thread(new ThreadStart(vrecving_proc));
            tBroadCast_r.Start();
        }
        private void vrecving_proc()
        {
            
            while(true)
            {
                BeginInvoke((Action)(()=>{statusText.Text = "Ready For Reciving";
                System.Diagnostics.Debug.WriteLine(statusText.Text);
                }));


                byte[] data;
                IPEndPoint receving= new IPEndPoint(IPAddress.Any, PORT);
                data = memberRetrive.Receive(ref receving);
                string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
                System.Diagnostics.Debug.WriteLine("received:"+stringData+" "+receving.Address);
                BeginInvoke((Action)(() =>
                {
                    Label l = new Label();
                    l.Text = receving.Address.ToString();
                    tableLayoutPanel.Controls.Add(l);
                }));

            }
        }
        private void MainIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            AbortThread(tBroadCast_r);
            if (memberRetrive != null) memberRetrive.Close();
        }
        private void AbortThread(Thread t){
            
            if (t!= null && t.IsAlive)
                t.Abort();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {

        }

    }

}
