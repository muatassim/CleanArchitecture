using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Validations.Rules
{
    [TestClass]
    public class RequiredFieldRuleTests
    {
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenValueIsNotNullOrEmpty()
        {
            var validator = new DummyValidator { TestProperty = "value" };
            var rule = new RequiredFieldRule("TestProperty","TestProperty is not Valid",validator.TestProperty);
            Assert.IsTrue(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenValueIsNull()
        {
            var validator = new DummyValidator { TestProperty = null };
            var rule = new RequiredFieldRule("TestProperty", "TestProperty is not Valid", validator.TestProperty);
            Assert.IsFalse(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenValueIsEmpty()
        {
            var validator = new DummyValidator { TestProperty = "" };
            var rule = new RequiredFieldRule("TestProperty", "TestProperty is not Valid", validator.TestProperty);
            Assert.IsFalse(rule.ValidateRule(validator));
        }
    }
}