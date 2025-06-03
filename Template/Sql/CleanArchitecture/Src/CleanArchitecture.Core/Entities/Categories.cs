using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Core.Validations;
using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.Core.Validations.Services;
using System.Text.Json.Serialization;
namespace CleanArchitecture.Core.Entities
{
    [method: JsonConstructor]
    public partial class Categories(int id, string categoryName, string description) : Entity<int>(id)
    {
        internal static Categories InternalCreate(int id, string categoryName, string description)
        {
            return new Categories(id,categoryName,description);
        }
        /// <summary>
        /// CategoryName  
        /// </summary>
        public string CategoryName {get; private set;}= categoryName;
        /// <summary>
        /// Description  
        /// </summary>
        public string Description {get; private set;}= description;  
        public Result<Categories> Update(CategoriesBuilder builder)
        {
            var validationService = new CategoriesValidationService();
            Categories updatedCategories = builder.BuildPartial(this);  // BuildPartial will merge changes
            (bool IsValid, List<ValidationError> Errors) validator = validationService.IsValid(updatedCategories);
            if (!validator.IsValid)
            {
                var errors = ValidationHelper.GetErrors(validator.Errors);
                return Result.Failure<Categories>(errors);
            }
            // Update properties 
            CategoryName = updatedCategories.CategoryName;
            Description = updatedCategories.Description; 
            return Result.Success(this);
        }
        public static Result<Categories> Create(CategoriesBuilder builder)
        {
            Categories categories = builder.Build();
            var validationService = new CategoriesValidationService();
            (bool IsValid, List<ValidationError> Errors) validator = validationService.IsValid(categories);
            if (!validator.IsValid)
            {
                var errors = ValidationHelper.GetErrors(validator.Errors);
                return Result.Failure<Categories>(errors);
            }
            return Result.Success(categories);
        }
        #region Validation Rules
        public override List<Rule> CreateRules()
        {
            var rules = base.CreateRules();
            rules.Add(new CustomRule(nameof(CategoryName), $"{nameof(CategoryName)} validation failed.", ()=> CheckCustomRule(CategoryName)));
            rules.Add(new RequiredFieldRule(nameof(CategoryName), CategoryName));
            rules.Add(new MaximumFieldLengthRule(nameof(CategoryName), CategoryName, 15));
            rules.Add(new MaximumFieldLengthRule(nameof(Description), Description, 8));
            return rules;
        }
        private static bool CheckCustomRule(string value)
        {
            //Add logic here
            return !string.IsNullOrEmpty(value);
        }
        #endregion
    }
}
