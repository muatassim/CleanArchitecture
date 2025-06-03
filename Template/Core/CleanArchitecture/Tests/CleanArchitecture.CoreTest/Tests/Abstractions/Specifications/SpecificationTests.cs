using CleanArchitecture.CoreTest.Data;
using System.Linq.Expressions;

namespace CleanArchitecture.CoreTest.Tests.Abstractions.Specifications
{
    [TestClass]
        public class SpecificationTests
        {
            [TestMethod]
            public void Constructor_SetsCriteria()
            {
                var spec = new TestEntitySpecification();
                Assert.IsNotNull(spec.Criteria);
            }

            [TestMethod]
            public void AddInclude_AddsInclude()
            {
                var spec = new TestEntitySpecification();
                Assert.AreEqual(1, spec.Includes.Count);
                Assert.AreEqual("Name", ((MemberExpression)spec.Includes[0].Body).Member.Name);
            }

            [TestMethod]
            public void AddOrderBy_SetsOrderBy()
            {
                var spec = new TestEntitySpecification();
                Assert.IsNotNull(spec.OrderBy);
            }

            [TestMethod]
            public void AddOrderByDescending_SetsOrderByDescending()
            {
                var spec = new TestEntitySpecification();
                Assert.IsNotNull(spec.OrderByDescending);
            }

            [TestMethod]
            public void ApplyPaging_SetsPagingProperties()
            {
                var spec = new TestEntitySpecification();
                Assert.AreEqual(5, spec.Skip);
                Assert.AreEqual(10, spec.Take);
                Assert.IsTrue(spec.IsPagingEnabled);
            }

            [TestMethod]
            public void AddThenInclude_AddsThenInclude()
            {
                var spec = new TestEntitySpecification();
                Expression<Func<TestEntity, object>> include = e => e.Name;
                Expression<Func<object, object>> thenInclude = o => o.ToString();
                spec.AddTestThenInclude(include, thenInclude);

                Assert.IsTrue(spec.ThenIncludes.ContainsKey(include));
                Assert.AreEqual(1, spec.ThenIncludes[include].Count);
            }

            [TestMethod]
            public void AddCriteria_OverridesCriteria()
            {
                var spec = new TestEntitySpecification();
                Expression<Func<TestEntity, bool>> newCriteria = e => !e.IsActive;
                spec.AddTestCriteria(newCriteria);

                Assert.AreEqual(newCriteria, spec.Criteria);
            }
        }
}
