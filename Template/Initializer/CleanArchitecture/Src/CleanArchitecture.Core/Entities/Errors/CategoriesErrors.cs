using CleanArchitecture.Core.Abstractions; 
namespace CleanArchitecture.Core.Entities.Errors
{
    public static class CategoriesErrors
    {
        public static readonly Error NotFound = new(
            "Categories.NotFound",
            "The Categories with the specified identifier was not found");
        public static readonly Error Saved = new(
           "Categories.Saved",
           "The Categories with the specified identifier was Saved");
        public static readonly Error NotSaved = new(
        "Categories.NotSaved",
        "The Categories with the specified identifier was not Saved");
        public static readonly Error Created = new(
           "Categories.Created",
           "The Categories with the specified identifier was created");
    }
}
