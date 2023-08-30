using JeeLee.Signals.Delegates;
using JeeLee.Signals.Domain;
using System;
using System.Collections.Generic;

namespace JeeLee.Signals.Subscriptions
{
    public class Subscription<TSignal> : ISubscription
        where TSignal : ISignal
    {
        private readonly HashSet<SignalHandler<TSignal>> _handlers;
        private readonly List<SignalHandler<TSignal>> _retroAddHandlers;
        private readonly List<SignalHandler<TSignal>> _retroRemoveHandlers;

        private bool _processing;

        public bool Muted { get; set; }

        public Subscription()
        {
            _handlers = new HashSet<SignalHandler<TSignal>>();
            _retroAddHandlers = new List<SignalHandler<TSignal>>();
            _retroRemoveHandlers = new List<SignalHandler<TSignal>>();
        }

        public void AddHandler(SignalHandler<TSignal> handler)
        {
            if (handler == null)
            {
                throw new NullReferenceException("Handler cannot be null");
            }

            if (!_processing)
            {
                _handlers.Add(handler);
            }
            else
            {
                _retroRemoveHandlers.RemoveAll(handle => handle == handler);
                _retroAddHandlers.Add(handler);
            }
        }

        public void RemoveHandler(SignalHandler<TSignal> handler)
        {
            if (handler == null)
            {
                throw new NullReferenceException("Handler cannot be null");
            }

            if (!_processing)
            {
                _handlers.Remove(handler);
            }
            else
            {
                _retroAddHandlers.RemoveAll(handle => handle == handler);
                _retroRemoveHandlers.Add(handler);
            }
        }

        public void Handle(TSignal signal)
        {
            if (Muted)
            {
                return;
            }
            
            _processing = true;

            foreach (var handler in _handlers)
            {
                handler?.Invoke(signal);
            }

            _processing = false;

            ProcessHandlerQueues();
        }

        private void ProcessHandlerQueues()
        {
            foreach (var handler in _retroAddHandlers)
            {
                AddHandler(handler);
            }

            _retroAddHandlers.Clear();

            foreach (var handler in _retroRemoveHandlers)
            {
                RemoveHandler(handler);
            }

            _retroRemoveHandlers.Clear();
        }
    }
}
