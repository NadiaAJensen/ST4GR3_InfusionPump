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
        static SerLCD lcdDisplay;

        public Display()
        {
            lcdDisplay = new SerLCD();
        }

        public void DisplayMenu(string[] menuText)
        {
            byte c = 0;
            foreach (var line in menuText)
            {
                lcdDisplay.lcdGotoXY(0, c); 
                lcdDisplay.lcdPrint(line);
                    c++;
            }
        }
    }
}
