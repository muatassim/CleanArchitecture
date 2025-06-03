namespace DesignPatterns.Mediatr
{
    // Command and Query Handlers
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
