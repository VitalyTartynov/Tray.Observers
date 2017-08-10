// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           Cache.cs
// Created:            10.08.2017
// \***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Tray.Observers
{
    class Cache
    {
        public event EventHandler<AddingNewItemEventArgs> OnValuesChanged;

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

            OnValuesChanged?.Invoke(this, new AddingNewItemEventArgs {NewItem = item});
        }

        public IEnumerable<CacheItem> GetAll()
        {
            return _values.AsEnumerable();
        }
    }
}