using JeeLee.UniSignals.Domain;

namespace JeeLee.UniSignals
{
    /// <summary>
    /// Interface used to allow signals to be muted by type.
    /// </summary>
    public interface ISignalMuter
    {
        /// <summary>
        /// Mutes all signal handlers from this type and prevents them from being invoked.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to mute.</typeparam>
        void Mute<TSignal>()
            where TSignal : Signal;

        /// <summary>
        /// Unmutes all signal handlers from this type and allows them to be invoked again.
        /// </summary>
        /// <typeparam name="TSignal">The signal type to unmute</typeparam>
        void Unmute<TSignal>()
            where TSignal : Signal;
    }
}
