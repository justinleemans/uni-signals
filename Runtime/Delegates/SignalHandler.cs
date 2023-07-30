using JeeLee.Signals.Domain;

namespace JeeLee.Signals.Delegates
{
    public delegate void SignalHandler<in TSignal>(TSignal signal)
        where TSignal : ISignal;
}
