using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Interfaces.Messaging
{
    /// <summary>
    /// Defines a handler for a command that does not return a value.
    /// Implementations encapsulate the logic to process a specific command type.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to handle. Must implement <see cref="ICommand"/>.</typeparam>
    /// <remarks>
    /// The handler receives a command request and a cancellation token, and returns a <see cref="Result"/>
    /// indicating success or failure, along with any errors.
    /// </remarks>
    /// <example>
    /// public class CreateUserCommand : ICommand { ... }
    /// public class CreateUserCommandHandler : ICommandHandler&lt;CreateUserCommand&gt;
    /// {
    ///     public async Task&lt;Result&gt; Handle(CreateUserCommand request, CancellationToken cancellationToken)
    ///     {
    ///         // Command handling logic here
    ///         return Result.Success();
    ///     }
    /// }
    /// </example>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="request">The command to handle.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the command.</returns>
        Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines a handler for a command that returns a value of type <typeparamref name="TResponse"/>.
    /// Implementations encapsulate the logic to process a specific command type and return a result.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to handle. Must implement <see cref="ICommand{TResponse}"/>.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the command handler.</typeparam>
    /// <remarks>
    /// The handler receives a command request and a cancellation token, and returns a <see cref="Result{TResponse}"/>
    /// containing the response value or errors.
    /// </remarks>
    /// <example>
    /// public class CreateUserCommand : ICommand&lt;UserDto&gt; { ... }
    /// public class CreateUserCommandHandler : ICommandHandler&lt;CreateUserCommand, UserDto&gt;
    /// {
    ///     public async Task&lt;Result&lt;UserDto&gt;&gt; Handle(CreateUserCommand request, CancellationToken cancellationToken)
    ///     {
    ///         // Command handling logic here
    ///         var user = new UserDto(...);
    ///         return Result.Success(user);
    ///     }
    /// }
    /// </example>
    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        /// <summary>
        /// Handles the specified command asynchronously and returns a response.
        /// </summary>
        /// <param name="request">The command to handle.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{TResponse}"/> containing the response value or errors.</returns>
        Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);
    }
}