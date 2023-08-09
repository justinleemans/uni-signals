namespace JeeLee.Signals.Domain
{
    public abstract class Signal : ISignal
    {
        public void Clear()
        {
            OnClear();
        }

        public virtual void OnClear()
        {
        }
    }
}
