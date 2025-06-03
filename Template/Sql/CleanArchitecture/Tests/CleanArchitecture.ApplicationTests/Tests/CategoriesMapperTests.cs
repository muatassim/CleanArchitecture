using CleanArchitecture.Shared.Interfaces;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.ApplicationTests.Data;
namespace CleanArchitecture.ApplicationTests.Tests
{
    [TestClass]
    public class CategoriesMapperTests
    {
        private IMapper<CategoriesModel, Categories, int> mapper;
        [TestInitialize]
        public void TestInitialize()
        {
            mapper = (IMapper<CategoriesModel, Categories, int>)
                KernelMapper.ServiceProvider.GetService(typeof(IMapper<CategoriesModel, Categories, int>));
        }
        [TestMethod]
        public void Should_Map_CategoriesModel_To_Categories()
        {
            // Arrange
            var categoriesModel = new CategoriesModel
            {
                Id = DataHelper.RandomNumber(10,500),
                CategoryName = DataHelper.RandomString(15,false),
                Description = DataHelper.RandomString(8,false)
            };
            // Act
            Core.Abstractions.Result<Categories> categoriesResult = mapper.Get(categoriesModel);
            var categories = categoriesResult.Value;
            // Assert
            Assert.AreEqual(categoriesModel.Id, categories.Id);
            Assert.AreEqual(categoriesModel.CategoryName, categories.CategoryName);
            Assert.AreEqual(categoriesModel.Description, categories.Description);
        }
        [TestMethod]
        public void Should_Map_Categories_To_CategoriesModel()
        {
            // Arrange
            var categoriesBuilder = new CategoriesBuilder()
                .SetId(DataHelper.RandomNumber(10,500))
                .SetCategoryName(DataHelper.RandomString(15,false))
                .SetDescription(DataHelper.RandomString(8,false));
            var result = Categories.Create(categoriesBuilder);
            var categories = result.Value;
            // Act
            var categoriesModel = mapper.Get(categories);
            // Assert
            Assert.AreEqual(categoriesModel.Id, categories.Id);
            Assert.AreEqual(categoriesModel.CategoryName, categories.CategoryName);
            Assert.AreEqual(categoriesModel.Description, categories.Description);
        }
    }
}
