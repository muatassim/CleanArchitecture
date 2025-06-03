using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Commands.Categories.Add
{
    public class AddCategoriesCommandRequestHandler(ICategoriesDataRepository repository, CategoriesMapper categoriesMapper) 
        : ICommandHandler<AddCategoriesCommandRequest, CategoriesModel>
    {
        public async Task<Result<CategoriesModel>> Handle(
            AddCategoriesCommandRequest request, CancellationToken cancellationToken)
        {
            Result<Core.Entities.Categories> categoriesResult = categoriesMapper.Get(request.Categories);
            if (categoriesResult.IsFailure)
            {
                return Result.Failure<CategoriesModel>(categoriesResult.Errors);
            }
            Core.Entities.Categories newCategories =
                await repository.AddAsync(categoriesResult.Value);
            //Save the Changes
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(categoriesMapper.Get(newCategories));
        }
    }
}
