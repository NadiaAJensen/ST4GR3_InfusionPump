using System;
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
        private DTO_Treatmentplan currentInfusionTreatmentplan;

        public InfusionControl(ITimer timer)
        {
            _timer = timer;
            //skal nok koble sig på et event
        }

        public void Prime()
        {
            //Control pump to fill pipe. start pumpe prime program
        }

        public void StartInfusionProgram()
        {
            InfusionProgramIsActive = true;
            _timer.Start(currentInfusionTreatmentplan.Hours,currentInfusionTreatmentplan.Minuttes);

            //control the pump
            //start the infusion plan
        }

        public void StopInfusionProgram()
        {
            InfusionProgramIsActive = false;
            _timer.Stop();
            // skal gemme og sende data retur
            //stop pumpen
            // stop pumpe mv.
        }
        public void PauseInfusionProgram()
        {
            InfusionProgramIsActive = false;
            _timer.Stop();
            //stop pumpen.
        }

        public void SaveInfusionPlan(object sender, EventArgs e)
        {
            currentInfusionTreatmentplan = new DTO_Treatmentplan(3, 40);
            //Skal modtage den rigtige DTO
        }
    }
}
