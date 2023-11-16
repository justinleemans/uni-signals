using JeeLee.Signals.Domain;

namespace JeeLee.Signals.Subscriptions
{
    public interface ISubscription
    {
        void Handle(ISignal signal);
        void Mute();
        void Unmute();
    }
}
