using CleanArchitecture.Application.Features.Queries.Categories.Get;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.ApplicationTests.Data;
using Moq;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.ApplicationTests.Tests.Features.Queries
{
    [TestClass]
    public class GetCategoriesPagedRequestHandlerTest
    {
        private CategoriesMapper mapper;
        [TestInitialize]
        public void TestInitialize()
        {
            mapper = (CategoriesMapper)
                KernelMapper.ServiceProvider.GetService(typeof(CategoriesMapper));
        }
        [TestMethod]
        [DataRow(5)]
        public async Task GetCategoriesPagedRequestTestAsync(int recordCount)
        {
            var mockSearchFactory = new Mock<ICategoriesSearchSpecificationFactory>();
            var mockRepository = new Mock<ICategoriesDataRepository>();
            Page<Categories> result = EntityDataHelper.GetCategoriesPage(recordCount);
            SearchRequest searchParameters = new()
            {
                PageSize = recordCount,
                SortOrder = OrderBy.Descending,
                Skip = 0
            };
            var categoriesSearchSpecification = new Mock<ISearchSpecification<Categories, int>>();
            mockSearchFactory.Setup(factory => factory.Create(It.IsAny<SearchRequest>()))
                         .Returns(categoriesSearchSpecification.Object);
            mockRepository.Setup(repo => repo.GetAsync(It.IsAny<ISearchSpecification<Categories, int>>())).ReturnsAsync(result);
            var request = new GetCategoriesSearchRequest(searchParameters);
            var cancellationToken = new CancellationToken();
            GetCategoriesSearchRequestHandler getCategoriesQueryRequestHandler =
                new(mapper, mockRepository.Object, mockSearchFactory.Object);
            Core.Abstractions.Result<Page<Application.Models.CategoriesModel>> categoriesList =
                await getCategoriesQueryRequestHandler.Handle(request, cancellationToken);
            Assert.IsNotNull(categoriesList);
        }
    }
}
