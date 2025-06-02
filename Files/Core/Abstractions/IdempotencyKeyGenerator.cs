using System.Text.Json;
using CleanArchitecture.Core.Interfaces.Idempotent;
using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Generates idempotency keys for entities of type <typeparamref name="T"/>.
    /// Ensures that operations on the same entity are not repeated by producing a unique hash based on the entity's serialized state.
    /// </summary>
    /// <typeparam name="T">The type of entity for which to generate idempotency keys. Must be a class.</typeparam>
    public class IdempotencyKeyGenerator<T> : IIdempotencyKeyGenerator<T> where T : class
    {
        // Serializer options to ensure consistent JSON output for hashing.
        readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Generates an idempotency key by serializing the entity and hashing the result.
        /// </summary>
        /// <param name="entity">The entity to generate a key for.</param>
        /// <returns>A hash string representing the idempotency key.</returns>
        public string GenerateIdempotencyKey(T entity)
        {
            var data = JsonSerializer.Serialize(entity, entity.GetType(), _jsonSerializerOptions);
            return HashGenerator.GenerateHash(data);
        }
    }
}