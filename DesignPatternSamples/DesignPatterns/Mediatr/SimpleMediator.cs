namespace DesignPatterns.Mediatr
{
    // Simple Mediator
    public class SimpleMediator
    {
        public static void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command is CreateUserCommand createUser)
            {
                new CreateUserCommandHandler().Handle(createUser);
            }
            // Add more command handlers here
        }

        public static TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query is GetUserQuery getUser)
            {
                return (TResult)(object)new GetUserQueryHandler().Handle(getUser);
            }
            // Add more query handlers here
            throw new InvalidOperationException("No handler found.");
        }
    }
}
