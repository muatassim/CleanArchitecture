namespace DesignPatterns.Mediatr
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public void Handle(CreateUserCommand command)
        {
            Console.WriteLine($"User '{command.UserName}' created.");
        }
    }
}
