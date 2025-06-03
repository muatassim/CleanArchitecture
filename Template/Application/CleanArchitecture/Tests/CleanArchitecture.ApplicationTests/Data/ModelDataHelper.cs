using CleanArchitecture.Application.Models; 
using CleanArchitecture.Core.Shared; 
namespace CleanArchitecture.ApplicationTests.Data
{
    internal static class ModelDataHelper
    {
        static readonly DateTimeProvider _dateTimeProvider;
        static ModelDataHelper() => _dateTimeProvider = new DateTimeProvider();
    
        public static CategoriesModel GetCategories(bool addRelatedColumns = true)
        {
            var domain = new CategoriesModel
            {
                Id = DataHelper.RandomNumber(10,500),
                CategoryName = DataHelper.RandomString(15,false),
                Description = DataHelper.RandomString(8,false) 
            }; 
            return domain;
        }
        public static Page<CategoriesModel> GetCategoriesPage(int recordCount)
        {
            var list = new List<CategoriesModel>();
            for (int i = 0; i < recordCount; i++)
            {
                list.Add(GetCategories());
            }
            var page = new Page<CategoriesModel>
            {
                Results = list,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = recordCount 
            };
            return page;
        }
         
    }
}
