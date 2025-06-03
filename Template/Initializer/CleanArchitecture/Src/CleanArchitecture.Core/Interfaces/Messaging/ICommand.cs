namespace CleanArchitecture.Core.Interfaces.Messaging
{
    /// <summary>
    /// Marker interface representing a command with no return value.
    /// Used to encapsulate an action or intent to change state in the system.
    /// </summary>
    public interface ICommand
    {
    }

    /// <summary>
    /// Marker interface representing a command that returns a response of type TReponse.
    /// Used for actions that expect a result after execution.
    /// </summary>
    public interface ICommand<TReponse>
    {
    }

    /// <summary>
    /// Represents a command that is idempotent, meaning it can be safely retried without causing duplicate effects.
    /// Inherits from ICommand and adds a RequestId property to uniquely identify the command request.
    /// </summary>
    public interface IIdempotentCommand : ICommand
    {
        /// <summary>
        /// Unique identifier for the command request, used to ensure idempotency.
        /// </summary>
        string RequestId { get; }
    }

    /// <summary>
    /// Represents an idempotent command that returns a response of type TReponse.
    /// Inherits from ICommand&lt;TReponse&gt; and adds a RequestId property to uniquely identify the command request.
    /// </summary>
    public interface IIdempotentCommand<TReponse> : ICommand<TReponse>
    {
        /// <summary>
        /// Unique identifier for the command request, used to ensure idempotency.
        /// </summary>
        string RequestId { get; }
    }
}
