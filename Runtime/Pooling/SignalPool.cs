using JeeLee.Signals.Domain;
using System;
using System.Collections.Generic;

namespace JeeLee.Signals.Pooling
{
    public class SignalPool
    {
        private readonly Dictionary<Type, Queue<ISignal>> _signalPool;

        public SignalPool()
        {
            _signalPool = new Dictionary<Type, Queue<ISignal>>();
        }

        public TSignal Get<TSignal>()
            where TSignal : ISignal
        {
            AllocateInternalPool<TSignal>(out Queue<ISignal> queue);

            return queue.Count > 0 ? (TSignal)queue.Dequeue() : Activator.CreateInstance<TSignal>();
        }

        public void Release<TSignal>(TSignal signal)
            where TSignal : ISignal
        {
            signal.OnClear();
            
            AllocateInternalPool<TSignal>(out Queue<ISignal> queue);

            queue.Enqueue(signal);
        }

        private Queue<ISignal> AllocateInternalPool<TSignal>(out Queue<ISignal> queue)
        {
            if (!_signalPool.TryGetValue(typeof(TSignal), out queue))
            {
                queue = new Queue<ISignal>();
                _signalPool.Add(typeof(TSignal), queue);
            }

            return queue;
        }
    }
}
