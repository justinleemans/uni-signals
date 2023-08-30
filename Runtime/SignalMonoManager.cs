using JeeLee.Signals.Delegates;
using JeeLee.Signals.Domain;
using JeeLee.Signals.Pooling;
using JeeLee.Signals.Subscriptions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JeeLee.Signals
{
    /// <summary>
    /// Singleton signals manager class. Implements methods from `ISignalTransmitter` and `ISignalReceiver` to create general signals workflow.
    /// Also adds ISignalMuter to allow for signal handling to be paused.
    /// This version of the signals manager is build using a MonoBehaviour to be used as a singleton game object.
    /// </summary>
    public sealed class SignalMonoManager : MonoBehaviour, ISignalTransmitter, ISignalReceiver, ISignalMuter
    {
        private static SignalMonoManager _instance;

        private SignalPool _signalPool;
        private Dictionary<Type, ISubscription> _signalSubscriptions;

        /// <summary>
        /// The static instance of the singleton signals manager.
        /// </summary>
        public static SignalMonoManager Instance => _instance ??= CreateInstance();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                _instance.Initialize();
                DontDestroyOnLoad(_instance);
            }
        }

        private void Initialize()
        {
            _signalPool = new SignalPool();
            _signalSubscriptions = new Dictionary<Type, ISubscription>();
        }

        private static SignalMonoManager CreateInstance()
        {
            _instance = new GameObject("SignalMonoManager").AddComponent<SignalMonoManager>();
            _instance.Initialize();
            DontDestroyOnLoad(_instance);

            return _instance;
        }

        #region ISignalTransmitter Members

        /// <summary>
        /// Sends a new instance of this signal without setting properties.
        /// </summary>
        /// <typeparam name="TSignal">The type of signal to be send.</typeparam>
        public void Send<TSignal>()
            where TSignal : Signal
        {
            TSignal signal = GetSignal<TSignal>();
            Send(signal);
        }

        /// <summary>
        /// Sends a signal of the given instance. Properties can be set before hand.
        /// </summary>
        /// <typeparam name="TSignal">The type of signal to be send.</typeparam>
        /// <param name="signal">The signal instance to be send.</param>
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

        /// <summary>
        /// Allocates a signal instance of the given type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to allocate.</typeparam>
        /// <returns>An instance of the signal of the requested type.</returns>
        public TSignal GetSignal<TSignal>()
            where TSignal : Signal
        {
            return _signalPool.Get<TSignal>();
        }

        #endregion

        #region ISignalReceiver Members

        /// <summary>
        /// Subscribe a given method to signals of this type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to subscribe to.</typeparam>
        /// <param name="handler">The method to be called when this signal is fired.</param>
        public void Subscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal
        {
            if (!_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                _signalSubscriptions.Add(typeof(TSignal), subscription = new Subscription<TSignal>());
            }

            ((Subscription<TSignal>)subscription).AddHandler(handler);
        }

        /// <summary>
        /// Unsubscribe a given method from signals of this type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to unscubscribe from.</typeparam>
        /// <param name="handler">The method which needs to be unsubscribed.</param>
        public void Unsubscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal
        {
            if (_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                ((Subscription<TSignal>)subscription).RemoveHandler(handler);
            }
        }

        #endregion

        #region ISignalMuter Members

        /// <summary>
        /// Mutes all signal handlers from this type and prevents them from being invoked.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to mute.</typeparam>
        public void Mute<TSignal>()
            where TSignal : Signal
        {
            if (_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                subscription.Mute();
            }
        }

        /// <summary>
        /// Unmutes all signal handlers from this type and allows them to be invoked again.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to unmute</typeparam>
        public void Unmute<TSignal>()
            where TSignal : Signal
        {
            if (_signalSubscriptions.TryGetValue(typeof(TSignal), out var subscription))
            {
                subscription.Unmute();
            }
        }

        #endregion
    }
}
