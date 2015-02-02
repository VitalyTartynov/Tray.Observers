using System;
using System.Windows.Forms;
using SystemTrayModule.Base;

namespace SystemTrayModule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var runner = new SingleRunChecker())
            {
                if (runner.ApplicationMayRun)
                {
                    using (var mainElement = new SystemTrayMainElement())
                    {
                        Application.Run();
                    }
                }
            }            
        }
    }
}
