using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPumpApplication
{
    public class Display : IDisplay
    {
        private SerLCD _lcdDisplay;
        private TWIST _encoder;
        

        public Display()
        {
            _lcdDisplay = new SerLCD();
            _encoder = new TWIST();
        }

        public byte DisplayMenu(string[] menuText)
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
                    return c;
                }
            }


        }
    }
}
