using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_DataAccessLayer1.TCP
{
    public interface IListener
    {
        event EventHandler<TreatmentPlanRecievedEventArgs> TreatmentplanRecieved;
        void RecieveData();
        void Run();

    }
}
