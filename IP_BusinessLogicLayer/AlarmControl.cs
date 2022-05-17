using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;


namespace IP_BusinessLogicLayer
{
    public class AlarmControl : IAlarmControl
    {
        private IBatteryStatus _batteryStatus;
        private ITimer _timer;
        public event EventHandler Alarm;
        public string[] LastAlarmMessage { get; private set; }
        public AlarmControl(IBatteryStatus batteryStatus, ITimer timer)
        {
            _batteryStatus = batteryStatus;
            _timer = timer;
            _timer.Expired += new EventHandler(OnTimerExpired);
            _batteryStatus.LowBatteryLevel += new EventHandler(AlertLowBatteryLevel);
        }

        public void Run()
        {
            
            while (true)
            {
                _batteryStatus.CalculateBatteryStatus();
                Thread.Sleep(30000); // så læser den hvert 30 sekund

            }
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            
        }
        public void AlertLowBatteryLevel(object sender, EventArgs e)
        {
            int value = _batteryStatus.GetBatteryLevel();
            LastAlarmMessage = new[] {$"Batteristatus: {value}%"};
            Alarm?.Invoke(this, System.EventArgs.Empty);
        }


    }
}
