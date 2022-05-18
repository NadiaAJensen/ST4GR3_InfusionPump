﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer1;
using IP_DataAccessLayer1.TCP;


namespace IP_BusinessLogicLayer
{
    public class AlarmControl : IAlarmControl
    {
        private IBatteryStatus _batteryStatus;
        private ITimer _timer;
        private ISender _sender;
        public event EventHandler Alarm;
        public string[] LastAlarmMessage { get; private set; }
        public string AlarmCode { get; private set; }
        public AlarmControl(IBatteryStatus batteryStatus, ITimer timer, ISender sender)
        {
            _batteryStatus = batteryStatus;
            _timer = timer;
            _sender = sender;
            _timer.Expired += new EventHandler(OnTimerExpired);
            _batteryStatus.LowBatteryLevel += new EventHandler(AlertLowBatteryLevel);
            //Skal i teorien også koble sig til bobbelsensor - men denne implementeres ikke. 
            LastAlarmMessage = new string[2];
        }

        public void Run()
        {
            // Måske denne tråd skal sætte tiden i gang
            while (true)
            {
                _batteryStatus.CalculateBatteryStatus();
                Thread.Sleep(30000); // så læser den hvert 30 sekund

            }
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            LastAlarmMessage[0] = "Behandlingen er";
            LastAlarmMessage[1] = "faerdig";
            AlarmCode = "Tid";
            Alarm?.Invoke(this,System.EventArgs.Empty);
            _sender.SendData("Besked til ICA: Alarm: tid udløbet");
        }
        public void AlertLowBatteryLevel(object sender, EventArgs e)
        {
            int value = _batteryStatus.GetBatteryLevel();
            LastAlarmMessage[0] = ($"Batteristatus: {value}%");
            LastAlarmMessage[1] = "";
            AlarmCode = "Batteri";
            Alarm?.Invoke(this, System.EventArgs.Empty);
            _sender.SendData("Besked til ICA: Lavt batteriniveau!");
        }

        public void BobbleDetected(object sender, EventArgs e)
        {
            //Simulerer, at der er bobbel i røret. 
            LastAlarmMessage[0] = ("Boble detekteret");
            LastAlarmMessage[1] = "";
            AlarmCode = "Bobbel";
            Alarm?.Invoke(this, System.EventArgs.Empty);
            _sender.SendData("Besked til ICA: Bobbel detekteret");
            //Der burde nok laves et filter på denne.
        }


    }
}
