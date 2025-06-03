using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.Core.Validations.Services
{
    /// <summary>
    /// How to override for each class not necessary
    /// </summary>
    public class CategoriesValidationService : ValidationService<Categories, int>
    {
        public override (bool IsValid, List<ValidationError> Errors) IsValid(Categories? entity)
        {
            return entity == null ? 
                (false, [new ValidationError("Entity", "Entity is null")]) 
                : base.IsValid(entity);
        }
    }
}
