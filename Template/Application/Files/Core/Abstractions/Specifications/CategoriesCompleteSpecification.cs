using NorthwindTest.Core.Entities.Dbo;
namespace NorthwindTest.Core.Abstractions.Specifications
{
    public class CategoriesCompleteSpecification : EntityByIdSpecification<Categories, Int32>
    {
        public CategoriesCompleteSpecification(Int32 id) : base(id)  
        {
            AddInclude(e => e.ProductsCategoryIDList);
            AddThenInclude(e => e.ProductsCategoryIDList, o => ((Products)o).OrderDetailsProductIDList);
        }
    }
}
