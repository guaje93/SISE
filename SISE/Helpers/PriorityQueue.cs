using System;
using System.Collections.Generic;
using System.Linq;

namespace SISE.Helpers
{
    public class PriorityQueue<T>
    {
        #region Fields

        private readonly SortedDictionary<int, Queue<T>> _priorityQueue = new SortedDictionary<int, Queue<T>>();

        #endregion

        #region Methods

        public void Enqueue(T item, int priority = -1)
        {
            if (!_priorityQueue.ContainsKey(priority))
            {
                _priorityQueue.Add(priority, new Queue<T>());
            }
            _priorityQueue[priority].Enqueue(item);
        }

        public T Dequeue()
        {
            int minKey = _priorityQueue.Keys.Min();
            T item = _priorityQueue[minKey].Dequeue();
            if (_priorityQueue[minKey].Count == 0)
            {
                _priorityQueue.Remove(minKey);
            }
            return item;
        }

        public int Count() => _priorityQueue.Count();
        
        #endregion
    }
}