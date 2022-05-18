using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace IP_DataAccessLayer1
{
    public class TreatmentPlanRecievedEventArgs : EventArgs
    {
        public DTO_Treatmentplan Treatmentplan { get; set; }

        public TreatmentPlanRecievedEventArgs(DTO_Treatmentplan plan)
        {
            Treatmentplan = plan;
        }
    }
}
