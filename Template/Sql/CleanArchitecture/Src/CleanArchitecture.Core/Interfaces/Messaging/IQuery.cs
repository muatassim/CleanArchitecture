namespace CleanArchitecture.Core.Interfaces.Messaging
{
    /// <summary>
    /// Marker interface representing a query that returns a response of type <typeparamref name="TResponse"/>.
    /// Used in the CQRS (Command Query Responsibility Segregation) pattern to encapsulate a request for data or information.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
    /// <remarks>
    /// Queries are used to retrieve data without modifying system state. Implementations of this interface
    /// are typically handled by a corresponding IQueryHandler that processes the query and returns the result.
    /// </remarks>
    /// <example>
    /// public class GetUserByIdQuery : IQuery&lt;UserDto&gt;
    /// {
    ///     public Guid UserId { get; set; }
    /// }
    /// </example>
    public interface IQuery<TResponse>
    {
    }
}