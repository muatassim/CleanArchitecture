namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Represents a standardized structure for returning exception or error details,
    /// typically used in API responses to provide clients with meaningful error information.
    /// 
    /// This record is conceptually similar to ASP.NET Core's <see cref="Microsoft.AspNetCore.Mvc.ProblemDetails"/>,
    /// which is the recommended way to return machine-readable error details in HTTP APIs.
    /// </summary>
    /// <param name="Status">The HTTP status code associated with the error (e.g., 400, 404, 500).</param>
    /// <param name="Type">A URI or string identifier for the error type (e.g., "https://tools.ietf.org/html/rfc7231#section-6.5.1").</param>
    /// <param name="Title">A short, human-readable summary of the error.</param>
    /// <param name="Detail">A detailed, human-readable explanation of the error.</param>
    /// <param name="Errors">An optional collection of additional error details (e.g., validation errors).</param>
    public record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}