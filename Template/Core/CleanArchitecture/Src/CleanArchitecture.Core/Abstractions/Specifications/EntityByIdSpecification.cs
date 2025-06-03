namespace CleanArchitecture.Core.Abstractions.Specifications
{
    /// <summary>
    /// Specification for retrieving a single entity by its unique identifier.
    /// Encapsulates the criteria to filter entities where the entity's <c>Id</c> matches the provided value.
    /// Useful for repository queries such as "find by ID" in a type-safe and reusable way.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    /// <example>
    /// // Example: Find a user by their unique ID
    /// var spec = new EntityByIdSpecification&lt;User, Guid&gt;(userId);
    /// var user = repository.Get(spec);
    /// </example>
    public class EntityByIdSpecification<TEntity, TEntityId>(TEntityId id) : Specification<TEntity, TEntityId>
        (e => e.Id != null && e.Id.Equals(id))
        where TEntity : Entity<TEntityId>
    {
    }
}