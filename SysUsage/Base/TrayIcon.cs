using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SysUsageTrayMonitor
{
    class TrayIcon : IDisposable
    {
        private readonly Color _color;
        private readonly PerformanceCounter _counter;
        private readonly NotifyIcon _notifyIcon;

        public TrayIcon(string name, Color color, PerformanceCounter counter, ContextMenu menu)
        {
            _color = color;
            _counter = counter;
            _notifyIcon = new NotifyIcon
            {
                Text = name,
                Visible = true,
                ContextMenu = menu
            };
        }

        public void Update()
        {
            if (_notifyIcon.Icon != null) IconHelper.Destroy(_notifyIcon.Icon.Handle);
            _notifyIcon.Icon = IconHelper.Create(Convert.ToInt32(_counter.NextValue()), _color);
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}