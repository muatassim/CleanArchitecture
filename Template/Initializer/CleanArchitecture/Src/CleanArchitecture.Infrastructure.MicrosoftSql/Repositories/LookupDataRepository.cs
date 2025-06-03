using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories
{
    public class LookupDataRepository(ApplicationDbContext context) : ILookupDataRepository
    {
        private readonly ApplicationDbContext _context = context;
       
        public async Task<IEnumerable<LookUp>> GetDboCategoriesAsync()
        {
            try
            {
                var query = await (from Categories in _context.Set<Categories>()
                                   select new LookUp
                                   {
                                       Code = Categories.Id.ToString(),
                                       Description = Categories.CategoryName
                                   }).ToListAsync();
                return query;
            }
            catch
            {
                // ignored
            }
            return [];
        }
        
    }
}
