using CleanArchitecture.Application.Features.Commands.Categories.Add;
using CleanArchitecture.Application.Features.Commands.Categories.Delete;
using CleanArchitecture.Application.Features.Commands.Categories.Update;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Models;
using CleanArchitecture.ApplicationTests.Data;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Core.Interfaces;
using Moq;
namespace CleanArchitecture.ApplicationTests.Tests.Features.Commands
{
    [TestClass]
    public class CategoriesCommandTests
    {
        CategoriesMapper mapper;
        CancellationToken _CancellationToken;
        [TestInitialize]
        public void TestInitialize()
        {
            _CancellationToken = new CancellationToken();
            mapper = (CategoriesMapper)
                KernelMapper.ServiceProvider.GetService(typeof(CategoriesMapper));
        }
        [TestMethod]
        public async Task AddCategoriesRequestTestAsync()
        {
            CategoriesModel categoriesModel = ModelDataHelper.GetCategories();
            var mockRepository = new Mock<ICategoriesDataRepository>();
            var categoriesBuilder = new CategoriesBuilder()
                .SetId(categoriesModel.Id)
                .SetCategoryName(categoriesModel.CategoryName)
                .SetDescription(categoriesModel.Description)
;
            var categories = categoriesBuilder.Build();
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Categories>()))
                .ReturnsAsync(categories);
            mockRepository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(_CancellationToken))
                .ReturnsAsync(1);
            var addRequest = new AddCategoriesCommandRequest(categoriesModel);
            AddCategoriesCommandRequestHandler addRequestHandler = new(mockRepository.Object, mapper);
            var addResult = await addRequestHandler.Handle(addRequest, _CancellationToken);
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(addResult.IsSuccess);
            Assert.IsTrue(addResult.IsSuccess);
        }
        [TestMethod]
        public async Task UpdateCategoriesTestAsync()
        {
            var categoriesModel = ModelDataHelper.GetCategories();
            var mockRepository = new Mock<ICategoriesDataRepository>();
            var categoriesBuilder = new CategoriesBuilder()
                .SetId(categoriesModel.Id)
                .SetCategoryName(categoriesModel.CategoryName)
                .SetDescription(categoriesModel.Description)
;
            var categories = categoriesBuilder.Build();
            mockRepository.Setup(repo => repo.Update(It.IsAny<Categories>()))
                .Returns(categories);
            mockRepository.Setup(repo => repo.GetCompleteAsync(It.IsAny<CategoriesCompleteSpecification>()))
                .ReturnsAsync(categories);
            mockRepository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(_CancellationToken))
            .ReturnsAsync(1);
            var updatedRequest = new UpdateCategoriesCommandRequest(categoriesModel);
            UpdateCategoriesCommandRequestHandler updateRequestHandler = new(mockRepository.Object, mapper);
            var updatedCategories = await updateRequestHandler.Handle(updatedRequest, _CancellationToken);
            Assert.IsNotNull(updatedCategories);
            Assert.IsTrue(updatedCategories.IsSuccess);
        }
        [TestMethod]
        public async Task DeleteCategoriesTestAsync()
        {
            var mockRepository = new Mock<ICategoriesDataRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            mockRepository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(_CancellationToken))
            .ReturnsAsync(1);
            DeleteCategoriesCommandRequest deleteRequest = new(0);
            DeleteCategoriesCommandRequestHandler deleteRequestHandler = new(mockRepository.Object);
            var isDeleted = await deleteRequestHandler.Handle(deleteRequest, _CancellationToken);
            Assert.IsTrue(isDeleted.IsSuccess);
        }
    }
}
