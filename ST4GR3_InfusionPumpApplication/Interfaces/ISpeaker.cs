using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST4GR3_InfusionPumpApplication.Interfaces
{
    public interface ISpeaker
    {
        void HandleAlarm(object sender, EventArgs e);
        void AlarmSpeakerOn();
        void AlarmSpeakerOff();
        void AlarmSpeakerMute();
    }
}
