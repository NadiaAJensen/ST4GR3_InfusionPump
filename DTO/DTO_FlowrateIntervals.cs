using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DTO
{
    public class DTO_FlowrateIntervals
    {
        public int Time { get; set; }
        public double Flowrate { get; set; }

        public DTO_FlowrateIntervals(double time, double flowrate)
        {
            Time = Convert.ToInt32(time);
            Flowrate = flowrate;
        }
    }
}
