using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_DataAccessLayer1
{
    public class MenuList
    {
        public List<string[]> MenuListDansk;
        public string BatteriNiveau { get; set; }
        public string Flowrate { get; set; }
        public string Timer { get; set; }
        public string Minutter { get; set; }

        public MenuList()
        {
            MenuListDansk = new List<string[]>();
            ReloadMenues();
        }

        public void ReloadMenues()
        {
            MenuListDansk.Clear();

            MenuListDansk.Add(new string[] { "Hovedmenu:", "Prime", "", $"Batteristatus:    {BatteriNiveau}%" });
            MenuListDansk.Add(new string[] { "Prime startes..." });
            MenuListDansk.Add(new string[] { "Primes foretaget" });
            MenuListDansk.Add(new string[] { "Oensker du at", "pause behandlingen?", "Ja", "Nej" });
            MenuListDansk.Add(new string[] { "Behandlingen sat paa", "pause", "", "Genoptag her" });
            MenuListDansk.Add(new string[] { $"Flow rate:    {Flowrate}ml/t", "Tid i alt:  t", $"Tid tilbage:  {Timer}t {Minutter}min", $"Batteristatus:   {BatteriNiveau}%"});
            MenuListDansk.Add(new string[] { "Oensker du at", "afslutte?", "Ja", "Nej" });
        }



        
    }
}
