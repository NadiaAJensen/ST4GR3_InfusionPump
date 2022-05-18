using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_BusinessLogicLayer.Interfaces
{
    public interface IInfusionControl
    {
        bool InfusionProgramIsActive { get; }
        double Flowrate { get; }
        event EventHandler ChangedFlowrate;
        void Prime();
        void StartInfusionProgram();
        void StopInfusionProgram();
        void PauseInfusionProgram();

    }
}
