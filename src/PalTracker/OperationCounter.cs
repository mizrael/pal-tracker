using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PalTracker
{
    public class OperationCounter<T> : IOperationCounter<T>
    {
        public OperationCounter()
        {
            GetCounts = new ConcurrentDictionary<TrackedOperation, int>();
        }

        public void Increment(TrackedOperation operation)
        {
            if(!this.GetCounts.ContainsKey(operation))
                this.GetCounts.Add(operation, 1);
            else
                this.GetCounts[operation]++;
        }

        public IDictionary<TrackedOperation, int> GetCounts { get; }

        public string Name => $"{typeof(T).Name}Operations";
    }
}