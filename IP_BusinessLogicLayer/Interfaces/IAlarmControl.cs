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
        event EventHandler Alarm;
        void Run();
    }
}
