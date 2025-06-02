using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Marker interface for validation handlers.
    /// </summary>
    public interface IValidationHandler { }

    /// <summary>
    /// Defines a contract for validating a request of type <typeparamref name="T"/>.
    /// Implementations should return a <see cref="ValidationResult"/> indicating success or containing validation errors.
    /// </summary>
    /// <typeparam name="T">The type of request to validate.</typeparam>
    public interface IValidationHandler<in T> : IValidationHandler
    {
        /// <summary>
        /// Validates the given request asynchronously.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating the outcome of the validation.</returns>
        Task<ValidationResult> Validate(T request);
    }
}