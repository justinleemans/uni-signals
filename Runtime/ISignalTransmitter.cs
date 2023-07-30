using JeeLee.Signals.Domain;

namespace JeeLee.Signals
{
    public interface ISignalTransmitter
    {
        void Send<TSignal>()
            where TSignal : class, ISignal;

        void Send<TSignal>(TSignal signal)
            where TSignal : class, ISignal;

        TSignal GetSignal<TSignal>()
            where TSignal : class, ISignal;
    }
}
