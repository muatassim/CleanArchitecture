using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Entities.Errors;
namespace CleanArchitecture.Application.Features.Queries.Categories.Get
{
    public class GetCategoriesSearchRequestHandler(CategoriesMapper mapper, ICategoriesDataRepository categoriesRepository, ICategoriesSearchSpecificationFactory categoriesSearchSpecificationFactory ) : IQueryHandler<GetCategoriesSearchRequest, Page<CategoriesModel>>
    {
        public async Task<Result<Page<CategoriesModel>>> Handle(GetCategoriesSearchRequest request, CancellationToken cancellationToken)
        {  
            ISearchSpecification<Core.Entities.Categories,int> categoriesSearchSpecification = categoriesSearchSpecificationFactory.Create(request.SearchParameters);
            Page<Core.Entities.Categories> results = await categoriesRepository.GetAsync(
                categoriesSearchSpecification);
            if (results == null)
            {
                return Result.Failure<Page<CategoriesModel>>(CategoriesErrors.NotFound);
            }
            var mappedResults = mapper.Get(results);
            if (mappedResults == null) return Result.Success(new Page<CategoriesModel>());
            return Result.Success(mappedResults);
        }
    }
}
