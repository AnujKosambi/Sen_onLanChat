using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using System.Threading;
namespace SEN_Project_v1
{
    public partial class AudioControl : UserControl
    {
        public List<WaveInCapabilities> sources = new List<WaveInCapabilities>();
        public static WaveInCapabilities _default;
        public WaveIn sourceStream;
        public List<Byte> listBytes = new List<Byte>();
       
        public WinSound.Recorder recorder;
        public AudioControl()
        {
            InitializeComponent();
             for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                sources.Add(WaveIn.GetCapabilities(i));
               audioDevices.Items.Add(sources.Last().ProductName);
            }

             audioDevices.SelectedIndex = 0;
            init();
        }


  
        public void init()
        {
            if (audioDevices.Items.Count >= 0)
            {
               
                    int deviceNumber = audioDevices.SelectedIndex;
                    sourceStream = new WaveIn();
                    sourceStream.DeviceNumber = deviceNumber;
                    sourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels);
                    sourceStream.RecordingStopped += sourceStream_RecordingStopped;
                    sourceStream.DataAvailable += sourceStream_DataAvailable;
        
            }   
        }

        void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            listBytes = listBytes.Concat(e.Buffer).ToList();
        }

        void sourceStream_RecordingStopped(object sender, EventArgs e)
        {

            listBytes.Clear();
        }
    }
}
