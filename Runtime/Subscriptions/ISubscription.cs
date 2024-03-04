using JeeLee.UniSignals.Domain;

namespace JeeLee.UniSignals.Subscriptions
{
    public interface ISubscription
    {
        void Handle(ISignal signal);
        void Mute();
        void Unmute();
    }
}
