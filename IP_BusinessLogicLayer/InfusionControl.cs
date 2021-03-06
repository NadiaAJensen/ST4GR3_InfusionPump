using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;
using IP_DataAccessLayer1.TCP;

namespace IP_BusinessLogicLayer
{
    public class InfusionControl : IInfusionControl
    {
        public bool InfusionProgramIsActive { get; private set; }
        public double Flowrate { get; private set; }
        private ITimer _timer;
        private IPump _pump;
        private IListener _listener;
        private ISender _sender;
        private DTO_Treatmentplan _currentInfusionTreatmentplan;
        public event EventHandler ChangedFlowrate;

        public InfusionControl(ITimer timer, IListener listener, ISender sender)
        {
            _timer = timer;
            _sender = sender;
            _pump = new Pump();
            _listener = listener;
            _listener.TreatmentplanRecieved += SaveInfusionPlan;
        }

        public void Prime()
        {
            _pump.PrimeProgram();
            //Control pump to fill pipe. start pumpe prime program
        }

        public void StartInfusionProgram()
        {
            InfusionProgramIsActive = true;
            _sender.SendData("Besked til ICA: Nu er behandlingen startet");
            _timer.Start(_currentInfusionTreatmentplan.Hours, _currentInfusionTreatmentplan.Minuttes);
            int totalTime = _timer.TotalTimeRemainingInMinutes;

            while (_timer.TimeRemainingHour!=0 && _timer.TimeRemainingMinutes!=0)
            {
                foreach (var dtoInterval in _currentInfusionTreatmentplan.FlowrateIntervals)
                {
                    while (_timer.TotalTimeRemainingInMinutes!=0 || totalTime - _timer.TotalTimeRemainingInMinutes == dtoInterval.Time)
                    {
                        if (totalTime - _timer.TotalTimeRemainingInMinutes == dtoInterval.Time)
                        {
                            Flowrate = dtoInterval.Flowrate;
                            _pump.Start(Flowrate);
                            ChangedFlowrate?.Invoke(this, EventArgs.Empty);
                            // Sikre at flowrate også opdateres.
                            //Går kun derind og sætter flowrate, når når det passer med intervallet
                        }
                    }
                }
            }
            StopInfusionProgram();
            //Run through all the intervals
            //And then stops det program
        }

        public void StopInfusionProgram()
        {
            InfusionProgramIsActive = false;
            _timer.Stop();
            _pump.Stop();
            _sender.SendData("Besked til ICA: Nu er behandlingen stoppet. Her er data");
            // skal gemme og sende data retur til ICA
        }
        public void PauseInfusionProgram()
        {
            InfusionProgramIsActive = false;
            _timer.Stop();
            _pump.Stop();
            _sender.SendData($"Besked til ICA: Nu er behandlingen pauset. Manglende tid er.{_timer.TotalTimeRemainingInMinutes}");
        }

        private void SaveInfusionPlan(object sender, TreatmentPlanRecievedEventArgs e)
        {
            if (!InfusionProgramIsActive)
            {
                _currentInfusionTreatmentplan = e.Treatmentplan;
                _sender.SendData("Besked til ICA: Behandlingsplan modtaget");
            }
            //Gemmer kun ny plan, hvis den ikke er i gang
        }
    }
}
