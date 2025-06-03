using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TEntityId>(IQueryable<TEntity> inputQuery, Specification<TEntity, TEntityId> spec) where TEntity : Entity<TEntityId>
        {
            var query = inputQuery;
            // Apply Criteria
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Apply Includes
            if (spec.Includes != null && spec.Includes.Count != 0)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            // Apply ThenIncludes
            if (spec.ThenIncludes != null && spec.ThenIncludes.Count != 0)
            {
                foreach (var include in spec.ThenIncludes)
                {
                    var parentInclude = include.Key;
                    var childIncludes = include.Value;
                    // Apply each ThenInclude for the parent include
                    foreach (var childInclude in childIncludes)
                    {
                        query = query.Include(parentInclude).ThenInclude(childInclude);
                    }
                }
            }
            // Apply OrderBy
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Apply Paging
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
        public static IQueryable<TEntity> GetQuery<TEntity, TEntityId>(IQueryable<TEntity> inputQuery, ISearchSpecification<TEntity, TEntityId> spec) where TEntity : Entity<TEntityId>
        {
            var query = inputQuery;
            // Apply Criteria
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Apply Includes
            if (spec.Includes != null && spec.Includes.Count != 0)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            // Apply OrderBy
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Apply Paging
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISearchSpecification<TEntity> spec) where TEntity : Entity
        {
            var query = inputQuery;
            // Apply Criteria
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Apply Includes
            if (spec.Includes != null && spec.Includes.Count != 0)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            // Apply OrderBy
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Apply Paging
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, Specification<TEntity> spec) where TEntity : Entity
        {
            var query = inputQuery;
            // Apply Criteria
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Apply Includes
            if (spec.Includes != null && spec.Includes.Count != 0)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            // Apply ThenIncludes
            if (spec.ThenIncludes != null && spec.ThenIncludes.Count != 0)
            {
                foreach (var include in spec.ThenIncludes)
                {
                    var parentInclude = include.Key;
                    var childIncludes = include.Value;
                    // Apply each ThenInclude for the parent include
                    foreach (var childInclude in childIncludes)
                    {
                        query = query.Include(parentInclude).ThenInclude(childInclude);
                    }
                }
            }
            // Apply OrderBy
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Apply Paging
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
    }
}
