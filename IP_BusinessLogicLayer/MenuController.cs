using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;

namespace IP_BusinessLogicLayer
{
    public class MenuController : IMenuController
    {
        private MenuList _menuList;
        private int _lastMenuIndex;
        private byte _returnMenuCode;
        private string[] _newMenu;
        private IAlarmControl _alarmControl;
        private IBatteryStatus _batteryStatus;
        private ITimer _timer;
        private bool _treatmentActive;
        public MenuController(IAlarmControl alarmControl, IBatteryStatus batteryStatus, ITimer timer)
        {
            _alarmControl = alarmControl;
            _batteryStatus = batteryStatus;
            _timer = timer;
            _menuList = new MenuList();
            _newMenu = new string[4];
            _treatmentActive = false;
            _batteryStatus.ChangedBatteryStatus += new EventHandler(BatteryStatusChanged);
            _timer.Expired += new EventHandler(OnTimerExpired);
            _timer.TimerTick += new EventHandler(OnTimerTick);

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
                            _treatmentActive = true;
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
                            _timer.Stop();
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
                            _treatmentActive = false;
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

        public void BatteryStatusChanged(object sender, EventArgs e)
        {
            _menuList.BatteriNiveau = Convert.ToString(_batteryStatus.GetBatteryLevel());
            _menuList.ReloadMenues();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (_treatmentActive)
            {
                _treatmentActive = false; //behandling stoppet
                //Hvad skal der ske når programmet er slut
                //Information skal sendes til ICA
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (_treatmentActive)
            {
                _menuList.Timer = Convert.ToString(_timer.TimeRemainingHour);
                _menuList.Minutter = Convert.ToString(_timer.TimeRemainingMinutes);
            }
            _menuList.ReloadMenues();
        }




    }
}
