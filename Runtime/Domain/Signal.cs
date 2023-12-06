namespace JeeLee.Signals.Domain
{
    /// <summary>
    /// Abstract signal class used to define signals.
    /// </summary>
    public abstract class Signal : ISignal
    {
        /// <summary>
        /// Used internally by the singal pool to reset properties.
        /// </summary>
        public void Clear()
        {
            OnClear();
        }

        /// <summary>
        /// Method used to define what needs to happen to a signal when it gets reset for future use.
        /// </summary>
        protected virtual void OnClear()
        {
        }
    }
}
