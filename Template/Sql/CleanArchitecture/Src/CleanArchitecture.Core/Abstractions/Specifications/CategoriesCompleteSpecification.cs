using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.Core.Abstractions.Specifications
{
    public class CategoriesCompleteSpecification(Int32 id) : EntityByIdSpecification<Categories, Int32>(id)
    {
    }
}
