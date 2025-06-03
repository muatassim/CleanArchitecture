using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.CoreTest.Tests.Validations
{
    [TestClass]
    public class ValidationHelperTests
    {
        [TestMethod]
        public void GetErrors_ReturnsEmptyList_WhenInputIsNull()
        {
            var result = ValidationHelper.GetErrors(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetErrors_ReturnsEmptyList_WhenInputIsEmpty()
        {
            var result = ValidationHelper.GetErrors([]);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetErrors_ConvertsValidationErrorsToErrors()
        {
            var validationErrors = new List<ValidationError>
            {
                new("Property1", "Error 1"),
                new("Property2", "Error 2")
            };

            var result = ValidationHelper.GetErrors(validationErrors);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Property1", result[0].Name);
            Assert.AreEqual("Error 1", result[0].Message);
            Assert.AreEqual("Property2", result[1].Name);
            Assert.AreEqual("Error 2", result[1].Message);
        }
    }
}