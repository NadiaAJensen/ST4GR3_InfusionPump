﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;

namespace IP_BusinessLogicLayer
{
    public class InfusionControl : IInfusionControl
    {
        public bool InfusionProgramIsActive { get; private set; }
        private ITimer _timer;
        private IPump _pump;
        private DTO_Treatmentplan _currentInfusionTreatmentplan;
        
        public InfusionControl(ITimer timer)
        {
            _timer = timer;
            _pump = new Pump();
            //skal nok koble sig på et event på DTO'en
        }

        public void Prime()
        {
            _pump.PrimeProgram();
            //Control pump to fill pipe. start pumpe prime program
        }

        public void StartInfusionProgram()
        {
            InfusionProgramIsActive = true;
            _timer.Start(_currentInfusionTreatmentplan.Hours, _currentInfusionTreatmentplan.Minuttes);
            int totalTime = _timer.TotalTimeRemainingInMinutes;

            while (_timer.TimeRemainingHour!=0 && _timer.TimeRemainingMinutes!=0)
            {
                foreach (var dtoInterval in _currentInfusionTreatmentplan.FlowrateIntervals)
                {
                    while (_timer.TotalTimeRemainingInMinutes!=0)
                    {
                        if (totalTime - _timer.TotalTimeRemainingInMinutes == dtoInterval.Time)
                        {
                            _pump.Start(dtoInterval.Flowrate);
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
            // skal gemme og sende data retur til ICA
        }
        public void PauseInfusionProgram()
        {
            InfusionProgramIsActive = false;
            _timer.Stop();
            _pump.Stop();
        }

        public void SaveInfusionPlan(object sender, EventArgs e)
        {
            _currentInfusionTreatmentplan = new DTO_Treatmentplan(3, 40);
            //Skal modtage den rigtige DTO
        }
    }
}