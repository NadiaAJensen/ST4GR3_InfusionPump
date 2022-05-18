using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IPump
    {
        void Start(double flowrate);
        void Stop();
        void PrimeProgram();
    }
}
