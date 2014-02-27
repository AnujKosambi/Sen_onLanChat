using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSR.LST;
using MSR.LST.Net.Rtp;
using System.Drawing;
using NAudio.Wave;
using System.Net;
namespace SEN_Project_v1
{
    public class RTPHelper
    {
        public RtpSender rtpSender;
        public RtpSession rtpSession;
        VideoChatting form;
        IPEndPoint remoteIP;
        IPAddress localIP;
        public RTPHelper (VideoChatting vc,IPEndPoint ip,PayloadType type)
        {
            remoteIP = ip;
        
            AttachEventHandler();
            form = vc;
            ConnectSession(type);
            
           
       
        }
        public void ConnectSession(PayloadType type)
        {
            string name=remoteIP.Address.ToString();
            string cname = name;
            rtpSession = new RtpSession(remoteIP, new RtpParticipant(cname, name), true, true);
           
            rtpSender = rtpSession.CreateRtpSenderFec(name, type, null, 0, 1);
            localIP=rtpSession.Participant(cname).IPAddress;   
        }
        public void AttachEventHandler()
        {
        
            RtpEvents.RtpStreamAdded += (a, b) => {
                b.RtpStream.FrameReceived += RtpStream_FrameReceived;
                b.RtpStream.DataStarted += RtpStream_DataStarted;
                b.RtpStream.DataStopped += RtpStream_DataStopped;
            };
            RtpEvents.RtpStreamRemoved += (a, b) => {
                b.RtpStream.FrameReceived -= RtpStream_FrameReceived;
               b.RtpStream.DataStarted -= RtpStream_DataStarted;
               b.RtpStream.DataStopped -= RtpStream_DataStopped;
            };
            
        }

        void RtpStream_DataStopped(object sender, EventArgs ea)
        {
            System.Diagnostics.Debug.WriteLine("Data Stops");
        }

        private void RtpStream_DataStarted(object sender, EventArgs ea)
        {
            System.Diagnostics.Debug.WriteLine("Data Started");
        }

        public DirectSoundOut waveOut;
        public BufferedWaveProvider waveProvider;
        void RtpStream_FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {



            if (ea.RtpStream.Properties.Name == localIP.ToString())
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(ea.Frame.Buffer);
                int sizeBytes=0;
                if (ea.RtpStream.PayloadType == PayloadType.JPEG || ea.RtpStream.PayloadType==PayloadType.Chat)
                {
                   
                    Byte[] buffer = new Byte[4];
                    ms.Read(buffer, 0, 4);
                     sizeBytes = BitConverter.ToInt32(buffer, 0);
                    System.Diagnostics.Debug.WriteLine("Data" + sizeBytes);
                    Byte[] image = new Byte[sizeBytes];
                    ms.Read(image, 0, image.Length);
                    form.pictureBox_rec.Image = Image.FromStream(new System.IO.MemoryStream(image));
                }
                 if (ea.RtpStream.PayloadType == PayloadType.dynamicAudio || ea.RtpStream.PayloadType==PayloadType.Chat)
                {
                    Byte[] audio = new Byte[ ms.Length-ms.Position];
                    ms.Read(audio, 0, audio.Length);
                    if (waveOut == null)
                    {
                        waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 2));
                        waveOut = new DirectSoundOut();
                        
                        
                        waveProvider.DiscardOnBufferOverflow = true;
                        waveProvider.AddSamples(audio, 0, audio.Length);
                        waveOut.Init(waveProvider);
                        waveOut.Play();
                    }
                    else
                    {
                        waveProvider.DiscardOnBufferOverflow = true;
                    
                        waveProvider.AddSamples(audio, 0, audio.Length);
                        if(waveOut.PlaybackState!=PlaybackState.Playing)
                         waveOut.Play();
                    }

                }
            }
        }



    }
}
