using JeeLee.Signals.Delegates;
using JeeLee.Signals.Domain;
using JeeLee.Signals.Pooling;
using JeeLee.Signals.Subscriptions;
using System;
using System.Collections.Generic;

namespace JeeLee.Signals
{
    public class SignalManager : ISignalTransmitter, ISignalReceiver
    {
        private readonly SignalPool _signalPool;
        private readonly Dictionary<Type, ISubscription> _signalSubscriptions;

        public SignalManager()
        {
            _signalPool = new SignalPool();
            _signalSubscriptions = new Dictionary<Type, ISubscription>();
        }

        public void Send<TSignal>()
            where TSignal : Signal
        {
            TSignal signal = GetSignal<TSignal>();
            Send(signal);
        }

        public void Send<TSignal>(TSignal signal)
            where TSignal : Signal
        {
            Type type = signal.GetType();

            while (type != null)
            {
                if (typeof(ISignal).IsAssignableFrom(type) && _signalSubscriptions.TryGetValue(type, out var subscription))
                {
                    ((Subscription<TSignal>)subscription).Handle(signal);
                }
                
                type = type.BaseType;
            }

            _signalPool.Release(signal);
        }

        public TSignal GetSignal<TSignal>()
            where TSignal : Signal
        {
            return _signalPool.Get<TSignal>();
        }

        public void Subscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal
        {
            if (!_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                _signalSubscriptions.Add(typeof(TSignal), subscription = new Subscription<TSignal>());
            }

            ((Subscription<TSignal>)subscription).AddHandler(handler);
        }

        public void Unsubscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal
        {
            if (_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                ((Subscription<TSignal>)subscription).RemoveHandler(handler);
            }
        }
    }
}
