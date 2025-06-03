using CleanArchitecture.Application.Features.Commands.Categories.Add;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Validations;
namespace CleanArchitecture.Application.Features.Validators.Categories
{
    public class AddCategoriesCommandRequestValidator(CategoriesMapper mapper)  : IValidationHandler<AddCategoriesCommandRequest>
    {
        public async Task<ValidationResult> Validate(AddCategoriesCommandRequest request)
        {
            List<ValidationError> errors = [];
            //TODO: ADD CUSTOM VALIDATION USING with ApplicationValidator 
            ApplicationValidators applicationValidator = new(); 
            if (applicationValidator.IsEmpty(request.Categories.CategoryName))
            {
                errors.Add(new ValidationError(propertyName: nameof(CategoriesModel.CategoryName).CamelCase(), "CategoryName is required"));
            }
            Result<Core.Entities.Categories> result = mapper.Get(request.Categories);
            if (result.IsFailure)
            {
                foreach (var error in result.Errors)
                {
                    errors.Add(new ValidationError(error.Name,error.Message));
                }
            }
            if (errors.Count > 0)
            {
                return await Task.FromResult(ValidationResult.Fail(errors));
            }
            return await Task.FromResult(ValidationResult.Success);
        }
    }
}
