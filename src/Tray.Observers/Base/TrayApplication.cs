// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           TrayApplication.cs
// Created:            10.08.2017
// \***************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tray.Observers.Resources;

namespace Tray.Observers
{
    class TrayApplication : IDisposable
    {
        private readonly Timer _timer;

        private readonly ICollection<TrayObserver> _icons = new List<TrayObserver>();

        public TrayApplication()
        {
            _timer = new Timer
            {
                Interval = 1000
            };
            _timer.Tick += Timer_Tick;

            var contextMenu = new ContextMenu();
            var exitItem = new MenuItem(Localization.Exit, OnExitClick);
            contextMenu.MenuItems.Add(exitItem);

            _icons.Add(new TrayObserver(name: Localization.UsingCpuTooltip, color: Color.OrangeRed,
                counter: new PerformanceCounter("Processor Information", "% Priority Time", "_Total"),
                menu: contextMenu));
            _icons.Add(new TrayObserver(name: Localization.UsingMemoryTooltip, color: Color.Yellow,
                counter: new PerformanceCounter("Память", "% использования выделенной памяти"), menu: contextMenu));
            _icons.Add(new TrayObserver(name: Localization.UsingDiskTooltip, color: Color.LightBlue,
                counter: new PerformanceCounter("Физический диск", "% активности диска", "_Total"), menu: contextMenu));

            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var trayIcon in _icons)
            {
                trayIcon.Update();
            }
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Dispose()
        {
            _timer.Stop();

            foreach (var trayIcon in _icons)
            {
                trayIcon.Dispose();
            }
            _icons.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
