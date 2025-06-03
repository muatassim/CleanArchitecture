using System.Linq.Expressions;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications
{
    public class CategoriesSearchSpecification : GenericSearchSpecification<Categories, int>, ISearchSpecification<Categories, int>
    {
        public CategoriesSearchSpecification(SearchRequest searchParameters)
            : base(searchParameters)
        {
            if (!string.IsNullOrEmpty(searchParameters.SearchValue))
            {
                string searchValue = $"%{searchParameters.SearchValue}%";
                AddCriteria(e => 
                           EF.Functions.Like(e.Id.ToString(), searchValue)||
                           EF.Functions.Like(e.CategoryName, searchValue)||
                           EF.Functions.Like(e.Description, searchValue));
            }
            if (!string.IsNullOrEmpty(searchParameters.SortOrderColumn))
            {
                if (searchParameters.SortOrder == Core.Shared.OrderBy.Ascending)
                {
                    AddOrderBy(GetOrderByExpression(searchParameters.SortOrderColumn));
                }
                else if (searchParameters.SortOrder == Core.Shared.OrderBy.Descending)
                {
                    AddOrderByDescending(GetOrderByExpression(searchParameters.SortOrderColumn));
                }
            }
            PageSize = searchParameters.PageSize;
        }
        private static Expression<Func<Categories, object>> GetOrderByExpression(string columnName)
        {
            return columnName switch
            {
                nameof(Categories.CategoryName) => e => e.CategoryName,
                nameof(Categories.Description) => e => e.Description, 
                _ => e => e.Id
            };
        }
    }
}
