using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IAlarmControl
    {
        event EventHandler ChangedBatteryStatus;
        void Run();
        void CalculateBatteryStatus();
        int GetBatteryLevel();
    }
}
