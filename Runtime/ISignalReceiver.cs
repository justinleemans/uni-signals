using JeeLee.Signals.Delegates;
using JeeLee.Signals.Domain;

namespace JeeLee.Signals
{
    public interface ISignalReceiver
    {
        void Subscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal;

        void Unsubscribe<TSignal>(SignalHandler<TSignal> handler)
            where TSignal : Signal;
    }
}
