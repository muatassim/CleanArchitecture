using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Validations.Rules
{
    [TestClass]
    public class MaximumFieldLengthRuleTests
    {
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenLengthIsBelowOrEqualToMax()
        {
            var validator = new DummyValidator { TestProperty = "abc" };
            var rule = new MaximumFieldLengthRule("TestProperty", "des", 3);
            Assert.IsTrue(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenLengthIsAboveMax()
        {
            var validator = new DummyValidator { TestProperty = "abcd" };
            var rule = new MaximumFieldLengthRule("TestProperty", "desc", 3);
            Assert.IsFalse(rule.ValidateRule(validator));
        }
    }
}