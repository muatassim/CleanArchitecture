using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Entities.Errors;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using System.Threading.Tasks;
using System.Threading;
namespace CleanArchitecture.Application.Features.Queries.Categories.GetById
{
    public class GetCategoriesByIdQueryRequestHandler(CategoriesMapper mapper, ICategoriesDataRepository categoriesRepository) : IQueryHandler<GetCategoriesByIdQueryRequest, CategoriesModel>
    {
        public async Task<Result<CategoriesModel>> Handle(GetCategoriesByIdQueryRequest request, CancellationToken cancellationToken)
        {
            CategoriesCompleteSpecification specification = new(request.Id);
            var domainObject = await categoriesRepository.GetCompleteAsync(specification);
            if (domainObject != null)
            {
                var categoriesModel = mapper.Get(domainObject);
                return Result.Success(categoriesModel);
            }
            return Result.Failure<CategoriesModel>(error: CategoriesErrors.NotFound);
        }
    }
}
