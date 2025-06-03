using System.Linq.Expressions;
namespace CleanArchitecture.Core.Abstractions.Specifications
{
    /// <summary>
    /// Abstract base class for encapsulating query logic (the Specification pattern) for entities.
    /// Allows you to define filtering (criteria), eager loading (includes), sorting, and paging in a reusable, composable way.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    /// <remarks>
    /// This class is typically used with repositories to build dynamic queries based on business rules or API input.
    /// Derived classes can use the protected methods to add criteria, includes, ordering, and paging.
    /// </remarks>
    /// <example>
    /// // Example: Specification for active users, ordered by last name, with paging
    /// public class ActiveUsersSpecification : Specification&lt;User, Guid&gt;
    /// {
    ///     public ActiveUsersSpecification()
    ///         : base(user =&gt; user.IsActive)
    ///     {
    ///         AddOrderBy(user =&gt; user.LastName);
    ///         ApplyPaging(0, 20);
    ///     }
    /// }
    /// </example>
    public abstract class Specification<TEntity, TEntityId>
       where TEntity : Entity<TEntityId>
    {
        protected Specification() { }
        protected Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        /// <summary>
        /// The main filter condition for the specification.
        /// </summary>
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        /// <summary>
        /// List of related entities to include (for eager loading).
        /// </summary>
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        /// <summary>
        /// Dictionary for nested includes (ThenInclude in EF).
        /// </summary>
        public Dictionary<Expression<Func<TEntity, object>>, List<Expression<Func<object, object>>>> ThenIncludes { get; } = [];
        /// <summary>
        /// Expression for ordering results in ascending order.
        /// </summary>
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        /// <summary>
        /// Expression for ordering results in descending order.
        /// </summary>
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        /// <summary>
        /// Number of records to take (for paging).
        /// </summary>
        public int Take { get; private set; }
        /// <summary>
        /// Number of records to skip (for paging).
        /// </summary>
        public int Skip { get; private set; }
        /// <summary>
        /// Indicates if paging is enabled.
        /// </summary>
        public bool IsPagingEnabled { get; private set; }
        /// <summary>
        /// Adds an include expression for eager loading.
        /// </summary>
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            ThenIncludes[includeExpression] = [];
        }
        /// <summary>
        /// Adds a nested include expression for eager loading.
        /// </summary>
        protected void AddThenInclude(Expression<Func<TEntity, object>> includeExpression, Expression<Func<object, object>> thenIncludeExpression)
        {
            if (ThenIncludes.TryGetValue(includeExpression, out var value))
            {
                value.Add(thenIncludeExpression);
            }
            else
            {
                ThenIncludes[includeExpression] = [thenIncludeExpression];
            }
        }
        /// <summary>
        /// Sets the ascending order by expression.
        /// </summary>
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        /// <summary>
        /// Sets the descending order by expression.
        /// </summary>
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        /// <summary>
        /// Enables paging with the specified skip and take values.
        /// </summary>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        /// <summary>
        /// Sets the main filter criteria.
        /// </summary>
        protected void AddCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
    }

    /// <summary>
    /// Abstract base class for encapsulating query logic for entities without a generic ID.
    /// Provides the same functionality as <see cref="Specification{TEntity, TEntityId}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class Specification<TEntity>
     where TEntity : Entity
    {
        protected Specification() { }
        protected Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        public Dictionary<Expression<Func<TEntity, object>>, List<Expression<Func<object, object>>>> ThenIncludes { get; } = [];
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            ThenIncludes[includeExpression] = [];
        }
        protected void AddThenInclude(Expression<Func<TEntity, object>> includeExpression, Expression<Func<object, object>> thenIncludeExpression)
        {
            if (ThenIncludes.TryGetValue(includeExpression, out var value))
            {
                value.Add(thenIncludeExpression);
            }
            else
            {
                ThenIncludes[includeExpression] = [thenIncludeExpression];
            }
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        protected void AddCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}