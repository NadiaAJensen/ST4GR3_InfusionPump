﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace IP_DataAccessLayer1.TCP
{
    public class Listener : IListener
    {
        public event EventHandler<TreatmentPlanRecievedEventArgs> TreatmentplanRecieved;
        public Listener()
        {
            //Ved oprettelse simueleres en plan. Skulle lyttet efter en plan. 
            RecieveData();
        }

        public void RecieveData()
        {
            //Simulering af plan
            DTO_Treatmentplan T_Plan = new DTO_Treatmentplan(2, 30);
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(0,1));
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(30, 1.3));
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(60, 1.4));
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(90, 1.7));
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(120, 2));
            T_Plan.FlowrateIntervals.Add(new DTO_FlowrateIntervals(150, 2));

            TreatmentplanRecieved?.Invoke(this, new TreatmentPlanRecievedEventArgs(T_Plan));
            
        }
    }
}