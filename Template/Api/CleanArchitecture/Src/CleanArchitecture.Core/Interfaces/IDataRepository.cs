using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Generic repository interface for aggregate root entities with a specific identifier type.
    /// Provides methods for adding, updating, deleting, and querying entities using the Specification pattern.
    /// </summary>
    /// <typeparam name="TEntity">The entity type, must inherit from Entity&lt;TEntityId&gt; and implement IAggregateRoot.</typeparam>
    /// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
    public interface IDataRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId>, IAggregateRoot
    {
        /// <summary>
        /// Gets the unit of work for managing transactions.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Returns the total count of entities.
        /// </summary>
        Task<int> CountAsync();

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        bool Delete(TEntity entity);

        /// <summary>
        /// Gets a paged list of entities matching the search specification.
        /// </summary>
        Task<Page<TEntity>> GetAsync(ISearchSpecification<TEntity, TEntityId> specification);

        /// <summary>
        /// Finds all entities matching the specification.
        /// </summary>
        Task<IEnumerable<TEntity>> FindAllAsync(Specification<TEntity, TEntityId> specification);

        /// <summary>
        /// Finds a single entity matching the specification.
        /// </summary>
        Task<TEntity> FindAsync(Specification<TEntity, TEntityId> specification);

        /// <summary>
        /// Takes a specified number of entities.
        /// </summary>
        Task<IEnumerable<TEntity>> TakeAsync(int numberOfRecords);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity by its identifier asynchronously.
        /// </summary>
        Task<bool> DeleteAsync(TEntityId id);

        /// <summary>
        /// Finds an entity by its identifier asynchronously.
        /// </summary>
        Task<TEntity> FindAsync(TEntityId id);
    }
    public interface IDataRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<Page<TEntity>> GetAsync(ISearchSpecification<TEntity> specification);
        /// <summary>
        /// Find 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAllAsync(Specification<TEntity> specification);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Specification<TEntity> specification);
        /// <summary>
        /// Take Entity 
        /// </summary>
        /// <param name="numberOfRecords"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> TakeAsync(int numberOfRecords);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
    }
}