using System;
using System.Windows.Forms;
using SystemTrayModule.Properties;

namespace SystemTrayModule.Base
{
    class SystemTrayMainElement : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        public SystemTrayMainElement()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.system_network_icon,
                Text = @"Tray plugins container",
                Visible = true
            };

            _notifyIcon.MouseClick += OnMouseClick;
            _notifyIcon.MouseDoubleClick += OnMouseDoubleClick;
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            Application.Exit();
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
