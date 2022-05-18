using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IMenuController
    {
        bool PlanRecieved { get; set; }
        string[] FindMenuArray(int menuIndex);

        string[] HandleMenuFeedback(byte choice);
    }
}
