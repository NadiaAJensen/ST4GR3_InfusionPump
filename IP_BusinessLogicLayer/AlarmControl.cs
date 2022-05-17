using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;


namespace IP_BusinessLogicLayer
{
    public class AlarmControl : IAlarmControl
    {
        private IBatteryStatus _batteryStatus;
        public AlarmControl(IBatteryStatus batteryStatus)
        {
            _batteryStatus = batteryStatus;
        }

        public void Run()
        {
            while (true)
            {
                _batteryStatus.CalculateBatteryStatus();

            }
        }


    }
}
