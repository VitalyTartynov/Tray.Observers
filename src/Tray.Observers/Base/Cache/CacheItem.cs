using System;

namespace Tray.Observers
{
    class CacheItem
    {
        public DateTime Stamp { get; }
        public int Value { get; }

        public CacheItem(DateTime stamp, int value)
        {
            Stamp = stamp;
            Value = value;
        }
    }
}