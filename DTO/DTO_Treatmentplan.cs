using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Treatmentplan
    {
        public int Hours { get; private set; }
        public int Minuttes { get; private set; }
        public List<DTO_FlowrateIntervals> FlowrateIntervals { get; set; }

        public DTO_Treatmentplan(int hours, int minuttes)
        {
            Hours = hours;
            Minuttes = minuttes;
            FlowrateIntervals = new List<DTO_FlowrateIntervals>();
        }
    }
}
