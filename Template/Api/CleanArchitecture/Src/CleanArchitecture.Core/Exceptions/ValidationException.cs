using System.Text;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.Core.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when validation fails.
    /// Encapsulates one or more <see cref="ValidationError"/> objects for detailed error reporting.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// The collection of validation errors associated with this exception.
        /// </summary>
        public readonly IReadOnlyCollection<ValidationError> Errors;

        /// <summary>
        /// Initializes a new instance with a list of validation errors.
        /// </summary>
        public ValidationException(List<ValidationError> errors) : base("Validation Errors") => Errors = errors;

        /// <summary>
        /// Initializes a new instance with a single validation error.
        /// </summary>
        public ValidationException(ValidationError error) : base(error.ErrorMessage) => Errors = [error];

        /// <summary>
        /// Initializes a new instance with a string error message.
        /// </summary>
        public ValidationException(string error) : base(error) => Errors = [new ValidationError("", error)];

        /// <summary>
        /// Initializes a new instance with a property name and error message.
        /// </summary>
        public ValidationException(string name, string error) : base(error) => Errors = [new ValidationError(name, error)];

        /// <summary>
        /// Returns a string representation of the exception and its validation errors.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var error in Errors)
            {
                sb.AppendLine($"name: {error.PropertyName}= message:{error.ErrorMessage}");
            }
            return $"{sb} {base.ToString()}";
        }
    }
}