using CleanArchitecture.Core.Abstractions.Specifications;
using System.Linq.Expressions;

namespace CleanArchitecture.CoreTest.Data
{ 

        // Concrete specification for testing
        public class TestEntitySpecification : Specification<TestEntity, Guid>
        {
            public TestEntitySpecification()
                : base(e => e.IsActive)
            {
                AddInclude(e => e.Name);
                AddOrderBy(e => e.Name);
                AddOrderByDescending(e => e.Id);
                ApplyPaging(5, 10);
            }

            public void AddTestThenInclude(Expression<Func<TestEntity, object>> include, Expression<Func<object, object>> thenInclude)
            {
                AddThenInclude(include, thenInclude);
            }

            public void AddTestCriteria(Expression<Func<TestEntity, bool>> criteria)
            {
                AddCriteria(criteria);
            }
        }
}
