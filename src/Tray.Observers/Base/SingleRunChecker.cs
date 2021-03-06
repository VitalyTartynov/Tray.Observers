﻿// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           SingleRunChecker.cs
// Created:            05.05.2017
// \***************************************************************************/

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tray.Observers
{
    class SingleRunChecker : IDisposable
    {
        private readonly Mutex _singleRunMutex;

        public bool ApplicationMayRun { get; }

        public SingleRunChecker()
        {
            var guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            bool applicationMayRun;
            _singleRunMutex = new Mutex(true, guid, out applicationMayRun);

            ApplicationMayRun = applicationMayRun;
        }

        public void Dispose()
        {
            _singleRunMutex?.ReleaseMutex();
            GC.SuppressFinalize(this);
        }
    }
}
