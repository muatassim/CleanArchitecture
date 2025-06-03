using CleanArchitecture.Application.Features.Queries.Categories.GetById;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.ApplicationTests.Data;
using Moq;
namespace CleanArchitecture.ApplicationTests.Tests.Features.Queries
{
    [TestClass]
    public class GetCategoriesByIdQueryRequestHandlerTest
    {
        private CategoriesMapper mapper;
        [TestInitialize]
        public void TestInitialize()
        {
            mapper = (CategoriesMapper)
                KernelMapper.ServiceProvider.GetService(typeof(CategoriesMapper));
        }
        [TestMethod]
        [DataRow(0)]
        public async Task GetCategoriesByIdQTestAsync(int id)
        {
           var categories = EntityDataHelper.GetCategories(id); 
            var mockRepository = new Mock<ICategoriesDataRepository>();
            mockRepository.Setup(repo => repo.GetCompleteAsync(It.IsAny<CategoriesCompleteSpecification>()))
                .ReturnsAsync(categories);
            var request = new GetCategoriesByIdQueryRequest(id);
            var cancellationToken = new CancellationToken();
            GetCategoriesByIdQueryRequestHandler getCategoriesByIdQueryRequestHandler =
                new(mapper, mockRepository.Object);
            var result = await getCategoriesByIdQueryRequestHandler.Handle(request, cancellationToken);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Id == id);
        }
        [TestCleanup]
        public void TestCleanup()
        {
        }
    }
}
