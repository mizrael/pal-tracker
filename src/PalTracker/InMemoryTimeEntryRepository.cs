using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private readonly Dictionary<long, TimeEntry> _items;
        private long _lastId = 0;

        public InMemoryTimeEntryRepository()
        {
            _items = new Dictionary<long, TimeEntry>();
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            if (!timeEntry.Id.HasValue)
            {
                Interlocked.Increment(ref _lastId);
                timeEntry.Id = _lastId;
            }

            _items.TryAdd(timeEntry.Id.Value, timeEntry);
            return timeEntry;
        }

        public TimeEntry Find(long id)
        {
            if (!Contains(id))
                throw new ArgumentOutOfRangeException(nameof(id), $"{id} is not a valid id");

            return _items[id];
        }

        public bool Contains(long id)
        {
            return _items.ContainsKey(id);
        }

        public IEnumerable<TimeEntry> List()
        {
            return _items.Values.ToArray();
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            if (!Contains(id))
                throw new ArgumentOutOfRangeException(nameof(id), $"{id} is not a valid id");
            timeEntry.Id = id;
            _items[id] = timeEntry;
            return timeEntry;
        }

        public void Delete(long id)
        {
            if (!Contains(id))
                throw new ArgumentOutOfRangeException(nameof(id), $"{id} is not a valid id");
            _items.Remove(id);
        }
    }
}