using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPumpApplication
{
    public class Speaker : ISpeaker
    {
        private IAlarmControl _alarmControl;
        private static SerialPort _serialPort;
        private List<double> sampleList = new List<double>();
        private double sample = new double();

        public Speaker(IAlarmControl alarmControl)
        {
            _alarmControl = alarmControl;
            _serialPort = new SerialPort("/dev/ttyS0", 9600, Parity.None, 8); ;
            _alarmControl.Alarm += new EventHandler(HandleAlarm);
            //Er det denne port vi bruger?
        }

        public void HandleAlarm(object sender, EventArgs e)
        {
            switch (_alarmControl.AlarmCode) // Tjekker først alarm koden, hvis den nu skulle agere forskelligt
            {
                case "Batteri":
                    AlarmSpeakerOn();
                    break;
                case "Tid":
                    AlarmSpeakerOn();
                    break;
                case "Bobbel":
                    AlarmSpeakerOn();
                    break;
            }
            Thread.Sleep(5000);
            AlarmSpeakerOff();
        }

        public void AlarmSpeakerOn()
        {
            _serialPort.Open();
            byte[] playTrack0101 = new byte[] { 0x7E, 0x0F, 0x00, 0x01, 0x01, 0xFF, 0xEF, 0xEF };
            _serialPort.Write(playTrack0101, 0, 8);
            
        }

        public void AlarmSpeakerOff()
        {
            byte[] stopTrack = new byte[] { 0x7E, 0x16, 0x00, 0x00, 0x00, 0xFF, 0xEA, 0xEF };
            _serialPort.Write(stopTrack, 0, 8);
            _serialPort.Close();
            
        }

        public void AlarmSpeakerMute()
        {
            byte[] pauseTrack = new byte[] { 0x7E, 0x0E, 0x00, 0x00, 0x00, 0xFF, 0xF2, 0xEF };
            _serialPort.Write(pauseTrack, 0, 8);
            _serialPort.Close();
        }
    }
}
