using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;
using IP_DataAccessLayer1.TCP;

//using ST4GR3_InfusionPumpApplication;

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
        private IInfusionControl _infusionControl;
        public bool PlanRecieved { get; private set; } //Hvis den har modtaget infusionsplan
        public MenuController(IAlarmControl alarmControl, IBatteryStatus batteryStatus, ITimer timer, IInfusionControl infusionControl)
        {
            _alarmControl = alarmControl;
            _batteryStatus = batteryStatus;
            _timer = timer;
            _infusionControl = infusionControl;
            _menuList = new MenuList();
            _newMenu = new string[4];
            PlanRecieved = false;
            _batteryStatus.ChangedBatteryStatus += new EventHandler(BatteryStatusChanged);
            _timer.Expired += new EventHandler(OnTimerExpired);
            _timer.TimerTick += new EventHandler(OnTimerTick);
            _infusionControl.ChangedFlowrate += new EventHandler(HandleFlowrateChanged);
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
                            _infusionControl.Prime();
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
                            _infusionControl.PauseInfusionProgram();
                            _newMenu = FindMenuArray(4);
                            //Behandlingen er sat på pause i infusion control
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
                            //Behandling afsluttes
                            _infusionControl.StopInfusionProgram();
                            PlanRecieved = false; // Nu er er ikke længere en plantilgængelig
                            _newMenu = FindMenuArray(0);
                            //returnere til hoved og data skal gemmes (før den går til hovedmenu). Dette gøres fra infusionControl
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

        private void BatteryStatusChanged(object sender, EventArgs e)
        {
            _menuList.BatteriNiveau = Convert.ToString(_batteryStatus.GetBatteryLevel());
            _menuList.ReloadMenues();
        }

        private void OnTimerExpired(object sender, EventArgs e)
        {
            if (_infusionControl.InfusionProgramIsActive)
            {
                _infusionControl.StopInfusionProgram();
                //Information skal sendes til ICA, hvilket gøres fra stop metoden
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_infusionControl.InfusionProgramIsActive)
            {
                _menuList.Timer = Convert.ToString(_timer.TimeRemainingHour);
                _menuList.Minutter = Convert.ToString(_timer.TimeRemainingMinutes);
            }
            _menuList.ReloadMenues();
        }
        private void HandleFlowrateChanged(object sender, EventArgs e)
        {
            _menuList.Flowrate = Convert.ToString(_infusionControl.Flowrate);
            _menuList.ReloadMenues();
        }




    }
}
