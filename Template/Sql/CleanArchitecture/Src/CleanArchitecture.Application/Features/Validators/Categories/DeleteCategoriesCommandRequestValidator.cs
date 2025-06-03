using CleanArchitecture.Application.Features.Commands.Categories.Delete;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CleanArchitecture.Application.Features.Validators.Categories
{
    public class DeleteCategoriesCommandRequestValidator : IValidationHandler<DeleteCategoriesCommandRequest>
    {
        public async Task<ValidationResult> Validate(DeleteCategoriesCommandRequest request)
        {
            List<ValidationError> errors = [];
            if (request.Id == 0)
            {
                errors.Add(new ValidationError(propertyName: nameof(CategoriesModel).CamelCase(), "Categories is required"));
                return await Task.FromResult(ValidationResult.Fail(errors));
            }
            if (errors.Count > 0)
            {
                return await Task.FromResult(ValidationResult.Fail(errors));
            }
            return await Task.FromResult(ValidationResult.Success);
        }
    }
}
