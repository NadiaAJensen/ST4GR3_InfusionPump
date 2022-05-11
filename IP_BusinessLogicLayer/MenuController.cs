using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_DataAccessLayer;
using ST4GR3_InfusionPumpApplication;

namespace IP_BusinessLogicLayer
{
    public class MenuController
    {
        private MenuList _menuList;
        private Display _display;
        private byte _lastMenuIndex;
        private byte _returnMenuCode;
        public MenuController()
        {
            _menuList = new MenuList();
            _display = new Display();
        }

        public void HandleMenuFeedback()
        {
            while (true)
            {
                _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[0]);
                _lastMenuIndex = 0;

                switch (_lastMenuIndex)
                {
                    case 0:
                        if (_returnMenuCode == 1)
                        {
                            //start prime program
                            break;
                        }
                        else
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[0]);
                            break;
                        }
                    case 3:
                        if (_returnMenuCode == 2)
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[4]);
                            //Behandlingen er sat på pause
                            break;
                        }
                        if (_returnMenuCode == 3)
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[5]);
                            //Sendes tilbage til behandlingsmenu
                            break;
                        }
                        else
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[3]);
                            //Sendes retur, hvis der ikke trykkes ja eller nej
                            break;
                        }
                    case 6:
                        if (_returnMenuCode == 2)
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[0]);
                            //Behandlingen er afsluttet
                            //returnere til hoved og data skal gemmes (før den går til hovedmenu)
                            break;
                        }
                        if (_returnMenuCode == 3)
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[5]);
                            //Sendes tilbage til behandlingsmenu
                            break;
                        }
                        else
                        {
                            _returnMenuCode = _display.DisplayMenu(_menuList.MenuListDansk[6]);
                            //Sendes retur, hvis der ikke trykkes ja eller nej
                            break;
                        }
                }
            }
        }

        public void Run()
        {
            HandleMenuFeedback();
        }
    }
}
