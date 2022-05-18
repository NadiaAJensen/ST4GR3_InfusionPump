using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IInfusionControl
    {
        public bool InfusionProgramIsActive { get; }
        void Prime();
        void StartInfusionProgram();
        void StopInfusionProgram();
        void PauseInfusionProgram();

    }
}
