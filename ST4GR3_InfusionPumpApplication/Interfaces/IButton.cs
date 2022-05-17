using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.WiringPi;

namespace ST4GR3_InfusionPumpApplication.Interfaces
{
    public interface IButton
    {
        bool IsPressed();
    }
}
