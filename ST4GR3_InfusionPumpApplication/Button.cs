using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RaspberryPiNetCore.WiringPi;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPumpApplication
{
    public class Button : IButton
    {
        public GpioController GpioControl
        {
            get; set;

        }

        private int _gpioPin;
        private bool _value;
        public Button(int pinNumber)
        {
            _gpioPin = pinNumber;
            _value = false;
        }
        public bool IsPressed()
        {
            if (GpioControl.Read(_gpioPin)==PinValue.High)
                _value = true;
            else if (GpioControl.Read(_gpioPin) == PinValue.Low)
                _value = false;
            Thread.Sleep(50);

            return _value;
        }
    }
}
