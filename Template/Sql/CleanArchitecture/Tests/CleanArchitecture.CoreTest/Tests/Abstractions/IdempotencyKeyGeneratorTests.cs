using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Abstractions
{
    [TestClass]
        public class IdempotencyKeyGeneratorTests
        {
            [TestMethod]
            public void GenerateIdempotencyKey_ProducesConsistentHash_ForEquivalentEntities()
            {
                // Arrange
                var generator = new IdempotencyKeyGenerator<TestEntity>();
            var entity1 = new TestEntity(Guid.NewGuid())
            {
                Value = "A"
            };
            var entity2 = new TestEntity(entity1.Id) { Value = "A" };

                // Ac
                var hash1 = generator.GenerateIdempotencyKey(entity1);
                var hash2 = generator.GenerateIdempotencyKey(entity2);

                // Assert
                Assert.AreEqual(hash1, hash2, "Hashes should be equal for equivalent entities.");
            }

            [TestMethod]
            public void GenerateIdempotencyKey_ProducesDifferentHash_ForDifferentEntities()
            {
                // Arrange
                var generator = new IdempotencyKeyGenerator<TestEntity>();
                var entity1 = new TestEntity ( Guid.NewGuid()){ Value = "A" };
                var entity2 = new TestEntity(Guid.NewGuid()) { Value = "B" };

                // Act
                var hash1 = generator.GenerateIdempotencyKey(entity1);
                var hash2 = generator.GenerateIdempotencyKey(entity2);

                // Assert
                Assert.AreNotEqual(hash1, hash2, "Hashes should differ for different entities.");
            }
        }
 
}