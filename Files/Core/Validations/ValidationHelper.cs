using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Validations
{
    /// <summary>
    /// Provides utility methods for working with validation errors.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Converts a list of <see cref="ValidationError"/> objects to a list of <see cref="Error"/> objects.
        /// This is useful for mapping validation results to a standard error format used throughout the application.
        /// </summary>
        /// <param name="validationErrors">The list of validation errors to convert.</param>
        /// <returns>A list of <see cref="Error"/> objects.</returns>
        public static List<Error> GetErrors(List<ValidationError> validationErrors)
        {
            List<Error> errors = [];
            if (validationErrors != null && validationErrors.Count > 0)
            {
                foreach (var validationError in validationErrors)
                {
                    errors.Add(new Error(validationError.PropertyName, validationError.ErrorMessage));
                }
            }
            return errors;
        }
    }
}