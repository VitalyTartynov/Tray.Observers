using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace SysUsageTrayMonitor.Base
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
