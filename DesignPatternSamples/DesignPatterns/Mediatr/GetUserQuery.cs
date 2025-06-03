namespace DesignPatterns.Mediatr
{
    // Example Query and Handler
    public class GetUserQuery : IQuery<string>
    {
        public int UserId { get; set; }
    }
}
