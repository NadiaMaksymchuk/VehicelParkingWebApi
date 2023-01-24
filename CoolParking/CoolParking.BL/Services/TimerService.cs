using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService, IDisposable
    {
        public double Interval { get; set; }
        public bool IsDisposed { get; private set; }

        private static Timer _timer;

        public event ElapsedEventHandler Elapsed;
        
        public void Start()
        {
            _timer = new Timer(Interval);
            _timer.Elapsed += Elapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.AutoReset = false;
            _timer.Enabled = false;
        }

        public void Dispose()
        {
            _timer=null;
            _timer.Dispose();
        }

    }
}