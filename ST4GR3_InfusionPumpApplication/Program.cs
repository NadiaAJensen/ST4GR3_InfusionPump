using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using System.Threading;
using IP_BusinessLogicLayer;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1.TCP;
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
            IListener _listener = new Listener();
            ISender _sender = new Sender();
            ITimer _timer = new IP_BusinessLogicLayer.Timer();

            IAlarmControl _alarmController = new AlarmControl(_batteryStatus, _timer, _sender);
            IInfusionControl _infusionControl = new InfusionControl(_timer, _listener, _sender);
            IMenuController _menuController = new MenuController(_alarmController, _batteryStatus, _timer, _infusionControl);

            IDisplay _display = new Display(_menuController,  _startButton, _pauseButton, _stopButton, _alarmController, _infusionControl);

            Thread displayThread = new Thread(_display.Run);
            Thread alarmThread = new Thread(_alarmController.Run);
            Thread listenThread = new Thread(_listener.Run);

            displayThread.IsBackground = true;
            alarmThread.IsBackground = true;
            listenThread.IsBackground = true;

            displayThread.Start();
            alarmThread.Start();
            listenThread.Start();


            while (true)
            {
                //Denne tråd kører bare, så skal afbrydes for stop.
            }
            
        }
    }
}
