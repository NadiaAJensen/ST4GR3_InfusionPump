using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RaspberryPiNetCore.WiringPi;
using ST4GR3_InfusionPumpApplication.Interfaces;
//using System.Device.Gpio;

namespace ST4GR3_InfusionPumpApplication
{
    public class Button : IButton
    {
        private int _gpioPin;
        private bool _value;
        public Button(int pinNumber)
        {
            _gpioPin = pinNumber;
           GPIO.pinMode(pinNumber, 0);
           _value = false;
        }
        public bool IsPressed()
        {
            int buttonDigitalRead = GPIO.digitalRead(_gpioPin);

            if (buttonDigitalRead == 1)
                _value = true;
            else if (buttonDigitalRead == 0)
                _value = false;
            Thread.Sleep(50);

            return _value;
        }
    }
}
