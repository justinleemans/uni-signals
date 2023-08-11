using JeeLee.Signals.Delegates;
using JeeLee.Signals.Domain;

namespace JeeLee.Signals
{
    /// <summary>
    /// Interface used to access all signal receiver methods of the signals system.
    /// </summary>
    public interface ISignalReceiver
    {
        /// <summary>
        /// Subscribe a given method to signals of this type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to subscribe to.</typeparam>
        /// <param name="handler">The method to be called when this signal is fired.</param>
        void Subscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal;

        /// <summary>
        /// Unsubscribe a given method from signals of this type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to unscubscribe from.</typeparam>
        /// <param name="handler">The method which needs to be unsubscribed.</param>
        void Unsubscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal;
    }
}
