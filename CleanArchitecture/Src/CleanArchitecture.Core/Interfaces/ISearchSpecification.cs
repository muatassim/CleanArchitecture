using System.Linq.Expressions;
namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for specifying search criteria, includes, ordering, and paging for queries on entities with a specific identifier type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    public interface ISearchSpecification<TEntity, TEntityId>
    {
        /// <summary>
        /// The filter criteria as a LINQ expression.
        /// </summary>
        Expression<Func<TEntity, bool>>? Criteria { get; }

        /// <summary>
        /// List of related entities to include in the query.
        /// </summary>
        List<Expression<Func<TEntity, object>>> Includes { get; }

        /// <summary>
        /// Expression for ordering the results ascending.
        /// </summary>
        Expression<Func<TEntity, object>>? OrderBy { get; }

        /// <summary>
        /// Expression for ordering the results descending.
        /// </summary>
        Expression<Func<TEntity, object>>? OrderByDescending { get; }

        /// <summary>
        /// Number of records to take (for paging).
        /// </summary>
        int Take { get; }

        /// <summary>
        /// Number of records to skip (for paging).
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// Indicates if paging is enabled.
        /// </summary>
        bool IsPagingEnabled { get; }

        /// <summary>
        /// The page size for paging.
        /// </summary>
        int PageSize { get; set; }
    }

    /// <summary>
    /// Defines a contract for specifying search criteria, includes, ordering, and paging for queries on entities without a specific identifier type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface ISearchSpecification<TEntity>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        int PageSize { get; set; }
    }
}