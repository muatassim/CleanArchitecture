using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.CoreTest.Data
{
    public class TestValidationEntityWithId : Entity<Guid>
    {
        private readonly bool _isValid;
        private readonly List<ValidationError> _errors;

        public TestValidationEntityWithId(Guid id, bool isValid, List<ValidationError> errors = null) : base(id)
        {
            _isValid = isValid;
            _errors = errors ?? new List<ValidationError>();
        }

        public override (bool IsValid, List<ValidationError> Errors) IsValid()
            => (_isValid, _errors);
    }
}