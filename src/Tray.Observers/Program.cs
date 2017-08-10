// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           Program.cs
// Created:            05.05.2017
// \***************************************************************************/

using System;
using System.Windows.Forms;

namespace Tray.Observers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var dialog = new ChartForm();
            dialog.ShowDialog();

            return;

            using (var runner = new SingleRunChecker())
            {
                if (!runner.ApplicationMayRun)
                {
                    MessageBox.Show(Localization.ApplicationIsAlreadyRunning, Localization.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();

                    return;
                }

                // ReSharper disable once UnusedVariable
                using (var application = new TrayApplication())
                {
                    Application.Run();
                }
            }
        }
    }
}
