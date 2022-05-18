using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;

namespace IP_BusinessLogicLayer
{
    public class Pump : IPump
    {
        public Pump()
        {
            //Open connection to pump
        }

        public void Start(double flowrate)
        {
            //Pump at specific rate
        }

        public void Stop()
        {
            //Stop pump
        }

        public void PrimeProgram()
        {
            //Run the prime program
        }
    }
}
