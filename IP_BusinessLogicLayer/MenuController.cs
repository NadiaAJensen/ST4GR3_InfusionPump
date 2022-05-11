using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer;
//using ST4GR3_InfusionPumpApplication;

namespace IP_BusinessLogicLayer
{
    public class MenuController : IMenuController
    {
        private MenuList _menuList;
        //private Display _display;
        private int _lastMenuIndex;
        private byte _returnMenuCode;
        private string[] _newMenu;
        public MenuController()
        {
            _menuList = new MenuList();
            _newMenu = new string[4];

        }

        public string[] FindMenuArray(int menuIndex)
        {
            _lastMenuIndex = menuIndex;

            return _menuList.MenuListDansk[menuIndex];
        }

        public string[] HandleMenuFeedback(byte choice)
        {
            _returnMenuCode = choice;
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
                            _newMenu= FindMenuArray(0);
                            break;
                        }
                    case 3:
                        if (_returnMenuCode == 2)
                        {
                            _newMenu = FindMenuArray(4);
                            //Behandlingen er sat på pause
                            break;
                        }
                        if (_returnMenuCode == 3)
                        {
                            _newMenu = FindMenuArray(5);
                            //Sendes tilbage til behandlingsmenu
                            break;
                        }
                        else
                        {
                            _newMenu = FindMenuArray(3);
                            //Sendes retur, hvis der ikke trykkes ja eller nej
                            break;
                        }
                    case 6:
                        if (_returnMenuCode == 2)
                        {
                            _newMenu = FindMenuArray(0);
                            //Behandlingen er afsluttet
                            //returnere til hoved og data skal gemmes (før den går til hovedmenu)
                            break;
                        }
                        if (_returnMenuCode == 3)
                        {
                            _newMenu = FindMenuArray(5);
                            //Sendes tilbage til behandlingsmenu
                            break;
                        }
                        else
                        {
                            _newMenu = FindMenuArray(6);
                            //Sendes retur, hvis der ikke trykkes ja eller nej
                            break;
                        }
                    default:
                        //no change
                        break;//no change
                }

            return _newMenu;

        }
        
    }
}
