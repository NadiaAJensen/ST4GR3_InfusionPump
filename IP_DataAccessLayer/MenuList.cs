using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_DataAccessLayer
{
    public class MenuList
    {
        public List<string[]> MenuListDansk;

        public MenuList()
        {
            MenuListDansk = new List<string[]>();

            MenuListDansk.Add(new string[]{"Hovedmenu:", "Prime", "", "Batteristatus:    %"});
            MenuListDansk.Add(new string[] { "Prime startes..." });
            MenuListDansk.Add(new string[] { "Primes foretaget" });
            MenuListDansk.Add(new string[] { "Oensker du at", "pause behandlingen?", "Ja", "Nej" });
            MenuListDansk.Add(new string[] { "Behandlingen sat paa", "pause", "", "Genoptag her" });
            MenuListDansk.Add(new string[] { "Flow rate:    ml/t", "Tid i alt:  t", "Tid tilbage:  t  min", "Batteristatus:    %"});
            MenuListDansk.Add(new string[] { "Oensker du at", "afslutte?", "Ja", "Nej" });
        }
    }
}
