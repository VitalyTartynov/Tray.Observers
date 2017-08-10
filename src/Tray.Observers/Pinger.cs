// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           Pinger.cs
// Created:            05.05.2017
// \***************************************************************************/

using System;
using System.Net.NetworkInformation;

namespace Tray.Observers
{
    public class Pinger
    {
        public static bool Ping(Uri uri)
        {
            try
            {
                var host = uri.Host;
                using (var pinger = new Ping())
                {
                    var reply = pinger.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

            }

            return false;
        }
    }
}