namespace JeeLee.UniSignals.Domain
{
    /// <summary>
    /// Signal interface, used internally by the signal system. To make instances of a signal you should inherit from `Signal` instead.
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Used internally by the singal pool to reset properties.
        /// </summary>
        void Clear();
    }
}
