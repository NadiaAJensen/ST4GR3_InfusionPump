using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPumpApplication
{
    public class Display : IDisplay
    {
        private SerLCD _lcdDisplay;
        private TWIST _encoder;
        private MenuController _menuController;
        private string[] _currentMenu;
        

        public Display()
        {
            _lcdDisplay = new SerLCD();
            _encoder = new TWIST();
            _menuController = new MenuController();
            _currentMenu = new string[4];
        }

        public void Run()
        {
            DisplayMenu((_menuController.FindMenuArray(0)));
        }

        public void DisplayMenu(string[] menuText)
        {
            while (true)
            {
                byte c = 0;
                foreach (var line in menuText)
                {
                    _lcdDisplay.lcdGotoXY(0, c);
                    _lcdDisplay.lcdPrint(line);
                    c++;
                }

                while (true)
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
                    }
                }
            }


        }
    }
}
