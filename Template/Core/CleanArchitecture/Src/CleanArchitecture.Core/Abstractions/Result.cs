using System.Diagnostics.CodeAnalysis;
using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Represents the outcome of an operation, indicating success or failure and containing error information if failed.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Protected constructor to enforce valid state: 
        /// - Success must not have errors.
        /// - Failure must have at least one error.
        /// </summary>
        protected internal Result(bool isSuccess, List<Error> errors)
        {
            if (isSuccess && errors.Count > 0)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && errors.Count == 0)
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            Errors = errors;
        }

        /// <summary>
        /// Indicates if the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Indicates if the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// List of errors if the operation failed.
        /// </summary>
        public List<Error> Errors { get; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        public static Result Success() => new(true, []);

        /// <summary>
        /// Creates a failed result with a single error.
        /// </summary>
        public static Result Failure(Error error) => new(false, [error]);

        /// <summary>
        /// Creates a failed result with multiple errors.
        /// </summary>
        public static Result Failure(List<Error> errors) => new(false, errors);

        /// <summary>
        /// Creates a successful result with a value.
        /// </summary>
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, []);

        /// <summary>
        /// Creates a failed result with a single error and a value type.
        /// </summary>
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, [error]);

        /// <summary>
        /// Creates a failed result with multiple errors and a value type.
        /// </summary>
        public static Result<TValue> Failure<TValue>(List<Error> errors) => new(default, false, errors);

        /// <summary>
        /// Creates a result based on whether the value is null.
        /// </summary>
        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>([ErrorHelpers.NullValue]);

        /// <summary>
        /// Creates a result based on isSuccess and a single error.
        /// </summary>
        public static Result Create(bool isSuccess, Error error) =>
            isSuccess ? Success() : Failure(error);

        /// <summary>
        /// Creates a result based on isSuccess and a list of errors.
        /// </summary>
        public static Result Create(bool isSuccess, List<Error> errors) =>
            isSuccess ? Success() : Failure(errors);
    }

    /// <summary>
    /// Represents the outcome of an operation that returns a value.
    /// </summary>
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        /// <summary>
        /// Protected constructor for Result with value.
        /// </summary>
        protected internal Result(TValue? value, bool isSuccess, List<Error> errors)
            : base(isSuccess, errors) => _value = value;

        /// <summary>
        /// Gets the value if the result is successful; throws if not.
        /// </summary>
        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

        /// <summary>
        /// Allows implicit conversion from TValue to Result&lt;TValue&gt;.
        /// </summary>
        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}