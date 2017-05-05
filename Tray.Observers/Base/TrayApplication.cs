using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tray.Observers
{
    class TrayApplication : IDisposable
    {
        private readonly Timer _timer;

        private readonly ICollection<TrayObserver> _observers = new List<TrayObserver>();

        public TrayApplication()
        {
            _timer = new Timer
            {
                Interval = 500                
            };
            _timer.Tick += Timer_Tick;

            var contextMenu = new ContextMenu();
            var exitItem = new MenuItem(Localization.Exit, OnExitClick);
            contextMenu.MenuItems.Add(exitItem);
            
            _observers.Add(new TrayObserver(name: Localization.UsingCpuTooltip, color: Color.OrangeRed,
                counter: new PerformanceCounter("Processor Information", "% Priority Time", "_Total"),
                menu: contextMenu));
            _observers.Add(new TrayObserver(name: Localization.UsingMemoryTooltip, color: Color.Yellow,
                counter: new PerformanceCounter("Память", "% использования выделенной памяти"), menu: contextMenu));
            _observers.Add(new TrayObserver(name: Localization.UsingDiskTooltip, color: Color.LightBlue,
                counter: new PerformanceCounter("Физический диск", "% активности диска", "_Total"), menu: contextMenu));
            
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }        

        public void Dispose()
        {
            _timer.Stop();

            while (_observers.Any())
            {
                var observer = _observers.First();
                _observers.Remove(observer);
                observer.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
