using System.Text.Json;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Core.Shared;
namespace CleanArchitecture.ApplicationTests.Data
{
    public static class EntityDataHelper
    {
        private static readonly DateTimeProvider DateTimeProvider;
        static readonly JsonSerializerOptions JsonSerializerOptions;
        static EntityDataHelper()
        {
            DateTimeProvider = new DateTimeProvider();
            JsonSerializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
       
        public static Categories GetCategories(Int32? id)
        {
            var categoriesBuilder = new CategoriesBuilder()

                    .SetCategoryName(DataHelper.RandomString(15))
                    .SetDescription(DataHelper.RandomString(8));
            if (id != null) categoriesBuilder.SetId(id.Value);
            var result = Categories.Create(categoriesBuilder); ;
            return result.Value;
        }
        public static Page<Categories> GetCategoriesPage(int recordCount)
        {
            var list = new List<Categories>();
            for (int i = 0; i < recordCount; i++)
            {
                list.Add(GetCategories(i+ 1));
            }
            var page = new Page<Categories>
            {
                Results = list,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = recordCount,
                TotalRecords = recordCount
            };
            return page;
        }
        
    }
}
