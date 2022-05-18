using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IP_BusinessLogicLayer;
using IP_BusinessLogicLayer.Interfaces;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPumpApplication
{
    public class Display : IDisplay
    {
        private SerLCD _lcdDisplay;
        private TWIST _encoder;
        private IMenuController _menuController;
        private IButton _startButton;
        private IButton _pauseButton;
        private IButton _stopButton;
        private IAlarmControl _alarmControl;
        private bool _breakloop;
        private string[] _currentMenu;
        
        

        public Display(IMenuController menuController, IButton startButton, IButton pauseButton, IButton stopButton, IAlarmControl alarmControl)
        {
            _lcdDisplay = new SerLCD();
            _encoder = new TWIST();
            _menuController = menuController;
            _startButton = startButton;
            _pauseButton = pauseButton;
            _stopButton = stopButton;
            _alarmControl = alarmControl;
            _currentMenu = new string[4];
            _breakloop = true;

            _alarmControl.Alarm += new EventHandler(DisplayAlarm);

        }

        public void Run()
        {
            DisplayMenu((_menuController.FindMenuArray(0)));
        }

        public void DisplayMenu(string[] menuText)
        {
            _lcdDisplay.lcdClear();
            while (true)
            {
                byte c = 0;
                foreach (var line in menuText)
                {
                    _lcdDisplay.lcdGotoXY(0, c);
                    _lcdDisplay.lcdPrint(line);
                    c++;
                }

                _breakloop = true;

                while (_breakloop)
                {
                    int a = _encoder.getDiff(true);
                    if (a < 0)
                        a = -a;
                    for (int i = a; i >= 0; i = i - 4)
                    {
                        if (i < 2)
                        {
                            c = Convert.ToByte(i + 1);
                            _lcdDisplay.lcdGotoXY(0, c);
                            _lcdDisplay.lcdBlink();
                        }
                    }

                    if (_encoder.isPressed())
                    {
                        _currentMenu = _menuController.HandleMenuFeedback(c);
                        _breakloop = false;
                    }

                    if (_startButton.IsPressed())
                    {
                        // skal tjekkes om den er klar til program først
                        if (_menuController.TreatmentActive)
                        {
                            _currentMenu = _menuController.HandleMenuFeedback(5);
                            _breakloop = false;
                        }
                    }

                    if (_pauseButton.IsPressed())
                    {
                        // skal tjekkes om den er i gang allerede
                        if (_menuController.TreatmentActive)
                        {
                            _currentMenu = _menuController.HandleMenuFeedback(3);
                            _breakloop = false;
                            // Og så skal der pauses
                        }
                    }

                    if (_stopButton.IsPressed())
                    {
                        // skal tjekkes om behandlin er i gang
                        if (_menuController.TreatmentActive)
                        {
                            _currentMenu = _menuController.HandleMenuFeedback(6);
                            _breakloop = false;
                            //og så skal behandlingen stoppes.
                        }
                    }
                }
            }
        }

        public void DisplayAlarm(object sender, EventArgs e)
        {
            _lcdDisplay.lcdClear();
            switch (_alarmControl.AlarmCode) // Tjekker først alarm koden
            {
                case "Batteri":
                    _lcdDisplay.lcdSetBackLight(255, 0, 0);
                    break;
                case "Tid":
                    _lcdDisplay.lcdSetBackLight(255,255,0);
                    break;
            }

            byte c = 0;
            foreach (var line in _alarmControl.LastAlarmMessage)
            {
                _lcdDisplay.lcdGotoXY(0, c);
                _lcdDisplay.lcdPrint(line);
                c++;
            }

            while (true)
            {
                if (_encoder.isPressed())
                    break;
            }
            

        }
    }
}
