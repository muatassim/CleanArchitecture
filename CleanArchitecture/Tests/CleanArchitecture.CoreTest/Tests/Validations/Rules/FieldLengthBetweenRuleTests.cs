using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Validations.Rules
{

    [TestClass]
    public class FieldLengthBetweenRuleTests
    {
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenLengthWithinRange()
        {
            var validator = new DummyValidator { TestProperty = "abc" };
            var rule = new FieldLengthBetweenRule("TestProperty", "desc", 2, 5);
            Assert.IsTrue(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenLengthBelowMin()
        {
            var validator = new DummyValidator { TestProperty = "a" };
            var rule = new FieldLengthBetweenRule("TestProperty", validator.TestProperty, 2, 5);
            Assert.IsFalse(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenLengthAboveMax()
        {
            var validator = new DummyValidator { TestProperty = "abcdef" };
            var rule = new FieldLengthBetweenRule("TestProperty", 
                validator.TestProperty, 2, 5);
            Assert.IsFalse(rule.ValidateRule(validator));
        }
    }
}