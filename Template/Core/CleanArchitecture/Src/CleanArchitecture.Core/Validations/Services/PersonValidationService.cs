using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.Core.Validations.Services
{
    /// <summary>
    /// How to override for each class not necessary
    /// </summary>
    public class PersonValidationService : ValidationService<Person, int>
    {
        public override (bool IsValid, List<ValidationError> Errors) IsValid(Person? entity)
        {
            return entity == null ? 
                (false, [new ValidationError("Entity", "Entity is null")]) 
                : base.IsValid(entity);
        }
    }
}
