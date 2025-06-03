using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Validations;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Abstractions
{

    [TestClass]
    public class ValidationServiceTests
    {
        [TestMethod]
        public void IsValid_ReturnsTrue_WhenEntityIsValid()
        {
            var entity = new TestValidationEntity(true);
            var service = new ValidationService<TestValidationEntity>();
            var (isValid, errors) = service.IsValid(entity);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenEntityIsInvalid()
        {
            var errors = new List<ValidationError>
            {
                new("Name", "Name is required")
            };
            var entity = new TestValidationEntity(false, errors);
            var service = new ValidationService<TestValidationEntity>();
            var (isValid, resultErrors) = service.IsValid(entity);

            Assert.IsFalse(isValid);
            CollectionAssert.AreEqual(errors, resultErrors);
        }

        [TestMethod]
        public void IsValid_WithId_ReturnsTrue_WhenEntityIsValid()
        {
            var entity = new TestValidationEntityWithId(Guid.NewGuid(), true);
            var service = new ValidationService<TestValidationEntityWithId, Guid>();
            var (isValid, errors) = service.IsValid(entity);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void IsValid_WithId_ReturnsFalse_WhenEntityIsInvalid()
        {
            var errors = new List<ValidationError>
            {
                new("Id", "Id is invalid")
            };
            var entity = new TestValidationEntityWithId(Guid.NewGuid(), false, errors);
            var service = new ValidationService<TestValidationEntityWithId, Guid>();
            var (isValid, resultErrors) = service.IsValid(entity);

            Assert.IsFalse(isValid);
            CollectionAssert.AreEqual(errors, resultErrors);
        }
    }
}