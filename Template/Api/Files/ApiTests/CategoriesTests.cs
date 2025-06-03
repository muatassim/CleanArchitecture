using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Exceptions;
using CleanArchitecture.ApiShared.Interfaces.Api;
using CleanArchitecture.ApiShared.Models;
using CleanArchitecture.ApiTests.Data;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.ApiTests
{
    [TestClass]
    public class CategoriesModelApiTest : BaseTest
    {
        static ILookupApiHelper? lookupHelper;
        static ICategoriesModelApi? _helper;
        static ILogger<CategoriesModelApiTest>? _logger;
        [TestInitialize]
        public void TestInitialize()
        {
            lookupHelper = ServiceProvider.GetService(typeof(ILookupApiHelper)) as ILookupApiHelper;
            _helper = (ICategoriesModelApi?)ServiceProvider.GetService(typeof(ICategoriesModelApi));
            _logger = (ILogger<CategoriesModelApiTest>?)ServiceProvider.GetService(typeof(ILogger<CategoriesModelApiTest>));
            Assert.IsNotNull(_helper, "ICategoriesModelApi service is not available.");
            Assert.IsNotNull(_logger, "ILogger service is not available.");
        }
        [DataTestMethod]
        [DataRow(2)] //break early if you have more than 15 records
        public async Task GetSearchRequestTest(int totalCount)
        {
            if (_helper == null || _logger == null)
            {
                Assert.Fail("ICategoriesModelApi or logger service is not available.");
            }
            List<CategoriesModel> allRecords = [];
            //First Page 
            var searchParameters = ModelDataHelper.GetSearchParameters();
            var pagedList = await _helper.GetAsync(searchParameters);
            Assert.IsNotNull(pagedList, "Paged list is null.");
            allRecords.AddRange([.. pagedList.Results]);
            while (allRecords.Count < pagedList.TotalRecords && allRecords.Count < totalCount)
            {
                searchParameters.Skip += searchParameters.PageSize;
                pagedList = await _helper.GetAsync(searchParameters);
                Assert.IsNotNull(pagedList, "Paged list is null.");
                allRecords.AddRange(pagedList.Results);
            }
            _logger.LogInformation("Records for CategoriesModel ====>  allRecords.Count = {allRecords}, pagedList.RecordCount {totalRecords}{NewLine}",
                   allRecords.Count, pagedList.TotalRecords, Environment.NewLine);
            Assert.IsTrue(allRecords.Count>=pagedList.TotalRecords);
        }
        [DataTestMethod]
        [DataRow("a")]
        public async Task GetSearchByCategoryName(string searchValue)
        {
            if (_helper == null || _logger == null)
            {
                Assert.Fail("ICategoriesModelApi or logger service is not available.");
            }
            var searchRequest = new SearchRequest
            {
                SearchValue = searchValue,
                SortOrderColumn = nameof(CategoriesModel.CategoryName),
                SortOrder = OrderBy.Ascending,
                Skip = 0,
                PageSize = 10
            };
            var pagedList = await _helper.GetAsync(searchRequest);
            Assert.IsNotNull(pagedList, "Paged list is null.");
            Assert.IsTrue(pagedList.Results.Any(), "No results found.");
        }
        [TestMethod]
        public async Task Add_Get_Update_Delete_Categories_Test()
        {
            if (_helper == null || _logger == null)
            {
                Assert.Fail("ICategoriesModelApi or logger service is not available.");
            }
            var CategoriesModel = ModelDataHelper.GetCategories();
            CategoriesModel addedRecord = new();
            try
            {
                addedRecord = await _helper.PostAsync(CategoriesModel);
                Assert.IsNotNull(addedRecord?.Id, "Added record ID is null.");
            }
            catch (ApiErrorException ex)
            {
                _logger.LogError(ex, "Error in Add_Get_Update_Delete_Categories_Test");
                Assert.Fail(ex.Message);
            }
            if (addedRecord == null || addedRecord.Id == default)
            {
                Assert.Fail("CategoriesModel is null");
            }
            var savedRecord = await _helper.GetAsync(addedRecord.Id);
            Assert.IsNotNull(savedRecord, "Saved record is null.");
            var updatedCategories = ModelDataHelper.GetCategories();
            //Keep Primary Key and Foreign Key same, also any other unique key
            updatedCategories.Id = addedRecord.Id;
            var recordUpdated = await _helper.PutAsync(addedRecord);
            Assert.IsTrue(recordUpdated, "Record update failed.");
            var deleted = await _helper.DeleteAsync(addedRecord.Id);
            Assert.IsTrue(deleted, "Record deletion failed.");
        }
    }
}
