using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.WiringPi;

namespace ST4GR3_InfusionPumpApplication.Interfaces
{
    public interface IButton
    {
        GpioController GpioControl { get; set; }
        bool IsPressed();
    }
}
