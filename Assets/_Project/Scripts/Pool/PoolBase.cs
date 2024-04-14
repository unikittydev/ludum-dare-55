using System;
using System.Collections.Generic;

namespace UniOwl
{
    public class PoolBase<T>
    {
        private readonly Queue<T> _queue = new();

        private readonly Func<T> _preloadAction;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;

        public PoolBase(Func<T> preloadAction, Action<T> getAction, Action<T> returnAction, int initialCount)
        {
            _preloadAction = preloadAction;
            _getAction = getAction;
            _returnAction = returnAction;

            for (int i = 0; i < initialCount; i++)
                Return(preloadAction());
        }

        public T Get()
        {
            T item = _queue.Count > 0 ? _queue.Dequeue() : _preloadAction();
            _getAction(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _queue.Enqueue(item);
        }
    }
}
