using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;

namespace IP_BusinessLogicLayer
{
    public class BatteryStatus : IBatteryStatus
    {
        private ReadBatteryStatus _batteryStatus;
        private int _ADCValue;
        private int _batteryLevel;
        public event EventHandler ChangedBatteryStatus;
        public event EventHandler LowBatteryLevel;

        public BatteryStatus()
        {
            _batteryStatus = new ReadBatteryStatus();
        }

        public void CalculateBatteryStatus()
        {
            _ADCValue = _batteryStatus.GetBatteryADC_Value();

            double sample = ((Convert.ToDouble(_ADCValue) / 2048.0) * 6.144);

            if (sample >= 2.9)
            {
                _batteryLevel = 100;
            }
            else if (sample >= 2.765 && sample < 2.9)
            {
                _batteryLevel = 80;
            }
            else if (sample >= 2.701 && sample < 2.765)
            {
                _batteryLevel = 60;
            }
            else if (sample >= 2.657 && sample < 2.701)
            {
                _batteryLevel = 40;
            }
            else if (sample >= 2.593 && sample < 2.657)
            {
                _batteryLevel = 20;
            }
            else if (sample >= 2.506 && sample < 2.593)
            {
                _batteryLevel = 10;
            }
            else if (sample < 2.506)
            {
                _batteryLevel = 1;
            }

            ChangedBatteryStatus?.Invoke(this, System.EventArgs.Empty);
            if (_batteryLevel < 21)
            {
                LowBatteryLevel?.Invoke(this, System.EventArgs.Empty); //Alarmer ved lavt batteri
            }
        }

        public int GetBatteryLevel()
        {
            return _batteryLevel;
        }
    }
}
