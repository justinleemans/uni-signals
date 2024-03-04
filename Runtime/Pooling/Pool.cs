using System;
using System.Collections.Generic;
using JeeLee.UniSignals.Domain;

namespace JeeLee.UniSignals.Pooling
{
    public class Pool<TSignal> : IPool
        where TSignal : ISignal
    {
        private readonly Queue<ISignal> _pool;
        
        public Pool()
        {
            _pool = new Queue<ISignal>();
        }

        public TSignal Get()
        {
            return _pool.Count > 0 ? (TSignal)_pool.Dequeue() : Activator.CreateInstance<TSignal>();
        }

        public void Release(TSignal signal)
        {
            signal.Clear();
            _pool.Enqueue(signal);
        }
    }
}