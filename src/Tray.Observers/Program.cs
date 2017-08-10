// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           Program.cs
// Created:            05.05.2017
// \***************************************************************************/

using System;
using System.Windows;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.MessageBox;

namespace Tray.Observers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var runner = new SingleRunChecker())
            {
                if (!runner.ApplicationMayRun)
                {
                    MessageBox.Show(Localization.ApplicationIsAlreadyRunning, Localization.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
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
