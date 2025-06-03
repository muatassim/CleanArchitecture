using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Shared;
namespace CleanArchitecture.Core.Interfaces
{
    public interface ICategoriesSearchSpecificationFactory
    { 
        ISearchSpecification<Categories, int> Create(SearchRequest searchParameters);
    }
}
