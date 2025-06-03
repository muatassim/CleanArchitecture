using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Shared.Mappers;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Mappers
{
    public class CategoriesMapper() : BaseMapper<CategoriesModel, Categories, int>
    {
        public override CategoriesModel Get(Categories entity)
        {
            var domain = new CategoriesModel
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
                Description = entity.Description 
            }; 
            return domain;
        }
        public override Result<Categories> Get(CategoriesModel domain)
        {
            List<Error> errors = [];
            var builder = new CategoriesBuilder()
                .SetId(domain.Id)
                .SetCategoryName(domain.CategoryName)
                .SetDescription(domain.Description);
            Result<Categories> result = Categories.Create(builder);
            if (result.IsFailure)
            {
                errors.AddRange(result.Errors);
                return Result.Failure<Categories>(errors);
            }
            var entity = result.Value;
             
            if (errors.Count > 0)
            {
              return Result.Failure<Categories>(errors);
            }
            return Result.Success(entity);
        } 
    }
}
