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
using System.Net.NetworkInformation;
using System.Xml;
namespace SEN_Project_v1
{
    public partial class MainIP : Form
    {
        int REFRESHINTERVAL=10;
        #region INIT
        public static UdpClient receiviedClient= null;
        public static UdpClient sendingClient = null;
        Dictionary<IPAddress,Boolean> l_ipaddress = null;
        Dictionary<IPAddress, ListViewItem> list_ipaddress = null;

        static int PORT = 1716;
        static int PORT2 = 1617;
        IPEndPoint BROADCAST_SENDING = new IPEndPoint(IPAddress.Parse("255.255.255.255"), PORT);
        IPEndPoint BROADCAST_RECIVEING= new IPEndPoint(IPAddress.Any, PORT);
        public static VideoChatting _videoChatting;
        Thread tBroadCast_r=null;
        Thread tMemberRetriving = null;
        #endregion
        
        public MainIP()
        {
            receiviedClient = new UdpClient(PORT);
            sendingClient = new UdpClient(PORT2);
            l_ipaddress = new Dictionary<IPAddress, Boolean>();
            list_ipaddress = new Dictionary<IPAddress, ListViewItem>();
            InitializeComponent();
 
        }
        private void MainIP_Load(object sender, EventArgs e)
        {
        

            listView.CheckBoxes = true;
            
            listView.View = View.Details;
            tBroadCast_r = new Thread(new ThreadStart(vrecving_proc));
            tBroadCast_r.Start();
            tMemberRetriving = new Thread(new ThreadStart(() => {
                while (true)
                {

                    sendBroadcastMsg("<#Connect#>" + Environment.MachineName);
                    Thread.Sleep(1000 * REFRESHINTERVAL);
                }            
            }));
            tMemberRetriving.Start();        
        }
        private void vrecving_proc()
        {
            receiviedClient.Client.ReceiveBufferSize = 1024 * 1024;
            while(true)
            {
                BeginInvoke((Action)(()=>{
                   System.Diagnostics.Debug.WriteLine("Ready For Reciving");
                }));
                byte[] data;
                IPEndPoint receving= new IPEndPoint(IPAddress.Any, PORT);
                data = receiviedClient.Receive(ref receving);
                string stringData = Encoding.ASCII.GetString(data);
                System.Diagnostics.Debug.WriteLine("received:" + stringData);
                if (stringData.StartsWith("<#Connect#>"))
                {
                    String pc_name=stringData.Split(new String[] { "<#Connect#>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    receving.Port = 1716;
                    sendingClient.Connect(receving);
                    string response_data = "<\\#Connect#>" + Environment.MachineName;
                    sendingClient.Send(Encoding.ASCII.GetBytes(response_data),response_data.Length);
                    System.Diagnostics.Debug.WriteLine("Added:" + receving.Address);

                    AddItem(receving.Address,pc_name);
                }
                else if(stringData.StartsWith("<\\#Connect#>")){
                    System.Diagnostics.Debug.WriteLine("Added:"+receving.Address);
                    AddItem(receving.Address, stringData.Split(new String[] { "<\\#Connect#>" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
                else if (stringData == "<#Disconnect#>")
                {
                    l_ipaddress.Remove(receving.Address);
                     BeginInvoke((Action)(() =>
                {
                    listView.Items.Remove(list_ipaddress[receving.Address]);
                }));
                    System.Diagnostics.Debug.WriteLine("Removed:" + receving.Address);
                }
                else if (stringData.StartsWith("<#Message#>"))
                {
                    BeginInvoke((Action)(() =>
                    {
                        statusText.Text = stringData.Split(new String[] { "<#Message#>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }));
                }
               
              
           
            }
        }
        private void AddItem(IPAddress ipaddress,String name)
        {
       
            bool exist = false;
            l_ipaddress.TryGetValue(ipaddress, out exist);
            if (!exist)
            {
                l_ipaddress.Add(ipaddress, true);
                BeginInvoke((Action)(() =>
                {
                    ListViewItem item = new ListViewItem(new String[]{ipaddress.ToString(),name});
                    list_ipaddress[ipaddress]=(item);
                    listView.Items.Add(item);
                    

            
                }));
                String localPath = "";
                String[] ip_parts = ipaddress.ToString().Split('.');
                for (int i = 0; i < 3; i++)
                {
                    if (!System.IO.Directory.Exists(localPath + ip_parts[i])) ;
                    System.IO.Directory.CreateDirectory(localPath + ip_parts[i]);
                    localPath = localPath + ip_parts[i] + "/";
                }
            }
      
        }
        private void sendBroadcastMsg(string data)
        {
           
            sendingClient.Connect(BROADCAST_SENDING);
        
            sendingClient.Send(Encoding.ASCII.GetBytes(data), data.Length);
        }       
        private void sendButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.CheckedItems)
            {

                sendingClient.Connect(new IPEndPoint(list_ipaddress.ElementAt(item.Index).Key, PORT));
                String data = "<#Message#>" + sendBox.Text + "<#Message#>";
                sendingClient.Send(Encoding.ASCII.GetBytes(data), data.Length);

            }
            foreach (ListViewItem item in listView.SelectedItems)
            {

                sendingClient.Connect(new IPEndPoint(list_ipaddress.ElementAt(item.Index).Key, PORT));
                String data = "<#Message#>" + sendBox.Text + "<#Message#>";
                sendingClient.Send(Encoding.ASCII.GetBytes(data), data.Length);
            }
        }
        string getLocalIP()
        {
            string localIP="";
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)//ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 )
                {
                   
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            localIP=ip.Address.ToString();
                        }
                    }
                }
            }
            return localIP;
        }
        #region Closing Stuff
        private void MainIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendBroadcastMsg("<#Disconnect#>");

            AbortThread(tBroadCast_r);
            AbortThread(tMemberRetriving);

            if (receiviedClient != null) receiviedClient.Close();
            if (sendingClient != null) sendingClient.Close();

        }
        private void AbortThread(Thread t)
        {

            if (t != null && t.IsAlive)
                t.Abort();
        }
        #endregion
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingControl sc = new SettingControl();
            sc.ShowDialog();
        }

    }

}
