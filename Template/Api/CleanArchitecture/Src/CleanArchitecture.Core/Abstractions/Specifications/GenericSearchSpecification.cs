using System.Linq.Expressions;
using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.Core.Abstractions.Specifications
{
    /// <summary>
    /// A generic specification for searching, sorting, and paging entities based on a <see cref="SearchRequest"/>.
    /// This class dynamically builds query criteria, ordering, and paging instructions for any entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    /// <remarks>
    /// This specification is useful for implementing flexible, reusable search endpoints or repository methods.
    /// It uses the <see cref="SearchRequest"/> to determine search value, sort order, sort column, page size, and skip count.
    /// </remarks>
    /// <example>
    /// // Example: Search for users with paging and sorting
    /// var searchRequest = new SearchRequest
    /// {
    ///     SearchValue = "john",
    ///     SortOrderColumn = "LastName",
    ///     SortOrder = OrderBy.Ascending,
    ///     PageSize = 10,
    ///     Skip = 0
    /// };
    /// var spec = new GenericSearchSpecification&lt;User, Guid&gt;(searchRequest);
    /// var users = repository.List(spec);
    /// </example>
    public class GenericSearchSpecification<TEntity, TEntityId> : Specification<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericSearchSpecification{TEntity, TEntityId}"/> class
        /// using the provided <paramref name="searchRequest"/> to configure criteria, ordering, and paging.
        /// </summary>
        /// <param name="searchRequest">The search request containing filter, sort, and paging options.</param>
        public GenericSearchSpecification(SearchRequest searchRequest)
        {
            if (string.IsNullOrEmpty(searchRequest.SearchValue))
            {
                AddCriteria(e => true); // Default search expression
            }
            if (!string.IsNullOrEmpty(searchRequest.SortOrderColumn))
            {
                if (searchRequest.SortOrder == Shared.OrderBy.Ascending)
                {
                    AddOrderBy(GetOrderByExpression(searchRequest.SortOrderColumn));
                }
                else if (searchRequest.SortOrder == Shared.OrderBy.Descending)
                {
                    AddOrderByDescending(GetOrderByExpression(searchRequest.SortOrderColumn));
                }
            }
            else
            {
                AddOrderBy(e => true); // Default order by expression
            }
            PageSize = searchRequest.PageSize;
            // Set Skip and Take values based on the caller's input
            ApplyPaging(searchRequest.Skip, searchRequest.PageSize);
        }

        /// <summary>
        /// The size of the page (number of records to return).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Builds a dynamic order by expression for the specified column name.
        /// </summary>
        /// <param name="columnName">The name of the property to order by.</param>
        /// <returns>An expression for ordering entities by the specified property.</returns>
        private static Expression<Func<TEntity, object>> GetOrderByExpression(string columnName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, columnName);
            var conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(conversion, parameter);
        }
    }

    /// <summary>
    /// A generic specification for searching, sorting, and paging entities (non-generic ID version).
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class GenericSearchSpecification<TEntity> : Specification<TEntity>
     where TEntity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericSearchSpecification{TEntity}"/> class
        /// using the provided <paramref name="searchRequest"/> to configure criteria, ordering, and paging.
        /// </summary>
        /// <param name="searchRequest">The search request containing filter, sort, and paging options.</param>
        public GenericSearchSpecification(SearchRequest searchRequest)
        {
            if (string.IsNullOrEmpty(searchRequest.SearchValue))
            {
                AddCriteria(e => true); // Default search expression
            }
            if (!string.IsNullOrEmpty(searchRequest.SortOrderColumn))
            {
                if (searchRequest.SortOrder == Shared.OrderBy.Ascending)
                {
                    AddOrderBy(GetOrderByExpression(searchRequest.SortOrderColumn));
                }
                else if (searchRequest.SortOrder == Shared.OrderBy.Descending)
                {
                    AddOrderByDescending(GetOrderByExpression(searchRequest.SortOrderColumn));
                }
            }
            else
            {
                AddOrderBy(e => true); // Default order by expression
            }
            PageSize = searchRequest.PageSize;
            // Set Skip and Take values based on the caller's input
            ApplyPaging(searchRequest.Skip, searchRequest.PageSize);
        }

        /// <summary>
        /// The size of the page (number of records to return).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Builds a dynamic order by expression for the specified column name.
        /// </summary>
        /// <param name="columnName">The name of the property to order by.</param>
        /// <returns>An expression for ordering entities by the specified property.</returns>
        private static Expression<Func<TEntity, object>> GetOrderByExpression(string columnName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, columnName);
            var conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(conversion, parameter);
        }
    }
}