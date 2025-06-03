using CleanArchitecture.Core.Abstractions.Specifications;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Abstractions.Specifications
{
    [TestClass]
    public class EntitiesOrderByIdSpecificationTests
    {
        [TestMethod]
        public void Constructor_SetsPagingAndOrderById()
        {
            // Arrange
            int take = 7;
            var spec = new EntitiesOrderByIdSpecification<TestEntity, Guid>(take);

            // Assert
            Assert.AreEqual(0, spec.Skip, "Skip should be 0 by default.");
            Assert.AreEqual(take, spec.Take, "Take should match the constructor argument.");
            Assert.IsTrue(spec.IsPagingEnabled, "Paging should be enabled.");
            Assert.IsNotNull(spec.OrderBy, "OrderBy should be set.");
        }

        [TestMethod]
        public void OrderBy_ExpressionOrdersById()
        {
            // Arrange
            var spec = new EntitiesOrderByIdSpecification<TestEntity, Guid>(3);
            var orderBy = spec.OrderBy;
            Assert.IsNotNull(orderBy);

            // Act
            var compiled = orderBy!.Compile();
            var entity = new TestEntity(Guid.NewGuid() );

            // Assert
            Assert.AreEqual(entity.Id, compiled(entity));
        }
    }
}
