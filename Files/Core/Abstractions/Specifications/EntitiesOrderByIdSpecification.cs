namespace CleanArchitecture.Core.Abstractions.Specifications
{
    /// <summary>
    /// Specification for retrieving a limited set of entities ordered by their ID.
    /// Applies paging to return only a specified number of entities, ordered by the entity's <c>Id</c> property.
    /// Useful for scenarios such as fetching the first N entities in a consistent order.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    /// <example>
    /// // Example: Get the first 5 users ordered by their ID
    /// var spec = new EntitiesOrderByIdSpecification&lt;User, Guid&gt;(5);
    /// var users = repository.List(spec);
    /// </example>
    public class EntitiesOrderByIdSpecification<TEntity, TEntityId> : Specification<TEntity, TEntityId>
            where TEntity : Entity<TEntityId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesOrderByIdSpecification{TEntity, TEntityId}"/> class.
        /// </summary>
        /// <param name="takeRecords">The number of entities to return.</param>
        public EntitiesOrderByIdSpecification(int takeRecords)
        {
            ApplyPaging(0, takeRecords); // Return a limited number of entities
            AddOrderBy(e => e.Id!);      // Order by ID
        }
    }
}