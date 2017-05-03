using System;
using System.Windows.Forms;
using SystemTrayModule.Properties;

namespace SystemTrayModule.Base
{
    class SystemTrayApplication : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        public SystemTrayApplication()
        {
            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Exit", OnExitClick);
            contextMenu.MenuItems.Add(menuItem);
            
            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.system_network_icon,
                Text = @"Tray plugins container",
                Visible = true
            };

            _notifyIcon.MouseClick += OnMouseClick;
            _notifyIcon.MouseDoubleClick += OnMouseDoubleClick;
            _notifyIcon.ContextMenu = contextMenu;
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}
