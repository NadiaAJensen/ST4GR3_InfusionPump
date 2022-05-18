using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IP_BusinessLogicLayer.Interfaces;

namespace IP_BusinessLogicLayer
{
    public class Timer : ITimer
    {
        public int TimeRemainingHour { get; private set; }
        public int TimeRemainingMinutes { get; private set; }
        public int TotalTimeRemainingInMinutes { get; private set; }
        public event EventHandler Expired;
        public event EventHandler TimerTick;

        private System.Timers.Timer timer;

        public Timer()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += OnTimerEvent; //Hvad den gør efter hver cyklus
            timer.Interval = 60000; // 1 minute intervals
            timer.AutoReset = true;  // Repeatable timer
        }

        public void Start(int hours, int minuttes)
        {
            if (hours >= 0 && minuttes >= 0 && minuttes < 60)
            {
                TimeRemainingHour = hours;
                TimeRemainingMinutes = minuttes;
                TotalTimeRemainingInMinutes = hours * 60 + minuttes;
                timer.Enabled = true;
            }

        }

        public void Stop()
        {
            timer.Enabled = false;
        }

        public void Resume()
        {
            timer.Enabled = true;
        }

        private void Expire()
        {
            timer.Enabled = false;
            Expired?.Invoke(this, System.EventArgs.Empty);
        }

        private void OnTimerEvent(object sender, System.Timers.ElapsedEventArgs args)
        {
            //Tjekker om der skal tælles ned på timer eller minutter
            if (TimeRemainingMinutes == 0)
            {
                TimeRemainingHour -= 1;
                TimeRemainingMinutes = 59;
                TotalTimeRemainingInMinutes -= 1;
            }
            else
            {
                TimeRemainingMinutes -= 1;
                TotalTimeRemainingInMinutes -= 1;
            }
            
            TimerTick?.Invoke(this, EventArgs.Empty);

            if (TimeRemainingHour <= 0 && TimeRemainingMinutes <= 0)
            {
                Expire();
            }
        }
    }
}
