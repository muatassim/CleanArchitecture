namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Provides an abstraction for retrieving the current UTC date and time.
    /// Useful for decoupling time retrieval from system calls, enabling easier testing and mocking.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        DateTime UtcNow { get; }
    }
}