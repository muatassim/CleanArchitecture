using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for validating entities with a specific identifier type.
    /// Implementations should return whether the entity is valid and a list of validation errors.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to validate.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    public interface IValidatable<in TEntity, TEntityId> where TEntity : Entity<TEntityId>
    {
        /// <summary>
        /// Validates the given entity and returns a tuple indicating validity and a list of validation errors.
        /// </summary>
        (bool IsValid, List<ValidationError> Errors) IsValid(TEntity entity);
    }

    /// <summary>
    /// Defines a contract for validating entities without specifying an identifier type.
    /// Implementations should return whether the entity is valid and a list of validation errors.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to validate.</typeparam>
    public interface IValidatable<in TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Validates the given entity and returns a tuple indicating validity and a list of validation errors.
        /// </summary>
        (bool IsValid, List<ValidationError> Errors) IsValid(TEntity entity);
    }
}