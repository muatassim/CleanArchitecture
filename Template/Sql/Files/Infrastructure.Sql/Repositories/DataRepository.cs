using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Infrastructure.MicrosoftSql.Extensions;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories
{
    /// <summary>
    /// Abstract repository class for data operations.
    /// </summary> 
    /// <typeparam name="TEntityId">Entity ID type.</typeparam>
    public abstract class DataRepository<TEntity, TEntityId> :
        IDataRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId>, IAggregateRoot
    {
        protected readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger _logger;
        public DataRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ILogger logger)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _context = _contextFactory.CreateDbContext();
            UnitOfWork = _context;
            _logger = logger;
        }
        /// <summary>
        /// Database context.
        /// </summary>
        protected readonly ApplicationDbContext _context;
        /// <summary>
        /// Unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// Adds an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.DetachLocal<TEntity, TEntityId>(entity, entity.Id);
            return (await _context.AddAsync(entity)).Entity;
        }
        /// <summary>
        /// Count 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }  
        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        public virtual async Task<Page<TEntity>> GetAsync(ISearchSpecification<TEntity, TEntityId> specification)
        {
            List<TEntity> results;
            int totalRecords;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var query = SpecificationQueryBuilder.GetQuery(context.Set<TEntity>().AsNoTracking(), specification);
                // Debugging: Log the query
                _logger.LogInformation("{q}",query.ToQueryString());
                // Apply pagination
                totalRecords = await query.CountAsync();
                results = await query.Skip(specification.Skip).Take(specification.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // Debugging: Log the exception
                _logger.LogError("Error: {ex.Message}",ex.Message);
                throw;
            }
            // Calculate PageIndex based on Skip and PageSize
            int pageIndex = specification.Skip == 0 ? 1 : (specification.Skip / specification.PageSize) + 1;
            return new Page<TEntity>
            {
                Results = results,
                CurrentPage = pageIndex,
                PageSize = specification.PageSize,
                TotalRecords = totalRecords,
                PageCount = (int)Math.Ceiling((double)totalRecords / specification.PageSize)
            };
        }
        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>True if the entity was deleted.</returns>
        public virtual bool Delete(TEntity entity)
        {
            _context.DetachLocal<TEntity, TEntityId>(entity, entity.Id);
            _context.Remove(entity);
            return true;
        } 
        public virtual async Task<TEntity> FindAsync(Specification<TEntity, TEntityId> specification)
        {
            return await SpecificationQueryBuilder.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).SingleOrDefaultAsync();
        }
        public async Task<TEntity> FindAsync(TEntityId id)
        {
            var specification = new EntityByIdSpecification<TEntity, TEntityId>(id);
            return await SpecificationQueryBuilder.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Specification<TEntity, TEntityId> specification)
        {
            return await SpecificationQueryBuilder.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).ToListAsync();
        }
        /// <summary>
        /// Takes a specified number of entities asynchronously.
        /// </summary>
        /// <param name="numberOfRecords">The number of records to take.</param>
        /// <returns>A collection of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> TakeAsync(int numberOfRecords)
        {
            return await _context.Set<TEntity>().AsNoTracking().OrderBy(e => e.Id).Take(numberOfRecords).ToListAsync();
        }
        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>True if the entity was updated.</returns>
        public virtual TEntity Update(TEntity entity)
        {
            _context.DetachLocal<TEntity, TEntityId>(entity, entity.Id);
            return _context.Update(entity).Entity;
        }
        public async Task<bool> DeleteAsync(TEntityId id)
        {
            var entity = await FindAsync(id);
            if (entity != null)
            {
                return Delete(entity);
            }
            return false;
        }
    }
    /// <summary>
    /// Abstract repository class for data operations.
    /// </summary> 
    /// <typeparam name="TEntityId">Entity ID type.</typeparam>
    public abstract class DataRepository<TEntity> :
        IDataRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger _logger;
        public DataRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ILogger logger)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _context = _contextFactory.CreateDbContext();
            UnitOfWork = _context;
            _logger = logger;
        }
        public abstract bool Delete(TEntity entity);
        public abstract Task<TEntity> AddAsync(TEntity entity);
        public abstract Task<TEntity> FindAsync(Specification<TEntity> specification);
        public abstract TEntity Update(TEntity entity);
        public abstract Task<IEnumerable<TEntity>> TakeAsync(int numberOfRecords);
        /// <summary>
        /// Database context.
        /// </summary>
        protected readonly ApplicationDbContext _context;
        /// <summary>
        /// Unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// Count 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        public virtual async Task<Page<TEntity>> GetAsync(ISearchSpecification<TEntity> specification)
        {
            List<TEntity> results;
            int totalRecords;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var query = SpecificationQueryBuilder.GetQuery(context.Set<TEntity>().AsNoTracking(), specification);
                // Debugging: Log the query
                _logger.LogInformation("{q}", query.ToQueryString());
                // Apply pagination
                totalRecords = await query.CountAsync();
                results = await query.Skip(specification.Skip).Take(specification.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // Debugging: Log the exception
                _logger.LogError("Error: {ex.Message}", ex.Message);
                throw;
            }
            // Calculate PageIndex based on Skip and PageSize
            int pageIndex = specification.Skip == 0 ? 1 : (specification.Skip / specification.PageSize) + 1;
            return new Page<TEntity>
            {
                Results = results,
                CurrentPage = pageIndex,
                PageSize = specification.PageSize,
                TotalRecords = totalRecords,
                PageCount = (int)Math.Ceiling((double)totalRecords / specification.PageSize)
            };
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Specification<TEntity> specification)
        {
            return await SpecificationQueryBuilder.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).ToListAsync();
        }
    }
}
