using System;

namespace Tray.Observers
{
    class CacheItem
    {
        public DateTime Stamp { get; }
        public double Value { get; }

        public CacheItem(DateTime stamp, double value)
        {
            Stamp = stamp;
            Value = value;
        }
    }
}