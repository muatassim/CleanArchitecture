using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories
{
    public class CategoriesDataRepository(IDbContextFactory<ApplicationDbContext> context, ILogger<CategoriesDataRepository> logger) : DataRepository<Categories, int>(context, logger), ICategoriesDataRepository
    {
        /// <summary>
        /// Get Complete Record 
        /// ensuring that a fresh DbContext is used for each operation.
        /// This helps manage the context's lifetime and scope effectively.
        /// </summary>
        /// <param name="specification">specification</param>
        /// <returns></returns>
        public async Task<Categories> GetCompleteAsync(CategoriesCompleteSpecification specification)
        {      
            await using var context = await _contextFactory.CreateDbContextAsync();
            var query = SpecificationQueryBuilder.GetQuery(context.Set<Categories>().AsQueryable(), specification);
            logger.LogInformation("{q}", query.ToQueryString());
            var entity = await query
                .AsNoTracking() 
                ?.SingleOrDefaultAsync(); 
            return entity;
        }
    }
}
