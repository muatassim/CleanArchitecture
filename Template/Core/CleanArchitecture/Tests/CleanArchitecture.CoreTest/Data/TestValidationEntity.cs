using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.CoreTest.Data
{
    // Test entity with/without ID
    public class TestValidationEntity : Entity
    {
        private readonly bool _isValid;
        private readonly List<ValidationError> _errors;

        public TestValidationEntity(bool isValid, List<ValidationError> errors = null)
        {
            _isValid = isValid;
            _errors = errors ?? new List<ValidationError>();
        }

        public override (bool IsValid, List<ValidationError> Errors) IsValid()
            => (_isValid, _errors);
    }
}