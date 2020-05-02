using System.Collections.Generic;
using System.Linq;

namespace SISE.Helpers
{
    public class PriorityQueue<T>
    {
        SortedDictionary<int, Queue<T>> priorityQueue = new SortedDictionary<int, Queue<T>>();
        public int Count = 0;

        public void Enqueue(T item)
        {
            this.Enqueue(item, -1);
        }

        public void Enqueue(T item, int priority)
        {
            if (!priorityQueue.ContainsKey(priority))
            {
                priorityQueue.Add(priority, new Queue<T>());
            }
            priorityQueue[priority].Enqueue(item);
            Count++;
        }

        public T Dequeue()
        {
            int minKey = priorityQueue.Keys.Min();
            T item = priorityQueue[minKey].Dequeue();
            if (priorityQueue[minKey].Count == 0)
            {
                priorityQueue.Remove(minKey);
            }
            Count--;
            return item;
        }
    }
}