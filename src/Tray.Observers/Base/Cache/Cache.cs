using System.Collections.Generic;
using System.Linq;

namespace Tray.Observers
{
    class Cache
    {
        private readonly Queue<CacheItem> _values;

        public int MaxCount { get; }

        public Cache(int maxCount)
        {
            MaxCount = maxCount;
            _values = new Queue<CacheItem>(MaxCount);
        }

        public void Add(CacheItem item)
        {
            if (_values.Count == MaxCount)
            {
                _values.Dequeue();
            }

            _values.Enqueue(item);
        }

        public IEnumerable<CacheItem> GetAll()
        {
            return _values.AsEnumerable();
        }
    }
}