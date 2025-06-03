namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for generating idempotency keys for domain events.
    /// Used to ensure that the same domain event is not processed more than once.
    /// </summary>
    public interface IDomainEventIdempotencyKeyGenerator
    {
        /// <summary>
        /// Generates an idempotency key for the given domain event instance.
        /// </summary>
        /// <param name="domainEvent">The domain event to generate a key for.</param>
        /// <returns>A string representing the idempotency key.</returns>
        string GenerateIdempotencyKey(IDomainEvent domainEvent);

        /// <summary>
        /// Generates an idempotency key from a JSON representation of a domain event.
        /// </summary>
        /// <param name="json">The JSON string representing the domain event.</param>
        /// <returns>A string representing the idempotency key.</returns>
        string GenerateIdempotencyKey(string json);
    }
}