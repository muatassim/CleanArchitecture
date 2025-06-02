using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Validations;
using CleanArchitecture.Core.Validations.Rules;
using CleanArchitecture.Core.Validations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.CoreTest.Tests
{ 
    [TestClass]
    public class PersonEntityTest
    {
        [DataTestMethod]
        [DataRow(1, "John Doe", "a@gmail.com")]
        public void ValidatePersonRulesTest(int id, string name, string email)
        {
            Person person = new Person(id)
            {
                Name = name,
                Age = 35,
                Email = email
            };
            var rules = person.CreateRules();
            var isValid = rules.All(rule => rule.ValidateRule(person));
            Assert.IsTrue(isValid, "Person entity should be valid based on the provided rules.");
        }

        [DataTestMethod]
        [DataRow(1, "John Doe", "a@gmail.com")]
        public void ValidatePersonRulesServiceTest(int id, string name, string email)
        {
            Person person = new Person(id)
            {
                Name = name,
                Age = 35,
                Email = email
            };
            var personValidationService = new PersonValidationService();
            (bool IsValid, List<ValidationError> Errors)  = personValidationService.IsValid(person);
            Assert.IsTrue(IsValid, "Person entity should be valid based on the provided rules.");
        }
    }
}
