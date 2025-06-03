using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Abstractions.Specifications
{
    

    [TestClass]
    public class EntityByIdSpecificationTests
    {
        [TestMethod]
        public void Constructor_SetsCriteriaToMatchId()
        {
            // Arrange  
            var id = Guid.NewGuid();
            var spec = new EntityByIdSpecification<TestEntity, Guid>(id);

                    // Act  
            var compiled = spec.Criteria!.Compile();
            var matchingEntity = new TestEntity(id );
            var nonMatchingEntity = new TestEntity (Guid.NewGuid() );

            // Assert  
            Assert.IsNotNull(spec.Criteria);
            Assert.IsTrue(compiled(matchingEntity), "Criteria should match entity with the same ID.");
            Assert.IsFalse(compiled(nonMatchingEntity), "Criteria should not match entity with a different ID.");
        }

        [TestMethod]
        public void Constructor_CriteriaHandlesNullId()
        {
            // Arrange  
            var spec = new EntityByIdSpecification<TestEntity, Guid>(Guid.Empty);

            // Act  
            var compiled = spec.Criteria!.Compile();
            var entity = new TestEntity ( Guid.Empty);

            // Assert  
            Assert.IsTrue(compiled(entity), "Criteria should match entity with Guid.Empty ID.");
        }
    }
}
