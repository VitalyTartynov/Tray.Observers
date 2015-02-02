using System;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SystemTrayModule.Base
{
    class SingleRunChecker : IDisposable
    {
        private readonly Mutex _singleRunMutex;

        public bool ApplicationMayRun { get; set; }

        public SingleRunChecker()
        {
            var guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            bool applicationMayRun;
            _singleRunMutex = new Mutex(true, guid, out applicationMayRun);

            ApplicationMayRun = applicationMayRun;
        }

        public void Dispose()
        {
            if (_singleRunMutex != null)
            {
                _singleRunMutex.ReleaseMutex();
            }
        }
    }
}
