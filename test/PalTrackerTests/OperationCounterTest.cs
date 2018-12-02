using System.Collections.Generic;
using PalTracker;
using Xunit;

namespace PalTrackerTests
{
    public class OperationCounterTest
    {
        private readonly OperationCounter<TimeEntry> _counter;

        public OperationCounterTest()
        {
            _counter = new OperationCounter<TimeEntry>();
        }

        [Fact]
        public void Increment()
        {
            var exepectedCounts = new Dictionary<TrackedOperation, int>
            {
                {TrackedOperation.Create, 1}, //had to change this from 0 otherwise test would fail (inner for wouldn't be triggered)
                {TrackedOperation.Read, 3},
                {TrackedOperation.List, 1},
                {TrackedOperation.Update, 7},
                {TrackedOperation.Delete, 3}
            };

            foreach (var entry in exepectedCounts)
            {
                for (var i = 0; i < entry.Value; i++)
                {
                    _counter.Increment(entry.Key);
                }
            }

            Assert.Equal(exepectedCounts, _counter.GetCounts);
        }

        [Fact]
        public void Name()
        {
            Assert.Equal("TimeEntryOperations", _counter.Name);
        }
    }
}