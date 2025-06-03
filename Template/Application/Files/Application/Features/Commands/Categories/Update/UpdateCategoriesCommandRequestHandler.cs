using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Core.Entities.Errors;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.Application.Features.Commands.Categories.Update
{
    public class UpdateCategoriesCommandRequestHandler(ICategoriesDataRepository repository, CategoriesMapper categoriesMapper) : ICommandHandler<UpdateCategoriesCommandRequest, bool>
    {
        public async Task<Result<bool>> Handle(UpdateCategoriesCommandRequest request, CancellationToken cancellationToken)
        {
            CategoriesCompleteSpecification specification = new(request.Categories.Id);
            var existingRecord = await repository.GetCompleteAsync(specification);
            Result<Core.Entities.Categories> categoriesResult = categoriesMapper.Get(request.Categories);
            if (categoriesResult.IsFailure)
            {
                return Result.Failure<bool>(categoriesResult.Errors);
            }
            var categories = categoriesResult.Value;
            //Create CategoriesBuilder and update the Categories with new Values
            var categoriesBuilder = new CategoriesBuilder().
                SetCategoryName(categories.CategoryName).
                SetDescription(categories.Description);
            //Update the existing record with the new record  
            Result<Core.Entities.Categories> updatingRecordResult = existingRecord.Update(categoriesBuilder);
            if (updatingRecordResult.IsFailure)
            {
                return Result.Failure<bool>(updatingRecordResult.Errors);
            }
            //Update Record in Database
            Core.Entities.Categories updatedCategories = repository.Update(updatingRecordResult.Value);
            if (updatedCategories == null)
            {
                return Result.Failure<bool>(CategoriesErrors.NotSaved);
            }
            //Looks good , save the changes
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(updatedCategories != null);
        }
    }
}
