using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications
{
    public class CategoriesSearchSpecificationFactory : ICategoriesSearchSpecificationFactory
    {
        public ISearchSpecification<Categories, int> Create(SearchRequest searchParameters)
        {
            return new CategoriesSearchSpecification(searchParameters);
        }
    }
}
