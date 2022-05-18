using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_DataAccessLayer1.TCP
{
    public interface ISender
    {
        void SendData(string message);
        
    }
}
