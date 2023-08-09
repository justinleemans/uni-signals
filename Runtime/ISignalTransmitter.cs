using JeeLee.Signals.Domain;

namespace JeeLee.Signals
{
    public interface ISignalTransmitter
    {
        void Send<TSignal>()
            where TSignal : Signal;

        void Send<TSignal>(TSignal signal)
            where TSignal : Signal;

        TSignal GetSignal<TSignal>()
            where TSignal : Signal;
    }
}
