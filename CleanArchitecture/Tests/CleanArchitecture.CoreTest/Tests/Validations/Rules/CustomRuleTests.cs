using CleanArchitecture.Core.Validations.Rules;

namespace CleanArchitecture.CoreTest.Tests.Validations.Rules
{
    [TestClass]
    public class CustomRuleTests
    {
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenDelegateReturnsTrue()
        {
            var rule = new CustomRule("TestProperty", "Should be valid", () => true);
            var result = rule.ValidateRule(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenDelegateReturnsFalse()
        {
            var rule = new CustomRule("TestProperty", "Should be invalid", () => false);
            var result = rule.ValidateRule(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CustomRule_SetsPropertyName_And_Description()
        {
            var rule = new CustomRule("MyProperty", "Broken rule", () => true);
            Assert.AreEqual("MyProperty", rule.PropertyName);
            Assert.AreEqual("Broken rule", rule.Description);
        }
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenRecordIsUnique()
        {
            // Arrange: Simulate a uniqueness check (e.g., unique username)
            bool IsRecordUnique()
            {
                // Simulate a check against a data source; here, always unique for test
                return true;
            }

            var rule = new CustomRule("Username", "Username must be unique", IsRecordUnique);

            // Act
            var result = rule.ValidateRule(null);

            // Assert
            Assert.IsTrue(result);
        }



    }
}