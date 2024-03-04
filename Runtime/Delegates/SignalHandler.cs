using JeeLee.UniSignals.Domain;

namespace JeeLee.UniSignals.Delegates
{
    public delegate void SignalHandler<in TSignal>(TSignal signal)
        where TSignal : ISignal;
}
