using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.CoreTest.Data
{
    public class DummyValidator : Validator
    {
        public string TestProperty { get; set; }
        public override List<Rule> CreateRules() => [];
    }
}