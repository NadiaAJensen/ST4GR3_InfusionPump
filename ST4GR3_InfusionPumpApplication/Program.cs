using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using Microsoft.VisualBasic.CompilerServices;


namespace ST4GR3_InfusionPumpApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Display _display = new Display();

            string[] text1 = new[] {"Hej", " Det", "er", "en Test"};
            _display.DisplayMenu(text1);
        }
    }
}
