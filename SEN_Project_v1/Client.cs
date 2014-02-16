using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;
namespace SEN_Project_v1
{
    public class Client
    {
        private IPAddress ipaddress;
        private String username;
        public XmlDocument xmlDoc;
        private string path;
        public Message lastMessage;
        
        public Client(IPAddress ipaddress,String name)
        {
            this.ipaddress = ipaddress;
            this.username = name;
            this.ipaddress = ipaddress;
            path = ".\\" + ipaddress.ToString().Replace('.', '\\') + ".xml";
            xmlDoc = new XmlDocument();
            
            if (!System.IO.File.Exists(path))
            {
                XmlWriter xmlWriter = XmlWriter.Create(path);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Messages");
                xmlWriter.WriteStartElement("Count");
                xmlWriter.WriteCData("0");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Unread");
                xmlWriter.WriteCData("0");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
               
            }

                xmlDoc.Load(path);
        }
        public List<Message> fetchMessages(){
            List<Message> messageList = new List<Message>();
           XmlNodeList nodelist = xmlDoc.SelectNodes("//Messages//Message");
            foreach(XmlNode xn in nodelist)
            {
                Message m= new Message();
                m.index=Int32.Parse(xn.Attributes.GetNamedItem("index").Value);
                m.time=DateTime.Parse(xn.Attributes.GetNamedItem("time").Value);
                m.self = Boolean.Parse(xn.Attributes.GetNamedItem("self").Value);
                m.value=xn.InnerText;

                messageList.Add(m);
            }
            return messageList;
        }
        public XmlNode fetchRootOfMessages()
        {
            
            return xmlDoc.GetElementsByTagName("Messages")[0];
        }
        public void addMessage(DateTime time,String value)
        {
            XmlElement message = xmlDoc.CreateElement("Message");
            message.SetAttribute("index", CountMessage.ToString());
            message.SetAttribute("time", DateTime.Now + "");
            message.SetAttribute("self", "false");
            message.InnerText = value;
            fetchRootOfMessages().AppendChild(message);
            lastMessage.time = time;
            lastMessage.index = CountMessage;
            lastMessage.value = value;
            CountMessage++;
            UnreadMessages++;
  
         

        }
        public void addMessage2(DateTime time, String value)
        {
            XmlElement message = xmlDoc.CreateElement("Message");
            message.SetAttribute("index", CountMessage.ToString());
            message.SetAttribute("time", DateTime.Now + "");
            message.SetAttribute("self", "true");

            message.InnerText = value;
            fetchRootOfMessages().AppendChild(message);
            xmlDoc.Save(path);
        }
        public struct Message
        {
           public int index;
           public DateTime time;
           public String value;
           public Boolean self;
        }
        public int CountMessage
        {
            get {

                return Int32.Parse(xmlDoc.SelectSingleNode("//Messages//Count").InnerText);
            }
            set
            {
                xmlDoc.SelectSingleNode("//Messages//Count").InnerText = value+"";
                try
                {
                    xmlDoc.Save(path);
                }
                catch (Exception e) { }
            }
        }
        public int UnreadMessages
        {
            get
            {

                return Int32.Parse(xmlDoc.SelectSingleNode("//Messages//Unread").InnerText);
            }
            set
            {
                xmlDoc.SelectSingleNode("//Messages//Unread").InnerText = value + "";
                try
                {
                    xmlDoc.Save(path);
                }
                catch (Exception e) { }
            }
        }
    }
}
