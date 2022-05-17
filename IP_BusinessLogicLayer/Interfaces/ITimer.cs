using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface ITimer
    {
        int TimeRemainingHour { get; }
        int TimeRemainingMinutes { get; }
        event EventHandler Expired;
        event EventHandler TimerTick;

        void Start(int hours, int minuttes);
        void Stop();
    }
}
