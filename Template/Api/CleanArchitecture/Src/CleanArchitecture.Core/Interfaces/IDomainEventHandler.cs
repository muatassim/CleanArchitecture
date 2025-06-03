namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for handling domain events of type <typeparamref name="T"/>.
    /// Implement this interface to create event handlers that react to specific domain events.
    /// </summary>
    /// <typeparam name="T">The type of domain event to handle. Must implement IDomainEvent.</typeparam>
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event instance.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        Task Handle(T domainEvent, CancellationToken cancellationToken);
    }
}