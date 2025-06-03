using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.Core.Interfaces
{
    public interface ICategoriesDataRepository : IDataRepository<Categories, int>
    {
        Task<Categories> GetCompleteAsync(CategoriesCompleteSpecification specification);
    }
}
