using System.Text.Json;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Generates idempotency keys for domain events to ensure that event processing is not repeated.
    /// </summary>
    public class DomainEventIdempotencyKeyGenerator : IDomainEventIdempotencyKeyGenerator
    {
        // Serializer options to ensure consistent JSON output for hashing.
        readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Generates an idempotency key by serializing the domain event and hashing the result.
        /// </summary>
        /// <param name="domainEvent">The domain event to generate a key for.</param>
        /// <returns>A hash string representing the idempotency key.</returns>
        public string GenerateIdempotencyKey(IDomainEvent domainEvent)
        {
            var data = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), _jsonSerializerOptions);
            return HashGenerator.GenerateHash(data);
        }

        /// <summary>
        /// Generates an idempotency key by hashing the provided JSON string.
        /// </summary>
        /// <param name="json">The JSON representation of a domain event.</param>
        /// <returns>A hash string representing the idempotency key.</returns>
        public string GenerateIdempotencyKey(string json)
        {
            return HashGenerator.GenerateHash(json);
        }
    }
}