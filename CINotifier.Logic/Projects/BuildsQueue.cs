using System.Collections.Concurrent;
using System.Collections.Generic;
using CINotifier.Logic.Infrastructure.Json;
using Newtonsoft.Json;

namespace CINotifier.Logic.Projects
{
    [JsonConverter(typeof(BuildsQueueConverter))]
    internal class BuildsQueue<T> : ConcurrentQueue<T>
    {
        private const int BuildsMaxNumber = 5;
        private readonly int limit;

        public BuildsQueue(IEnumerable<T> items) : base(items)
        {
            this.limit = BuildsMaxNumber;
        }

        internal BuildsQueue(int limit)
        {
            this.limit = limit;
        }

        public new void Enqueue(T item)
        {            
            if (this.Count >= this.limit)
            {
                T itemToDequeue;
                base.TryDequeue(out itemToDequeue);
            }

            base.Enqueue(item);
        }
    }
}
