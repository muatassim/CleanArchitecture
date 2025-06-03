using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Abstractions.Specifications
{
    [TestClass]
    public class GenericSearchSpecificationTests
    {
        [TestMethod]
        public void Constructor_SetsDefaultCriteria_WhenSearchValueIsNullOrEmpty()
        {
            var request = new SearchRequest
            {
                SearchValue = "",
                SortOrderColumn = "Name",
                SortOrder = OrderBy.Ascending,
                PageSize = 10,
                Skip = 0
            };

            var spec = new GenericSearchSpecification<GenericTestEntity>(request);

            Assert.IsNotNull(spec.Criteria);
        }

        [TestMethod]
        public void Constructor_SetsOrderBy_WhenSortOrderIsAscending()
        {
            var request = new SearchRequest
            {
                SearchValue = "",
                SortOrderColumn = "Name",
                SortOrder = OrderBy.Ascending,
                PageSize = 10,
                Skip = 0
            };

            var spec = new GenericSearchSpecification<GenericTestEntity>(request);

            Assert.IsNotNull(spec.OrderBy);
            Assert.IsNull(spec.OrderByDescending);
        }

        [TestMethod]
        public void Constructor_SetsOrderByDescending_WhenSortOrderIsDescending()
        {
            var request = new SearchRequest
            {
                SearchValue = "",
                SortOrderColumn = "Age",
                SortOrder = OrderBy.Descending,
                PageSize = 10,
                Skip = 0
            };

            var spec = new GenericSearchSpecification<GenericTestEntity>(request);

            Assert.IsNull(spec.OrderBy);
            Assert.IsNotNull(spec.OrderByDescending);
        }

        [TestMethod]
        public void Constructor_SetsDefaultOrderBy_WhenSortOrderColumnIsNullOrEmpty()
        {
            var request = new SearchRequest
            {
                SearchValue = "",
                SortOrderColumn = "",
                SortOrder = OrderBy.Ascending,
                PageSize = 10,
                Skip = 0
            };

            var spec = new GenericSearchSpecification<GenericTestEntity>(request);

            Assert.IsNotNull(spec.OrderBy);
        }

        [TestMethod]
        public void Constructor_SetsPagingProperties()
        {
            var request = new SearchRequest
            {
                SearchValue = "",
                SortOrderColumn = "Name",
                SortOrder = OrderBy.Ascending,
                PageSize = 15,
                Skip = 5
            };

            var spec = new GenericSearchSpecification<GenericTestEntity>(request);

            Assert.AreEqual(15, spec.PageSize);
            Assert.AreEqual(15, spec.Take);
            Assert.AreEqual(5, spec.Skip);
            Assert.IsTrue(spec.IsPagingEnabled);
        }
    }
}
