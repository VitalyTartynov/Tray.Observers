using System;
using System.Diagnostics;
using System.Windows.Forms;
using SystemTrayModule.Properties;

namespace SystemTrayModule.Base
{
    class SystemTrayApplication : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private MenuItem _cpu;
        private MenuItem _memory;

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public SystemTrayApplication()
        {
            var contextMenu = new ContextMenu();
            var exitItem = new MenuItem("Exit", OnExitClick);
            contextMenu.MenuItems.Add(exitItem);

            _cpu = new MenuItem(string.Empty);
            contextMenu.MenuItems.Add(_cpu);

            _memory = new MenuItem(string.Empty);
            contextMenu.MenuItems.Add(_memory);

            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.system_network_icon,
                Text = @"Tray plugins container",
                Visible = true,
            };

            _notifyIcon.MouseClick += OnMouseClick;
            _notifyIcon.MouseDoubleClick += OnMouseDoubleClick;
            _notifyIcon.ContextMenu = contextMenu;
            _notifyIcon.MouseDown += NotifyIconOnMouseDown;
            
            _notifyIcon.BalloonTipShown += NotifyIconOnBalloonTipShown;
            _notifyIcon.MouseMove += NotifyIconOnBalloonTipShown;

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        private void NotifyIconOnBalloonTipShown(object sender, EventArgs eventArgs)
        {
            //_notifyIcon.BalloonTipTitle = "title";
            //_notifyIcon.BalloonTipText = $"CPU: {cpuCounter.NextValue()}% RAM: {ramCounter.NextValue()}Mb";
            _notifyIcon.Text = $"CPU: {cpuCounter.NextValue()}% RAM: {ramCounter.NextValue()}Mb";
        }

        private void NotifyIconOnMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            _cpu.Text = $"CPU: {cpuCounter.NextValue()}%";
            _memory.Text = $"RAM: {ramCounter.NextValue()}Mb";
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            throw new NotImplementedException();
            //var connected = Pinger.Ping(new Uri("http://www.google.com/"));
            //MessageBox.Show("Ping result = " + connected, "Results", MessageBoxButtons.OK);
        }

        private void OnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}
