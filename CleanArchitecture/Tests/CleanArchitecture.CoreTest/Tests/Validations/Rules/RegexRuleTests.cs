using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.CoreTest.Data;

namespace CleanArchitecture.CoreTest.Tests.Validations.Rules
{
    [TestClass] 
    public class RegexRuleTests
    {
        [TestMethod]
        public void ValidateRule_ReturnsTrue_WhenValueMatchesRegex()
        {
            var validator = new DummyValidator { TestProperty = "abc123" };
            var rule = new RegexRule("TestProperty", "desc", @"^[a-z]+\d+$");
            Assert.IsTrue(rule.ValidateRule(validator));
        }

        [TestMethod]
        public void ValidateRule_ReturnsFalse_WhenValueDoesNotMatchRegex()
        {
            var validator = new DummyValidator { TestProperty = "123abc" };
            var rule = new RegexRule("TestProperty", "desc", @"^[a-z]+\d+$");
            Assert.IsFalse(rule.ValidateRule(validator));
        }

        [TestMethod]
        [DataRow("maziz@gpo.gov")]
        public void ValidateRule_EmailValidator(string email)
        {
            var validator = new DummyValidator { TestProperty = email };
            var rule = new RegexRule("TestProperty", "Email must be valid", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Assert.IsTrue(rule.ValidateRule(validator));

            validator.TestProperty = "invalid-email";
            Assert.IsFalse(rule.ValidateRule(validator));
        }

        [TestMethod]
        [DataRow("+12345678901")]
        public void ValidateRule_PhoneValidator(string usaPhone)
        {
            var validator = new DummyValidator { TestProperty =  usaPhone };
            var rule = new RegexRule("TestProperty", "Phone must be valid", @"^\+?\d{10,15}$");
            Assert.IsTrue(rule.ValidateRule(validator));

            validator.TestProperty = "12345";
            Assert.IsFalse(rule.ValidateRule(validator));
        }

        [TestMethod]
        [DataRow("https://home.gpo.gov")]
        public void ValidateRule_WebAddressValidator(string url)
        {
            var validator = new DummyValidator { TestProperty =url };
            var rule = new RegexRule("TestProperty", "Web address must be valid", @"^https?:\/\/[^\s$.?#].[^\s]*$");
            Assert.IsTrue(rule.ValidateRule(validator));

            validator.TestProperty = "not a url";
            Assert.IsFalse(rule.ValidateRule(validator));
        }
    }
}