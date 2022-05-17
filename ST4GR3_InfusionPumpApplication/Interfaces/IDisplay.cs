using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST4GR3_InfusionPumpApplication.Interfaces
{
    interface IDisplay
    {
        void DisplayMenu(string[] MenuText);
        void DisplayAlarm(object sender, EventArgs e);
        void Run();
    }
}
