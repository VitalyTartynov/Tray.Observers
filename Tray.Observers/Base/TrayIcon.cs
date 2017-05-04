using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tray.Observers
{
    class TrayIcon : IDisposable
    {
        private readonly Color _color;
        private readonly PerformanceCounter _counter;
        private readonly NotifyIcon _notifyIcon;
        private readonly Dictionary<int, Icon> _iconCache = new Dictionary<int, Icon>();

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
            var value = Convert.ToInt32(_counter.NextValue());
            if (_iconCache.ContainsKey(value))
            {
                _notifyIcon.Icon = _iconCache[value];
                return;
            }

            var newIcon = IconHelper.Create(value, _color);
            _iconCache.Add(value, newIcon);
            _notifyIcon.Icon = newIcon;
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;

            while (_iconCache.Any())
            {
                var currentPair = _iconCache.First();
                IconHelper.Destroy(currentPair.Value.Handle);
                _iconCache.Remove(currentPair.Key);
            }

            _notifyIcon.Dispose();
        }
    }
}