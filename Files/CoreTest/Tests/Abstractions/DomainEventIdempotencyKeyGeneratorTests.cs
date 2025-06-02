using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.CoreTest.Tests.Abstractions
{

    [TestClass]
    public class DomainEventIdempotencyKeyGeneratorTests
    {
        private static readonly System.Text.Json.JsonSerializerOptions CachedJsonSerializerOptions = new()
        {
            IncludeFields = true,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        [TestMethod]
        public void DomainEventIdempotencyKeyGeneratorIsNotNull()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddTransient<IDomainEventIdempotencyKeyGenerator, DomainEventIdempotencyKeyGenerator>();
            var provider = services.BuildServiceProvider();
            // Act
            var generator = provider.GetService<IDomainEventIdempotencyKeyGenerator>();
            // Assert
            Assert.IsNotNull(generator);
        }


        [TestMethod]
        public void GenerateIdempotencyKey_FromDomainEvent_ProducesConsistentHash()
        {
            // Arrange
            var generator = new DomainEventIdempotencyKeyGenerator();
            var evt1 = new TestDomainEvent { Id = Guid.NewGuid(), Name = "Test", OccurredOn = DateTime.UtcNow };
            var evt2 = new TestDomainEvent { Id = evt1.Id, Name = "Test", OccurredOn = evt1.OccurredOn };

            // Act
            var hash1 = generator.GenerateIdempotencyKey(evt1);
            var hash2 = generator.GenerateIdempotencyKey(evt2);

            // Assert
            Assert.AreEqual(hash1, hash2, "Hashes should be equal for equivalent events.");
        }

        [TestMethod]
        public void GenerateIdempotencyKey_FromDomainEvent_ProducesDifferentHashForDifferentEvents()
        {
            // Arrange
            var generator = new DomainEventIdempotencyKeyGenerator();
            var evt1 = new TestDomainEvent { Id = Guid.NewGuid(), Name = "Test1", OccurredOn = DateTime.UtcNow };
            var evt2 = new TestDomainEvent { Id = Guid.NewGuid(), Name = "Test2", OccurredOn = DateTime.UtcNow };

            // Act
            var hash1 = generator.GenerateIdempotencyKey(evt1);
            var hash2 = generator.GenerateIdempotencyKey(evt2);

            // Assert
            Assert.AreNotEqual(hash1, hash2, "Hashes should differ for different events.");
        }

        [TestMethod]
        public void GenerateIdempotencyKey_FromJson_ProducesSameHashAsFromDomainEvent()
        {
            // Arrange
            var generator = new DomainEventIdempotencyKeyGenerator();
            var evt = new TestDomainEvent { Id = Guid.NewGuid(), Name = "Test", OccurredOn = DateTime.UtcNow };
            var json = System.Text.Json.JsonSerializer.Serialize(evt, evt.GetType(), CachedJsonSerializerOptions);

            // Act
            var hashFromEvent = generator.GenerateIdempotencyKey(evt);
            var hashFromJson = generator.GenerateIdempotencyKey(json);

            // Assert
            Assert.AreEqual(hashFromEvent, hashFromJson, "Hashes should be equal for the same event and its JSON.");
        }
    }
 
}