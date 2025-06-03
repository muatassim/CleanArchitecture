using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Builders;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications;
using CleanArchitecture.Infrastructure.MicrosoftSqlTests.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests.Tests
{
    [TestClass]
    public class CategoriesDataRepositoryTest
    {
        private ILogger<CategoriesDataRepositoryTest> _logger;
        private ICategoriesDataRepository _categoriesRepository;
        private IServiceScope _scope;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create a new scope for each test to ensure a new DbContext and repository
            _scope = KernelMapper.ServiceProvider.CreateScope();
            _categoriesRepository = (ICategoriesDataRepository)_scope.ServiceProvider.GetService(typeof(ICategoriesDataRepository));
            _logger = (ILogger<CategoriesDataRepositoryTest>)_scope.ServiceProvider.GetService(typeof(ILogger<CategoriesDataRepositoryTest>));

            // Optionally seed data for each test
            var existingCategoriesRecord = _categoriesRepository.TakeAsync(1).Result.FirstOrDefault();
            if (existingCategoriesRecord == null)
            {
                var insertedCategoriesRecord = EntityDataHelper.GetCategories();
                _categoriesRepository.AddAsync(insertedCategoriesRecord).Wait();
                _categoriesRepository.UnitOfWork.SaveChangesAsync().Wait();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _scope?.Dispose();
        }

        [TestMethod]
        public async Task Categories_DataRepository_Add_DeleteById_Take_Find_Test()
        {
            var newlyAddedEntity = EntityDataHelper.GetCategories();
            Assert.IsNotNull(newlyAddedEntity); // Ensure the entity is not null before proceeding.
            var addResult = await _categoriesRepository.AddAsync(newlyAddedEntity);
            Assert.IsNotNull(addResult); // Ensure the addition result is not null.
            await _categoriesRepository.UnitOfWork.SaveChangesAsync();

            EntityByIdSpecification<Categories, int> spec = new(newlyAddedEntity.Id);
            Categories newRecord = await _categoriesRepository.FindAsync(spec);
            Assert.IsNotNull(newRecord); // Ensure the record is found.
            Assert.IsNotNull(newRecord?.Id); // Ensure the ID is not null.

            var deleted = await _categoriesRepository.DeleteAsync(newRecord.Id);
            Assert.IsTrue(deleted); // Ensure the deletion was successful.
            await _categoriesRepository.UnitOfWork.SaveChangesAsync();
        }

        [TestMethod]
        public async Task Categories_DataRepository_Add_Update_DeleteByObject_Take_Find_Test()
        {
            // Add  
            Categories newlyAddedEntity = EntityDataHelper.GetCategories();
            newlyAddedEntity = await _categoriesRepository.AddAsync(newlyAddedEntity);
            Assert.IsNotNull(newlyAddedEntity);
            // Commit Changes  
            await _categoriesRepository.UnitOfWork.SaveChangesAsync();
            EntityByIdSpecification<Categories, int> spec = new(newlyAddedEntity.Id);
            var addedRecord = await _categoriesRepository.FindAsync(spec);
            Assert.IsNotNull(addedRecord);
            Assert.IsNotNull(addedRecord?.Id);
            Assert.AreEqual(addedRecord?.Id, newlyAddedEntity.Id);
            // Update Properties  
            CategoriesBuilder updateCategoriesBuilder = new CategoriesBuilder()
                .SetId(newlyAddedEntity.Id)
                .SetCategoryName(DataHelper.RandomString(15, false))
                .SetDescription(DataHelper.RandomString(8, false));
            var updatedCategories = newlyAddedEntity.Update(updateCategoriesBuilder);
            Assert.IsNotNull(updatedCategories);
            // Updated the Record  
            var updated = _categoriesRepository.Update(newlyAddedEntity);
            Assert.IsNotNull(updated);
            // Commit Changes  
            await _categoriesRepository.UnitOfWork.SaveChangesAsync();
            // Delete the Record  
            var deleted = _categoriesRepository.Delete(newlyAddedEntity);
            Assert.IsTrue(deleted);
            // Commit Changes  
            await _categoriesRepository.UnitOfWork.SaveChangesAsync();
        }

        [TestMethod]
        public async Task FindById_Test()
        {
            var dbRecord = (await _categoriesRepository.TakeAsync(1)).FirstOrDefault();
            if (dbRecord != null)
            {
                var entity = await _categoriesRepository.FindAsync(dbRecord.Id);
                Assert.IsNotNull(entity);
            }
            Assert.IsNotNull(dbRecord);
        }

        [TestMethod]
        public async Task GetCompleteAsync_Test()
        {
            var dbRecord = (await _categoriesRepository.TakeAsync(1)).FirstOrDefault();
            if (dbRecord != null)
            {
                CategoriesCompleteSpecification specification = new(dbRecord.Id);
                var entity = await _categoriesRepository.GetCompleteAsync(specification);
                Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public async Task AddBulkCategoriesAsync()
        {
            int count = 5; // You can parameterize this if needed
            Stopwatch watch = new();
            watch.Start();
            _logger.LogInformation("Starting request to insert records: {count} in {Categories}", count, nameof(Categories));
            foreach (var i in Enumerable.Range(0, count))
            {
                var insertedCategoriesRecord = EntityDataHelper.GetCategories();
                try
                {
                    _logger.LogInformation("Attempting to add record {i}", i + 1);
                    var entity = await _categoriesRepository.AddAsync(insertedCategoriesRecord);
                    _logger.LogInformation("Entity Id: {entity.Id}", entity.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding Categories record {d}", i + 1);
                    throw;
                }
                // Commit Changes  
                try
                {
                    _logger.LogInformation("Committing changes");
                    await _categoriesRepository.UnitOfWork.SaveChangesAsync();
                    _logger.LogInformation("Changes committed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving changes");
                }
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            _logger.LogInformation("Finishing request for inserting Count:{count} records in {cat} and it took {Hours}:{Minutes}:{Seconds}:{Milliseconds / 10}",
                count, nameof(Categories), ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            var records = await _categoriesRepository.CountAsync();
            Assert.IsTrue(records >= count, "The number of records in the database is less than the expected count.");
            _logger.LogInformation("Total Records in {cat}: {records}", nameof(Categories), records);
        }

        [TestMethod]
        [DataRow(5)]
        public async Task Categories_DataRepository_GetAsync_Test(int take)
        {
            EntitiesOrderByIdSpecification<Categories, int> entitiesOrderByIdSpecification =
                new(take);
            var list = await _categoriesRepository.FindAllAsync(entitiesOrderByIdSpecification);
            Assert.IsTrue(list != null && list.ToList().Count > 0);
        }

        [TestMethod]
        public async Task Categories_DataRepository_SearchAndOrder_Test()
        {
            var dbRecord = (await _categoriesRepository.TakeAsync(1)).FirstOrDefault();
            if (dbRecord != null)
            {
                var searchParameters = new SearchRequest
                {
                    SearchValue = dbRecord.CategoryName,
                    SortOrderColumn = nameof(Categories.CategoryName),
                    SortOrder = OrderBy.Ascending,
                    Skip = 0,
                    PageSize = 10
                };
                // Arrange  
                var categoriesSpecification = new CategoriesSearchSpecification(searchParameters);
                // Act  
                var result = await _categoriesRepository.GetAsync(categoriesSpecification);
                // Assert  
                Assert.IsNotNull(result);
                var orderedList = result.Results.OrderBy(x => x.CategoryName).ToList();
                Assert.IsTrue(result.Results.SequenceEqual(orderedList), "The records are not in ascending order by CategoryName.");
            }
            else
            {
                Assert.Inconclusive("No records found in the database to perform the test.");
            }
        }
    }
}