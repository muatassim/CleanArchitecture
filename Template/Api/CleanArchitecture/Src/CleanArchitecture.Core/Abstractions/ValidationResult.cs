using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Represents the result of a validation operation, including any validation errors.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// List of validation errors. If empty, the validation is considered successful.
        /// </summary>
        public List<ValidationError> Errors
        {
            get => _errors ??= [];
            set => _errors = value;
        }

        /// <summary>
        /// Indicates if the validation was successful (settable).
        /// </summary>
        public bool IsSuccessful { get; set; } = true;

        // Backing field for Errors.
        private List<ValidationError> _errors = [];

        /// <summary>
        /// Indicates if the validation is valid (no errors).
        /// </summary>
        public virtual bool IsValid => Errors.Count == 0;

        /// <summary>
        /// Optional error message for general validation failure.
        /// </summary>
        public string? Error { get; init; }

        /// <summary>
        /// Returns a successful ValidationResult.
        /// </summary>
        public static ValidationResult Success => new();

        /// <summary>
        /// Returns a failed ValidationResult with a list of errors.
        /// </summary>
        public static ValidationResult Fail(List<ValidationError> errors) => new()
        {
            IsSuccessful = false,
            _errors = errors
        };
    }
}