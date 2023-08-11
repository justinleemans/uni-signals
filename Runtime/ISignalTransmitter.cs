using JeeLee.Signals.Domain;

namespace JeeLee.Signals
{
    /// <summary>
    /// Interface used to access all signal transmission methods of the signals system.
    /// </summary>
    public interface ISignalTransmitter
    {
        /// <summary>
        /// Sends a new instance of this signal without setting properties.
        /// </summary>
        /// <typeparam name="TSignal">The type of signal to be send.</typeparam>
        void Send<TSignal>()
            where TSignal : Signal;

        /// <summary>
        /// Sends a signal of the given instance. Properties can be set before hand.
        /// </summary>
        /// <typeparam name="TSignal">The type of signal to be send.</typeparam>
        /// <param name="signal">The signal instance to be send.</param>
        void Send<TSignal>(TSignal signal)
            where TSignal : Signal;

        /// <summary>
        /// Allocates a signal instance of the given type.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to allocate.</typeparam>
        /// <returns>An instance of the signal of the requested type.</returns>
        TSignal GetSignal<TSignal>()
            where TSignal : Signal;
    }
}
