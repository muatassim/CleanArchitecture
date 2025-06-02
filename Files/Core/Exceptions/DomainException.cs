using System.Text;
using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Exceptions
{
    /// <summary>
    /// Represents an application-level exception for domain or validation errors.
    /// Can encapsulate one or more <see cref="Error"/> objects for structured error reporting.
    /// </summary>
    public partial class DomainException : Exception
    {
        /// <summary>
        /// The collection of errors associated with this exception.
        /// </summary>
        public readonly IReadOnlyCollection<Error> Errors;

        /// <summary>
        /// Initializes a new instance with a list of errors.
        /// </summary>
        public DomainException(List<Error> errors) : base("Validation Errors") => Errors = errors;

        /// <summary>
        /// Initializes a new instance with a single error.
        /// </summary>
        public DomainException(Error error) : base(error.Message) => Errors = [error];

        /// <summary>
        /// Initializes a new instance with a string error message.
        /// </summary>
        public DomainException(string error) : base(error) => Errors = [new("", error)];

        /// <summary>
        /// Initializes a new instance with a named error and message.
        /// </summary>
        public DomainException(string name, string error) : base(error, new ArgumentException(error)) => Errors = [new Error(name, error)];

        /// <summary>
        /// Initializes a new instance with a message and an inner exception.
        /// </summary>
        public DomainException(string message, Exception exception) : base(message, exception) => Errors = [];

        /// <summary>
        /// Returns a string representation of the exception and its errors.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var error in Errors)
            {
                sb.AppendLine($"name: {error.Name}= message:{error.Message}");
            }
            return $"{sb} {base.ToString()}";
        }
    }
}