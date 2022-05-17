using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IBatteryStatus
    {
        event EventHandler ChangedBatteryStatus;
        event EventHandler LowBatteryLevel;
        void CalculateBatteryStatus();
        int GetBatteryLevel();
    }
}
