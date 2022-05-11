using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using System.Threading;
using IP_BusinessLogicLayer;
using Microsoft.VisualBasic.CompilerServices;




namespace ST4GR3_InfusionPumpApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Display _display = new Display();
            Thread displayThread = new Thread(_display.Run);
            displayThread.IsBackground = true;

            while (true)
            {
                
            }
            
        }
    }
}
