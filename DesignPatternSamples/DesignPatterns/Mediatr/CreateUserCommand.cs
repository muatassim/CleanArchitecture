namespace DesignPatterns.Mediatr
{
    // Example Command and Handler
    public class CreateUserCommand : ICommand
    {
        //! tell the compiler that this property will be initialized later
        public string UserName { get; set; } = null!;
    }
}
