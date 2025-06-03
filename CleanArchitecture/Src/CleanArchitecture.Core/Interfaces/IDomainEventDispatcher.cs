namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for dispatching domain events.
    /// Implementations are responsible for invoking event handlers for each domain event.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches a collection of domain events asynchronously.
        /// </summary>
        /// <param name="domainEvents">The domain events to dispatch.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}