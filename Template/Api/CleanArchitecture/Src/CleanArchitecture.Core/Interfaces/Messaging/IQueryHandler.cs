using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Interfaces.Messaging
{
    /// <summary>
    /// Defines a handler for a query that returns a response of type <typeparamref name="TResponse"/>.
    /// Used in the CQRS (Command Query Responsibility Segregation) pattern to encapsulate the logic for processing queries.
    /// </summary>
    /// <typeparam name="TQuery">The type of query to handle. Must implement <see cref="IQuery{TResponse}"/>.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the query handler.</typeparam>
    /// <remarks>
    /// The handler receives a query request and a cancellation token, and returns a <see cref="Result{TResponse}"/>
    /// containing the response value or errors. Queries are used to retrieve data and do not modify system state.
    /// </remarks>
    /// <example>
    /// public class GetUserByIdQuery : IQuery&lt;UserDto&gt;
    /// {
    ///     public Guid UserId { get; set; }
    /// }
    ///
    /// public class GetUserByIdQueryHandler : IQueryHandler&lt;GetUserByIdQuery, UserDto&gt;
    /// {
    ///     public async Task&lt;Result&lt;UserDto&gt;&gt; Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    ///     {
    ///         // Query handling logic here
    ///         var user = await userRepository.FindByIdAsync(request.UserId);
    ///         return user is not null ? Result.Success(user) : Result.Failure&lt;UserDto&gt;(new Error("NotFound", "User not found"));
    ///     }
    /// }
    /// </example>
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// Handles the specified query asynchronously and returns a response.
        /// </summary>
        /// <param name="request">The query to handle.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{TResponse}"/> containing the response value or errors.</returns>
        Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }

    //public interface IQueryHandler<in TQuery> where TQuery : IQuery    {        Task<Result> Handle(TQuery request, CancellationToken cancellationToken);    }
}