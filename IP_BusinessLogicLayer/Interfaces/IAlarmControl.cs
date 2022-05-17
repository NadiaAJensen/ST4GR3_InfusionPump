using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IAlarmControl
    {
        string[] LastAlarmMessage { get; }
        string AlarmCode { get; }
        event EventHandler Alarm;
        void Run();
        void BobbleDetected(object sender, EventArgs e);
        void AlertLowBatteryLevel(object sender, EventArgs e);
        void OnTimerExpired(object sender, EventArgs e);

    }
}
