using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SysUsageTrayMonitor.Base
{
    class SystemTrayApplication : IDisposable
    {
        private Timer _timer;
        private readonly NotifyIcon _cpuIcon;
        private readonly NotifyIcon _memoryIcon;
        private readonly NotifyIcon _ioIcon;

        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _memoryCounter;
        private PerformanceCounter _ioCounter;

        public SystemTrayApplication()
        {
            _timer = new Timer
            {
                Interval = 1000                
            };
            _timer.Tick += Timer_Tick;

            var contextMenu = new ContextMenu();
            var exitItem = new MenuItem("Exit", OnExitClick);
            contextMenu.MenuItems.Add(exitItem);

            _cpuIcon = new NotifyIcon
            {
                Text = "CPU",
                Visible = true,
                ContextMenu = contextMenu
            };

            _memoryIcon = new NotifyIcon
            {
                Text = "Memory",
                Visible = true,
                ContextMenu = contextMenu
            };

            _ioIcon = new NotifyIcon
            {
                Text = "I/O",
                Visible = true,
                ContextMenu = contextMenu
            };

            _cpuCounter = new PerformanceCounter("Processor Information", "% Priority Time", "_Total");
            _ioCounter = new PerformanceCounter("Физический диск", "% активности диска", "_Total");
            _memoryCounter = new PerformanceCounter("Память", "% использования выделенной памяти");

            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _cpuIcon.Icon = CreateIcon(Convert.ToInt32(_cpuCounter.NextValue()), Color.OrangeRed);
                _memoryIcon.Icon = CreateIcon(Convert.ToInt32(_memoryCounter.NextValue()), Color.Yellow);
                _ioIcon.Icon = CreateIcon(Convert.ToInt32(_ioCounter.NextValue()), Color.LightBlue);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Icon CreateIcon(int value, Color color)
        {
            var brush = new SolidBrush(color);
            var bitmap = new Bitmap(32, 32);
            var font = new Font("Tahoma", 16, FontStyle.Bold);
            var graphics = Graphics.FromImage(bitmap);
            var leftMargin = value > 9 ? 1 : 7;
            graphics.DrawString(value.ToString(), font, brush, leftMargin, 2);

            return Icon.FromHandle(bitmap.GetHicon());
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }        

        public void Dispose()
        {
            _timer.Stop();
            _cpuIcon.Visible = false;
            _memoryIcon.Visible = false;
            _ioIcon.Visible = false;
            _cpuIcon.Dispose();
            _memoryIcon.Dispose();
            _ioIcon.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
