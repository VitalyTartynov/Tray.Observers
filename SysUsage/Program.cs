using System;
using System.Windows.Forms;
using SysUsageTrayMonitor.Base;

namespace SysUsageTrayMonitor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var runner = new SingleRunChecker())
            {
                if (!runner.ApplicationMayRun)
                {
                    MessageBox.Show("Приложение уже запущено");
                    return;
                }

                using (var application = new SystemTrayApplication())
                {
                    Application.Run();
                }
            }            
        }
    }
}
