using CleanArchitecture.ApiShared.Models;
namespace CleanArchitecture.ApiShared.Exceptions
{
    public class ApiErrorException : Exception
    {
        public readonly IReadOnlyCollection<ApiError> Errors;
        public ApiErrorException(List<ApiError> errors) : base("Validation Errors") => Errors = errors;
        public ApiErrorException(ApiError error) : base(error.Message) => Errors = [error];
        public ApiErrorException(string error) : base(error) => Errors = [new ApiError("", error)];
        public ApiErrorException(string name, string error) : base(error) => Errors = [new ApiError(name, error)];
    }
}
