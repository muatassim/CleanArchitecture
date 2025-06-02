namespace CleanArchitecture.Core.Interfaces.Messaging
{
    /// <summary>
    /// Abstract base class for commands that are idempotent (safe to execute multiple times without side effects).
    /// Implements <see cref="ICommand"/> and provides a unique <see cref="RequestId"/> to identify the command request.
    /// Useful for scenarios where duplicate command submissions may occur (e.g., network retries, distributed systems).
    /// </summary>
    public abstract class IdempotentCommand(string requestId) : ICommand
    {
        /// <summary>
        /// Unique identifier for the command request, used to ensure idempotency.
        /// </summary>
        public string RequestId { get; } = requestId;
    }

    /// <summary>
    /// Abstract base class for idempotent commands that return a response of type <typeparamref name="TResponse"/>.
    /// Implements <see cref="ICommand{TResponse}"/> and provides a unique <see cref="RequestId"/> to identify the command request.
    /// Useful for safely handling commands that may be retried and expect a result.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
    public abstract class IdempotentCommand<TResponse>(string requestId) : ICommand<TResponse>
    {
        /// <summary>
        /// Unique identifier for the command request, used to ensure idempotency.
        /// </summary>
        public string RequestId { get; } = requestId;
    }
}