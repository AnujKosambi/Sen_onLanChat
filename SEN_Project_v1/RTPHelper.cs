using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSR.LST;
using MSR.LST.Net.Rtp;
using System.Drawing;
using System.Net;
namespace SEN_Project_v1
{
    public class RTPHelper
    {
        public RtpSender rtpSender;
        public RtpSession rtpSession;
        VideoChatting form;
        IPEndPoint remoteIP;

        public RTPHelper (VideoChatting vc,IPEndPoint ip)
        {
            AttachEventHandler();
            form = vc;
            remoteIP = ip;
            ConnectSession();
        }
        public void ConnectSession()
        {
            string name="Anuj";
            rtpSession = new RtpSession(remoteIP, new RtpParticipant(name, name), true, true);
            rtpSender = rtpSession.CreateRtpSenderFec(name, PayloadType.JPEG, null, 0, 1);

        }
        public void AttachEventHandler()
        {
            RtpEvents.RtpParticipantAdded += (a,b) => { };
            RtpEvents.RtpParticipantRemoved += (a, b) => { };
            RtpEvents.RtpStreamAdded += (a, b) => { b.RtpStream.FrameReceived += RtpStream_FrameReceived; };
            RtpEvents.RtpStreamRemoved += (a, b) => { b.RtpStream.FrameReceived -= RtpStream_FrameReceived; };

        }

        void RtpStream_FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(ea.Frame.Buffer);
            form.pictureBox_rec.Image = Image.FromStream(ms);
        }
    }
}
