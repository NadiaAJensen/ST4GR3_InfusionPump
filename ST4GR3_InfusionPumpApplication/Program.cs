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
            IBatteryStatus _batteryStatus = new BatteryStatus();
            ITimer _timer = new IP_BusinessLogicLayer.Timer();
            IAlarmControl _alarmController = new AlarmControl(_batteryStatus);
            IMenuController _menuController = new MenuController(_alarmController, _batteryStatus, _timer);

            IDisplay _display = new Display(_menuController,  _startButton, _pauseButton, _stopButton);
            Thread displayThread = new Thread(_display.Run);
            Thread alarmThread = new Thread(_alarmController.Run);
            displayThread.IsBackground = true;
            alarmThread.IsBackground = true;

            while (true)
            {
                
            }
            
        }
    }
}
