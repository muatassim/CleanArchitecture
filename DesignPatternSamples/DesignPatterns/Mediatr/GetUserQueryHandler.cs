namespace DesignPatterns.Mediatr
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, string>
    {
        public string Handle(GetUserQuery query)
        {
            // Simulate fetching user
            return $"User with ID {query.UserId}";
        }
    }
}
