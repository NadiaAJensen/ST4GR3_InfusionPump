using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.ADC;

namespace IP_DataAccessLayer1
{
    public class BatteryStatus
    {
        private ADC1015 _adc1015;
        private int _batteryADC;

        public BatteryStatus()
        {
            _adc1015 = new ADC1015(72, 512);
        }

        public int GetBatteryADC_Value()
        {
            _batteryADC = _adc1015.ReadADC_SingleEnded(1);
            return _batteryADC;
        }
    }
}
