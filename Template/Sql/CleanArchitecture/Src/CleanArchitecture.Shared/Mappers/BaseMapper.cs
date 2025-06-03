using System.Linq.Expressions;
using System.Text.Json;
using System.Collections.Generic; 
using CleanArchitecture.Shared.Interfaces;
using CleanArchitecture.Shared.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Core.Exceptions;
namespace CleanArchitecture.Shared.Mappers
{
    public abstract class BaseMapper<TDomainModel, TEntity> : IMapper<TDomainModel, TEntity>
                   where TDomainModel : BaseModel
                   where TEntity : Entity
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString |
                             System.Text.Json.Serialization.JsonNumberHandling.WriteAsString
        };
        /// <summary>
        /// Converts a JsonDocument to a domain model.
        /// </summary>
        /// <param name="jsonDocument">The JsonDocument to convert.</param>
        /// <returns>The domain model.</returns>
        public virtual TDomainModel? Get(JsonDocument? jsonDocument)
        {
            if (jsonDocument == null) return null;
            try
            {
                var model = jsonDocument.Deserialize<TDomainModel>(_options);
                return model;
            }
            catch (JsonException ex)
            {
                throw new Core.Exceptions.DomainException("Json Deserialize Exception", ex.Message);
            }
        }
        /// <summary>
        /// Converts an entity to a domain model.
        /// </summary>
        /// <param name="entity">The entity to convert.</param>
        /// <returns>The domain model.</returns>
        public abstract TDomainModel Get(TEntity entity);
        /// <summary>
        /// Converts a domain model to an entity.
        /// </summary>
        /// <param name="domain">The domain model to convert.</param>
        /// <returns>The entity.</returns>
        public abstract Result<TEntity> Get(TDomainModel domain);
        /// <summary>
        /// Converts a list of entities to a list of domain models.
        /// </summary>
        /// <param name="entityList">The list of entities to convert.</param>
        /// <returns>The list of domain models.</returns>
        public virtual IEnumerable<TDomainModel> Get(IEnumerable<TEntity>? entityList)
        {
            var inList = new List<TDomainModel>();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    inList.Add(Get(entity));
                }
            }
            return inList;
        }
        /// <summary>
        /// Converts a list of domain models to a list of entities.
        /// </summary>
        /// <param name="domainList">The list of domain models to convert.</param>
        /// <returns>The list of entities.</returns>
        public virtual IEnumerable<TEntity> Get(IEnumerable<TDomainModel>? domainList)
        {
            var inList = new List<TEntity>();
            if (domainList != null)
            {
                foreach (var entity in domainList)
                {
                    var result = Get(entity);
                    if (result.IsSuccess)
                    {
                        inList.Add(result.Value);
                    }
                }
            }
            return inList;
        }
        /// <summary>
        /// Converts a domain model to a JsonDocument.
        /// </summary>
        /// <param name="model">The domain model to convert.</param>
        /// <returns>The JsonDocument.</returns>
        public virtual JsonDocument? GetJson(TDomainModel? model)
        {
            if (model == null) return null;
            try
            {
                var jsonDocument = JsonSerializer.Serialize(model, _options);
                return JsonDocument.Parse(jsonDocument);
            }
            catch (JsonException ex)
            {
                throw new DomainException("Json Serializer Exception", ex.Message);
            }
        }
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Page<TEntity>? Get(Page<TDomainModel>? page)
        {
            if (page == null) return null;
            Page<TEntity> results = new()
            {
                CurrentPage = page.CurrentPage,
                PageCount = page.PageCount,
                PageSize = page.PageSize,
                TotalRecords = page.TotalRecords,
                Results = Get(page.Results)
            };
            return results;
        }
        public Expression<Func<TEntity, bool>> MapPredicate(Expression<Func<TDomainModel, bool>> predicate)
        {
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
                var rewriter = new ExpressionRewriter<TDomainModel, TEntity>();
                return rewriter.Rewrite(predicate);
                // Compile the predicate for the domain model, has a performance Hit  
                // var compiledPredicate = predicate.Compile(); 
                // Return a new expression for the entity model that uses the compiled predicate 
                // return entity => compiledPredicate(MapToDomain(entity)); 
            }
        }
        public Page<TDomainModel>? Get(Page<TEntity>? page)
        {
            if (page == null) return null;
            Page<TDomainModel> results = new()
            {
                CurrentPage = page.CurrentPage,
                PageCount = page.PageCount,
                PageSize = page.PageSize,
                TotalRecords = page.TotalRecords,
                Results = Get(page.Results)
            };
            return results;
        }
        /// <summary> 
        /// Get Entity 
        /// </summary>  
        /// <param name="domain"></param>
        /// <returns></returns> 
        public virtual Result<TEntity> MapToEntity(TDomainModel domain) => Get(domain);
        /// <summary>
        /// Get Domain
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<TDomainModel> MapToDomain(TEntity entity) => Get(entity);
    }
    public abstract class BaseMapper<TDomainModel, TEntity, TEntityId> : IMapper<TDomainModel, TEntity, TEntityId>
                   where TDomainModel : BaseModel
                   where TEntity : Entity<TEntityId>
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString |
                             System.Text.Json.Serialization.JsonNumberHandling.WriteAsString
        };
        /// <summary>
        /// Converts a JsonDocument to a domain model.
        /// </summary>
        /// <param name="jsonDocument">The JsonDocument to convert.</param>
        /// <returns>The domain model.</returns>
        public virtual TDomainModel? Get(JsonDocument? jsonDocument)
        {
            if (jsonDocument == null) return null;
            try
            {
                var model = jsonDocument.Deserialize<TDomainModel>(_options);
                return model;
            }
            catch (JsonException ex)
            {
                throw new Core.Exceptions.DomainException("Json Deserialize Exception", ex.Message);
            }
        }
        /// <summary>
        /// Converts an entity to a domain model.
        /// </summary>
        /// <param name="entity">The entity to convert.</param>
        /// <returns>The domain model.</returns>
        public abstract TDomainModel Get(TEntity entity);
        /// <summary>
        /// Converts a domain model to an entity.
        /// </summary>
        /// <param name="domain">The domain model to convert.</param>
        /// <returns>The entity.</returns>
        public abstract Result<TEntity> Get(TDomainModel domain);
        /// <summary>
        /// Converts a list of entities to a list of domain models.
        /// </summary>
        /// <param name="entityList">The list of entities to convert.</param>
        /// <returns>The list of domain models.</returns>
        public virtual IEnumerable<TDomainModel> Get(IEnumerable<TEntity>? entityList)
        {
            var inList = new List<TDomainModel>();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    inList.Add(Get(entity));
                }
            }
            return inList;
        }
        /// <summary>
        /// Converts a list of domain models to a list of entities.
        /// </summary>
        /// <param name="domainList">The list of domain models to convert.</param>
        /// <returns>The list of entities.</returns>
        public virtual IEnumerable<TEntity> Get(IEnumerable<TDomainModel>? domainList)
        {
            var inList = new List<TEntity>();
            if (domainList != null)
            {
                foreach (var entity in domainList)
                {
                    var result = Get(entity);
                    if (result.IsSuccess)
                    {
                        inList.Add(result.Value);
                    }
                }
            }
            return inList;
        }
        /// <summary>
        /// Converts a domain model to a JsonDocument.
        /// </summary>
        /// <param name="model">The domain model to convert.</param>
        /// <returns>The JsonDocument.</returns>
        public virtual JsonDocument? GetJson(TDomainModel? model)
        {
            if (model == null) return null;
            try
            {
                var jsonDocument = JsonSerializer.Serialize(model, _options);
                return JsonDocument.Parse(jsonDocument);
            }
            catch (JsonException ex)
            {
                throw new DomainException("Json Serializer Exception", ex.Message);
            }
        }
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Page<TEntity>? Get(Page<TDomainModel>? page)
        {
            if (page == null) return null;
            Page<TEntity> results = new()
            {
                CurrentPage = page.CurrentPage,
                PageCount = page.PageCount,
                PageSize = page.PageSize,
                TotalRecords = page.TotalRecords,
                Results = Get(page.Results)
            };
            return results;
        }
        public Page<TDomainModel>? Get(Page<TEntity>? page)
        {
            if (page == null) return null;
            Page<TDomainModel> results = new()
            {
                CurrentPage = page.CurrentPage,
                PageCount = page.PageCount,
                PageSize = page.PageSize,
                TotalRecords = page.TotalRecords,
                Results = Get(page.Results)
            };
            return results;
        }
        public Expression<Func<TEntity, bool>> MapPredicate(Expression<Func<TDomainModel, bool>> predicate)
        {
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null."); 
                var rewriter = new ExpressionRewriter<TDomainModel, TEntity>();
                return rewriter.Rewrite(predicate);
                // Compile the predicate for the domain model, has a performance Hit  
                // var compiledPredicate = predicate.Compile(); 
                // Return a new expression for the entity model that uses the compiled predicate 
                // return entity => compiledPredicate(MapToDomain(entity)); 
            }
        }
        /// <summary> 
        /// Get Entity 
        /// </summary>  
        /// <param name="domain"></param>
        /// <returns></returns> 
        public virtual Result<TEntity> MapToEntity(TDomainModel domain) => Get(domain);
        /// <summary>
        /// Get Domain
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<TDomainModel> MapToDomain(TEntity entity) => Get(entity);
    }
}
