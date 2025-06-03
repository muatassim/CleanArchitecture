using System.Text.Json.Serialization;
namespace CleanArchitecture.Core.Validations
{
    /// <summary>
    /// Represents a validation error for a specific property.
    /// Used to communicate which property failed validation and why.
    /// </summary>
    public sealed class ValidationError
    {
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public ValidationError()
        {
            PropertyName = string.Empty;
            ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Creates a new validation error for a specific property and error message.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        /// <param name="errorMessage">The error message describing the validation failure.</param>
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// The name of the property that failed validation.
        /// </summary>
        [JsonPropertyName("propertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message describing the validation failure.
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}