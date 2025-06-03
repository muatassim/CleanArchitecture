namespace CleanArchitecture.Core.Interfaces.Idempotent
{
    /// <summary>
    /// Defines a contract for generating idempotency keys for entities of type <typeparamref name="T"/>.
    /// Used to ensure that operations on the same entity are not repeated by producing a unique key based on the entity's state.
    /// </summary>
    /// <typeparam name="T">The type of entity for which to generate idempotency keys. Must be a class.</typeparam>
    public interface IIdempotencyKeyGenerator<T> where T : class
    {
        /// <summary>
        /// Generates an idempotency key for the given entity.
        /// </summary>
        /// <param name="entity">The entity to generate a key for.</param>
        /// <returns>A string representing the idempotency key.</returns>
        string GenerateIdempotencyKey(T entity);
    }
}