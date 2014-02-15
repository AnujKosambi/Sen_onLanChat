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
        int REFRESHINTERVAL=20;
        #region INIT
        public static UdpClient receiviedClient= null;
        public static UdpClient sendingClient = null;
        List<IPAddress> l_selectedaddress = null;
        List<IPAddress> l_ipaddress = null;
        Dictionary<IPAddress,Client> d_client = null;
       // Dictionary<IPAddress, ListViewItem> list_ipaddress = null;
        Dictionary<IPAddress, Control> list_ipaddress = null;

        public static TcpClient tcpSendingClient = null;

        static int PORT = 1716;
        static int PORT2 = 1617;
        static int PORTTCP = 12316;
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
            
            l_ipaddress = new List<IPAddress>();
            l_selectedaddress = new List<IPAddress>();
            d_client = new Dictionary<IPAddress, Client>();
            list_ipaddress = new Dictionary<IPAddress, Control>();
            InitializeComponent();
 
        }
        private void MainIP_Load(object sender, EventArgs e)
        {
        

           // listView.CheckBoxes = true;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
          

          //  listView.View = View.Details;
            
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
                       tableLayoutPanel.Controls.Remove(list_ipaddress[receving.Address]);
                   }));
                    System.Diagnostics.Debug.WriteLine("Removed:" + receving.Address);
                }
                else if (stringData.StartsWith("<#Message#>"))
                {
                    BeginInvoke((Action)(() =>
                    {
                        statusText.Text = stringData.Split(new String[] { "<#Message#>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }));
                    savetToFile(receving.Address, stringData.Split(new String[] { "<#Message#>" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
               
              
           
            }
        }
        private void AddItem(IPAddress ipaddress,String name)
        {
       
            
            if (!l_ipaddress.Contains(ipaddress))
            {
                d_client.Add(ipaddress, new Client(ipaddress, name));
                l_ipaddress.Add(ipaddress);
                BeginInvoke((Action)(() =>
                {
                    
                  /*  ListViewItem item = new ListViewItem(new String[]{ipaddress.ToString(),name});
                    list_ipaddress[ipaddress]=(item);
                    listView.Items.Add(item);*/
                    Control c= MakeCustomControl(ipaddress, name);
                    tableLayoutPanel.Controls.Add(c);
                    list_ipaddress[ipaddress] = c;
                
                    

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
        private UserControl MakeCustomControl(IPAddress ip, String Name)
        {
            UserControl uc=new UserControl();
            uc.Anchor = AnchorStyles.Top;
            Panel textPanel = new Panel();
            Label l_ip = new Label();
            Label l_name = new Label();
            textPanel.Controls.Add(l_name);
            textPanel.Controls.Add(l_ip);
            CheckBox checkBox = new CheckBox();
            checkBox.CheckedChanged += (a, e) =>
            {
                if (checkBox.Checked)
                    l_selectedaddress.Add(ip);
                else if (l_selectedaddress.Contains(ip))
                    l_selectedaddress.Remove(ip);
            
            };
            l_name.Text = Name;
            l_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l_name.Location = new System.Drawing.Point(5, 5);
            l_ip.Location = new System.Drawing.Point(5, 26);
            l_ip.Text = ip.ToString();
            
            l_name.BackColor = Color.Transparent;
            checkBox.Dock = DockStyle.Left;
            checkBox.AutoSize = true;

            textPanel.MouseHover += (a, b) => { uc.BackColor = Color.AliceBlue; };
            textPanel.MouseLeave += (a, b) => { uc.BackColor = Color.LightSkyBlue; };
            textPanel.MouseMove += (a, b) => { uc.BackColor = Color.AliceBlue; };
            textPanel.MouseEnter += (a, b) => { uc.BackColor = Color.AliceBlue; };
            textPanel.Click += (a, b) => {};
            textPanel.AutoSize = true;
            textPanel.Dock = DockStyle.Fill;

            uc.Controls.Add(textPanel);
            uc.Controls.Add(checkBox);
            uc.BackColor = Color.LightSkyBlue;
            uc.AutoSize = true;
            uc.Dock = DockStyle.Top;
            uc.AutoSizeMode = AutoSizeMode.GrowOnly;
            string lastMessage = d_client[ip].fetchMessages().Last().value;
            if (lastMessage != null)
                l_ip.Text +=":"+lastMessage ;
         
      
            return uc;
        }

     
        private void sendBroadcastMsg(string data)
        {
            #region Option2 ping logic
            /*
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(BROADCAST_SENDING.Address, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("---Address: {0}", reply.Address.ToString());
                Console.WriteLine("---RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("---Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("---Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("---Buffer size: {0}", reply.Buffer.Length);
            }*/
            #endregion
            sendingClient.Connect(BROADCAST_SENDING);
            
            sendingClient.Send(Encoding.ASCII.GetBytes(data), data.Length);
        }       
        private void sendButton_Click(object sender, EventArgs e)
        {

            foreach(IPAddress ip in l_selectedaddress)
            {
                sendingClient.Connect(new IPEndPoint(ip, PORT));
                String data = "<#Message#>" + sendBox.Text + "<#Message#>";
                sendingClient.Send(Encoding.ASCII.GetBytes(data), data.Length);

            }
        }
        private void savetToFile(IPAddress ip,String messageText) {
            String newWorkingDir=".\\"+ip.ToString().Replace('.','\\')+".xml";
            System.Diagnostics.Debug.WriteLine(newWorkingDir);
            XmlDocument xmlDoc = new XmlDocument();
           
            if(System.IO.File.Exists(newWorkingDir))
            {
                xmlDoc.Load(newWorkingDir);
                XmlNodeList list = xmlDoc.GetElementsByTagName("Messages")[0].ChildNodes;


                XmlNode countNode = xmlDoc.SelectSingleNode("//Messages//Count");
        //  if(countNode==null)
        //                  xmlDoc.GetElementsByTagName("Messages")[0].AppendChild();
             
                XmlElement message = xmlDoc.CreateElement("Message");
                message.SetAttribute("index", countNode.InnerText);
                message.SetAttribute("time", DateTime.Now + "");
                message.InnerText = messageText;
                xmlDoc.GetElementsByTagName("Messages")[0].AppendChild(message);
                countNode.InnerText = Int32.Parse(countNode.InnerText) + 1 + "";
                xmlDoc.Save(newWorkingDir);
                
            }
            else
            {
          
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            statusText.Text = openFileDialog1.FileName;
          /*  if (listView.SelectedItems.Count > 0)
            {
                tcpSendingClient = new TcpClient(listView.SelectedItems[0].Name, PORTTCP);
                tcpSendingClient.Connect(listView.SelectedItems[0].Name, PORTTCP);
              
            }*/
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void sendBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))

              e.Effect = DragDropEffects.All;

            
        }

        private void sendBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    sendBox.Text = filePath;
                }
            }
        }

      
    }

}
