using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Provides validation logic for entities with a specific identifier type.
    /// Implements IValidatable for entities with an ID.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    public class ValidationService<T, TEntityId> : IValidatable<T, TEntityId> where T : Entity<TEntityId>
    {
        /// <summary>
        /// Validates the given entity by delegating to its IsValid method.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A tuple indicating if the entity is valid and a list of validation errors.</returns>
        public virtual (bool IsValid, List<ValidationError> Errors) IsValid(T entity)
        {
            return entity.IsValid();
        }
    }

    /// <summary>
    /// Provides validation logic for entities without a specific identifier type.
    /// Implements IValidatable for entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class ValidationService<T> : IValidatable<T> where T : Entity
    {
        /// <summary>
        /// Validates the given entity by delegating to its IsValid method.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A tuple indicating if the entity is valid and a list of validation errors.</returns>
        public virtual (bool IsValid, List<ValidationError> Errors) IsValid(T entity)
        {
            return entity.IsValid();
        }
    }
}