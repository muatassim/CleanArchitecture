using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces.Messaging;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for dispatching commands and queries to their respective handlers.
    /// Supports both commands (with or without results) and queries, returning standardized Result objects.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Sends a command that returns a result.
        /// </summary>
        /// <typeparam name="TCommand">The command type.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="command">The command instance.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A Result containing the response.</returns>
        Task<Result<TResponse>> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>;

        /// <summary>
        /// Sends a command that does not return a result.
        /// </summary>
        /// <typeparam name="TCommand">The command type.</typeparam>
        /// <param name="command">The command instance.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A Result indicating success or failure.</returns>
        Task<Result> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;

        /// <summary>
        /// Sends a query and returns a result.
        /// </summary>
        /// <typeparam name="TQuery">The query type.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="query">The query instance.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A Result containing the response.</returns>
        Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;
    }
}