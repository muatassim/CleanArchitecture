using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Provides common, reusable <see cref="Error"/> instances for standard error scenarios.
    /// Use these to avoid magic strings and ensure consistency across your application.
    /// </summary>
    public static class ErrorHelpers
    {
        /// <summary>
        /// Represents the absence of an error.
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty);

        /// <summary>
        /// Represents an error when a null value is provided where it is not allowed.
        /// </summary>
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
    }
}