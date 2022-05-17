using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using System.Threading;
using IP_BusinessLogicLayer;
using IP_BusinessLogicLayer.Interfaces;
using Microsoft.VisualBasic.CompilerServices;
using ST4GR3_InfusionPumpApplication.Interfaces;


namespace ST4GR3_InfusionPumpApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IButton _startButton = new Button(23);
            IButton _pauseButton = new Button(24);
            IButton _stopButton = new Button(25); // Pin skal ændres til hvordan det sættes op.
            IAlarmControl _alarmController = new AlarmControl();
            IMenuController _menuController = new MenuController(_alarmController);
            
            


            IDisplay _display = new Display(_menuController,  _startButton, _pauseButton, _stopButton);
            Thread displayThread = new Thread(_display.Run);
            displayThread.IsBackground = true;

            while (true)
            {
                
            }
            
        }
    }
}
