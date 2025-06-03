using System.Text.Json;
using System.Linq.Expressions; 
using CleanArchitecture.Shared.Models;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Core.Abstractions;
namespace CleanArchitecture.Shared.Interfaces
{
    /// <summary>
    /// Mapper 
    /// </summary>
    /// <typeparam name="TDomainModel">Domain Object </typeparam>
    /// <typeparam name="TEntity">Entity Object </typeparam>
    /// <typeparam name="TEntityId">Entity Identifier Type</typeparam>
    public interface IMapper<TDomainModel, TEntity, TEntityId>
        where TDomainModel : BaseModel
        where TEntity : Entity<TEntityId>
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        TDomainModel? Get(JsonDocument? jObject);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDomainModel Get(TEntity entity);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Result<TEntity> Get(TDomainModel domain);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IEnumerable<TDomainModel> Get(IEnumerable<TEntity> entityList);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="domainList"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(IEnumerable<TDomainModel> domainList);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonDocument? GetJson(TDomainModel? model);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Page<TDomainModel>? Get(Page<TEntity>? page);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Page<TEntity>? Get(Page<TDomainModel>? page);
        /// <summary>
        /// Maps a predicate from the domain model to the entity model.
        /// </summary>
        /// <param name="predicate">The predicate for the domain model.</param>
        /// <returns>The predicate for the entity model.</returns>
        Expression<Func<TEntity, bool>> MapPredicate(Expression<Func<TDomainModel, bool>> predicate);
    }
    /// <summary>
    /// Mapper 
    /// </summary>
    /// <typeparam name="TDomainModel">Domain Object </typeparam>
    /// <typeparam name="TEntity">Entity Object </typeparam>
    public interface IMapper<TDomainModel, TEntity>
        where TDomainModel : BaseModel
        where TEntity : Entity
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        TDomainModel? Get(JsonDocument? jObject);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDomainModel Get(TEntity entity);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Result<TEntity> Get(TDomainModel domain);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IEnumerable<TDomainModel> Get(IEnumerable<TEntity> entityList);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="domainList"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(IEnumerable<TDomainModel> domainList);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonDocument? GetJson(TDomainModel? model);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Page<TDomainModel>? Get(Page<TEntity>? page);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Page<TEntity>? Get(Page<TDomainModel>? page);
        /// <summary>
        /// Maps a predicate from the domain model to the entity model.
        /// </summary>
        /// <param name="predicate">The predicate for the domain model.</param>
        /// <returns>The predicate for the entity model.</returns>
        Expression<Func<TEntity, bool>> MapPredicate(Expression<Func<TDomainModel, bool>> predicate);
    }
}
